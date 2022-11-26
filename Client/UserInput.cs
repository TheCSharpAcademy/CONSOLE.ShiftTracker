using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace Client
{
    internal class UserInput
    {
        static internal int GetInt(string title)
        {
            int value = 0;

            while (value == 0)
            {
                Console.WriteLine($"\nInput [{title}]: ");
                string? input = Console.ReadLine();

                Int32.TryParse(input, out value);

                if (value == 0) Console.WriteLine("Wrong format!");
            }

            return value;
        }

        static internal string GetIntToString(string title)
        {
            string? input = null;

            while (!Int32.TryParse(input, out _))
            {
                Console.WriteLine($"\nInput [{title}]: ");
                input = Console.ReadLine();

                if (!Int32.TryParse(input, out _)) Console.WriteLine("Wrong format!");
            }

            return input;
        }

        static internal decimal GetDecimal(string title)
        {
            decimal value = 0;

            while (value == 0)
            {
                Console.WriteLine($"\nInput [{title}]: ");
                string? input = Console.ReadLine();

                Decimal.TryParse(input, out value);

                if (value == 0) Console.WriteLine("Wrong format!");
            }

            return value;
        }
        
        static internal DateTime GetDate(string title, DateTime secondDate = new())
        {
            DateTime date;

            Console.WriteLine($"\nInput [{title}]: ");

            while (true)
            {
                Console.WriteLine("<yyyy-MM-dd HH:mm>:");
                string? input = Console.ReadLine();

                if (!DateTime.TryParse(input, out date))
                {
                    Console.WriteLine("Wrong Date Format!");
                    continue;
                }

                if (!(date.CompareTo(secondDate) > 0))
                {
                    Console.WriteLine("EndTime must be greater than StartTime!\n");
                    continue;
                }

                break;
            }

            return date;
        }

        static internal string GetString(string title)
        {
            string? input = "";

            while (input == "")
            {
                Console.WriteLine($"Input [{title}]: ");
                input = Console.ReadLine();
            }

            return input;
        }

        static internal string GetUpdateOptionString()
        {
            string[] options = { "1", "2", "3", "4", "5" };

            string? input = "";

            while (!Array.Exists(options, element => element == input))
            {
                Console.WriteLine($"Input [Option]: ");
                input = Console.ReadLine();
            }

            return input;
        }
    }
}
