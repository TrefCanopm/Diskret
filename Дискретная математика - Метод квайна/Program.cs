using System.Numerics;
using System.Text.RegularExpressions;

namespace Дискретная_математика___Метод_квайна
{
    internal class Program
    { 
        //Фспомогательные функции для работы с меню
        void SpisokMenu(ref string[] mas)
        {
            for (int i = 0; i < mas.Length; i++)
            {
                Console.WriteLine((i + 1) + ") " + mas[i]);
            }
        }

        int SelectElement(int length)
        {
            bool isChek = false;
            int element = 0;
            string str;
            do
            {
                str = Console.ReadLine();

                Int32.TryParse(str, out element);

                isChek = element > 0 && element < length + 1;
            } while (!isChek);

            return element;
        }

        //Ввод вектора
        void WrittingDataFile(ref string vector)
        {
            StreamReader file = new StreamReader("D:\\Мои проекты Vethyal studia\\Дискретная математика - Метод квайна\\Вектор.txt");

            string str = "";

            while (!file.EndOfStream)
            {
                str = file.ReadLine();
            }

            Assembling(ref vector, str);
        }

        void InputVector(ref string vector)
        {
            string str = Console.ReadLine();

            Assembling(ref vector, str);
        }

        void Assembling(ref string vector, string str)
        {
            Regex regex = new Regex(@"\w+");

            MatchCollection matches = regex.Matches(str);

            foreach (Match match in matches)
            {
                vector += match.Value;
            }
        }

        void MenuInput(ref string vector)
        {
            string[] mas = new string[3];

            mas[0] = "Ввести вектор из файла";
            mas[1] = "Ввести вектор с клавиатуры";
            mas[2] = "Законьчить работу";

            SpisokMenu(ref mas);
            switch (SelectElement(mas.Length))
            {
                case 1:
                    {
                        WrittingDataFile(ref vector);
                        break;
                    }
                case 2:
                    {
                        InputVector(ref vector);
                        break;
                    }
            }
        }

        //Таблица истиности
        void CreateTableTru(ref char[,] tableTrue, string vector, ref int count)
        {
            string str = "";

            for(int i = 0; i < vector.Length; i++) 
            {
                int j = 0;
                str = Convert.ToString(i, 2);
                for(j = 0; j < 4-str.Length; j++)
                {
                    tableTrue[i, j] = '0';
                }

                for(j = 0; j < str.Length; j++)
                {
                    tableTrue[i, 4 - str.Length + j] = str[j];
                }
            }

            Regex regex = new Regex("[1]");

            MatchCollection matches = regex.Matches(vector);

            count = matches.Count;
        }

        void PrintTableTru(ref char[,] tableTrue, string vector)
        {
            Console.WriteLine("a"+"\t|"+ "b" + "\t|" + "c" + "\t|" + "d" + "\t|" + "F");
            Console.WriteLine("------------------------------------------------------");
            for (int i =0; i < vector.Length; i++)
            {
                for(int j = 0; j < tableTrue.GetLength(1); j++)
                {
                    Console.Write(tableTrue[i, j]+"\t|");
                }
                Console.WriteLine(vector[i]);
            }
        }
        //Сборка СДНФ
        string CreateCDNF(char[,] tableTrue, string vector, ref string [] tableSDNF)
        {
            int count = 0;

            string str = "";
            string element = "";

            for(int i = 0; i < vector.Length; i++)
            {
                if (vector[i] == '1')
                {
                    for(int j = 0; j < tableTrue.GetLength(1);j++)
                    {
                        switch (j) 
                        {
                            case 0:
                                {
                                    if (tableTrue[i, j] == '0') 
                                    {
                                        element += "A"; 
                                    }
                                    else
                                    {
                                        element += "a";
                                    }
                                    break;
                                }
                            case 1:
                                {
                                    if (tableTrue[i, j] == '0')
                                    {
                                        element += "B";
                                    }
                                    else
                                    {
                                        element += "b";
                                    }
                                    break;
                                }
                            case 2:
                                {
                                    if (tableTrue[i, j] == '0')
                                    {
                                        element += "C";
                                    }
                                    else
                                    {
                                        element += "c";
                                    }
                                    break;
                                }
                            case 3:
                                {
                                    if (tableTrue[i, j] == '0')
                                    {
                                        element += "D";
                                    }
                                    else
                                    {
                                        element += "d";
                                    }
                                    break;
                                }
                        }
                    }

                    tableSDNF[count] = element;
                    count++;

                    str += element+"+";

                    element = "";
                }
            }

            return str;
        }

