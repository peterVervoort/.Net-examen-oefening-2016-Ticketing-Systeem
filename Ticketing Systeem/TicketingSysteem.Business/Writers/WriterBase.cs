using TicketingSysteem.Business.Helpers;
using TicketingSysteem.Business.Validators;
using TicketingSysteem.DataAccess.Repositories;
using TicketingSysteem.Entities.Pocos;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace TicketingSysteem.Business.Writers
{
    public class WriterBase<TEntity> : IWriter<TEntity> where TEntity : EntityBase
    {
        [Dependency]
        public IRepository<TEntity> Repository { get; set; }
        [Dependency]
        public IValidatorBase<TEntity> Validator { get; set; }
        

        public virtual async Task PreInsert(TEntity entity)
        {
            return;
        }

        public async Task<EntityResult<TEntity>> InsertAsync(TEntity entity)
        {
            EntityResult<TEntity> result = new EntityResult<TEntity>(ResultCode.Success);
            result.Entity = entity;
            try
            {
                await PreInsert(entity);
                var validationMessages = await Validator.ValidateInsertAsync(entity);
                if (validationMessages.Any())
                {
                    return new EntityResult<TEntity>(ResultCode.ValidationError, validationMessages.ToArray());
                }
                entity = await Repository.CreateAsync(entity);
                await PostInsert(entity);
            }
            catch (Exception ex)
            {
                result.Code = ResultCode.Failed;
                result.Exception = ex;
            }

            return result;
        }

        public virtual async Task PostInsert(TEntity entity)
        {
            return;
        }

        public async Task<EntityResult<TEntity>> UpdateAsync(TEntity entity)
        {
            EntityResult<TEntity> result = new EntityResult<TEntity>(ResultCode.Success);
            result.Entity = entity;
            try
            {
                var validationMessages = await Validator.ValidateUpdateAsync(entity);
                if (validationMessages.Any())
                {
                    return new EntityResult<TEntity>(ResultCode.ValidationError, validationMessages.ToArray());
                }
                await Repository.UpdateAsync(entity);
            }
            catch (Exception ex)
            {
                result.Code = ResultCode.Failed;
                result.Exception = ex;
            }

            return result;
        }

        public async Task<EntityResult<TEntity>> DeleteAsync(int id)
        {
            EntityResult<TEntity> result = new EntityResult<TEntity>(ResultCode.Success);
            
            try
            {
                var entity = await Repository.GetAsync(id);
                result.Entity = entity;

                var validationMessages = await Validator.ValidateDeleteAsync(entity);
                if (validationMessages.Any())
                {
                    return new EntityResult<TEntity>(ResultCode.ValidationError, validationMessages.ToArray());
                }
                await Repository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                result.Code = ResultCode.Failed;
                result.Exception = ex;
            }

            return result;
        }
    }
}
