using Dynacoop.Desafio2.Controller;
using Microsoft.Xrm.Sdk;
using System;

namespace Dynacoop.Desafio2
{
    public class OpportunityManager : IPlugin
    {
        public IOrganizationService Service { get; set; }
        public ITracingService TracingService { get; set; }

        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            Service = serviceFactory.CreateOrganizationService(context.UserId);
            TracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            Entity opportunityPreImage = (Entity)context.PreEntityImages["PreImage"];

            if (context.MessageName == "Update")
            {
                Subtract(opportunityPreImage);
                var opportunityPostImage = (Entity)context.PostEntityImages["postimage"];
                Sum(opportunityPostImage);
            }
            if (context.MessageName == "Delete")
            {
                Subtract(opportunityPreImage);
            }
        }

        private void Subtract(Entity opportunityPreImage)
        {
            var accountReferencePre = opportunityPreImage.Contains("parentaccountid") ? (EntityReference)opportunityPreImage["parentaccountid"] : null;
            if (accountReferencePre != null)
            {
                var valueOpp = opportunityPreImage.Contains("estimatedvalue") ? (Money)opportunityPreImage["estimatedvalue"] : new Money(0);
                ContaController contaController = new ContaController(this.Service);
                var oppAcount = contaController.GetAccountById(accountReferencePre.Id, new string[] { "dcf1_valortotaldeoportunidades" });
                contaController.SubtractOpportunityTotalValue(oppAcount, valueOpp);
            }
        }

        private void Sum(Entity opportunityPostImage)
        {
            var accountReferencePost = opportunityPostImage.Contains("parentaccountid") ? (EntityReference)opportunityPostImage["parentaccountid"] : null;
            if (accountReferencePost != null)
            {
                var valueOpp = opportunityPostImage.Contains("estimatedvalue") ? (Money)opportunityPostImage["estimatedvalue"] : new Money(0);
                ContaController contaController = new ContaController(this.Service);
                var oppAcount = contaController.GetAccountById(accountReferencePost.Id, new string[] { "dcf1_valortotaldeoportunidades" });
                contaController.SumOpportunityTotalValue(oppAcount, valueOpp);
            }
        }
    }
}
