using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace first_non_repeated_occurance
{
    internal class Program
    {
        private static List<string> GetPrompt()
        {
            string line;
            List<string> words = new List<string>();

            Console.WriteLine("Enter the prompt:");
            while (!string.IsNullOrWhiteSpace(line = Console.ReadLine()))
            {
                Regex regex = new Regex("\\w+", RegexOptions.Compiled);

                foreach (Match m in regex.Matches(line))
                    words.Add(m.Value.Trim());
            }

            return words;
        }

        private static void Main(string[] args)
        {
            Console.WriteLine("This programme is designed to execute an algorithm that meets the following requirements:");
            Console.WriteLine("- the program takes an arbitrary text as input and finds in each word of this text the very first character that is NOT repeated in the analysed word");
            Console.WriteLine("- then, from the received set of characters, the program must select the first unique one (i.e. the one that no longer appears in the set) and return it.");
            List<string> words = GetPrompt();

            List<char> firstOccurancesInWords = FirstOccuranceInMultipleWords(words);

            char? result = FirstOccurance(string.Concat(firstOccurancesInWords));
            if (result != null)
                Console.WriteLine($"The result of the algorithm execution: '{result}'");
            else Console.WriteLine("There are no characters that meets requirements");
            Console.ReadKey();
        }

        private static char FirstOccurance(string line)
        {
            return FirstOccuranceInMultipleWords(
                new List<string>() { line }
                )
                .First();
        }

        private static List<char> FirstOccuranceInMultipleWords(List<string> words)
        {
            List<char> result = new List<char>();
            //split to words
            foreach (string word in words)
            {
                var charDicts = new Dictionary<char, int>();
                //process every character in line
                //make an dictionary, where every unique character has it number of occurance
                foreach (char letter in word.ToCharArray())
                {
                    if (charDicts.TryGetValue(letter, out int value))
                        charDicts[letter] = value + 1;
                    else charDicts.Add(letter, 1);
                }
                //add to result first unique character in line
                var resChar = charDicts.FirstOrDefault(pair => pair.Value == 1).Key;
                if (resChar != '\0')
                    result.Add(resChar);
            }
            return result;
        }
    }
}