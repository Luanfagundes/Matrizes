using System;
using System.IO;

namespace Matrizes
{
    class Program
    {

        //public static void Main(string[] args)
        //{
        //    try
        //    {
        //        String linha, nomeDoArquivo;
        //        Int32 qtColunas = 0, qtCaracteresLinha = 0;
        //        Char[,] matrizAtual = new Char[0, 0];
        //        Int16 opcaoMenu = 0;

        //        nomeDoArquivo = PerguntarNomeArquivo();

        //        using (StreamReader arquivo = new StreamReader("../../" + nomeDoArquivo))
        //        {
        //            while ((linha = arquivo.ReadLine()) != null)
        //            {

        //                //Popula os campos na primeira execução.
        //                if (qtCaracteresLinha == 0)
        //                {
        //                    qtCaracteresLinha = linha.Length;
        //                    matrizAtual = new Char[linha.Length, linha.Length];
        //                }

        //                //Salva no array na variavel array.
        //                for (int posicaoLinha = 0; posicaoLinha < linha.Length; posicaoLinha++)
        //                {
        //                    matrizAtual[qtColunas, posicaoLinha] = linha[posicaoLinha];
        //                }

        //                //Valida se a linha tem a mesma quantidade caracteres.
        //                ValidarFormatoMatriz(qtCaracteresLinha, linha);

        //                qtColunas++;
        //            }

        //            //Valida se a coluna tem a mesma quantidade de linha.
        //            ValidarMatrizQuadrada(qtColunas, qtCaracteresLinha);

        //            while (opcaoMenu != 57)
        //            {
        //                Menu();
        //                opcaoMenu = Convert.ToInt16(Console.ReadKey().KeyChar);
        //                Console.Clear();

        //                switch (opcaoMenu)
        //                {
        //                    case 49:
        //                        ImprimirMatrizOriginal(matrizAtual, qtCaracteresLinha);
        //                        break;
        //                    case 50:
        //                        ImprimirMatrizOriginal(matrizAtual, qtCaracteresLinha);
        //                        matrizAtual = InverterMatriz(matrizAtual, qtCaracteresLinha);
        //                        Console.WriteLine("\n\n");
        //                        ImprimirMatrizOriginal(matrizAtual, qtCaracteresLinha);
        //                        break;
        //                    case 51:
        //                        ImprimirMatrizOriginal(matrizAtual, qtCaracteresLinha);
        //                        matrizAtual = TrocarCaractere(matrizAtual, qtCaracteresLinha);
        //                        break;
        //                    case 52:
        //                        CopiarParaNovoArquivo(matrizAtual, qtCaracteresLinha);
        //                        break;
        //                    case 57:
        //                        break;
        //                    default:
        //                        Console.WriteLine("\nOpção invalida.");
        //                        break;
        //                }
        //                if (opcaoMenu != 57)
        //                {
        //                    Console.WriteLine("\n\n\n\n\t\t\t\t\t\tPressione uma tecla para voltar!");
        //                    Console.ReadKey();
        //                }
        //            }
        //        }
        //    }
        //    catch (IndexOutOfRangeException)
        //    {
        //        Console.WriteLine("\nMatriz em formato incorreto.");
        //        Console.ReadKey();
        //    }
        //    catch (FormatException e)
        //    {
        //        Console.WriteLine("\nValor incorreto!");
        //        Console.ReadKey();
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("\n" + e.Message);
        //        Console.ReadKey();
        //    }

        //}

        private static Char[,] TrocarCaractere(Char[,] antigoArray, int tamanho)
        {
            Char[,] novoArray = new Char[tamanho, tamanho];
            Char caractere;
            Char novoValor;

            Console.Write("\nDigite o CARACTERE que deseja alterar: ");
            caractere = Console.ReadKey().KeyChar;

            Console.Write("\nDigite o NOVO VALOR a ser alterado: ");
            novoValor = Console.ReadKey().KeyChar;

            for (int i = 0; i < tamanho; i++)
            {
                for (int j = 0; j < tamanho; j++)
                {
                    if (caractere == antigoArray[i, j])
                    {
                        novoArray[i, j] = novoValor;
                    }
                    else
                    {
                        novoArray[i, j] = antigoArray[i, j];
                    }
                }
            }

            Console.Clear();
            ImprimirMatrizOriginal(antigoArray, tamanho);
            Console.WriteLine("\n\n");
            ImprimirMatrizOriginal(novoArray, tamanho);

            return novoArray;
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
            Console.WriteLine("Matriz Quadrada !");
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

        private static Char[,] InverterMatriz(Char[,] array, int tamanho)
        {
            Char[,] novoArray = new Char[tamanho, tamanho];

            for (int i = 0; i < tamanho; i++)
            {
                for (int j = 0; j < tamanho; j++)
                {
                    novoArray[i, j] = array[j, i];
                }
            }

            return novoArray;
        }

        private static void CopiarParaNovoArquivo(Char[,] array, int tamanho)
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

        private static void ValidarFormatoMatriz(int qtCaracteresLinha, String linha)
        {
            if (qtCaracteresLinha != linha.Length)
            {
                throw new ArgumentException("Matriz em formato incorreto!", "Matriz não quadrada");
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

            if (String.IsNullOrEmpty(nomeDoArquivo)) nomeDoArquivo = "MatrizAtual";

            if (!(nomeDoArquivo.Split('.').Length > 1))
            {
                nomeDoArquivo = nomeDoArquivo + ".txt";
            }

            return nomeDoArquivo;
        }
    }
}