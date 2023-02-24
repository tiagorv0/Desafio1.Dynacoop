
using Dynacoop.Desafio2.Model;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;

namespace Dynacoop.Desafio2.Controller
{
    public class ContaController
    {
        public IOrganizationService CrmServiceClient { get; set; }
        public Conta Conta { get; set; }

        public ContaController(IOrganizationService crmServiceClient)
        {
            CrmServiceClient = crmServiceClient;
            Conta = new Conta(CrmServiceClient);
        }

        public ContaController(CrmServiceClient crmServiceClient)
        {
            CrmServiceClient = crmServiceClient;
            Conta = new Conta(CrmServiceClient);
        }

        public Entity GetAccountById(Guid id, string[] colums)
        {
            return Conta.GetAccountById(id, colums);
        }

        public void SumOpportunityTotalValue(Entity oppAccount, Money valueOpp)
        {
            Conta.SumOpportunityTotalValue(oppAccount, valueOpp);
        }

        public void SubtractOpportunityTotalValue(Entity oppAccount, Money valueOpp)
        {
            Conta.SubtractOpportunityTotalValue(oppAccount, valueOpp);
        }
    }
}