        //Склейка
        string Gluing(string SDNF)
        {
            bool isGluing = false;
            bool isCorectGluing = false;
            bool firstElement = true;

            bool[] isChek;

            string str = "";
            string element = "";

            Regex regex = new Regex(@"\w+");

            do
            {
                MatchCollection matches = regex.Matches(SDNF);
                isChek = new bool[matches.Count];

                firstElement = true;
                isGluing = false;

                for (int i = 0; i < matches.Count - 1; i++)
                {
                    for(int j = i+1; j < matches.Count; j++)
                    {
                        isCorectGluing = CorectGluing(matches[i].Value, matches[j].Value, ref element);

                        if(isCorectGluing)
                        {
                            if (firstElement)
                            {
                                str += element;
                                isChek[i] = true;
                                isChek[j] = true;

                                firstElement = false;
                            }
                            else
                            {
                                str += "+" + element;
                                isChek[i] = true;
                                isChek[j] = true;

                                isGluing = true;
                            }
                            Console.WriteLine(matches[i].Value+"---"+ matches[j].Value+"==="+element);
                        }

                        isCorectGluing = false;
                    }
                }

                Console.WriteLine(str);
                str = DellReplay(str);
                Console.WriteLine(str);
                for (int i = 0; i < isChek.Length; i++)
                {
                    if (!isChek[i])
                    {
                        if (firstElement)
                        {
                            str = matches[i].Value;
                            firstElement= false;
                        }
                        else
                        {
                            str += "+" + matches[i].Value;
                        }
                    }
                }

                SDNF = str;
                str = "";
                Console.WriteLine("------------------");
            } while (isGluing);

            return SDNF;
        }

