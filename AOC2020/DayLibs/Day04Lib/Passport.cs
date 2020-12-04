using System;
using System.Linq;
using System.Text.RegularExpressions;
using Measurement = System.Tuple<int, string>;

namespace AOC2020.DayLibs.Day04Lib
{
    public class Passport
    {
        public int byr;
        public int iyr;
        public int eyr;
        public Measurement hgt;
        public string hcl;
        public string ecl;
        public string pid;
        public string cid;

        public Passport(string byr=null,
                        string iyr=null,
                        string eyr=null,
                        string hgt=null,
                        string hcl=null,
                        string ecl=null,
                        string pid=null,
                        string cid=null)
        {
            this.byr = ParseYear(byr, 1920, 2002);
            this.iyr = ParseYear(iyr, 2010, 2020);
            this.eyr = ParseYear(eyr, 2020, 2030);
            this.hgt = ParseHeight(hgt);
            this.hcl = ParseHairColor(hcl);
            this.ecl = ParseEyeColor(ecl);
            this.pid = ParsePID(pid);
            this.cid = cid;

        }

        public override string ToString()
        {
            return $"<Passport::\n" +
                    $"byr={this.byr}\n" +
                    $"iyr={this.iyr}\n" +
                    $"eyr={this.eyr}\n" +
                    $"hgt={this.hgt}\n" +
                    $"hcl={this.hcl}\n" +
                    $"ecl={this.ecl}\n" +
                    $"pid={this.pid}\n" +
                    $"cid={this.cid}>";
        }

        private static int ParseYear(string year, int minDate, int maxDate)
        {
            if (year is null)
            {
                Throw("year is null");
                return 0;
            }

            if (year.Length != 4)
            {
                Throw($"{year}: Length is off.");
            }

            int _year = Int32.Parse(year);
            if (_year < minDate)
            {
                Throw($"{year} < {minDate}");
            }

            if (_year > maxDate)
            {
                Throw($"{year} > {maxDate}");
            }

            return _year;

        }

        private static string ParseHairColor(string hcl)
        {
            if (hcl is null)
            {
                Throw($"Hair color is null");
            }
            string pattern = @"^#(?:[0-9a-fA-F]{3}){1,2}$";
            Regex r = new Regex(pattern, RegexOptions.IgnoreCase);
            Match m = r.Match(hcl);
            if(!m.Success)
            {
                Throw($"Haircolor: {hcl}");
            }

            return hcl;
        }

        private static string ParseEyeColor(string ecl)
        {
            string[] allowed = { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
            bool contained = false;

            foreach(string good in allowed)
            {
                if (ecl == good)
                {
                    contained = true;
                }
            }

            if(!contained)
            {
                Throw($"EyeColor not allowed: {ecl}");
            }
            return ecl;
        }

        private static string ParsePID(string pid)
        {
            if (pid is null)
            {
                Throw("PID is null");
                return null;
            }

            if (pid.Length != 9)
            {
                Throw($"PID incorrect length: {pid}");
            }

            if(!IsNumerical(pid))
            {
                Throw($"PID not numerical: {pid}");
            }

            return pid;
        }

        private Measurement ParseHeight(string hgt)
        {
            if (hgt is null)
            {
                Throw("Height is null.");
                return new Measurement(0, "");
            }

            if (hgt.Length < 3)
            {
                Throw($"Incomplete height: {hgt}");
            }

            string val = hgt[0..^2];
            string units = hgt[^2..];

            
            if (!IsNumerical(val))
            {
                Throw($"Height value not numerical: {hgt}");
            }

            int _val = Int32.Parse(val);

            Regex r = new Regex(@"[A-ZÀ-ÚÄ-Ü\s]+", RegexOptions.IgnoreCase);
            if (!r.Match(units).Success)
            {
                Throw($"Height units invalid: {hgt}");
            }

            // If cm, the number must be at least 150 and at most 193.
            if (units=="cm")
            {
                if (_val < 150 || _val > 193)
                {
                    Throw($"Height value not within range: {_val}");
                }
            }
            else if (units=="in")
            {
                if (_val < 59 || _val > 76)
                {
                    Throw($"Height value not within range: {_val}");
                }
            }
            else
            {
                Throw($"Unrecognized height units: {units}");
            }

            // If in, the number must be at least 59 and at most 76.



            return new Measurement(_val, units);
        }

        private static void Throw(string val)
        {
            throw new ArgumentException($"Invalid value passed: {val}");
        }

        private static bool IsNumerical(string str)
        {
            if (str is null)
            {
                return false;
            }
            Regex r = new Regex(@"^\d+$", RegexOptions.IgnoreCase);
            Match m = r.Match(str);
            return m.Success;
        }
    }
}
