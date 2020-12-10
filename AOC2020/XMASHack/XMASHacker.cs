using System;
using System.Linq;
using System.Collections.Generic;

namespace AOC2020.XMASHack
{
    public class XMASHacker
    {
        private long[] encryptedData;
        private int preambleStartPointer;
        private int preambleLength;
        private int dataLength;

        public XMASHacker(long[] encrypted_data, int preamble_length=25, int line_pointer=0)
        {
            Initialize(encrypted_data, preamble_length, line_pointer);
        }

        public void Reset(long[] encrypted_data=null, int preamble_length=25, int line_pointer=0)
        {
            Initialize(encrypted_data, preamble_length, line_pointer);
        }

        public long SearchFirstExceptionVal()
        {
            int except = SearchFirstExceptionOverRange(preambleStartPointer, dataLength);
            if (except == -1)
            {
                return -1;
            }
            return encryptedData[except];
        }

        public List<long> EncryptionWeaknessList()
        {
            return WeaknessListFromVal(SearchFirstExceptionVal());
        }

        private void Initialize(long[] encrypted_data, int preamble_length, int line_pointer)
        {
            encryptedData = encrypted_data;
            preambleLength = preamble_length;
            preambleStartPointer = line_pointer;
            dataLength = encrypted_data.Length;
        }

        private int SearchFirstExceptionOverRange(int start, int length)
        {
            int startPtr = start;

            int valPtr() { return startPtr + preambleLength; }
            long val() { return encryptedData[valPtr()]; }

            while (startPtr < start + length)
            {
                long[] preamble = encryptedData[startPtr..valPtr()];

                long _val = val();
                if (IsException(preamble, _val))
                {
                    return valPtr();
                }
                else
                {
                    startPtr++;
                }
            }
            return -1;
        }

        private List<long> WeaknessListFromVal(long val)
        {

            for(int i = preambleStartPointer; i < dataLength; i++)
            {
                List<long> weaknessList = new List<long>();
                long sum = 0;

                for (int j = i; j < dataLength; j++)
                {
                    weaknessList.Add(encryptedData[j]);
                    sum += encryptedData[j];
                    if (sum == val)
                    {
                        return weaknessList;
                    }
                    else if (sum > val)
                    {
                        break;
                    }
                }
            }
            return new List<long>();
        }

        private bool IsException(long[] preamble, long val)
        {
            long[] complement = new long[preambleLength];

            HashSet<long> preambleSet = new HashSet<long>(preamble);

            for (int i=0; i < preambleLength; i++)
            {
                complement[i] = val - preamble[i];
            }

            preambleSet.IntersectWith(new HashSet<long>(complement));

            if (preambleSet.Count < 2) { return true; }
            return false;
        }
    }
}
