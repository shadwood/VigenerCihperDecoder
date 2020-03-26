using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VizinerDeshifrator
{
    class Program
    {
        //Vigenère cipher

        const double IndSovp = 0.553;
        const string EngAlphabet = "abcdefghijklmnopqrstuvwxyz";
        const string RusAlphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";


        static void Main(string[] args)
        {


            string Key;
            string Text;
            string CopyText;
            string KeyWord;
            char[,] Table = new char[0, 0];
            string OutputText = "";
            int KeyLetterCounter = 0;
            int[] KeyColumnIndex;
            int LetterRowIntex;
            bool PowerOn = true;

            string Language = "русский";
            string Alphabet = RusAlphabet;
            int NumberOfLetters = Alphabet.Length;

            ViginereTable VishinerTable = new ViginereTable(Alphabet);
            Table = VishinerTable;

            ViginereTable.RegisterLinePrinter(new ViginereTable.GetLine(Console.WriteLine));
            ViginereTable.RegisterLetterPrinter(new ViginereTable.GetLine(Console.Write));

            while (PowerOn)
            {
                Console.WriteLine($"\nВыберете режим:\n" + '\n' +
                    $"1 - Шифрование открытого текста" + '\n' +
                    $"2 - Расшифрофка закрытого текста" + '\n' +
                    $"3 - Взлом шифра(в разработке)\n" +
                    $"4 - вывод таблицы Вижинера\n" +
                    $"5 - сменить язык(текущий : {Language})" + '\n' +
                    $"6 - Выход" + '\n');

                Key = Console.ReadLine();

                switch (Key)
                {
                    case "1"://шифрование
                        Console.Clear();
                        Console.WriteLine($"1 - Шифрование открытого текста\n" +
                            $"Язык : {Language}\n");
                        Console.WriteLine("\nВведите ключевое слово");
                        KeyWord = (Console.ReadLine()).ToLower();
                        Console.WriteLine("Введите текст сообщения");

                        KeyLetterCounter = 0;
                        Text = (Console.ReadLine()).ToLower();
                        OutputText = "";
                        KeyColumnIndex = new int[KeyWord.Length];
                        KeyColumnIndex = VishinerTable.GetKeyColunmIndex(KeyWord);

                        if (Language == "русский")
                        {

                            foreach (char letter in Text)
                            {

                                if (( (int)letter > 1071 && (int)letter < 1104) || letter == 'ё')
                                {

                                    for (LetterRowIntex = 0; LetterRowIntex < NumberOfLetters; LetterRowIntex++)
                                    {
                                        if (letter == Table[LetterRowIntex, 0]) { break; }
                                    }

                                    OutputText += Table[LetterRowIntex, KeyColumnIndex[KeyLetterCounter]];
                                }
                                else
                                {
                                    OutputText += letter;
                                }

                                if (KeyLetterCounter < KeyWord.Length - 1) { KeyLetterCounter++; }
                                else { KeyLetterCounter = 0; }
                            }
                        }
                        else
                        {
                            foreach (char letter in Text)
                            {

                                if ((int)letter < 123 && (int)letter > 96)
                                {

                                    for (LetterRowIntex = 0; LetterRowIntex < NumberOfLetters; LetterRowIntex++)
                                    {
                                        if (letter == Table[LetterRowIntex, 0]) { break; }
                                    }

                                    OutputText += Table[LetterRowIntex, KeyColumnIndex[KeyLetterCounter]];
                                }
                                else
                                {
                                    OutputText += letter;
                                }

                                if (KeyLetterCounter < KeyWord.Length - 1) { KeyLetterCounter++; }
                                else { KeyLetterCounter = 0; }
                            }
                        }
                            Console.WriteLine(OutputText);
                            Console.ReadKey();
                            break;

                        case "2"://расшифровка с паролем

                            Console.Clear();
                            Console.WriteLine($"2 - Расшифрофка закрытого текста\n" +
                                $"Язык : {Language}");
                            Console.WriteLine("\nВведите ключевое слово");
                            KeyWord = (Console.ReadLine()).ToLower();
                            Console.WriteLine("Введите текст сообщения");

                            Text = (Console.ReadLine()).ToLower();
                            KeyLetterCounter = 0;
                            OutputText = "";
                            KeyColumnIndex = new int[KeyWord.Length];
                            KeyColumnIndex = VishinerTable.GetKeyColunmIndex(KeyWord);
                            foreach (char letter in Text)
                            {
                                //Console.WriteLine(letter+" - "+(int)letter);

                                if ((((int)letter < 1104 && (int)letter > 1071) || letter == 'ё') || ((int)letter < 123 && (int)letter > 96))
                                {

                                    for (LetterRowIntex = 0; LetterRowIntex < NumberOfLetters; LetterRowIntex++)
                                    {
                                        if (letter == Table[LetterRowIntex, KeyColumnIndex[KeyLetterCounter]]) { break; }
                                    }

                                    OutputText += Table[LetterRowIntex, 0];
                                }
                                else
                                {
                                    OutputText += letter;
                                }

                                if (KeyLetterCounter < KeyWord.Length - 1) { KeyLetterCounter++; }
                                else { KeyLetterCounter = 0; }

                            }

                            Console.WriteLine(OutputText);
                            Console.ReadKey();
                            break;

                        case "3"://взлом
                            Console.WriteLine("3 - Взлом шифра(в разработке)\n");
                            Console.Clear();
                            Text = Console.ReadLine();
                            CopyText = Text;

                            //счетчик букв
                            int[] LetterCounter = new int[NumberOfLetters];
                            for (int i = 0; i < NumberOfLetters; i++)
                            {
                                LetterCounter[i] = 0;
                            }

                            foreach (char letter in Text)
                            {
                                for (int i = 0; i < NumberOfLetters; i++)
                                {
                                    if (letter == Alphabet[i])
                                    {
                                        LetterCounter[i]++;
                                    }
                                }
                            }

                            for (int i = 0; i < NumberOfLetters; i++)
                            {
                                Console.WriteLine($"{Alphabet[i]} :: { LetterCounter[i]}");
                            }
                            break;

                        case "4"://print VishinerTable to the screen
                            Console.Clear();
                            ViginereTable.VisinerTablePrint(Table);
                            break;

                        case "5"://change laungeage
                            Console.Clear();
                            if (Alphabet == EngAlphabet)
                            {
                                Alphabet = RusAlphabet;
                                Console.WriteLine("Язык ШИФРА изменен на русский");
                                Language = "русский";
                            }
                            else
                            {
                                Alphabet = EngAlphabet;
                                Console.WriteLine("Язык ШИФРА текстов изменен на английский");
                                Language = "английский";
                            }

                            VishinerTable = new ViginereTable(Alphabet);
                            Table = VishinerTable;

                            break;

                        case "6"://выход
                            PowerOn = false;
                            break;

                        }

                }

            }


        }
    }
