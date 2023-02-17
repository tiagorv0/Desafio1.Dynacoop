
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Linq;

namespace Dynacoop.Desafio1.Model
{
    public class Contato
    {
        public CrmServiceClient ServiceClient { get; set; }
        public string LogicalName { get; set; }

        public Contato(CrmServiceClient ServiceClient)
        {
            this.ServiceClient = ServiceClient;
            LogicalName = "contact";
        }

        public Guid Create(Guid accountId, string nome, string sobrenome, string cpf)
        {
            Entity contato = new Entity(this.LogicalName);
            contato["firstname"] = nome;
            contato["lastname"] = sobrenome;
            contato["dcf1_cpf"] = cpf;
            contato["parentcustomerid"] = new EntityReference("account", accountId);
            return ServiceClient.Create(contato);
        }

        public bool GetAccountByCPF(string cpf)
        {
            QueryExpression queryAccount = new QueryExpression(LogicalName);
            queryAccount.ColumnSet.AddColumns("firstname", "dcf1_cpf");
            queryAccount.Criteria.AddCondition("dcf1_cpf", ConditionOperator.Equal, cpf);
            return RetrieveOneAccount(queryAccount);
        }

        private bool RetrieveOneAccount(QueryExpression queryAccount)
        {
            EntityCollection accounts = this.ServiceClient.RetrieveMultiple(queryAccount);

            if (accounts.Entities.Count() > 0)
                return true;
            else
                return false;
        }
    }
}
