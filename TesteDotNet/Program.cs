using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace TesteDotNet
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("============================");
            Console.WriteLine("Bem vindo ao TesteDotNet!");
            Console.WriteLine("============================");
            Console.WriteLine("Calculadora 1.0");
            Console.WriteLine("============================");

            Calcular();
        }

        public static void Calcular()
        {
            bool continuar = true;
            while (continuar)
            {
                try
                {
                    Console.WriteLine("Digite o nome da operação que deseja fazer:");
                    Console.WriteLine("Soma, Subtração, Multiplicação, Divisão, média ou ler arquivo");
                    var operacao = Console.ReadLine();
                    var enumOperacao = Calculadora.VerificarOperacao(operacao);

                    if (enumOperacao == Operacao.Arquivo)
                        LerArquivo();
                    else
                    {
                        Console.WriteLine("Digite os dois valores da operação com um ponto e virgula entre eles. Ex: 12;23");
                        var valoresString = Console.ReadLine();
                        var listaValores = Calculadora.VerificarValores(valoresString);

                        var simboloOperacao = "";
                        var resultado = Calculadora.FazerOperacao(enumOperacao, out simboloOperacao, listaValores);

                        if (simboloOperacao.Equals("media"))
                            Console.WriteLine("A média dos valores é : " + resultado);
                        else
                        {
                            Console.Write("O resultado de " + listaValores[0]);
                            listaValores.RemoveAt(0);
                            foreach (var valor in listaValores)
                            {
                                Console.Write(simboloOperacao + valor);
                            }
                            Console.Write(" = " + resultado + "\n");
                        }


                        Console.WriteLine("Pressione qualquer tecla para continuar ou ESC para sair");
                        if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                        {
                            Console.WriteLine("Obrigado por utilizar nossa calculadora");
                            continuar = false;
                            Thread.Sleep(2000);
                        }
                    }
                }
                catch(SemPontoVirgulaException e)
                {
                    Console.WriteLine("Separador ponto e virgula não encontrado. Ex: 12;23");
                }
                catch(NaoEumNumeroException e)
                {
                    Console.WriteLine("Você não digitou um dos números para fazer a operação.");
                }
                catch(OperacaoNaoEncontradaException e)
                {
                    Console.WriteLine("Operação não foi reconhecida.");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Um erro não tratado foi encontrado");
                    Console.WriteLine("Informações :" + e.Message);
                }
            }
        }

        private static void LerArquivo()
        {
            Console.WriteLine("Digite o caminho do arquivo:");
            var path = Console.ReadLine();
            if (!File.Exists(path))
            {
                Console.WriteLine("Arquivo não encontrado.");
                return;
            }
            var linhasDoArquivo = File.ReadAllText(path, System.Text.Encoding.UTF8).Split("\n");

            var resultados = new Dictionary<string, decimal>();

            foreach (var linha in linhasDoArquivo)
            {
                if (!string.IsNullOrEmpty(linha))
                {
                    var argumentos = linha.Split(";");
                    var nome = argumentos[0];
                    var enumOperacao = Calculadora.VerificarOperacao(argumentos[1]);

                    var valoresString = linha.Replace(nome + ";" + argumentos[1] + ";", "");

                    var listaValores = Calculadora.VerificarValores(valoresString);

                    var simboloOperacao = "";
                    var resultado = Calculadora.FazerOperacao(enumOperacao, out simboloOperacao, listaValores);

                    resultados.Add(nome, resultado);
                }
            }

            Console.WriteLine("Os resultados das operações do arquivo foram: ");
            foreach (var resultado in resultados)
            {
                Console.WriteLine(resultado.Key + " = " + resultado.Value);
            }
        }
    }
}
