using AutoMapper;
using TicketingSysteem.Business.Helpers;
using TicketingSysteem.Business.Repositories;
using TicketingSysteem.Business.Writers;
using TicketingSysteem.Entities.Pocos;
using TicketingSysteem.Models;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using TicketingSysteem.Entities.Enums;
using System.Security.Principal;
using TicketingSysteem.Entities.SearchPocos;

namespace TicketingSysteem.Controllers
{
    public class BaseController<TEntity, TModel, TPostModel, TSearchEntity> : ApiController
        where TEntity : EntityBase
        where TModel : ModelBase<TEntity>
        where TPostModel : PostModelBase<TEntity>
        where TSearchEntity : SearchBase<TEntity>
    {
        [Dependency]
        protected IReader<TEntity> EntityReader { get; set; }
        [Dependency]
        protected IWriter<TEntity> EntityWriter { get; set; }


        // GET: api/TEntity
        [Route("", Order = 0)]
        public virtual async Task<IHttpActionResult> Get()
        {
            try
            {
                var entities = await EntityReader.GetAllAsync();
                return Ok(Mapper.Map<IEnumerable<TModel>>(entities));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/TEntity/5
        [Route("{id:int}")]
        public virtual async Task<IHttpActionResult> GetById(int id)
        {
            try
            {
                var entity = await EntityReader.GetById(id);
                if (entity == null) return NotFound();
                return Ok(Mapper.Map<TModel>(entity));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/TEntity/5?fields=translation
        [Route("{id:int}")]
        public virtual async Task<IHttpActionResult> GetById(int id, [FromUri]string fields)
        {
            try
            {
                var entity = await EntityReader.GetById(id, fields.Split(','));
                if (entity == null) return NotFound();
                return Ok(Mapper.Map<TModel>(entity));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/TEntity?fields=translation
        [Route("")]
        public virtual async Task<IHttpActionResult> Get([FromUri]string fields)
        {
            try
            {
                if (string.IsNullOrEmpty(fields)) return Ok(EntityReader.GetIncludeList());
                var entities = await EntityReader.GetAllAsync(fields.Split(','));
                return Ok(Mapper.Map<IEnumerable<TModel>>(entities));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        // POST: api/TEntity
        [Route("")]
        public virtual async Task<IHttpActionResult> Post([FromBody]TPostModel model)
        {
            try
            {
                if (model == null) return BadRequest("Model not found");
                if (ModelState.IsValid)
                {
                    var entity = Mapper.Map<TEntity>(model);
                    var result = await EntityWriter.InsertAsync(entity);
                    return OkEntityResult(result);
                } else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/TEntity
        [HttpPost]
        [ActionName("search")]
        public virtual async Task<IHttpActionResult> Search([FromBody]TSearchEntity model, [FromUri]string fields)
        {
            //TODO:: Searchbase is niet proper als body, moet appart model worden ooit
            try
            {
                if (model == null) return BadRequest("Model not found");
                if (ModelState.IsValid)
                {
                    var entities = await EntityReader.Filter(model, fields.Split(','));
                    var models = Mapper.Map<IEnumerable<TModel>>(entities);
                    return Ok(models);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/TEntity
        [HttpPost]
        [ActionName("search")]
        public virtual async Task<IHttpActionResult> Search([FromBody]TSearchEntity model)
        {
            return await Search(model, "");
        }

        // PUT: api/TEntity/5
        [Route("")]
        public virtual async Task<IHttpActionResult> Put(int id, [FromBody]TPostModel model)
        {
            try
            {
                if (model == null) return BadRequest("Model not found");
                if (id != model.Id) return BadRequest("Id not matching");
                if (ModelState.IsValid)
                {
                    var entity = Mapper.Map<TEntity>(model);
                    var result = await EntityWriter.UpdateAsync(entity);
                    return OkEntityResult(result);
                } else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/TEntity/5
        [Route("")]
        public virtual async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                var result = await EntityWriter.DeleteAsync(id);
                if (result.Code == Business.Helpers.ResultCode.Failed)
                {
                    return InternalServerError(result.Exception);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        #region ReturnValues
        protected IHttpActionResult OkEntityResult(EntityResult<TEntity> result, object value = null)
        {
            if (result == null) return BadRequest("No EntityResult found");
            switch (result.Code)
            {
                case ResultCode.Failed:
                    return InternalServerError(result.Exception);
                case ResultCode.ValidationError:
                    return BadRequest(string.Join(";",result.ValidationMessages));
                case ResultCode.Success:
                    if (value != null) return Ok(value);
                    return Ok(result.Entity);
                default:
                    break;
            }
            return BadRequest("Unknown EntityResult code");
        }

        protected IHttpActionResult CreatedEntityResult(EntityResult<TEntity> result, string location, object value)
        {
            if (result == null) return BadRequest("No EntityResult found");
            switch (result.Code)
            {
                case ResultCode.Failed:
                    return InternalServerError(result.Exception);
                case ResultCode.ValidationError:
                    return BadRequest(string.Join(";", result.ValidationMessages));
                case ResultCode.Success:
                    return Created(location, value);
                default:
                    break;
            }
            return BadRequest("Unknown EntityResult code");
        }

        protected override ExceptionResult InternalServerError(Exception exception)
        {
            //Hier kunnen we later afhankelijk van de omgeving exceptions niet meer naar de user geven maar een algemene fout tonen
            return base.InternalServerError(exception);
        }
        
        protected StatusCodeResult NoContent()
        {
            return StatusCode(HttpStatusCode.NoContent);
        }
        #endregion
    }

    public static class IPrincipalExtentions
    {
        public static bool IsInRole(this IPrincipal user, params Rol[] rollen)
        {
            foreach (Rol rol in rollen)
            {
                if (user.IsInRole(rol.ToString())) return true;
            }
            return false;
        }
    }
}