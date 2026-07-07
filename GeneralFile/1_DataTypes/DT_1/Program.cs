using System.Text;

public class Program
{
    public static void Main()
    {
        try
        {
            Console.Write("Введите начальный вклад (положительное число): ");
            string input_initial_deposit = Console.ReadLine();
            double initial_deposit = double.Parse(input_initial_deposit);

            if (initial_deposit <= 0)
                throw new ArgumentException("Начальный вклад должен быть положительным числом.");

            Console.Write("Введите количество лет (положительное целое число): ");
            string input_years = Console.ReadLine();
            int years = int.Parse(input_years);

            if (years <= 0)
                throw new ArgumentException("Количество лет должно быть положительным целым числом.");

            Console.Write("Введите процентную ставку (положительное число): ");
            string input_interest_rate = Console.ReadLine();
            double interest_rate = double.Parse(input_interest_rate);

            if (interest_rate <= 0)
                throw new ArgumentException("Процентная ставка должна быть положительным числом.");

            string report = Calculations.CalculationOfCompoundInterest(initial_deposit, years, interest_rate);
            Console.WriteLine();
            Console.WriteLine(report);
        }

        catch (ArgumentException)
        {
            Console.WriteLine($"Ошибка: введено некорректное число. ");
        }
    }
}

public static class Calculations
{
    public static string CalculationOfCompoundInterest(double initial_deposit, int years, double interest_rate)
    {
        var result = new StringBuilder();

        double currentAmount = initial_deposit;
        double rateMultiplier = 1 + interest_rate / 100.0;

        for (int year = 1; year <= years; year++)
        {
            currentAmount = initial_deposit * Math.Pow(rateMultiplier, year);

            double roundedAmount = Math.Round(currentAmount, 2, MidpointRounding.AwayFromZero);

            result.AppendLine($"Год {year}: {roundedAmount:F2} руб.");
        }

        return result.ToString().TrimEnd();

    }
}