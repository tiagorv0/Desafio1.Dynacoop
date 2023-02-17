
using Dynacoop.Desafio1.Model;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;

namespace Dynacoop.Desafio1.Controller
{
    public class ContaController
    {
        public CrmServiceClient ServiceClient { get; set; }
        public Conta Conta { get; set; }

        public ContaController(CrmServiceClient ServiceClient)
        {
            this.ServiceClient = ServiceClient;
            Conta = new Conta(ServiceClient);
        }

        public Guid Create(string nome, string cnpj, decimal receitaAnual, int qtdeFiliais, int tipoEmpresa, Guid contaPrimaria)
         => Conta.Create(nome, cnpj, receitaAnual, qtdeFiliais, tipoEmpresa, contaPrimaria);

        public bool GetAccountByCNPJ(string cnpj) => Conta.GetAccountByCNPJ(cnpj);
    }
}
