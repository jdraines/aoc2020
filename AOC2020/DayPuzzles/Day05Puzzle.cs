using System;
using System.Linq;
using System.Collections.Generic;
using AOC2020.AOCInput;
using AOC2020.DayLibs.Day05Lib;


namespace AOC2020.DayPuzzles
{
    public class Day05Puzzle : IDayPuzzle
    {
        public Day05Puzzle()
        {
        }

        public string Day {get;} = "05";

        public void SolvePart1()
        {
            string[] input = InputParser.ReadToStringArr(Day, "1");
            Seat[] seats = new Seat[input.Length];

            for(int i=0; i < input.Length; i++)
            {
                seats[i] = new Seat(input[i]);
            };

            int maxSeatID = 0;
            foreach (Seat seat in seats)
            {
                if (seat.ID > maxSeatID)
                {
                    maxSeatID = seat.ID;
                }
            }
            Console.WriteLine($"Highest seat ID: {maxSeatID}");
        }

        public void SolvePart2()
        {
            string[] input = InputParser.ReadToStringArr(Day, "1");
            Seat[] seats = new Seat[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                seats[i] = new Seat(input[i]);
            };

            Seat[] sorted = seats.OrderBy(s => s.ID).ToArray();
            List<int> missingSeat = new List<int>();
            for(int i=0; i < sorted.Length - 1; i++)
            {
                if (sorted[i + 1].ID - 1 == sorted[i].ID + 1)
                {
                    Console.WriteLine($"Missing seat: {sorted[i].ID + 1}");
                }
            }
        }

        public void SolveTest()
        {
            string[] input = InputParser.ReadToStringArr(Day, "Test");
            Seat[] seats = new Seat[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                seats[i] = new Seat(input[i]);
            };

            int maxSeatID = 0;
            foreach (Seat seat in seats)
            {
                Console.WriteLine(seat.ToString());

                if (seat.ID > maxSeatID)
                {
                    maxSeatID = seat.ID;
                }
            }
        }
    }
}
