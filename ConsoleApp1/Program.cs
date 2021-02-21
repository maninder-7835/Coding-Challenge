using System;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            CNController cNController = new CNController();
            ConsolePrinter printer = new ConsolePrinter();
            char enteredKey;
            Tuple<string, string> names = null;
            printer.Value("Welcome to Joke Generator.").Print();
            // Line gap
            printer.Value("").Print();
            bool doExit = false;
            string selectedCategory = null;
            int noOfJokesRequested = 0;

            while (!doExit)
            {
                // Line gap
                printer.Value("").Print();
                printer.Value("Press a to get a joke").Print();
                printer.Value("Press r to get random jokes").Print();
                printer.Value("Press x to exit").Print();
                // Line gap
                printer.Value("").Print();

                enteredKey = GetEnteredKey(Console.ReadKey());
                if (enteredKey == 'a')
                {
                    noOfJokesRequested = 1;
                }
                if (enteredKey == 'r')
                {
                    printer.Value("Want to use a random name? y/n").Print();
                    char nameKey = GetEnteredKey(Console.ReadKey());
                    if (nameKey == 'y')
                        names = await cNController.GetNames();

                    printer.Value("Want to specify a category? y/n").Print();
                    char catKey = GetEnteredKey(Console.ReadKey());
                    if (catKey == 'y')
                    {
                        selectedCategory = await GetEnteredCategory(printer, cNController);
                    }

                    noOfJokesRequested = GetNumberOfJokes(printer);
                }

                if (enteredKey == 'a' || enteredKey == 'r')
                {
                    printer.Value("Getting Jokes... ").Print();
                    await GetRandomJokes(names, selectedCategory, noOfJokesRequested, cNController, printer).ConfigureAwait(false);
                }

                if (enteredKey == 'x')
                {
                    printer.Value("Bye for now.").Print();
                    doExit = true;
                }
            }
        }

        private static async Task<string> GetEnteredCategory(ConsolePrinter printer, CNController cNController)
        {
            string selectedCategory = string.Empty;
            bool isValidCategory = false;
            printer.Value("Getting list of Categories....").Print();
            var categories = await cNController.GetCategories().ConfigureAwait(false);
            printer.Value("Following is list of Categories....").Print();
            printer.PrintResults(categories.ToArray());

            while (!isValidCategory)
            {
                // Line gap
                printer.Value("").Print();
                printer.Value("Enter a Category").Print();
                selectedCategory = Console.ReadLine();
                if (categories.Contains(selectedCategory))
                    isValidCategory = true;
                else
                    printer.Value("Please enter valid Category. Please take a look at the list printed above.").Print();
            }

            return selectedCategory;
        }

        private static int GetNumberOfJokes(ConsolePrinter printer)
        {
            bool isValidNumber = false;
            int? noOfJokes = 0;
            while (!isValidNumber)
            {
                printer.Value("How many jokes do you want? (1-9)").Print();
                var input = GetEnteredKey(Console.ReadKey());

                noOfJokes = Convert.ToInt32(input.ToString());
                if (noOfJokes != null && noOfJokes >= 1 && noOfJokes <= 9)
                    isValidNumber = true;
                else
                    printer.Value("Please enter valid number between 1-9").Print();
            }

            return noOfJokes.Value;
        }

        private static char GetEnteredKey(ConsoleKeyInfo consoleKeyInfo)
        {
            char key = ' ';
            switch (consoleKeyInfo.Key)
            {
                case ConsoleKey.A:
                    key = 'a';
                    break;
                case ConsoleKey.C:
                    key = 'c';
                    break;
                case ConsoleKey.D0:
                    key = '0';
                    break;
                case ConsoleKey.D1:
                    key = '1';
                    break;
                case ConsoleKey.D2:
                    key = '2';
                    break;
                case ConsoleKey.D3:
                    key = '3';
                    break;
                case ConsoleKey.D4:
                    key = '4';
                    break;
                case ConsoleKey.D5:
                    key = '5';
                    break;
                case ConsoleKey.D6:
                    key = '6';
                    break;
                case ConsoleKey.D7:
                    key = '7';
                    break;
                case ConsoleKey.D8:
                    key = '8';
                    break;
                case ConsoleKey.D9:
                    key = '9';
                    break;
                case ConsoleKey.R:
                    key = 'r';
                    break;
                case ConsoleKey.X:
                    key = 'x';
                    break;
                case ConsoleKey.Y:
                    key = 'y';
                    break;
            }

            return key;
        }

        private static async Task GetRandomJokes(Tuple<string, string> names, string category, int number, CNController cNController, ConsolePrinter printer)
        {
            string firstName = names != null ? names.Item1 : "";
            string lastName = names != null ? names.Item2 : "";
            var result = await cNController.GetRandomJokes(firstName, lastName, category, number).ConfigureAwait(false);
            printer.PrintResults(result, true);
        }
    }
}
