using System;
using System.Collections.Generic;
using AOC2020.AOCInput;


namespace AOC2020.DayPuzzles
{
    public class Day06Puzzle : IDayPuzzle
    {
        public Day06Puzzle()
        {
        }

        public string Day { get; } = "06";

        public void SolvePart1()
        {
            string[] groups = InputParser.ReadChunksToStringArr(Day, "1");
            int decCount = 0;
            for(int i = 0; i < groups.Length; i++)
            {
                decCount += GetDeclarationSet(groups[i].Split('\n')).Count;
            }
            Console.WriteLine($"Total number of declarations: {decCount}");
        }

        public void SolvePart2()
        {
            string[] groups = InputParser.ReadChunksToStringArr(Day, "1");
            int decCount = 0;
            for (int i = 0; i < groups.Length; i++)
            {
                decCount += GetDeclarationSetP2(groups[i]).Count;
            }
            Console.WriteLine($"Total number of declarations: {decCount}");
        }

        public void SolveTest()
        {
            string[] groups = InputParser.ReadChunksToStringArr(Day, "Test");
            int decCount = 0;
            for (int i = 0; i < groups.Length; i++)
            {
                decCount += GetDeclarationSetP2(groups[i]).Count;
            }
            Console.WriteLine($"Total number of declarations: {decCount}");
        }

        private HashSet<char> GetDeclarationSet(string[] individual_declarations)
        {
            HashSet<char> decSet = new HashSet<char>();
            string[] idecs = individual_declarations;
            foreach (string dec in idecs)
            {
                foreach (char c in dec.Trim())
                {
                    if (!Char.IsWhiteSpace(c))
                    {
                        decSet.Add(c);
                    }
                }
            }
            return decSet;
        }

        private HashSet<char> GetDeclarationSetP2(string group_declaration)
        {
            HashSet<char> decSet = new HashSet<char>();
            string[] _idecs = group_declaration.Trim().Split(new char[] {'\n',' '});

            List<char>[] idecs = new List<char>[_idecs.Length];
            for (int i=0; i<_idecs.Length; i++)
            {
                idecs[i] = new List<char>(_idecs[i].ToCharArray());
            }

            foreach (List<char> dec in idecs)      // for each individual declaration in the group's chunk of declarations
            {
                foreach (char c in dec)            // for each char found in the individual declaration
                {
                    if (!Char.IsWhiteSpace(c))     // ignore whitespace
                    {
                        if (!decSet.Contains(c))   // ignore if this char has already been added to the set
                        {
                            bool inAll = true;
                            foreach (List<char> _dec in idecs)  // for each individual declaration in the group's chunk of declarations
                            {
                                if (!_dec.Contains(c))          // if that individual declaration does not contain the char
                                {
                                    inAll = false;              // unmark it as appearing in all declarations
                                }
                            }

                            if (inAll)            // Add a char to the declaration set only if it is marked as appearing in all declarations
                            {
                                decSet.Add(c);
                            }
                        }
                    }
                }
            }
            return decSet;
        }
    }
}
