using System;

namespace ConsoleApp1
{
    public class ConsolePrinter
    {
        public object PrintValue;

        public ConsolePrinter()
        {
        }

        public ConsolePrinter Value(string value)
        {
            PrintValue = value;
            return this;
        }

        public void Print()
        {
            Console.WriteLine(PrintValue);
        }

        public void PrintResults(string [] results, bool printMultipleLines = false)
        {
            if (printMultipleLines)
            {
                for (int i = 0; i < results.Length; i++)
                    Value(i + 1 + " " + results[i]).Print();
            }
            else
                Value("[" + string.Join(",", results) + "]").Print();
        }
    }
}
