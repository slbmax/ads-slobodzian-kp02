using System;       //finished

namespace lab5
{
    class Program
    {
        static void Main(string[] args)
        {
            int m, n, k;
            Console.WriteLine("Enter number M (rows):");
            bool parseM = int.TryParse(Console.ReadLine(), out m);
            if(!parseM || m <= 0)
            {
                Console.WriteLine("Error: wrong value (number M)");
                Environment.Exit(1);
            }
            Console.WriteLine("Enter number N (columns):");
            bool parseN = int.TryParse(Console.ReadLine(), out n);
            if(!parseN || n <= 0)
            {
                Console.WriteLine("Error: wrong value (number N)");
                Environment.Exit(1);
            }
            Console.WriteLine("Enter number K");
            bool parseK = int.TryParse(Console.ReadLine(), out k);
            if(!parseN || k == 0)
            {
                Console.WriteLine("Error: wrong value (number K)");
                Environment.Exit(1);
            }
            int[,] matrix;
            int[] secondaryArray = new int[m*n];                                            //it is a secondary array cuz idk how much elements
            for(int i = 0; i < secondaryArray.Length; i++)                                  //i will sort, so it has the same size as our matrix
                secondaryArray[i] = -1; 
            Console.WriteLine("\nCommands:\n\t1.'random' - create random matrix;\n\t2.'control' - create control matrix");
            string command = Console.ReadLine();
            if(command == "2" || command == "control")
                matrix = CreateControlMatrix(n, m, k, secondaryArray);    
            else if(command == "1" || command == "random")
                matrix = CreateRandomMatrix(n, m, k, secondaryArray);
            else
            {
                matrix = new int[1,1] {{1}};                                                //Do not pay attention
                Console.WriteLine("Error: wrong command");
                Environment.Exit(1);
            }
            int counter = 0;
            for(int i = 0; i  < secondaryArray.Length; i++)
            {
                if(secondaryArray[i] != -1)
                    counter++;
                else
                    break;    
            }
            if(counter == 0)
            {
                Console.WriteLine("There aren`t any elements to sort!");
                Console.WriteLine("So, our final matrix is:");
                for(int i = 0; i < m; i++)
                {
                    for(int j = 0; j < n; j++)
                        Console.Write("{0,4}", matrix[i,j]);
                    Console.WriteLine();
                }
                Environment.Exit(0);
            }
            int[] arrayToSort = new int[counter];                                               //my main array with normal size 
            Console.WriteLine("To Sort:");                                                      //sorting
            for(int i = 0; i < counter; i++)
            {
                arrayToSort[i] = secondaryArray[i];
                Console.Write("{0,4}", arrayToSort[i]);
            }
            int[] sortedArray = Sorting(arrayToSort);
            Console.WriteLine("\nSorted array:");
            for(int i = 0; i < sortedArray.Length; i++)
                Console.Write("{0,4}", sortedArray[i]);                                          //combine array and matrix 
            Console.WriteLine("\nOur sorted matrix is:");
            int matrixCounter = 0;
            int temp = 0;
            for(int i = 0; i < m; i++)
            {
                for(int j = 0; j < n; j++)
                {  
                    temp = matrix[i,j] % k;
                    if(temp == 0 || temp % 2 == 1)
                    {
                        matrix[i,j] = sortedArray[matrixCounter];
                        matrixCounter++;
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.Write("{0,4}", matrix[i,j]);
                    if(temp == 0 || temp % 2 == 1)
                        Console.ResetColor();
                }
                Console.WriteLine();
            }                                                                                    
        }
        static int[,] CreateControlMatrix(int n, int m, int k, int[] array)
        {
            int[,] matrix = new int[m,n];
            int c = m*n-1;
            int counter = 0;
            int temp;
            Console.WriteLine("\nOur matrix is:");
            for(int i = 0; i < m; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    matrix[i,j] = c;
                    temp = c % k;
                    if(temp == 0 || temp % 2 == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        array[counter] = c;
                        counter++;
                    }
                    Console.Write("{0,4}", matrix[i,j]);
                    if(temp == 0 || temp % 2 == 1)
                        Console.ResetColor();
                    c--; 
                }
                Console.WriteLine();
            }
            return matrix;
        }
        static int[,] CreateRandomMatrix(int n, int m, int k, int[] array)
        {
            int[,] matrix = new int[m,n];
            Random rand = new Random();
            int temp;
            int counter = 0;
            Console.WriteLine("\nOur matrix is:");
            for(int i = 0; i < m; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    temp = rand.Next(0, 100);
                    matrix[i,j] = temp;
                    if(temp % k == 0 || (temp % k) % 2 == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        array[counter] = temp;
                        counter++;
                    }
                    Console.Write("{0,4}", matrix[i,j]);
                    if(temp % k == 0 || (temp % k) % 2 == 1)
                        Console.ResetColor();
                }
                Console.WriteLine();
            }
            return matrix;
        }
        static int[] Sorting(int[] arrayToSort)
        {
            int max = arrayToSort[0];
            for(int i = 0; i < arrayToSort.Length; i++)
            {
                if(arrayToSort[i]>max)
                    max = arrayToSort[i];
            }
            int[] count = new int[max+1];
            for(int i = 0; i < arrayToSort.Length; i++)
                count[arrayToSort[i]]++;
            for(int i  = 1; i < count.Length; i++)
                count[i] += count[i-1];
            int[] sorted = new int[arrayToSort.Length];
            for(int i = arrayToSort.Length - 1; i >= 0; i--)
            {
                sorted[count[arrayToSort[i]]-1] = arrayToSort[i];
                count[arrayToSort[i]] --;
            }
            return sorted;
        }
    }
}