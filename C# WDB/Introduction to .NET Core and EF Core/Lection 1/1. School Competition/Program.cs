using System;
using System.Collections.Generic;
using System.Linq;

namespace _1.School_Competition
{
    class Program
    {
        static void Main(string[] args)
        {
            var categories = new Dictionary<string,SortedSet<string>>();
            var results = new SortedDictionary<string,int>();

            string line;

            while ((line = Console.ReadLine()) != "END")
            {
                string[] data = line.Split(' ');

                string name = data[0];
                string category = data[1];
                int points = int.Parse(data[2]);

                if (!categories.ContainsKey(name))
                {
                    categories.Add(name,new SortedSet<string>());
                }
                if (!results.ContainsKey(name))
                {
                    results.Add(name,0);
                }

                categories[name].Add(category);
                results[name] += points;
            }

            var orderedResults = results.OrderByDescending(st => st.Value).ThenBy(p => p.Key);
            foreach (var student in orderedResults)
            {
                var orderedCategories = categories[student.Key].OrderBy(c => c);
                string subjects = String.Join(", ", orderedCategories);
                Console.WriteLine($"{student.Key}: {student.Value} [{subjects}]");
            }
        }
    }
}