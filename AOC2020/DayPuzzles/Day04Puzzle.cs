using System;
using System.Collections.Generic;
using AOC2020.AOCInput;
using AOC2020.DayLibs.Day04Lib;

namespace AOC2020.DayPuzzles
{
    public class Day04Puzzle : IDayPuzzle
    {
        public Day04Puzzle()
        {
        }

        public string Day { get; } = "04";

        public void SolvePart1()
        {
            string[] records = InputParser.ReadChunksToStringArr(Day, "1");
            int valid = 0;
            foreach (string record in records)
            {
                Dictionary<string, string> recordMap = RecordParser.ParseRecord(record);
                if(RecordParser.ContainsRequiredFields(recordMap))
                {
                    valid++;
                }
            }
            Console.WriteLine($"Number of valid passports: {valid}");
        }

        public void SolvePart2()
        {
            var passports = new List<Passport>() { };
            string[] records = InputParser.ReadChunksToStringArr(Day, "1");
            List<string> errors = new List<string>();


            foreach (string rec in records)
            {
                try
                {
                    passports.Add(RecordParser.RecordToPassport(rec));
                }
                catch (Exception e)
                {
                    errors.Add(e.Message);
                }
            }

            Console.WriteLine("\nValid passports:");
            foreach (Passport pport in passports)
            {
                Console.WriteLine(pport.ToString() + "\n");
            }


            //Console.WriteLine("\nInvalid Passports:");
            //foreach (string msg in errors)
            //{
            //    Console.WriteLine(msg);
            //}

            Console.WriteLine($"\nNumber of valid passports: {passports.Count}");
        }

        public void SolveTest()
        {
            var passports = new List<Passport>() { };
            string[] records = InputParser.ReadChunksToStringArr(Day, "Test");
            List<string> errors = new List<string>();


            foreach (string rec in records)
            {
                try
                {
                    passports.Add(RecordParser.RecordToPassport(rec));
                }
                catch (Exception e)
                {
                    errors.Add(e.Message);
                }
            }

            Console.WriteLine("\nValid passports:");
            foreach (Passport pport in passports)
            {
                Console.WriteLine(pport.ToString());
            }


            Console.WriteLine("\nInvalid Passports:");
            foreach (string msg in errors)
            {
                Console.WriteLine(msg);
            }

            Console.WriteLine($"\nNumber of valid passports: {passports.Count}");
        }
    }
}
