using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VizinerDeshifrator
{
    class ViginereTable
    {
        public delegate void GetLine(string line);
        static GetLine _GetLine;
        static GetLine _GetLetter;

        public static void RegisterLinePrinter(GetLine _line)
        {
            _GetLine += _line;
        }

        public static void RegisterLetterPrinter(GetLine _letter)
        {
            _GetLetter += _letter;
        }


        public char[,] Table { get; set; }
        public int NumberOfLetters { get; set; }

        public ViginereTable(string Alphabet)
        {
            NumberOfLetters = Alphabet.Length;
            Alphabet += Alphabet;

            Table = new char[NumberOfLetters, NumberOfLetters];
            for (int k = 0; k < NumberOfLetters * 2; k++)
            {
                for (int i = 0; i < NumberOfLetters; i++)
                {
                    for (int j = 0; j < NumberOfLetters; j++)
                    {
                        if (i + j == k)
                        { Table[i, j] = Alphabet[k]; }
                    }

                }

            }

        }


        public static implicit operator char[,] (ViginereTable viginereTable)
        {
            return viginereTable.Table;
        }


        static public void VisinerTablePrint(char[,] Table)
        {
            _GetLine("");
            _GetLine("--------------------Таблица Вижинера----------------------------");
            _GetLine("_________________________________________________________________\n");
            for (int i = 0; i < Table.GetLength(0); i++)
            {
                for (int j = 0; j < Table.GetLength(1); j++)
                {
                    _GetLetter($"{Table[i, j]} ");
                }
                _GetLine("");
            }
            _GetLine("_________________________________________________________________\n");
        }

        public int[] GetKeyColunmIndex(string key)
        {
            int CurrentLetterIndex = 0;
            int[] KeyColumnIndex = new int[key.Length];
            while (CurrentLetterIndex < key.Length)
            {
                for (int CurrentIndex = 0; CurrentIndex < NumberOfLetters; CurrentIndex++)
                {
                    if (key[CurrentLetterIndex] == Table[0, CurrentIndex])
                    {
                        KeyColumnIndex[CurrentLetterIndex] = CurrentIndex;
                        CurrentLetterIndex++;
                        break;
                    }
                }
            }

            return KeyColumnIndex;
        }
    }
}
