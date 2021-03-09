using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApi.Helpers
{
    public class CreditCardVerification
    {
        public static bool IsValid(long number)
        {
            return (GetSize(number) >= 13 &&
                    GetSize(number) <= 16) &&
                    (PrefixMatched(number, 4) ||
                    PrefixMatched(number, 5) ||
                    PrefixMatched(number, 37) ||
                    PrefixMatched(number, 6)) &&
                    ((SumOfDoubleEvenPlace(number) +
                    SumOfOddPlace(number)) % 10 == 0);
        }

        // Get the result from Step 2
        public static int SumOfDoubleEvenPlace(long number)
        {
            int sum = 0;
            String num = number + "";
            for (int i = GetSize(number) - 2; i >= 0; i -= 2)
                sum += GetDigit(int.Parse(num[i] + "") * 2);

            return sum;
        }

        // Return this number if it is a 
        // single digit, otherwise, return 
        // the sum of the two digits
        public static int GetDigit(int number)
        {
            if (number < 9)
                return number;
            return number / 10 + number % 10;
        }

        // Return sum of odd-place digits in number
        public static int SumOfOddPlace(long number)
        {
            int sum = 0;
            String num = number + "";
            for (int i = GetSize(number) - 1; i >= 0; i -= 2)
                sum += int.Parse(num[i] + "");
            return sum;
        }

        // Return true if the digit d
        // is a prefix for number
        public static bool PrefixMatched(long number, int d)
        {
            return GetPrefix(number, GetSize(d)) == d;
        }

        // Return the number of digits in d
        public static int GetSize(long d)
        {
            String num = d + "";
            return num.Length;
        }

        // Return the first k number of digits from 
        // number. If the number of digits in number
        // is less than k, return number.
        public static long GetPrefix(long number, int k)
        {
            if (GetSize(number) > k)
            {
                String num = number + "";
                return long.Parse(num.Substring(0, k));
            }
            return number;
        }
    }
}
