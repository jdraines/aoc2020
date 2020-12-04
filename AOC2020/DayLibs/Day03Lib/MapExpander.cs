using System;
using System.Linq;
using CoordTuple = System.Tuple<int, int>;

namespace AOC2020.DayLibs.Day03Lib
{
    public static class MapExpander
    {
        public static string[] ExpandMap(string[] map, CoordTuple slope, string padEdge = "right")
        {
            int height_init = map.Length;
            int width_init = map[0].Length;
            int deltaX = slope.Item1;
            int deltaY = slope.Item2;

            double targetShapeFactor = FindShapeFactor(deltaX, deltaY);
            double mapShapeFactor = FindShapeFactor(width_init, height_init);

            if (RequiresWidthExpansion(mapShapeFactor, targetShapeFactor))
            {
                // Console.WriteLine($"Map requires expansion (shape: {mapShapeFactor}, target: {targetShapeFactor})...");
                return ExpandWidth(mapShapeFactor, targetShapeFactor, map, padEdge);
            }
            else
            {
                // Console.WriteLine($"Map does not require expansion (shape: {mapShapeFactor}, target: {targetShapeFactor})...");
                return map;
            }

        }

        private static double FindShapeFactor(int x, int y)
        {
            return (double)Math.Abs(x) / (double)Math.Abs(y);
        }

        public static bool RequiresWidthExpansion(double shape, double target)
        {
            return shape < target;
        }

        private static string[] ExpandWidth(double shape, double target, string[] map, string padEdge)
        {
            int factor = 1;
            string[] newMap = new string[map.Length];
            double factor_dbl = target / shape;

            if (padEdge == "left" || padEdge == "right")
            {
                factor = (int)Math.Ceiling(factor_dbl);
            }
            else
            {
                factor = (int)Math.Floor(factor_dbl);
            }

            Console.WriteLine($"Expansion factor: {factor}");

            for (int i=0; i < map.Length; i++)
            {
                newMap[i] = String.Concat(Enumerable.Repeat(map[i], factor));
            }
            return newMap;
        }

    }
}
