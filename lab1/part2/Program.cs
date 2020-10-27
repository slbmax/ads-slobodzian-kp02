using System;

namespace part2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the day:");
            int d = int.Parse(Console.ReadLine());
            Console.Write("Enter the month:");
            int m = int.Parse(Console.ReadLine());
            if (check_the_correct_date(m, d) == false)
            {
                Console.WriteLine("Error: wrong entered date or month");
            }
            else
            {
                int what_week_is = day_sum(m,d)%7;
                if(what_week_is == 1)
                Console.WriteLine("Day:{0}, month:{1}, it`s wednesday", d, m);
                else if(what_week_is == 2)
                Console.WriteLine("Day:{0}, month:{1}, it`s thursday", d, m);
                else if(what_week_is == 3)
                Console.WriteLine("Day:{0}, month:{1}, it`s friday", d, m);
                else if(what_week_is == 4)
                Console.WriteLine("Day:{0}, month:{1}, it`s saturday", d, m);
                else if(what_week_is == 5)
                Console.WriteLine("Day:{0}, month:{1}, it`s sunday", d, m);
                else if(what_week_is == 6)
                Console.WriteLine("Day:{0}, month:{1}, it`s monday", d, m);
                else
                Console.WriteLine("Day:{0}, month:{1}, it`s tuesday", d, m);
            }
        }

        static int days_in_month(int month)
        {
            
            if (month < 8)
            {
                if (month == 2)
                return 28;
                else
                    {
                    if (month % 2 == 1)
                    return 31;
                    else
                    return 30;
                    }
            }
            else
            {
                if (month % 2 == 1)
                    return 30;
                else
                    return 31;
            }
        }

        static int day_sum(int m, int d)
        {
            int days = 0;
            for (int n = 1; n < m; n++)
            {
                days = days + days_in_month(n);
            }
            return days + d;

        }
        static bool check_the_correct_date(int m, int d)
        {
            if (m > 0 && m < 13 && d > 0 && d < 32)
            {
                if (d <= days_in_month(m))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
    }


}
