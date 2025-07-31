using System;
using System.Numerics;
namespace PythagoreanTripleHelper
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            DateTime AnnaBirthday = new DateTime(2015, 3, 8, 16, 30, 0);
            DateTime EllieBirthday = new DateTime(2012, 3, 23, 16, 30, 0);
            DateTime FinlayBirthday = new DateTime(2004, 9, 22, 0, 27, 0);

            long AnnaBirthdayTicks = AnnaBirthday.Ticks;
            long EllieBirthdayTicks = EllieBirthday.Ticks;
            long FinlayBirthdayTicks = FinlayBirthday.Ticks;

            BigInteger RadicalContents = new BigInteger(2) * new BigInteger(AnnaBirthdayTicks - FinlayBirthdayTicks) * new BigInteger(EllieBirthdayTicks - FinlayBirthdayTicks);
            long Root = (long)RadicalContents.Sqrt();
            long Main = AnnaBirthdayTicks + EllieBirthdayTicks - FinlayBirthdayTicks;
            long SolutionTicks = Main + Root;

            long AnnaAgeAtSolutionTicks = SolutionTicks - AnnaBirthdayTicks;
            long EllieAgeAtSolutionTicks = SolutionTicks - EllieBirthdayTicks;
            long FinlayAgeAtSolutionTicks = SolutionTicks - FinlayBirthdayTicks;

            BigInteger SquaredError = (new BigInteger(AnnaAgeAtSolutionTicks) * new BigInteger(AnnaAgeAtSolutionTicks)) + (new BigInteger(EllieAgeAtSolutionTicks) * new BigInteger(EllieAgeAtSolutionTicks)) - (new BigInteger(FinlayAgeAtSolutionTicks) * new BigInteger(FinlayAgeAtSolutionTicks));
            if (SquaredError.Sign == -1)
            {
                SquaredError = -SquaredError;
            }
            long Error = (long)SquaredError.Sqrt();

            Console.WriteLine($"Solution: {SolutionTicks} ticks.");
            Console.WriteLine($"Solution Date: {new DateTime(SolutionTicks).ToString("MM/dd/yyyy hh:mm:ss tt")}.");
            Console.WriteLine($"Anna's Age: {new TimeSpan(AnnaAgeAtSolutionTicks).TotalDays / 365.0} year/s.");
            Console.WriteLine($"Ellie's Age: {new TimeSpan(EllieAgeAtSolutionTicks).TotalDays / 365.0} year/s.");
            Console.WriteLine($"Finlay's Age: {new TimeSpan(FinlayAgeAtSolutionTicks).TotalDays / 365.0} year/s.");
            Console.WriteLine($"Error: {Error} ticks.");
            Console.WriteLine($"Answer is accurate to within {new TimeSpan(Error).TotalSeconds} seconds.");

            Console.ReadLine();
        }
        public static BigInteger Sqrt(this BigInteger n)
        {
            if (n == 0) return 0;
            if (n > 0)
            {
                int bitLength = Convert.ToInt32(Math.Ceiling(BigInteger.Log(n, 2)));
                BigInteger root = BigInteger.One << (bitLength / 2);
                while (!isSqrt(n, root))
                {
                    root += n / root;
                    root /= 2;
                }
                return root;
            }

            throw new ArithmeticException("NaN");
        }
        private static Boolean isSqrt(BigInteger n, BigInteger root)
        {
            BigInteger lowerBound = root * root;
            BigInteger upperBound = (root + 1) * (root + 1);
            return (n >= lowerBound && n < upperBound);
        }
    }
}