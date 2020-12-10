using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2020.DayLibs.Day10Lib
{
    public static class VirtualConnectionTree
    {

        public static long TreeNodeCount(JoltAdapter[] adapters)
        {
            adapters = Utilities.SortAdapters(adapters);
            int[] ratings = new int[adapters.Length + 2];
            ratings[0] = 0;
            ratings[^1] = Utilities.GetDeviceRating(adapters);


            using AdapterChoices = Dictionary<int, List<int>>;

            AdapterChoices choices = new AdapterChoices();

            for (int i = 0; i < adapters.Length; i++)
            {
                // create an AdapterChoices dictionary using a function that creates a list of choices, given some rating
            }
        }

        public static List<int> GetChoices(int rating, JoltAdapter[] adapters)
        {
            // generate a List<int> of ratings choices.
        }

    }
}
