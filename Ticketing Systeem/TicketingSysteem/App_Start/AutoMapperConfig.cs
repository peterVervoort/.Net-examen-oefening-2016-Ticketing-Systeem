using AutoMapper;
using TicketingSysteem.Entities.Pocos;
using TicketingSysteem.Models;
using System;
using TicketingSysteem.Entities.Enums;
using TicketingSysteem.Models.Issue;
using TicketingSysteem.Models.ExtraInfo;
using System.Collections.Generic;
using System.Linq;

namespace TicketingSysteem
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            EntitiesToModels();
            ModelsToEntities();
        }

        public static void EntitiesToModels()
        {
            Mapper.CreateMap(typeof(ICollection<>), typeof(IEnumerable<>));

            Mapper.CreateMap<Gebruiker, GebruikerModel>()
                .ForMember(target => target.Verantwoordelijke, opt => opt.MapFrom(source => source.Verantwoordelijke == null ? null : $"{source.Verantwoordelijke.Voornaam} {source.Verantwoordelijke.Achternaam}"))
                .ForMember(target => target.Rol, opt => opt.MapFrom(source => source.Rol.ToString()));

            Mapper.CreateMap<Issue, IssueModel>()
                .ForMember(target => target.Gebruiker, opt => opt.MapFrom(source => source.Gebruiker != null ? (source.Gebruiker.Voornaam + ' ' + source.Gebruiker.Achternaam) : null))
                .ForMember(target => target.Niveau, opt => opt.MapFrom(source => source.Niveau.ToString()))
                .AfterMap((source, target) =>
                {
                    var lastIssueStatus = source.IssueStatussen?.OrderBy(i => i.CreationDate).LastOrDefault();
                    if (lastIssueStatus != null)
                    {
                        target.HuidigeStatus = lastIssueStatus.StatusBeschrijving.ToString();
                    }

                    //solver
                    target.Solver = lastIssueStatus.Solver?.Voornaam + " " + lastIssueStatus.Solver?.Achternaam;
                });

            Mapper.CreateMap<IssueStatus, IssueStatusModel>()
               .ForMember(target => target.Solver, opt => opt.MapFrom(source => source.Solver != null ? (source.Solver.Voornaam + ' ' + source.Solver.Achternaam) : null))
               .ForMember(target => target.ExtraInfos, opt => opt.MapFrom(source => source.ExtraInfos))
               .ForMember(target => target.StatusBeschrijving, opt => opt.MapFrom(source => source.StatusBeschrijving.ToString()));

            Mapper.CreateMap<ExtraInfo, ExtraInfoModel>();
        }

        public static void ModelsToEntities()
        {
            Mapper.CreateMap<GebruikerPostModel, Gebruiker>()
                .AfterMap((source, target) => {
                    Rol rol;
                    if (Enum.TryParse<Rol>(source.Rol, out rol))
                    {
                        target.Rol = rol;
                    }
                });

            Mapper.CreateMap<IssuePostModel, Issue>()
                .AfterMap((source, target) => {
                    IssueNiveau niveau;
                    if (Enum.TryParse<IssueNiveau>(source.Niveau, out niveau))
                    {
                        target.Niveau = niveau;
                    }
                });

            Mapper.CreateMap<IssueStatusPostModel, IssueStatus>()
                .AfterMap((source, target) => {
                    IssueStatusBeschrijving status;
                    if (Enum.TryParse<IssueStatusBeschrijving>(source.StatusBeschrijving, out status))
                    {
                        target.StatusBeschrijving = status;
                    }
                });

            Mapper.CreateMap<ExtraInfoPostModel, ExtraInfo > ();

        }
    }
}