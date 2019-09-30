using System;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Matrizes
{
    class Program2
    {
        public static string[] cabecalho;
        public static string[,] matriz1 = new string[33, 33];
        public static string[,] matriz2 = new string[129, 129];
        public static string[,] matriz3 = new string[193, 193];

        public static void Main(string[] args)
        {
            try
            {
                String nomeDoArquivo;
                Int16 opcaoMenu = 0;
                string[,] matriz;

                nomeDoArquivo = PerguntarNomeArquivo();
                
                using (StreamReader arquivo = new StreamReader("../../" + nomeDoArquivo))
                {
                    string file3 = new StreamReader("../../" + "tcened.txt").ReadToEnd();
                    string file2 = new StreamReader("../../" + "tcevo.txt").ReadToEnd();
                    string file1 = new StreamReader("../../" + "tted.txt").ReadToEnd();
                    string file = new StreamReader("../../" + nomeDoArquivo).ReadToEnd();
                    cabecalho = file.Replace("\r", String.Empty).Split('\n').Where((x, i) => i <= 3).ToArray();
                    string[] lines1 = file1.Replace("\r", String.Empty).Split('\n').Where((x, i) => i > 3).ToArray();
                    string[] lines2 = file2.Replace("\r", String.Empty).Split('\n').Where((x, i) => i > 3).ToArray();
                    string[] lines3 = file3.Replace("\r", String.Empty).Split('\n').Where((x, i) => i > 3).ToArray();
                    string[] lines = file.Replace("\r", String.Empty).Split('\n').Where((x, i) => i > 3).ToArray();
                    string[] col = new string[0];

                    //elimina os espaços duplos
                    /*for (int i = 0; i < lines.Length; i++)
                    {
                        lines[i] = Regex.Replace(lines[i].Trim(), @"\s+", " ");
                    }*/

                    for (int i = 0; i < lines.Length; i++)
                    {
                        //ValidarFormatoMatriz(lines.Length, lines[i].Split(' ').Length);
                        //ValidarConteudo(lines[i].Split(' '));

                        if (col.Length < lines[i].Split(' ').Length) col = new string[lines[i].Split(' ').Length];
                    }

                    //popula a matriz gerais.
                    matriz = new string[lines.Length, col.Length];

                    for (int i = 0; i < lines1.Length; i++)
                    {
                        for (int j = 0; j < lines1[i].Split(' ').Length; j++)
                        {
                            matriz1[i, j] = lines1[i].Split(' ')[j];
                        }
                    }

                    for (int i = 0; i < lines3.Length; i++)
                    {
                        for (int j = 0; j < lines3[i].Split(' ').Length; j++)
                        {
                            matriz3[i, j] = lines3[i].Split(' ')[j];
                        }
                    }

                    for (int i = 0; i < lines2.Length; i++)
                    {
                        for (int j = 0; j < lines2[i].Split(' ').Length; j++)
                        {
                            matriz2[i, j] = lines2[i].Split(' ')[j];
                        }
                    }

                    for (int i = 0; i < lines.Length; i++)
                    {
                        for (int j = 0; j < lines[i].Split(' ').Length; j++)
                        {
                            matriz[i, j] = lines[i].Split(' ')[j];
                        }
                    }

                    while (opcaoMenu != 48)
                    {
                        Menu();
                        opcaoMenu = Convert.ToInt16(Console.ReadKey().KeyChar);
                        Console.Clear();

                        switch (opcaoMenu)
                        {
                            case 49:
                                ImprimirMatrizOriginal(matriz, lines.Length, col.Length);
                                break;
                            case 50:
                                matriz = InverterMatriz(matriz, lines.Length, col.Length);
                                break;
                            case 52:
                                CopiarParaNovoArquivo(matriz, lines.Length, col.Length);
                                break;
                            case 51:
                                matriz = TrocarCaractere(matriz, lines.Length, col.Length);
                                break;
                            case 53:
                                ConvertParaImagem(matriz, lines.Length, col.Length);
                                break;
                            case 54:
                            case 55:
                            case 56:
                            case 57:
                                ReduzirImagem(matriz, opcaoMenu, lines.Length, col.Length);
                                break;
                            case 9:
                                GerarHistograma(matriz, lines.Length, col.Length);
                                break;
                            default:
                                Console.WriteLine("\nOpção invalida.");
                                break;
                        }
                        if (opcaoMenu != 48)
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

        private static void GerarHistograma(string[,] matriz, int linha, int coluna)
        {
            List<String> qtindice = new List<String>();
            Dictionary<String, Int64> qtdindicevalor = new Dictionary<String, Int64>();

            for (int i = 0; i < linha; i++)
            {
                for (int j = 0; j < coluna; j++)
                {
                    if (!String.IsNullOrEmpty(matriz[i, j]))
                    {
                        if (qtindice.FindAll(x => x.ToString() == matriz[i, j].ToString()).Count == 0) qtindice.Add(matriz[i, j].ToString());
                    }
                }
            }

            qtindice.Sort();

            for (int k = 0; k < qtindice.Count; k++)
            {
                Int64 valor = 0;
                for (int i = 0; i < linha; i++)
                {
                    for (int j = 0; j < coluna; j++)
                    {
                        if (!String.IsNullOrEmpty(matriz[i, j]) && matriz[i, j] == qtindice[k])
                        {
                            valor++;
                        }
                    }
                }
                qtdindicevalor.Add(qtindice[k], valor);
            }

            String caminho = "Histograma.txt";
            StreamWriter salvar = new StreamWriter("../../" + caminho);


            foreach (var item in qtdindicevalor)
            {
                salvar.WriteLine(item.Key + " " + item.Value);
            }

            salvar.Close();
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
                        pularLinhaComTabulacao + "\t4 - Salvar matriz em .TXT \t*" +
                        pularLinhaComTabulacao + "\t5 - Salvar nova IMAGEM \t\t*" +
                        pularLinhaComTabulacao + tabulacao +
                        pularLinhaComTabulacao + "\t6 - Diminuir a Imagem em 25% \t*" +
                        pularLinhaComTabulacao + "\t7 - Diminuir a Imagem em 50% \t*" +
                        pularLinhaComTabulacao + "\t8 - Diminuir a Imagem em 75% \t*" +
                        pularLinhaComTabulacao + "\t9 - Deixa a Imagem 100% \t*" +
                        pularLinhaComTabulacao + tabulacao +
                        pularLinhaComTabulacao + "\tTAB - Gerar histograma \t\t*" +
                        pularLinhaComTabulacao + tabulacao +
                        pularLinhaComTabulacao + "\t0 - Sair! \t\t\t*" +
                        pularLinhaComTabulacao + tabulacao +
                        pularLinhaComTabulacao + "****************************************");

            Console.WriteLine(textoMenu);
        }

        private static void ConvertParaImagem(string[,] array, int linhas, int colunas)
        {
            /*  Converter para PGM
                    magick convert PDI/yyoriginal.jpg -colorspace gray -compress none -depth 8 PDI/yypgm.pgm

                Converter para JPG
                    magick convert PDI/yypgm.pgm PDI/yyconvertidopgn.jpg
            */
            try
            {
                CopiarParaNovoArquivo(array, linhas, colunas);

                Process p = new Process();
                ProcessStartInfo info = new ProcessStartInfo();
                info.FileName = "cmd.exe";
                info.RedirectStandardInput = true;
                info.UseShellExecute = false;

                p.StartInfo = info;
                p.Start();

                using (StreamWriter sw = p.StandardInput)
                {
                    if (sw.BaseStream.CanWrite)
                    {
                        sw.WriteLine("cd " + AppDomain.CurrentDomain.BaseDirectory.ToString());
                        sw.WriteLine("cd../../../magik");
                        sw.WriteLine("magick convert ../Matriz/NovaMatriz.txt PDI/NovaImagem.jpg");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + e.Message);
            }
        }

        private static void ReduzirImagem(string[,] array, short opcaoMenu, int linha, int coluna)
        {

            int tamanho = opcaoMenu == 54 ? 193 : opcaoMenu == 55 ? 129 : 65;

            if (opcaoMenu == 57) tamanho = 257;

            string[,] novoArray = new string[linha, coluna];
            novoArray = array;

            //for (int i = 0; i < linha; i++)
            //{
            //    for (int j = 0; j < coluna; j++)
            //    {
            //        novoArray[i, j] = array[i, i];
            //    }
            //}

            cabecalho[2] = String.Format("{0} {1}", (tamanho - 1).ToString(), (tamanho - 1).ToString());

            if (opcaoMenu == 54)
            {
                novoArray = new string[193, 193];
                novoArray = matriz3;
                cabecalho[2] = "192 192";

                tamanho = 193;
            }

            if (opcaoMenu == 55)
            {
                novoArray = new string[129, 129];
                novoArray = matriz2;
                cabecalho[2] = "128 128";

                tamanho = 129;
            }

            if (opcaoMenu == 56)
            {
                novoArray = new string[33, 33];
                novoArray = matriz1;
                cabecalho[2] = "32 32";

                tamanho = 33;
            }

            ConvertParaImagem(novoArray, tamanho, tamanho);
        }

        private static void ImprimirMatrizOriginal(string[,] array, int linhas, int colunas)
        {
            for (int i = 0; i < linhas; i++)
            {
                for (int j = 0; j < colunas; j++)
                {
                    Console.Write(array[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        private static string[,] InverterMatriz(string[,] array, int linhas, int colunas)
        {
            string[,] novoArray = new string[linhas, colunas];

            for (int i = 0; i < linhas; i++)
            {
                for (int j = 0; j < colunas; j++)
                {
                    novoArray[i, j] = array[j, i];
                }
            }

            return novoArray;
        }

        private static string[,] TrocarCaractere(string[,] antigoArray, int linhas, int colunas)
        {
            string[,] novoArray = new string[linhas, colunas];
            int caractere;
            int novoValor;

            Console.Write("\nDigite o VALOR que deseja alterar: ");
            caractere = Convert.ToInt32(Console.ReadLine());

            Console.Write("\nDigite o NOVO VALOR a ser alterado: ");
            novoValor = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < linhas; i++)
            {
                for (int j = 0; j < colunas; j++)
                {
                    if (!String.IsNullOrEmpty(antigoArray[i, j]))
                    {
                        if (caractere == Convert.ToInt32(antigoArray[i, j]))
                        {
                            novoArray[i, j] = novoValor.ToString();
                        }
                        else
                        {
                            novoArray[i, j] = antigoArray[i, j];
                        }
                    }
                }
            }

            //Console.Clear();
            //ImprimirMatrizOriginal(antigoArray, linhas, colunas);
            //Console.WriteLine("\n\n");
            //ImprimirMatrizOriginal(novoArray, linhas, colunas);

            return novoArray;
        }

        private static void CopiarParaNovoArquivo(string[,] array, int linhas, int colunas)
        {
            //TODO 4 primeiras linhas da imagem estão fixa
            /* P2
             * 256 256
             * 255
            */

            String caminho = "NovaMatriz.txt";
            StreamWriter salvar = new StreamWriter("../../" + caminho);

            for (int k = 0; k < cabecalho.Length; k++)
            {
                salvar.WriteLine(cabecalho[k]);
            }

            for (int i = 0; i < linhas; i++)
            {
                for (int j = 0; j < colunas; j++)
                {
                    salvar.Write(array[i, j] );
                    if (j != colunas - 1) salvar.Write(" ");
                }
                if (i != linhas - 1) salvar.WriteLine();
            }

            Console.WriteLine("\nMatriz salva com o nome " + caminho);
            salvar.Close();
        }

        private static void ValidarConteudo(string[] linha)
        {
            for (int i = 0; i < linha.Length; i++)
            {
                if (Convert.ToInt32(linha[i]) > 255)
                {
                    throw new ArgumentException("Valor exece o limite de 255");
                }

                if (Convert.ToInt32(linha[i]) < 0)
                {
                    throw new ArgumentException("Valor inferior que 0");
                }
            }
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