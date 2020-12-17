using System;
using System.Linq;
using System.Collections.Generic;

namespace AOC2020.DayLibs.Day16Lib
{
    public static class D16TicketProcessor
    {

        public static bool TicketIsValid(int[] ticket, HashSet<int> ruleSet)
        {
            foreach (int num in ticket)
            {
                if (!ruleSet.Contains(num))
                {
                    return false;
                }
            }
            return true;
        }

        public static List<int[]> ValidateTickets(int[][] nearbyTickets, HashSet<int> masterSet)
        {
            List<int[]> validTickets = new List<int[]>();
            for (int i = 0; i < nearbyTickets.Length; i++)
            {
                if (TicketIsValid(nearbyTickets[i], masterSet))
                {
                    validTickets.Add(nearbyTickets[i]);
                }
            }
            return validTickets;
        }


        public static Dictionary<string, List<int>> GetPossibleFieldMappings(List<int[]> validTickets, Tuple<string, HashSet<int>>[] rules)
        {
            Dictionary<string, List<int>> possMap = new Dictionary<string, List<int>>();
            bool isContained;
            int[] valuesOneField;
            HashSet<int> intersection;

            for (int f = 0; f < validTickets[0].Length; f++)
            {
                valuesOneField = validTickets
                                        .Select(x => x[f])
                                        .ToArray();

                foreach ((string field, HashSet<int> ruleSet) in rules)
                {
                    intersection = new HashSet<int>(valuesOneField);
                    isContained = true;

                    foreach (int num in valuesOneField)
                    {
                        if (!ruleSet.Contains(num))
                        {
                            isContained = false;
                            break;
                        }
                    }

                    if (isContained)
                    {
                        if (possMap.ContainsKey(field))
                        {
                            possMap[field].Add(f);
                        }
                        else
                        {
                            possMap[field] = new List<int> { f };
                        }
                    }
                }
            }

            return possMap;
        }

        public static Dictionary<string, List<int>> DropMappedInts(Dictionary<string, List<int>> possMap, Dictionary<string, int> fieldMap)
        {
            Dictionary<string, List<int>> reduced = new Dictionary<string, List<int>>();

            foreach (var item in possMap)
            {
                foreach(int x in item.Value)
                {
                    if (!fieldMap.ContainsValue(x))
                    {
                        if (reduced.ContainsKey(item.Key))
                        {
                            reduced[item.Key].Add(x);
                        }
                        else
                        {
                            reduced[item.Key] = new List<int> { x };
                        }
                    }
                }
            }

            return reduced;
        }

        public static Dictionary<string, List<int>> AssignPerfectFits(Dictionary<string, List<int>> possMap, ref Dictionary<string, int> fieldMap)
        {
            Dictionary<string, List<int>> remainder = new Dictionary<string, List<int>>();

            foreach (var item in possMap)
            {
                if (item.Value.Count == 1)
                {
                    fieldMap.Add(item.Key, item.Value[0]);
                }
                else
                {
                    remainder.Add(item.Key, item.Value);
                }
            }
                return DropMappedInts(remainder, fieldMap);
        }

        public static Dictionary<string, List<int>> AssignUniqueFits(Dictionary<string, List<int>> possMap, ref Dictionary<string, int> fieldMap)
        {
            Dictionary<string, List<int>> remainder = new Dictionary<string, List<int>>();
            bool isUnique;

            foreach (var item in possMap)
            {
                foreach (int idx in item.Value)
                {
                    isUnique = true;
                    foreach (var keyval in possMap)
                    {
                        if (keyval.Key == item.Key)
                        {
                            continue;
                        }
                        else if (keyval.Value.Contains(idx))
                        {
                            isUnique = false;
                            break;
                        }
                    }

                    if (isUnique)
                    {
                        fieldMap.Add(item.Key, idx);
                        break;
                    }
                }

                if (!fieldMap.ContainsKey(item.Key))
                {
                    remainder.Add(item.Key, item.Value);
                }
            }

            return DropMappedInts(remainder, fieldMap);
        }
    }
}
