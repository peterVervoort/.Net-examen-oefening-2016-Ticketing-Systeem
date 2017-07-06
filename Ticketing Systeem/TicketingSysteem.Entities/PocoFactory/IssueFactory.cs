using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSysteem.Entities.Enums;
using TicketingSysteem.Entities.PocoFactory.FactoryUsedEnums;
using TicketingSysteem.Entities.Pocos;

namespace TicketingSysteem.Entities.PocoFactory
{
    public class IssueFactory
    {
        public static ICollection<Issue> CreateRandomIssues(ICollection<Gebruiker> gebruikers, ICollection<Gebruiker> solvers, int aantal)
        {
            ICollection<Issue> issues = new List<Issue>();
            for (int i = 0; i < aantal; i++)
            {
                var random = new Random(i);
                String RandomIssueStatus = Enum.GetName(typeof(IssueStatusBeschrijving), random.Next(Enum.GetNames(typeof(IssueStatusBeschrijving)).Length));

                if (RandomIssueStatus.Equals("Nieuw"))
                {
                    issues.Add(CreateNieuwIssue(gebruikers.ElementAt(random.Next(gebruikers.Count)),i));
                }
                else if (RandomIssueStatus.Equals("Toegewezen"))
                {
                    issues.Add(CreateToegewezenIssue(gebruikers.ElementAt(random.Next(gebruikers.Count)), solvers.ElementAt(random.Next(solvers.Count)), i));
                }
                else if (RandomIssueStatus.Equals("InBehandeling"))
                {
                    issues.Add(CreateInBehandelingIssue(gebruikers.ElementAt(random.Next(gebruikers.Count)), solvers.ElementAt(random.Next(solvers.Count)), i));
                }
                else if (RandomIssueStatus.Equals("Opgelost"))
                {
                    issues.Add(CreateOpgelostIssue(gebruikers.ElementAt(random.Next(gebruikers.Count)), solvers.ElementAt(random.Next(solvers.Count)), i));
                }
                else if (RandomIssueStatus.Equals("Afgesloten"))
                {
                    issues.Add(CreateAfgeslotenIssue(gebruikers.ElementAt(random.Next(gebruikers.Count)), solvers.ElementAt(random.Next(solvers.Count)), i));
                }
                else if (RandomIssueStatus.Equals("Canceled"))
                {
                    issues.Add(CreateCanceledIssue(gebruikers.ElementAt(random.Next(gebruikers.Count)), solvers.ElementAt(random.Next(solvers.Count)), i));
                }
                else if (RandomIssueStatus.Equals("ExtraInfo"))
                {
                    issues.Add(CreateIssueWithExtraInfoNeed(gebruikers.ElementAt(random.Next(gebruikers.Count)), solvers.ElementAt(random.Next(solvers.Count)), i));
                }
                else
                {
                    issues.Add(CreateGeweigerdIssue(gebruikers.ElementAt(random.Next(gebruikers.Count)), solvers.ElementAt(random.Next(solvers.Count)), i));
                }

            }
            return null;
        }

        private static Issue CreateNieuwIssue (Gebruiker gebruiker, int i = 0)
        {
            Issue issue = new Issue();
            var random = new Random(i);
            issue.Titel = Enum.GetName(typeof(IssueTitel), random.Next(Enum.GetNames(typeof(IssueTitel)).Length));
            issue.Beschrijving = "Beschrijving: " + i;
            issue.GebruikerId = gebruiker.Id;
            issue.CreationDate = DateTime.UtcNow;
            issue.IssueDate = DateTime.UtcNow;
            issue.Gebruiker = gebruiker;
            issue = CreateAndAddIssueStatusToIssue(issue, IssueStatusBeschrijving.Nieuw, null);
            return issue;
        }

        private static Issue CreateAndAddIssueStatusToIssue (Issue issue, IssueStatusBeschrijving issueStatusBeschrijving, Gebruiker solver)
        {
            IssueStatus issueStatus = new IssueStatus();
            issueStatus.StatusBeschrijving = issueStatusBeschrijving;
            issueStatus.CreationDate = DateTime.UtcNow;
            issueStatus.Issue = issue;
            if (solver != null)
            {
                issueStatus.Solver = solver;
            }
            issue.IssueStatussen.Add(issueStatus);
            return issue;
        }

        private static Issue CreateToegewezenIssue(Gebruiker gebruiker, Gebruiker solver, int i = 0)
        {
            Issue issue = CreateNieuwIssue(gebruiker, i);
            issue = CreateAndAddIssueStatusToIssue(issue, IssueStatusBeschrijving.Toegewezen, solver);
            return issue;
        }

        private static Issue CreateInBehandelingIssue (Gebruiker gebruiker, Gebruiker solver, int i = 0)
        {
            Issue issue = CreateToegewezenIssue(gebruiker, solver, i);
            issue = CreateAndAddIssueStatusToIssue(issue, IssueStatusBeschrijving.InBehandeling, solver);
            return issue;
        }

        private static Issue CreateOpgelostIssue (Gebruiker gebruiker, Gebruiker solver, int i = 0)
        {
            Issue issue = CreateToegewezenIssue(gebruiker, solver, i);
            issue = CreateAndAddIssueStatusToIssue(issue, IssueStatusBeschrijving.Opgelost, solver);
            issue.Oplossing = "Oplossing 1";
            return issue;
        }

        private static Issue CreateAfgeslotenIssue (Gebruiker gebruiker, Gebruiker solver, int i = 0)
        {
            Issue issue = CreateAfgeslotenIssue(gebruiker, solver, i);
            issue = CreateAndAddIssueStatusToIssue(issue, IssueStatusBeschrijving.Afgesloten, solver);
            return issue;
        }

        private static Issue CreateCanceledIssue (Gebruiker gebruiker, Gebruiker solver, int i = 0)
        {
            Issue issue = CreateInBehandelingIssue(gebruiker, solver, i);
            issue = CreateAndAddIssueStatusToIssue(issue, IssueStatusBeschrijving.Canceled, solver);
            return issue;
        }

        private static Issue CreateIssueWithExtraInfoNeed (Gebruiker gebruiker, Gebruiker solver, int i = 0)
        {
            Issue issue = CreateInBehandelingIssue(gebruiker, solver, i);
            issue = CreateAndAddIssueStatusToIssue(issue, IssueStatusBeschrijving.ExtraInfo, solver);
            IssueStatus issuestatus = issue.IssueStatussen.Single();
            ExtraInfo extraInfo = new ExtraInfo();
            extraInfo.IssueStatus = issuestatus;
            extraInfo.InfoVraag = "vraag: " + i;
            issuestatus.ExtraInfos.Add(extraInfo);
            return issue;
        }

        private static Issue CreateGeweigerdIssue (Gebruiker gebruiker, Gebruiker solver, int i = 0)
        {
            Issue issue = CreateInBehandelingIssue(gebruiker, solver, i);
            issue = CreateAndAddIssueStatusToIssue(issue, IssueStatusBeschrijving.Geweigerd, solver);   
            return issue;
        }
    }
}