        bool CorectGluing(string str1, string str2,ref string strResalt)
        {
            int count = 0;

            bool isCorect = true;

            string element = "";

            if(str1.Length == str2.Length)
            {
                for(int i = 0; i < str1.Length; i++)
                {
                    if (str1[i] != str2[i])
                    {
                        switch (str1[i])
                        {
                            case 'a':
                                {
                                    if (str2[i] == 'A')
                                    {
                                        count++;
                                    }
                                    else
                                    {
                                        isCorect = false;
                                    }
                                    break;
                                }
                            case 'b':
                                {
                                    if (str2[i] == 'B')
                                    {
                                        count++;
                                    }
                                    else
                                    {
                                        isCorect = false;
                                    }
                                    break;
                                }
                            case 'c':
                                {
                                    if (str2[i] == 'C')
                                    {
                                        count++;
                                    }
                                    else
                                    {
                                        isCorect = false;
                                    }
                                    break;
                                }
                            case 'd':
                                {
                                    if (str2[i] == 'D')
                                    {
                                        count++;
                                    }
                                    else
                                    {
                                        isCorect = false;
                                    }
                                    break;
                                }
                            case 'A':
                                {
                                    if (str2[i] == 'a')
                                    {
                                        count++;    
                                    }
                                    else
                                    {
                                        isCorect = false;
                                    }
                                    break;
                                }
                            case 'B':
                                {
                                    if (str2[i] == 'b')
                                    {
                                        count++;
                                    }
                                    else
                                    {
                                        isCorect = false;
                                    }
                                    break;
                                }
                            case 'C':
                                {
                                    if (str2[i] == 'c')
                                    {
                                        count++;
                                    }
                                    else
                                    {
                                        isCorect = false;
                                    }
                                    break;
                                }
                            case 'D':
                                {
                                    if (str2[i] == 'd')
                                    {
                                        count++;
                                    }
                                    else
                                    {
                                        isCorect = false;
                                    }
                                    break;
                                }
                        }
                    }
                    else
                    {
                        element += str1[i];
                    }
                }

                if(count == 1 && isCorect)
                {
                    strResalt = element;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        string DellReplay(string str)
        {
            string gluing = "";

            bool isReplay = false;
            bool firstElement = true;

            Regex regex = new Regex(@"\w+");
            MatchCollection matches = regex.Matches(str);

            for(int i = 0; i<matches.Count-1; i++)
            {
                for(int j = i+1; j < matches.Count; j++)
                {
                    if (matches[i].Value == matches[j].Value)
                    {
                        isReplay = true;
                    }
                }

                if(!isReplay)
                {
                    if(firstElement)
                    {
                        gluing = matches[i].Value;
                        firstElement = false;
                    }
                    else
                    {
                        gluing += "+"+matches[i].Value;
                    }
                }

                isReplay = false;
            }

            if (matches.Count > 0)
            {
                gluing += "+" + matches[matches.Count - 1].Value;
            }
            return gluing;
        }

        void CreatingTableGlaing(ref string[] tableGlaing, string glaing)
        {
            Regex regex = new Regex(@"\w+");
            MatchCollection matches = regex.Matches(glaing);

            tableGlaing = new string[matches.Count];

            for(int i = 0; i < matches.Count; i++)
            {
                tableGlaing[i] = matches[i].Value;
            }
        }

        //Покрытие
        void DefinitionCoverage(string[] tableSDNF, string[] tableGlaing)
        {
            bool[,] cover = new bool[tableGlaing.Length, tableSDNF.Length];
            bool isChek = true;
            int index = 0;
            for(int i = 0; i < tableGlaing.Length; i++)
            {
                for(int j = 0; j < tableSDNF.Length; j++)
                {
                    for(int k = 0; k < tableGlaing[i].Length; k++)
                    {
                        index = tableSDNF[j].IndexOf(tableGlaing[i][k]);

                        if(index == -1)
                        {
                            isChek = false;
                        }
                    }

                    if(isChek)
                    {
                        cover[i, j] = true;
                    }

                    isChek = true;
                }
            }

            Console.Write("\t|");

            for(int i = 0; i < tableSDNF.Length; i++)
            {
                Console.Write(tableSDNF[i]+"\t|");
            }
            Console.WriteLine();
            Console.WriteLine("--------------------------------------------------------------------------------");

            for(int i = 0; i < tableGlaing.Length; i++)
            {
                Console.Write(tableGlaing[i] + "\t|");

                for(int j = 0; j < tableSDNF.Length; j++)
                {
                    if (cover[i, j]) 
                    {
                        Console.Write("+"+"\t|");
                    }
                    else
                    {
                        Console.Write("\t|");
                    }
                }
                Console.WriteLine();
                Console.WriteLine("--------------------------------------------------------------------------------");
            }

            SearchMinCoverage(cover, tableGlaing);
        }

        void SearchMinCoverage(bool[,] cover, string[] tableGlaing)
        {
            bool minCover = false;

            int count = 0;
            int startCount = 0;

            bool[] isChekCover = new bool[cover.GetLength(1)];
            int[] counter= new int[tableGlaing.Length];
            
            while (!minCover)
            {
                for(int i = 0; i < counter.Length; i++) 
                {
                    
                }
            }
        }

        void Plus(ref int[] counter, int count)
        {
            
        }

        bool Chek(bool[] cover)
        {
            for(int i = 0; i < cover.GetLength(0);i++)
            {
                if (!cover[i])
                {
                    return false;
                }
            }
            return true;
        }

        //Основное меню
        void Menu()
        {
            string vector = "";
            string gluing = "";

            bool isEnd = false;

            string[] mas = new string[6];

            char[,] tableTrue = new char[0, 0];

            string[] tableSDNF = new string[0];

            string[] tableGluing = new string[0];

            string SDNF = "";

            int countCDNF = 0;

            mas[0] = "Ввод вектора";
            mas[1] = "Вывод таблицы истиности";
            mas[2] = "Создание СДНФ";
            mas[3] = "Не полное склеивание";
            mas[4] = "Определение покрытия";
            mas[5] = "Законьчить работу";

            do
            {
                SpisokMenu(ref mas);

                switch (SelectElement(mas.Length))
                {
                    case 1:
                        {
                            MenuInput(ref vector);
                            tableTrue = new char[vector.Length, 4];

                            CreateTableTru(ref tableTrue, vector, ref countCDNF);
                            tableSDNF = new string[countCDNF];

                            break;
                        }
                    case 2:
                        {
                            PrintTableTru(ref tableTrue, vector);
                            break;
                        }
                    case 3:
                        {
                            SDNF = CreateCDNF(tableTrue, vector, ref tableSDNF);
                            Console.WriteLine(SDNF);
                            break;
                        }
                    case 4:
                        {
                            gluing = Gluing(SDNF);
                            CreatingTableGlaing(ref tableGluing, gluing);
                            Console.WriteLine(gluing);
                            break;
                        }
                    case 5:
                        {
                            DefinitionCoverage(tableSDNF, tableGluing);
                            break;
                        }
                    case 6:
                        {
                            isEnd = false;
                            break;
                        }
                }
            } while (!isEnd);
        }

        static void Main()
        {
            Program main = new Program();

            main.Menu();
        }
    }
}