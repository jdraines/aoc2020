using System;
using System.Collections.Generic;

namespace AOC2020.DayLibs.Day05Lib
{
    public class Seat
    {

        public Seat(string raw)
        {
            Raw = raw;
            binRow = ToBinary(raw[0..7]);
            binColumn = ToBinary(raw[7..]);
            Row = Convert.ToInt32(binRow, 2);
            Column = Convert.ToInt32(binColumn, 2);
            ID = (Row * 8) + Column;
        }

        public override string ToString()
        {
            return $"<Seat::\n" +
                   $"    Raw={Raw}\n" +
                   $"    Row={Row}, binRow={binRow}\n" +
                   $"    Column={Column}, binColumn={binColumn}\n" +
                   $"    ID={ID}>";
        }

        private Dictionary<char, char> binConv = new Dictionary<char, char>()
            {
                {'F', '0'}, {'B','1'}, {'L', '0'}, {'R', '1'}
            };

        private string Raw;
        private string binRow;
        private string binColumn;
        public int Row;
        public int Column;
        public int ID;

        private string ToBinary(string seatRaw)
        {
            char[] seatBin = new char[seatRaw.Length];
            for (int i = 0; i < seatRaw.Length; i++)
            {
                seatBin[i] = binConv[seatRaw[i]];
            }
            return new string(seatBin);
        }

    }
}
