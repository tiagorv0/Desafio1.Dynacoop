
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System.Linq;
using System;

namespace Dynacoop.Desafio1.Model
{
    public class Conta
    {
        public CrmServiceClient ServiceClient { get; set; }
        public string LogicalName { get; set; }

        public Conta(CrmServiceClient serviceClient)
        {
            ServiceClient = serviceClient;
            LogicalName = "account";
        }
        

        public Guid Create(string nome, string cnpj, decimal receitaAnual, int qtdeFiliais, int tipoEmpresa, Guid contaPrimaria)
        {
            Entity conta = new Entity(this.LogicalName);
            conta["name"] = nome;
            conta["dcf1_cnpj"] = cnpj;
            conta["revenue"] = new Money(receitaAnual);
            conta["dcf1_quantidefiliais"] = qtdeFiliais;
            conta["businesstypecode"] = new OptionSetValue(tipoEmpresa);
            conta["parentaccountid"] = new EntityReference("account", contaPrimaria);

            return ServiceClient.Create(conta);
        }

        public bool GetAccountByCNPJ(string cnpj)
        {
            QueryExpression queryAccount = new QueryExpression(LogicalName);
            queryAccount.ColumnSet.AddColumns("name", "dcf1_cnpj");
            queryAccount.Criteria.AddCondition("dcf1_cnpj", ConditionOperator.Equal, cnpj);
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
