using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDiamond
{
    internal class Program
    {
        static void Main()
        {
            try
            {
                Console.Write("Введите целое нечётное положительное число: ");
                string input_N = Console.ReadLine();
                int N = int.Parse(input_N);

                if (N <= 0 || N % 2 == 0)
                    throw new ArgumentException("Начальный вклад должен быть положительным нечётным числом.");

                string diamond = Constructions.ConstructionDiamond(N);
                Console.WriteLine();
                Console.WriteLine(diamond);
            }
            
            catch (ArgumentException)
            {
                Console.WriteLine($"Ошибка: введено некорректное число.");
            }
        }
    }
}

public static class Constructions
{
    public static string ConstructionDiamond(int N)
    {
        var result = new StringBuilder();

        int center = N / 2;

        for (int row = 0; row < N; row++)
        {
            for (int column = 0; column < N; column++)
            {
                if (Math.Abs(row -  center) + Math.Abs(column - center) == center)
                {
                    result.Append($"X");
                }

                else
                {
                    result.Append($" ");
                }
            }
            result.AppendLine();
        }
        return result.ToString().TrimEnd();
    }
}

