using System;
using System.Collections.Generic;
using System.Linq;
using AdapterChoices = System.Collections.Generic.Dictionary<int, System.Collections.Generic.List<int>>;
using RatingEdgeCount = System.Collections.Generic.Dictionary<int, long>;

namespace AOC2020.DayLibs.Day10Lib
{
    public static class VirtualConnectionTree
    {

        public static long TreePathCount(JoltAdapter[] adapters)
        {
            adapters = Utilities.SortAdapters(adapters);
            int[] ratings = new int[adapters.Length + 2];
            ratings[^1] = Utilities.GetDeviceRating(adapters);
            for (int i=1; i < ratings.Length - 1; i++)
            {
                ratings[i] = adapters[i - 1].Rating;
            }

            AdapterChoices edgePossibilities = new AdapterChoices();

            for (int i = ratings.Length - 1; i > 0; i--)
            {
                edgePossibilities.Add(ratings[i], GetPossibleIncomingEdges(ratings[i], ratings, i));
            }

            RatingEdgeCount pathCounts = new RatingEdgeCount();

            for (int i = ratings.Length - 1; i >= 0; i--)
            {
                pathCounts.Add(ratings[i], GetOutgoingPathCount(i, ratings, pathCounts, edgePossibilities));
            }

            return pathCounts[ratings[0]];
        }

        public static List<int> GetPossibleIncomingEdges(int rating, int[] all_ratings, int idx)
        {
            List<int> possibilities = new List<int>();

            for (int j = idx; j > 0; j--)
            {
                int other_rating = all_ratings[j - 1];
                if (Utilities.CanBeConnected(other_rating, rating))
                {
                    possibilities.Add(other_rating);
                }
                else
                {
                    break;
                }
            }
            return possibilities;
        }

        public static long GetOutgoingPathCount(int idx, int[] all_ratings, RatingEdgeCount masterEdgeCount, AdapterChoices edgePoss)
        {
            long pathCount = 0;

            // handle the device, which has 0 outgoing edges
            if (idx == all_ratings.Length - 1)
            {
                return 1;
            }

            // Add the path counts for all subsequent rating connections
            for (int i = idx + 1; i < all_ratings.Length; i++)
            {
                int rating = all_ratings[idx];
                if (edgePoss[all_ratings[i]].Contains(rating))
                {
                    pathCount += masterEdgeCount[all_ratings[i]];
                }
            }
            return pathCount;
        }

    }
}
