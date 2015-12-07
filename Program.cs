using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertBases
{
    public class BaseConverter
    {
        public enum Base
        {
            binary = 2,
            ternary = 3,
            quadary = 4,
            quintary = 5,
            hexary = 6,
            septal = 7,
            octal = 8,
            nonal = 9,
            deciml = 10,
            ekdecimal = 11,
            duadecimal = 12,
            tridecimal = 13,
            quaddecimal = 14,
            pentadecimal = 15,
            hexadecimal = 16,
        };

        public string ConvertToBase(string x, Base source, Base target) 
        {
            string result;	

            // Convert string to decimal
            bool isNegative = (x[0].CompareTo('-') == 0) ? true : false;
            
            int i = 0;            
            // adjust start index 
            if (isNegative)
            {
                i = 1;
            }

            // convert to decimal
            long inputNumber = 0;
            for (; i < x.Length; i++)
            {
                inputNumber = inputNumber * (int) source + ConvertCharToDigit(x[i]);
            }
           
            Console.WriteLine("Converted number in decimal {0}", inputNumber);
            
            // Convert decimal to base.
            result = ConvertFromDecimal(inputNumber, target);

            return isNegative ? "-" + result : result;
        }

        /// <summary>
        /// '0' - '0'
        /// '1' - '0'
        /// ...
        /// 10 + 'A' - 'A'
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private long ConvertCharToDigit(char p)
        {
            if (Convert.ToInt64(p) >= Convert.ToInt64('A') &&
                    Convert.ToInt64(p) <= Convert.ToInt64('F'))
            {
                return 10 + Convert.ToInt64(p) - Convert.ToInt64('A');
            }

            if (Convert.ToInt64(p) >= Convert.ToInt64('0') &&
                    Convert.ToInt64(p) <= Convert.ToInt64('9'))
            {
                return Convert.ToInt64(p) - Convert.ToInt64('0');
            }

            throw new ArgumentOutOfRangeException("p", p, "argument out of range");
        }

        /// <summary>
        /// 10  - A
        /// 11 - B
        /// 12 - C
        /// 13 - D
        /// 14 - E
        /// 15 - F
        /// target (16) - digit (14) = 2  
        /// 'E' = 'F' - 2 + 1
        /// </summary>
        /// <param name="digit"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        char ConvertDigitToChar(long digit, Base target)
        {
            return digit < 10 ? Convert.ToChar(Convert.ToInt64('0') + digit) : 
                Convert.ToChar(Convert.ToInt64('F') - (int)target + digit + 1); 
        }

        string ConvertFromDecimal(long inputNumber, Base target)
        {
            StringBuilder result = new StringBuilder();

            do
            {
                long digit = digit = inputNumber % (long)target;                
                char digitChar = ConvertDigitToChar(digit, target);
                result.Append(digitChar);
                inputNumber /= (long)target;
            } while (inputNumber != 0);

            StringBuilder s = new StringBuilder();
            var revEnum = result.ToString().Reverse();
            foreach (var c in revEnum)
            {
                s.Append(c);
            }

            Console.WriteLine("Result in target base {0} : {1}", target.ToString(), s.ToString());
            return s.ToString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var baseConv = new BaseConverter();

            var result = 
                baseConv.ConvertToBase("-0111", 
                BaseConverter.Base.binary, 
                BaseConverter.Base.hexadecimal);

            var result1 =    baseConv.ConvertToBase("FF",
                BaseConverter.Base.hexadecimal,
                BaseConverter.Base.deciml);            

            var result2 = baseConv.ConvertToBase("255",
                BaseConverter.Base.deciml,
                BaseConverter.Base.hexadecimal);

            var result3 = baseConv.ConvertToBase("E",
                BaseConverter.Base.hexadecimal,
                BaseConverter.Base.deciml); 

            return;
        }
    }
}
