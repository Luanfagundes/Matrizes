using System;
using System.IO;

namespace Matrizes
{
    class Program
    {

        public static void Main(string[] args)
        {
            try
            {
                String linha;
                Int32 qtColunas = 0;
                Int32 qtCaracteresLinha = 0;
                Char[,] array = new Char[0, 0];
                Boolean validarFormatoMatriz = false, validarMatrizQuadrada = false;

                using (StreamReader arquivo = new StreamReader("../../matrix.txt"))
                {
                    while ((linha = arquivo.ReadLine()) != null)
                    {
                        //Popula os campos na primeira execução.
                        if (qtCaracteresLinha == 0)
                        {
                            qtCaracteresLinha = linha.Length;
                            array = new Char[linha.Length, linha.Length];
                        }

                        validarFormatoMatriz = !ValidarFormatoMatriz(qtCaracteresLinha, linha);
                        if (validarFormatoMatriz) break;

                        //Salva no array.
                        for (int posicaoLinha = 0; posicaoLinha < linha.Length; posicaoLinha++)
                        {
                            array[qtColunas, posicaoLinha] = linha[posicaoLinha];
                        }

                        qtColunas++;
                    }

                    validarMatrizQuadrada = !ValidarMatrizQuadrada(qtColunas, qtCaracteresLinha);

                }

                if (!validarFormatoMatriz && !validarMatrizQuadrada)
                {
                    ImprimirMatrizOriginal(array, qtCaracteresLinha);
                    ImprimirMatrizInvertida(array, qtCaracteresLinha);
                    CopiarParaNovoArquivo(array, qtCaracteresLinha);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Arquivo não encontrado:");
                Console.WriteLine(e.Message);
            }

        }

        private static void ImprimirMatrizOriginal(Char[,] array, int tamanho)
        {
            Console.WriteLine("Matriz Quadrada Original !");
            Console.WriteLine();
            for (int i = 0; i < tamanho; i++)
            {
                for (int j = 0; j < tamanho; j++)
                {
                    Console.Write(array[i, j]);
                }
                Console.WriteLine();
            }
        }

        private static void ImprimirMatrizInvertida(Char[,] array, int tamanho)
        {
            Console.WriteLine("\n\nMatriz Quadrada Invertida !");
            Console.WriteLine();
            for (int i = 0; i < tamanho; i++)
            {
                for (int j = 0; j < tamanho; j++)
                {
                    Console.Write(array[j, i]);
                }
                if (i != tamanho - 1) Console.WriteLine();
            }
        }

        private static void CopiarParaNovoArquivo(Char[,] array, int tamanho)
        {
            Console.WriteLine("\n\n\nDeseja salvar este nova matriz ?");
            Console.WriteLine("\nS - SIM\nN - NÃO");
            Char opcao = Console.ReadKey().KeyChar;

            if(opcao.ToString().Trim().ToUpper() == "S")
            {
                StreamWriter salvar = new StreamWriter("../../matrizInvertida.txt");

                for (int i = 0; i < tamanho; i++)
                {
                    for (int j = 0; j < tamanho; j++)
                    {
                        salvar.Write(array[j, i]);
                    }
                    if (i != tamanho - 1) salvar.WriteLine();
                }

                salvar.Close();
            }
        }

        private static Boolean ValidarFormatoMatriz(int qtCaracteresLinha, String linha)
        {
            if (qtCaracteresLinha != linha.Length)
            {
                Console.WriteLine("Matriz em formato incorreto!");
                return false;
            }

            return true;
        }

        private static Boolean ValidarMatrizQuadrada(int coluna, int linha)
        {
            if (coluna != linha)
            {
                Console.WriteLine("Não é uma matriz quadrada! Quantidade de linhas diferente de colunas!");
                return false;
            }
            return true;
        }
    }
}