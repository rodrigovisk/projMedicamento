using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projMedicamento
{
    class Program
    {
        static Medicamento medicamento;
        static Medicamentos medicamentos;

        static void Main(string[] args)
        {
            medicamentos = new Medicamentos();
            int opcao = 0;
            do
            {

                Console.WriteLine("0. Finalizar processo");
                Console.WriteLine("1. Cadastrar medicamento");
                Console.WriteLine("2. Consultar medicamento (sintético)");
                Console.WriteLine("3. Consultar medicamento (analítico)");
                Console.WriteLine("4. Comprar medicamento (cadastrar lote)");
                Console.WriteLine("5. Vender medicamento (abater do lote mais antigo)");
                Console.WriteLine("6. Listar medicamentos (informar dados sintéticos)");
                Console.Write("\nOPÇÃO: ");
                opcao = int.Parse(Console.ReadLine());

                Console.Clear();

                try
                {
                    if (opcao == 0)
                    {
                    }
                    else if (opcao == 1)
                    {
                        int idMed, idLote, qtde;
                        string nome, laboratorio;
                        DateTime venc;

                        Console.WriteLine("Preencha os dados do remédio a ser cadastrado:\n");
                        Console.Write("ID: ");
                        idMed = validaInt();
                        if (medicamentos.pesquisar(new Medicamento(idMed)) != null) throw new Exception("Já existe um medicamento com esse ID");
                        Console.Write("Nome: ");
                        nome = Console.ReadLine();
                        Console.Write("Laboratório: ");
                        laboratorio = Console.ReadLine();

                        Console.Write("Id do Lote: ");
                        idLote = validaInt();
                        Console.Write("Quantidade: ");
                        qtde = validaInt();
                        Console.Write("\nData de Vencimento: ");
                        venc = validaData();

                        if (venc != DateTime.MinValue)
                        {
                            // Adicionando na lista de medicamentos
                            medicamento = new Medicamento(idMed, nome, laboratorio);
                            medicamentos.adicionar(medicamento);
                            medicamento.comprar(new Lote(idLote, qtde, venc));
                            Console.WriteLine("Medicamento adicionado com sucesso!");
                        }
                        else
                        {
                            Console.WriteLine("Tente novamente.");
                        }

                        Console.ReadKey();
                    }
                    else if (opcao == 2)
                    {
                        Console.Write("Digite o ID do medicamento: ");
                        medicamento = new Medicamento(validaInt());
                        medicamento = medicamentos.pesquisar(medicamento);
                        if (medicamento == null) throw new Exception("Medicamento não encontrado!");
                        else
                        {
                            Console.WriteLine(medicamento.toString() + "\n");
                        }
                            
                        Console.ReadKey();
                    }
                    else if (opcao == 3)
                    {
                        Console.Write("Digite o ID do medicamento: ");
                        medicamento = new Medicamento(validaInt());
                        medicamento = medicamentos.pesquisar(medicamento);
                        if (medicamento == null) throw new Exception("Medicamento não encontrado!");
                        else
                        {
                            Console.WriteLine(medicamento.toString() + "\n");
                            foreach (Lote lote in medicamento.lotes)
                            {
                                Console.WriteLine(lote.toString());
                            }
                        }
                        Console.ReadKey();
                    }
                    else if (opcao == 4)
                    {
                        int idLote, qtde;
                        DateTime venc;

                        Console.Write("Digite o ID do medicamento: ");
                        medicamento = new Medicamento(validaInt());
                        medicamento = medicamentos.pesquisar(medicamento);
                        if (medicamento == null) throw new Exception("Medicamento não encontrado!");
                        else
                        {
                            Console.Write("Id do Lote: ");
                            idLote = validaInt();
                            Console.Write("Quantidade: ");
                            qtde = validaInt();
                            Console.Write("Data de Vencimento: ");
                            venc = validaData();
                            if (venc != DateTime.MinValue)
                            {
                                medicamento.comprar(new Lote(idLote, qtde, venc));
                                Console.WriteLine("Medicamento adicionado com sucesso!");
                            }
                            else
                                Console.WriteLine("Tente novamente.");
                        }

                        Console.ReadKey();
                    }
                    else if (opcao == 5)
                    {
                        int qtde;
                        Console.WriteLine("Digite o ID do medicamento.");
                        medicamento = new Medicamento(validaInt());
                        medicamento = medicamentos.pesquisar(medicamento);
                        if (medicamento == null) throw new Exception("Medicamento não encontrado!");
                        else
                        {
                            Console.Write("Quantidade: ");
                            qtde = validaInt();
                            if (medicamento.vender(qtde))
                            {
                                Console.WriteLine("Medicamento vendido!");
                                if (medicamentos.deletar(medicamento))
                                    Console.WriteLine("Medicamento zerado e deletado...");
                                else Console.WriteLine("Resta: " + medicamento.qtdeDisponivel() + " no estoque...");
                            }
                            else Console.WriteLine("Quantidade insuficiente...");
                        }

                        Console.ReadKey();
                    }
                    else if (opcao == 6)
                    {
                        Console.WriteLine("Lista de todos os medicamentos");
                        if (medicamentos.listaMedicamentos.Count != 0)
                        {
                            foreach (Medicamento medicamento in medicamentos.listaMedicamentos)
                            {
                                Console.WriteLine("\n " + medicamento.toString());
                            }
                        }
                        else Console.WriteLine("Estoque vazio...");
                        Console.ReadKey();
                    }
                    else
                    {
                        throw new Exception("Opção Inválida!");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                }
                Console.Clear();

            } while (opcao != 0);
            
        }
        
        static public int validaInt()
        {
            int num = 0;
            while (num == 0)
            {
                try { num = int.Parse(Console.ReadLine()); }
                catch { Console.Write("Digite um número válido: "); num = 0; }
            }

            return num;
        }

        static public DateTime validaData()
        {
            DateTime data;
            int dia, mes, ano;
            Console.Write("\nDia: ");
            dia = validaInt();
            Console.Write("Mês: ");
            mes = validaInt();
            Console.Write("Ano: ");
            ano = validaInt();
            try
            {
                data = new DateTime(ano, mes, dia);
                if (data.Ticks - DateTime.Now.Ticks > 0)
                    return data;
                else
                {
                    Console.WriteLine("Medicamento vencido! Cadastre outro.");
                    return DateTime.MinValue;
                }
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("Data inválida!\n\n");
                Console.ReadKey();
                return validaData();
            }
        }
    }

}