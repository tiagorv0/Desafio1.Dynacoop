
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;

namespace Dynacoop.Desafio2.Model
{
    public class Conta
    {
        public IOrganizationService CrmServiceClient { get; set; }
        public string LogicalName { get; set; }

        public Conta(IOrganizationService crmServiceClient)
        {
            CrmServiceClient = crmServiceClient;
            LogicalName = "account";
        }

        public Conta(CrmServiceClient crmServiceClient)
        {
            CrmServiceClient = crmServiceClient;
            LogicalName = "account";
        }

        public Entity GetAccountById(Guid id, string[] colums)
        {
            return CrmServiceClient.Retrieve(LogicalName, id, new ColumnSet(colums));
        }

        public void SumOpportunityTotalValue(Entity oppAccount, Money valueOpp)
        {
            var valueAccountOpp = oppAccount.Contains("dcf1_valortotaldeoportunidades") ? (Money)oppAccount["dcf1_valortotaldeoportunidades"] : new Money(0);

            valueAccountOpp.Value += valueOpp.Value;

            oppAccount["dcf1_valortotaldeoportunidades"] = valueAccountOpp;
            CrmServiceClient.Update(oppAccount);
        }

        public void SubtractOpportunityTotalValue(Entity oppAccount, Money valueOpp)
        {
            var valueAccountOpp = oppAccount.Contains("dcf1_valortotaldeoportunidades") ? (Money)oppAccount["dcf1_valortotaldeoportunidades"] : new Money(0);

            valueAccountOpp.Value -= valueOpp.Value;

            if(valueAccountOpp.Value < 0)
                valueAccountOpp.Value = 0;

            oppAccount["dcf1_valortotaldeoportunidades"] = valueAccountOpp;
            CrmServiceClient.Update(oppAccount);
        }
    }
}
