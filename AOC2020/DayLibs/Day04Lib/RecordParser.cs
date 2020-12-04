using System;
using System.Collections.Generic;

namespace AOC2020.DayLibs.Day04Lib
{

    public static class RecordParser
    {

        static string[] allFields =
        {
            "byr", //(Birth Year)
            "iyr", //(Issue Year)
            "eyr", //(Expiration Year)
            "hgt", //(Height)
            "hcl", //(Hair Color)
            "ecl", //(Eye Color)
            "pid", //(Passport ID)
            "cid" //(Country ID)
        };

        static string[] requiredFields =
        {
            "byr", //(Birth Year)
            "iyr", //(Issue Year)
            "eyr", //(Expiration Year)
            "hgt", //(Height)
            "hcl", //(Hair Color)
            "ecl", //(Eye Color)
            "pid", //(Passport ID)
        };

        public static Dictionary<string, string> ParseRecord(string record)
        {
            string[] fields = record.Split(' ');
            Dictionary<string, string> recordMap = new Dictionary<string, string>();

            foreach(string field in fields)
            {
                if (!String.IsNullOrWhiteSpace(field))
                {
                    string[] key_val = field.Split(':');
                    recordMap.Add(key_val[0], key_val[1]);
                }
            }
            return recordMap;
        }

        public static bool ContainsRequiredFields(Dictionary<string, string> recordMap)
        {
            foreach (string field in RecordParser.requiredFields)
            {
                if (!recordMap.ContainsKey(field))
                {
                    return false;
                }
            }
            return true;
        }

        public static Passport RecordToPassport(string record)
        {
            Dictionary<string, string> recordMap = ParseRecord(record);
            return new Passport(
                    byr: recordMap.GetValueOrDefault("byr", null),
                    iyr: recordMap.GetValueOrDefault("iyr", null),
                    eyr: recordMap.GetValueOrDefault("eyr", null),
                    hgt: recordMap.GetValueOrDefault("hgt", null),
                    hcl: recordMap.GetValueOrDefault("hcl", null),
                    ecl: recordMap.GetValueOrDefault("ecl", null),
                    pid: recordMap.GetValueOrDefault("pid", null),
                    cid: recordMap.GetValueOrDefault("cid", null)
                );
        }
    }
}
