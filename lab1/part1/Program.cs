using System;

namespace part1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter number x:");
            double x = double.Parse(Console.ReadLine());
            Console.Write("Enter number y:");
            double y = double.Parse(Console.ReadLine());
            Console.Write("Enter number z:");
            double z = double.Parse(Console.ReadLine());
            double ln = Math.Log(Math.Abs(y-x), Math.E);
            if (z==-x || x==y || ln==-2)
            {
            Console.WriteLine("Error:wrong data");
            }
            else
            {
                double a = 1+Math.Log10(Math.Abs(x+z))/(1+ln/2);
                double b = 1/(Math.Pow(a, 4)+1); 
                Console.WriteLine("a is: {0};", a);
                Console.WriteLine("b is: {0};", b);
            }
        }
    }
}
