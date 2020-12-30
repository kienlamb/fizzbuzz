using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;

namespace CodeReview
{
    class Program
    {
        static void Main(string[] args)
        {
            //functional approach
            Console.WriteLine("------------Functional Approach--------------");
            bool DivisbleByThree(int factor) => factor % 3 == 0;
            bool DivisbleByFive(int factor) => factor % 5 == 0;

            var fizz = new Tuple<Func<int, bool>, string>(DivisbleByThree, "fizz");
            var buzz = new Tuple<Func<int, bool>, string>(DivisbleByFive, "buzz");

            var functionOperations = new[] { fizz, buzz };

            Print(30, functionOperations);

            //object oriented approach
            Console.WriteLine("------------OOP Approach--------------");
            var classOperations = new[]
            {
                new Division(3, "fizz"),
                new Division(5, "buzz")
            };

            Print(30, classOperations);
        }

        public static void Print(int count, IEnumerable<Tuple<Func<int, bool>, string>> operations)
        {
            var output = "";

            for (var i = 1; i <= count; i++)
            {
                foreach (var operation in operations)
                {
                    if (operation.Item1(i))
                        output += operation.Item2;
                }

                Console.WriteLine(!string.IsNullOrEmpty(output) ? output : i.ToString());
                output = null;
            }
        }

        public static void Print(int count, IEnumerable<IIsDivisible> divisions)
        {
            var output = "";

            for (var i = 1; i <= count; i++)
            {
                foreach (var division in divisions)
                {
                    var result = division.GetValueOrDefault(i);
                    if (!string.IsNullOrEmpty(result))
                        output += result;
                }

                Console.WriteLine(!string.IsNullOrEmpty(output) ? output : i.ToString());
                output = null;
            }
        }
    }

    public interface IIsDivisible
    {
        string GetValueOrDefault(int number, string defaultValue = "");
    }

    public class Division : IIsDivisible
    {
        private readonly int _factor;
        private readonly string _value;

        public Division(int factor, string value)
        {
            _factor = factor;
            _value = value;
        }

        public string GetValueOrDefault(int num, string defaultValue = "")
        {
            return num % _factor == 0 ? _value : defaultValue;
        }
    }
}
