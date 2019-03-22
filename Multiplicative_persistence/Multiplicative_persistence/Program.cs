using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Numerics;
using System.Threading;

namespace Multiplicative_persistence
{
    class Program
    {
        public static double constant = Math.Log(2) / Math.Log(10);

        static void Main(string[] args)
        {
            //Checked up to 2^800(10^240)


            Console.Write("Lower limit: ");
            int start = Convert.ToInt32(Console.ReadLine());
            Console.Write("Upper limit: ");
            int end = Convert.ToInt32(Console.ReadLine());

            Parallel.For(start, end, Check_lim);

            Console.WriteLine("Done!");
            Console.ReadLine();
        }

        public static void Check_lim(int lim)
        {
            //Console.WriteLine("Starting: " + lim);
            for (int a = 0; a < lim + 1; a++)
            {
                for (int b = 0; b < lim - a + 1; b++)
                {
                    int c = lim - a - b;
                    BigInteger three = BigInteger.Pow(3, b);
                    BigInteger seven = BigInteger.Pow(7, c);
                    BigInteger num = BigInteger.Multiply(BigInteger.Multiply(BigInteger.Pow(2, a), three), seven);
                    BigInteger num2 = BigInteger.Multiply(BigInteger.Multiply(BigInteger.Pow(5, a), three), seven);
                    int x = Digits_product(num);
                    int y = Digits_product(num2);
                    if (x+1 > 10)
                    {
                        Console.WriteLine("x: " + x.ToString() + " " + lim.ToString() + " " + Compress(string.Concat(Enumerable.Repeat("2", a)) + string.Concat(Enumerable.Repeat("3", b)) + string.Concat(Enumerable.Repeat("7", c))));
                    }
                    if (y+1 > 10)
                    {
                        Console.WriteLine("y: " + y.ToString() + " " + lim.ToString() + " " + Compress(string.Concat(Enumerable.Repeat("5", a)) + string.Concat(Enumerable.Repeat("3", b)) + string.Concat(Enumerable.Repeat("7", c))));
                    }
                }
            }
            //Console.WriteLine("Calculated: " + lim);
        }
        static int Digits_product(BigInteger c)
        {
            string n = c.ToString();
            int count = 0;
            while(n.Length > 1)
            {
                BigInteger a = 1;
                //ulong a = 1;
                for(int i = 0; i < n.Length; i++)
                {
                    a = BigInteger.Multiply(Convert.ToByte(n.Substring(i, 1)), a);
                }
                n = a.ToString();
                count++;
            }
            return count;
        }

        static string Compress(string n)
        {
            int twos = 0;
            foreach (char c in n)
                if (c == '2') twos++;
            int threes = 0;
            foreach (char c in n)
                if (c == '3') threes++;

            n = n.Replace("2", "");
            n = n.Replace("3", "");
            string num = "";

            num += string.Concat(Enumerable.Repeat("8", (twos/3)));
            twos -= (twos / 3) * 3;

            num += string.Concat(Enumerable.Repeat("9", (threes / 2)));
            threes -= (threes / 2) * 2;

            if (threes != 0 && twos != 0)
            {
                num += "6";
                twos--;
                if (twos == 1)
                    num += "2";
            }
            else if (twos == 1)
                num += "2";
            else if (twos == 2)
                num += "4";
            else if (threes != 0)
                num += "3";

            return String.Concat((num + n).OrderBy(c => c));
        }
    }
}
