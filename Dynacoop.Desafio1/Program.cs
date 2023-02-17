using Dynacoop.Desafio1.Controller;
using Newtonsoft.Json.Serialization;
using System;
using System.Threading;

namespace Dynacoop.Desafio1
{
    public class Program
    {
        static void Main(string[] args)
        {
            var serviceClient = Singleton.GetService();

            var contaController = new ContaController(serviceClient);
            var contatoController = new ContatoController(serviceClient);

            do
            {
                Console.WriteLine("Por favor, informe o nome da Conta:");
                var nomeConta = Console.ReadLine();

                Console.WriteLine("Informe o CNPJ:");
                var cnpj = Console.ReadLine();

                var verificacaoCNPJ = contaController.GetAccountByCNPJ(cnpj);
                if (verificacaoCNPJ)
                {
                    Console.WriteLine("CNPJ já existente");
                    continue;
                }

                Console.WriteLine("Informe receita anual:");
                var receitaAnual = decimal.Parse(Console.ReadLine());

                Console.WriteLine("Quantidade Filiais:");
                var qtdeFiliais = int.Parse(Console.ReadLine());

                Console.WriteLine("Tipo Empresa:");
                var tipoEmpresa = int.Parse(Console.ReadLine());

                Console.WriteLine("Conta Primaria:");
                var contaPrimaria = Guid.Parse(Console.ReadLine());


                Console.WriteLine("Você deseja criar um contato para essa conta? (s/n)");
                var criarContato = Console.ReadLine();

                var contaId = new Guid();

                if (criarContato.ToLower() == "s")
                {
                    Console.WriteLine("Informe o CPF:");
                    var cpf = Console.ReadLine();

                    var verificaoCPF = contatoController.GetAccountByCPF(cpf);
                    if (verificaoCPF)
                    {
                        Console.WriteLine("CPF já existente");
                        continue;
                    }

                    Console.WriteLine("Informe o nome: ");
                    var nome = Console.ReadLine();

                    Console.WriteLine("Informe o Sobrenome:");
                    var sobrenome = Console.ReadLine();

                    contaId = CreateAccount(contaController, nomeConta, cnpj, receitaAnual, qtdeFiliais, tipoEmpresa, contaPrimaria);
                    var contatoId = contatoController.Create(contaId, nome, sobrenome, cpf);

                    Console.WriteLine("Contato:");
                    Console.WriteLine($"https://org2607c67f.crm2.dynamics.com/main.aspx?appid=ee380667-3bae-ed11-9885-002248365eb3&forceUCI=1&pagetype=entityrecord&etn=contact&id={contatoId}"); Console.ReadKey();
                    break;
                }
                else if (criarContato.ToLower() == "n")
                {
                    CreateAccount(contaController, nomeConta, cnpj, receitaAnual, qtdeFiliais, tipoEmpresa, contaPrimaria);

                    Console.ReadKey();
                    break;
                }
                else
                {
                    Console.WriteLine("Opção inválida");
                    continue;
                }
            } while (true);
        }

        private static Guid CreateAccount(ContaController contaController, string nomeConta, string cnpj, decimal receitaAnual, int qtdeFiliais, int tipoEmpresa, Guid contaPrimaria)
        {
            var contaId = contaController.Create(nomeConta, cnpj, receitaAnual, qtdeFiliais, tipoEmpresa, contaPrimaria);
            Console.WriteLine("Conta:");
            Console.WriteLine($"https://org2607c67f.crm2.dynamics.com/main.aspx?appid=ee380667-3bae-ed11-9885-002248365eb3&forceUCI=1&pagetype=entityrecord&etn=account&id={contaId}");
            return contaId;
        }
    }
}
