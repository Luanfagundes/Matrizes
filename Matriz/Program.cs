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
                String linha, nomeDoArquivo;
                Int32 qtColunas = 0, qtCaracteresLinha = 0;
                Char[,] matrizOriginal = new Char[0, 0];
                Int16 opcaoMenu = 0;

                nomeDoArquivo = PerguntarNomeArquivo();

                using (StreamReader arquivo = new StreamReader("../../" + nomeDoArquivo))
                {
                    while ((linha = arquivo.ReadLine()) != null)
                    {
                        //Popula os campos na primeira execução.
                        if (qtCaracteresLinha == 0)
                        {
                            qtCaracteresLinha = linha.Length;
                            matrizOriginal = new Char[linha.Length, linha.Length];
                        }

                        //Salva no array na variavel array.
                        for (int posicaoLinha = 0; posicaoLinha < linha.Length; posicaoLinha++)
                        {
                            matrizOriginal[qtColunas, posicaoLinha] = linha[posicaoLinha];
                        }

                        qtColunas++;
                    }

                    while (opcaoMenu != 57)
                    {
                        Menu();
                        opcaoMenu = Convert.ToInt16(Console.ReadKey().KeyChar);
                        Console.Clear();

                        switch (opcaoMenu)
                        {
                            case 49:
                                ImprimirMatrizOriginal(matrizOriginal, qtCaracteresLinha);
                                break;
                            case 50:
                                ImprimirMatrizInvertida(matrizOriginal, qtCaracteresLinha);
                                break;
                            case 51:
                                break;
                            case 52:
                                CopiarParaNovoArquivo(matrizOriginal, qtCaracteresLinha);
                                break;
                            case 57:
                                break;
                            default:
                                Console.WriteLine("\nOpção invalida.");
                                break;
                        }
                        if (opcaoMenu != 57)
                        {
                            Console.WriteLine("\n\n\n\n\t\t\t\t\t\tPressione uma tecla para voltar!");
                            Console.ReadKey();
                        }
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("\nMatriz em formato incorreto.");
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + e.Message);
                Console.ReadKey();
            }

        }

        private static void Menu()
        {
            Console.Clear();
            String tabulacao = "\t\t\t\t\t*", pularLinhaComTabulacao = "\n" + tabulacao;
            String textoMenu = (tabulacao + "****************************************" +
                        pularLinhaComTabulacao + "\tEscolha uma das opções \t\t*" +
                        pularLinhaComTabulacao + tabulacao +
                        pularLinhaComTabulacao + "\t1 - Imprimir matriz \t\t*" +
                        pularLinhaComTabulacao + "\t2 - Inverter matriz \t\t*" +
                        pularLinhaComTabulacao + "\t3 - Trocar caractere da matriz \t*" +
                        pularLinhaComTabulacao + "\t4 - Salvar matriz \t\t*" +
                        pularLinhaComTabulacao + tabulacao +
                        pularLinhaComTabulacao + "\t9 - Sair! \t\t\t*" +
                        pularLinhaComTabulacao + tabulacao +
                        pularLinhaComTabulacao + "****************************************");

            Console.WriteLine(textoMenu);
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

            if (opcao.ToString().Trim().ToUpper() == "S")
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

        private static String PerguntarNomeArquivo()
        {
            Console.Write("Digite o nome do arquivo TXT a ser a ser imprimido: ");
            String nomeDoArquivo = Console.ReadLine().ToString().Trim().ToLower();

            if (String.IsNullOrEmpty(nomeDoArquivo)) nomeDoArquivo = "matriz";

            if (!(nomeDoArquivo.Split('.').Length > 1))
            {
                nomeDoArquivo = nomeDoArquivo + ".txt";
            }

            return nomeDoArquivo;
        }
    }
}