using System;
using System.Collections.Generic;
using System.Text;

namespace TesteDotNet
{
    enum Operacao
    {
        Soma, Divisao, Multiplicacao, Subtracao, Media, Arquivo
    }
    class Calculadora
    {
        public static decimal Somar(decimal valor1, decimal valor2)
        {
            return valor1 + valor2;
        }
        public static decimal Somar(decimal[] valores)
        {
            var resultado = 0m;
            foreach (var valor in valores)
            {
                resultado += valor;
            }
            return resultado;
        }

        public static decimal SomarPares(decimal[] valores)
        {
            var resultado = 0m;
            foreach (var valor in valores)
            {
                if (valor % 2 == 0)
                    resultado += valor;
            }
            return resultado;
        }
        public static decimal Media(decimal[] valores)
        {
            var resultado = 0m;
            foreach (var valor in valores)
            {
                resultado += valor;
            }
            //se não fizer esse Parse a divisão pode dar um valor errado
            resultado /= decimal.Parse(valores.Length.ToString());
            return resultado;
        }
        public static decimal Divisao(decimal valor1, decimal valor2)
        {
            return valor1 / valor2;
        }
        public static decimal Subtracao(decimal valor1, decimal valor2)
        {
            return valor1 - valor2;
        }
        public static decimal Multiplicacao(decimal valor1, decimal valor2)
        {
            return valor1 * valor2;
        }
        public static Operacao VerificarOperacao(string operacao)
        {
            switch (operacao.ToLower())
            {
                case "somar":
                case "soma":
                    return Operacao.Soma;

                case "subtrair":
                case "subtracao":
                case "subtração":
                    return Operacao.Subtracao;

                case "dividir":
                case "divisao":
                case "divisão":
                    return Operacao.Divisao;

                case "multiplicar":
                case "multiplicação":
                case "multiplicacao":
                    return Operacao.Multiplicacao;

                case "media":
                case "média":
                    return Operacao.Media;

                case "ler arquivo":
                case "arquivo":
                    return Operacao.Arquivo;

                default:
                    throw new OperacaoNaoEncontradaException();
            }
        }
        public static List<Decimal> VerificarValores(string valores)
        {
            if (!valores.Contains(";"))
                throw new SemPontoVirgulaException();

            var arrayValores = valores.Split(";");
            var lista = new List<Decimal>();

            foreach (var valorString in arrayValores)
            {
                var valor = 0m;
                if (!decimal.TryParse(valorString, out valor))
                    throw new NaoEumNumeroException();
                lista.Add(valor);
            }

            return lista;
        }
        public static decimal FazerOperacao(Operacao operacao, out string simboloOperacao, List<Decimal> listaValores)
        {
            switch (operacao)
            {
                case Operacao.Soma:
                    simboloOperacao = " + ";
                    if (listaValores.Count == 2)
                        return Calculadora.Somar(listaValores[0], listaValores[1]);
                    else
                        return Calculadora.Somar(listaValores.ToArray());

                case Operacao.Subtracao:
                    simboloOperacao = " - ";
                    return Calculadora.Subtracao(listaValores[0], listaValores[1]);

                case Operacao.Divisao:
                    simboloOperacao = " / ";
                    return Calculadora.Divisao(listaValores[0], listaValores[1]);

                case Operacao.Multiplicacao:
                    simboloOperacao = " x ";
                    return Calculadora.Multiplicacao(listaValores[0], listaValores[1]);

                case Operacao.Media:
                    simboloOperacao = "media";
                    return Calculadora.Media(listaValores.ToArray());

                default:
                    throw new OperacaoNaoEncontradaException();
            }
        }
    }
}
