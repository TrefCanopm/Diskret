using System.Text.RegularExpressions;
using C__10;

namespace Дискретка_раскраска
{
    internal class Program
    {
        //********************************************************
        static void EnteringMatrix(int[,] matrix)
        {
            Console.WriteLine("Введите размерность матрицы");

            int size = 0;
            Int32.TryParse(Console.ReadLine(), out size);

            matrix = new int[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.WriteLine("Введите элемент в ечейка (" + (i + 1) + ", " + (j + 1) + ") ");
                    Int32.TryParse(Console.ReadLine(), out matrix[i, j]);
                }
            }
        }

        static string Assembling(string str)
        {
            string vector = "";
            Regex regex = new Regex(@"\w+");

            MatchCollection matches = regex.Matches(str);

            foreach (Match match in matches)
            {
                vector += match.Value;
            }

            return vector;
        }

        static void EnteringMatrixFile(ref int[,] matrix)
        {
            StreamReader file = new StreamReader("D:\\Мои проекты Vethyal studia\\Дискретка-раскраска\\Matrix.txt");

            string str = "";
            int count = 1;

            if (!file.EndOfStream)
            {
                str = file.ReadLine();

                str = Assembling(str);

                matrix = new int[str.Length, str.Length];

                for (int i = 0; i < str.Length; i++)
                {
                    if (str[i] != '0')
                    {
                        string s = Convert.ToString(str[i]);
                        Int32.TryParse(s, out matrix[0, i]);
                    }
                    else
                    {
                        matrix[0, i] = 0;
                    }
                }

                while (!file.EndOfStream)
                {
                    str = file.ReadLine();
                    str = Assembling(str);
                    for (int i = 0; i < str.Length; i++)
                    {
                        if (str[i] != '0')
                        {
                            string s = Convert.ToString(str[i]);
                            Int32.TryParse(s, out matrix[count, i]);
                        }
                        else
                        {
                            matrix[count, i] = 0;
                        }
                    }

                    count++;
                }

            }
            else
            {
                Console.WriteLine("Файл пуст");
            }
        }

        static void InputMatrix(ref int[,] matrix)
        {
            bool isEnd = false;
            string[] mas = new string[3];

            mas[0] = "1) Ввод в ручную";
            mas[1] = "2) Ввод из файла";

            foreach (string str in mas) { Console.WriteLine(str); }

            Console.WriteLine("Ввидите номер действия");

            int number = 0;
            Int32.TryParse(Console.ReadLine(), out number);

            if (number == 1)
            {
                EnteringMatrix(matrix);
            }
            else
            {
                EnteringMatrixFile(ref matrix);
            }
        }
        //********************************************************

        public static void ShowMatrix(int[,] matrix)
        {
            for(int i = 0; i < matrix.GetLength(0);i++)
            {
                for(int j = 0; j < matrix.GetLength(1);j++)
                {
                    Console.Write(matrix[i, j]+"|\t");
                }
                Console.WriteLine();
                Console.WriteLine("--------------------------------------------------------");
            }
        }
        //********************************************************

        public static void ColoringMatrix(int[,] matrix)
        {
            int countColor = 1;

            int[] colorVert = new int[matrix.GetLength(0)];

            colorVert[0] = 0;
            for(int i = 1; i < colorVert.Length; i++)
            {
                colorVert[i] = -1; 
            }

            int[] vert = new int[1];
            vert[0] = 0;

            NewVert(vert, ref vert, colorVert, matrix);

            while(!Chek(colorVert))
            {
                for(int i = 0; i < vert.Length; i++)
                {
                    int[] strMatrix = new int[matrix.GetLength(0)];
                    for (int j = 0; j < matrix.GetLength(0); j++)
                    {
                        strMatrix[j] = matrix[vert[i], j];
                    }

                    ColorVert(vert[i], strMatrix, colorVert, ref countColor);
                }

                NewVert(vert, ref vert, colorVert, matrix);
            }

            Print(colorVert);
        }

        public static bool Chek(int[] color)
        {
            for(int i = 0; i < color.Length; i++)
            {
                if (color[i] == -1)
                {
                    return false;
                }
            }
            return true;
        }

        public static void NewVert( int[] vert, ref int[] newVert, int[] colorVert, int[,] matrix)
        {
            int count = 0;
            int[] chekVert = new int[matrix.GetLength(0)];

            for(int i = 0; i < chekVert.Length;i++)
            {
                chekVert[i] = 0;
            }

            for(int i = 0; i < vert.Length; i++)
            {
                for(int j = 0; j < matrix.GetLength(0); j++) 
                {
                    if (matrix[vert[i], j] == 1)
                    {
                        chekVert[j]++;
                    }
                }
            }

            for(int i = 0; i < chekVert.Length; i++)
            {
                if (chekVert[i] != 0)
                {
                    count++;
                }
            }

            newVert = new int[count];
            count--;

            for(int i = 0; i < chekVert.Length; i++)
            {
                if (chekVert[i] != 0)
                {
                    if (colorVert[i] == -1)
                    {
                        newVert[count] = i;
                        count--;
                    }
                }
            }
        }

        public static void ColorVert(int element, int[] strMatrix, int[] colorVert, ref int countColor)
        {
            bool isColor = false;
            int[] color = new int[countColor];
            for (int i = 0; i < color.Length; i++)
            {
                color[i] = 0;
            }

            for(int i = 0; i < strMatrix.Length; i++)
            {
                if (strMatrix[i] != 0) 
                {
                    if (colorVert[i] != -1)
                    {
                        color[colorVert[i]]++;
                    }
                }
            }

            for(int i = 0; i < color.Length; i++)
            {
                if(!isColor)
                {
                    if (color[i] == 0)
                    {
                        colorVert[element] = i;
                        isColor = true;
                    }
                }
            }

            if(!isColor)
            {
                colorVert[element] = countColor;
                countColor++;
            }
        }

        public static void Print(int[] mas)
        {
            for(int i = 0; i < mas.Length; i++) 
            {
                Console.WriteLine((i+1)+"----"+mas[i]);
            }
        }
        //********************************************************
        static void Main()
        {
            int[,] matrix = new int[0, 0];

            bool isEnd = false;

            string[] mas = new string[4];

            mas[0] = "Ввод матрицы";
            mas[1] = "Вывод матрицы";
            mas[2] = "Раскраска графа";
            mas[3] = "Конец работы";

            do
            {
                WorkMenu.SpisokMenu(ref mas);

                switch (WorkMenu.SelectMenu(mas.Length))
                {
                    case 1:
                        {
                            InputMatrix(ref matrix);
                            break;
                        }
                    case 2:
                        {
                            ShowMatrix(matrix);
                            break;
                        }
                    case 3:
                        {
                            ColoringMatrix(matrix);
                            break;
                        }
                    case 4:
                        {
                            isEnd = true;
                            break;
                        }
                }
            } while (!isEnd);
;        }
    }
}
