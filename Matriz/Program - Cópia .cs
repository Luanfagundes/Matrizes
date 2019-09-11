using System;
using System.IO;

namespace Matrizes
{
    class Program2
    {

        public static void Main(string[] args)
        {
            try
            {
                String nomeDoArquivo;
                int[,] matriz;
                Int16 opcaoMenu = 0;

                nomeDoArquivo = PerguntarNomeArquivo();

                using (StreamReader arquivo = new StreamReader("../../" + nomeDoArquivo))
                {
                    string file = new StreamReader("../../" + nomeDoArquivo).ReadToEnd();
                    string[] lines = file.Replace("\r", String.Empty).Split('\n');
                    string[] col;

                    int valorMax = file.Replace("\n", String.Empty).Replace("\r", String.Empty).Split(' ').Length;
                    matriz = new int[valorMax, valorMax];

                    while (valorMax < 0)
                    {

                        for (int i = 0; i < valorMax; i++)
                        {
                            col = lines[i].Split(' ');
                            ValidarFormatoMatriz(lines.Length, col.Length);

                            //Popula os campos na primeira execução.
                            //qtCaracteresLinha = linha[i].Split(' ').Length;

                            //matrizAtual = new int[countOfLines, qtCaracteresLinha];

                            //matrizAtual[qtColunas, posicaoLinha] = int.Parse(linha[posicaoLinha].ToString());
                        }

                        valorMax--;
                    }

                    while (opcaoMenu != 57)
                    {
                        Menu();
                        opcaoMenu = Convert.ToInt16(Console.ReadKey().KeyChar);
                        Console.Clear();

                        switch (opcaoMenu)
                        {
                            //case 49:
                            //    ImprimirMatrizOriginal(matrizAtual, qtCaracteresLinha);
                            //    break;
                            //case 50:
                            //    ImprimirMatrizOriginal(matrizAtual, qtCaracteresLinha);
                            //    matrizAtual = InverterMatriz(matrizAtual, qtCaracteresLinha);
                            //    Console.WriteLine("\n\n");
                            //    ImprimirMatrizOriginal(matrizAtual, qtCaracteresLinha);
                            //    break;
                            //case 52:
                            //    CopiarParaNovoArquivo(matrizAtual, qtCaracteresLinha);
                            //    break;
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
            catch (FormatException e)
            {
                Console.WriteLine("\nValor incorreto!");
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

        private static void ImprimirMatrizOriginal(int[,] array, int tamanho)
        {
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

        private static int[,] InverterMatriz(int[,] array, int tamanho)
        {
            int[,] novoArray = new int[tamanho, tamanho];

            for (int i = 0; i < tamanho; i++)
            {
                for (int j = 0; j < tamanho; j++)
                {
                    novoArray[i, j] = array[j, i];
                }
            }

            return novoArray;
        }

        private static void CopiarParaNovoArquivo(int[,] array, int tamanho)
        {
            String caminho = "NovaMatriz.txt";
            StreamWriter salvar = new StreamWriter("../../" + caminho);

            for (int i = 0; i < tamanho; i++)
            {
                for (int j = 0; j < tamanho; j++)
                {
                    salvar.Write(array[i, j]);
                }
                if (i != tamanho - 1) salvar.WriteLine();
            }

            Console.WriteLine("\nMatriz salva com o nome " + caminho);
            salvar.Close();
        }

        private static void ValidarFormatoMatriz(int qtCaracteresLinha, int qtCaracteresColunas)
        {
            if (qtCaracteresLinha % 2 != 0 || qtCaracteresColunas % 2 != 0)
            {
                throw new ArgumentException("Matriz em formato incorreto!", "Matriz não deve ter número par de linhas e colunas.");
            }
            if (qtCaracteresLinha < 50 || qtCaracteresColunas < 50)
            {
                throw new ArgumentException("Matriz em formato incorreto!", "Matriz deve ter tamanho de, no mínimo, 50x50.");
            }
        }

        private static void ValidarMatrizQuadrada(int coluna, int linha)
        {
            if (coluna != linha)
            {
                throw new ArgumentException("Não é uma matriz quadrada! Quantidade de linhas diferente de colunas!",
                    "Matriz não quadrada");
            }
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