
using Dynacoop.Desafio1.Model;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;

namespace Dynacoop.Desafio1.Controller
{
    public class ContatoController
    {
        public CrmServiceClient ServiceClient { get; set; }
        public Contato Contato { get; set; }

        public ContatoController(CrmServiceClient ServiceClient)
        {
            this.ServiceClient = ServiceClient;
            Contato = new Contato(ServiceClient);
        }

        public Guid Create(Guid accountId, string nome, string sobrenome, string cpf)
         => Contato.Create(accountId, nome, sobrenome, cpf);

        public bool GetAccountByCPF(string cpf) => Contato.GetAccountByCPF(cpf);
    }
}
