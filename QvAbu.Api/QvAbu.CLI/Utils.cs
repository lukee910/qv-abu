using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace QvAbu.CLI
{
    public interface IUtils
    {
        int DoChoiceSelection(string prompt, params string[] options);
        string PromptInput(string prompt);
    }

    public class Utils : IUtils
    {
        public int DoChoiceSelection(string prompt, params string[] options)
        {
            while (true)
            {
                Console.WriteLine("--");
                Console.WriteLine(prompt);
                foreach (var option in options.Select((value, index) => new Tuple<int, string>(index + 1, value)))
                {
                    Console.WriteLine($" {option.Item1} -> {option.Item2}");
                }

                if (int.TryParse(Console.ReadLine(), out int selection) && selection <= options.Length && selection > 0)
                {
                    return selection;
                }
            }
        }

        public string PromptInput(string prompt)
        {
            while (true)
            {
                Console.WriteLine("--");
                Console.WriteLine(prompt);

                var input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input;
                }
            }
        }
    }
}
