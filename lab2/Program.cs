using System;

namespace lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter matrix size: ");
            int n = int.Parse(Console.ReadLine());
            Console.Write("Enter number k: ");
            int k = int.Parse(Console.ReadLine());
            if(n<2)
            {
                Console.WriteLine("Size of matrix can`t be less than 2");
            }
            else if(k==0)
            {
                Console.WriteLine("Number k can`t be equal to zero");
            }
            else
            {
                int[,] matrix;
                MinMax MinMax = new MinMax
                {
                    minvalue=1000,
                    maxvalue=-1,
                    minvalue_index_i=0,
                    minvalue_index_j=0,
                    maxvalue_index_i=0,
                    maxvalue_index_j=0,
                    maxvalue1=-1,
                    minvalue1=1000,
                    minvalue1_index_i = 0,
                    minvalue1_index_j = 0,
                    maxvalue1_index_i = 0,
                    maxvalue1_index_j = 0
                }; 
                Console.WriteLine("\nCommands:\n\t'random' - create random matrix;\n\t'control' - create control matrix");
                string command = Console.ReadLine();
                Console.WriteLine();
                if(command == "control")
                {
                    matrix = ControlMatrix(n);
                    BelowSideD(n, matrix, ref MinMax, k);
                    SideD(n, matrix,ref MinMax);            
                    AboveSideD(n, matrix, ref MinMax);
                    Results(ref MinMax);                 
                }
                else if(command == "random")
                {  
                    matrix = RandomMatrix(n);
                    BelowSideD(n, matrix, ref MinMax, k);
                    SideD(n, matrix,ref MinMax);            
                    AboveSideD(n, matrix, ref MinMax);
                    Results(ref MinMax);                   
                }
                else
                {
                    Console.WriteLine("Command does not exist");
                }   
            }
        }
        static int[,] ControlMatrix(int n)
        {
            int[,] matrix = new int[n,n];
            int c = 0;
            for(int i = 0; i<n ; i++)
            {
                for(int j = 0; j<n; j++)
                {   
                    matrix[i,j] = c;                
                    Console.Write("{0,4}", matrix[i,j]);
                    c++;
                }
                Console.WriteLine();
            }
            return matrix;
        }
        static int[,] RandomMatrix(int n)
        {
            int[,] matrix = new int[n,n];
            Random random = new Random();       
            for(int i = 0; i<n; i++)
            {
                for(int j = 0; j<n; j++)
                {
                matrix[i,j] = random.Next(10,100);
                Console.Write("{0} ", matrix[i,j]);
                }
                Console.WriteLine();
            }
            return matrix;
        }
        static void AboveSideD(int n, int[,] matrix, ref MinMax MinMax) 
        {
            int c = 0;
            double absolute = Math.Abs(MinMax.minvalue - MinMax.maxvalue)/2.0;
            for (int i = n - 2; i >= 0; i--)
            {
                if(n%2==1)
                {
                    if (c % 2 == 0)
                    {
                        for (int j = 0; j < (n - 1 - i); j++)
                        {
                            Console.Write("{0} ", matrix[i,j]);
                            if(matrix[i,j]<MinMax.minvalue1 && matrix[i,j] % absolute ==0)
                            {
                            MinMax.minvalue1 = matrix[i,j];
                            MinMax.minvalue1_index_i = i;
                            MinMax.minvalue1_index_j = j;
                            }    
                        }    
                    }
                    else 
                    {
                        for (int j = (n - 2 - i); j >= 0; j--)
                        {
                            Console.Write("{0} ", matrix[i,j]);
                            if(matrix[i,j]<MinMax.minvalue1 && matrix[i,j] % absolute == 0)
                            {
                                MinMax.minvalue1 = matrix[i,j];
                                MinMax.minvalue1_index_i = i;
                                MinMax.minvalue1_index_j = j;
                            }    
                        }        
                    }
                }
                else
                {
                    if(c%2==0)
                    {
                        for (int j = (n - 2 - i); j >= 0; j--)
                        {
                            Console.Write("{0} ", matrix[i,j]);
                            if(matrix[i,j]<MinMax.minvalue1 && matrix[i,j] % absolute == 0)
                            {
                                MinMax.minvalue1 = matrix[i,j];
                                MinMax.minvalue1_index_i = i;
                                MinMax.minvalue1_index_j = j;
                            }    
                        }
                    }
                    else
                    {
                        for (int j = 0; j < (n - 1 - i); j++)
                        {
                            Console.Write("{0} ", matrix[i,j]);
                            if(matrix[i,j]<MinMax.minvalue1 && matrix[i,j] % absolute ==0)
                            {
                            MinMax.minvalue1 = matrix[i,j];
                            MinMax.minvalue1_index_i = i;
                            MinMax.minvalue1_index_j = j;
                            }    
                        }
                    }
                }
                c++;
            }
        }
        static void BelowSideD(int n, int[,] matrix, ref MinMax MinMax, int k)

        {
            Console.WriteLine("\nMatrix traversal:\n");
            int c = 0;
            for (int j = 1; j<n; j++)
            {
                if(n%2==1)
                {
                    if(c%2 ==0)
                    {
                        for(int i = (n - j); i<n ;i++)
                        {
                            Console.Write("{0} ", matrix[i,j]);
                            if(matrix[i,j]>MinMax.maxvalue1 && matrix[i,j]%k==0)
                            { 
                                MinMax.maxvalue1 = matrix[i,j];  
                                MinMax.maxvalue1_index_i = i;
                                MinMax.maxvalue1_index_j = j;
                            } 
                        }
                    }
                    else
                    {
                        for(int i =n-1; i>=(n-j); i--)
                        {
                            Console.Write("{0} ", matrix[i,j]);
                            if(matrix[i,j]>MinMax.maxvalue1 && matrix[i,j]%k==0)
                            { 
                                MinMax.maxvalue1 = matrix[i,j];
                                MinMax.maxvalue1_index_i = i;
                                MinMax.maxvalue1_index_j = j;
                            }
                        }
                    }
                }
                else
                {
                    if(c%2==0)
                    {
                        for(int i =n-1; i>=(n-j); i--)
                        {
                            Console.Write("{0} ", matrix[i,j]);
                            if(matrix[i,j]>MinMax.maxvalue1 && matrix[i,j]%k==0)
                            { 
                                MinMax.maxvalue1 = matrix[i,j];
                                MinMax.maxvalue1_index_i = i;
                                MinMax.maxvalue1_index_j = j;
                            }
                        }
                    }
                    else
                    {
                        for(int i = (n - j); i<n ;i++)
                        {
                            Console.Write("{0} ", matrix[i,j]);
                            if(matrix[i,j]>MinMax.maxvalue1 && matrix[i,j]%k==0)
                            { 
                                MinMax.maxvalue1 = matrix[i,j];  
                                MinMax.maxvalue1_index_i = i;
                                MinMax.maxvalue1_index_j = j;
                            } 
                        }
                    }

                }    
                c++;
            }
        }
        static void SideD(int n, int[,] matrix, ref MinMax MinMax)
        {
            for(int i = 0; i<n; i++)            
            { 
                int j = n - 1 - i;
                if(matrix[i,j]< MinMax.minvalue)
                {
                    MinMax.minvalue = matrix[i,j];
                    MinMax.minvalue_index_i = i;
                    MinMax.minvalue_index_j = j;
                }
                if(matrix[i,j]>MinMax.maxvalue)
                {
                    MinMax.maxvalue = matrix[i,j];
                    MinMax.maxvalue_index_i = i;
                    MinMax.maxvalue_index_j = j;
                }
                Console.Write("{0} ", matrix[i,j]);
            }
        }
        static void Results(ref MinMax MinMax)
        {
            Console.WriteLine();
            if(MinMax.maxvalue1 == -1)
                Console.WriteLine("\nBelow the side diagonal:\nthere aren`t any values that are multiple of k");
            else
                Console.WriteLine("\nBelow the side diagonal:\nIndex of maximal value that is multiple of k: [{0},{1}] ", MinMax.maxvalue1_index_i, MinMax.maxvalue1_index_j);     
            Console.WriteLine("\nSide Diagonal:\nIndex of minimal value [i,j]: [{0},{1}]",MinMax.minvalue_index_i, MinMax.minvalue_index_j);
            Console.WriteLine("Index of maximal value [i,j]: [{0},{1}]",MinMax.maxvalue_index_i, MinMax.maxvalue_index_j);
            if(MinMax.minvalue1 == 1000)
                Console.WriteLine("\nAbove the side diagonal:\nThere aren`t any values that are multiple of that thing");
            else
                Console.WriteLine("\nAbove the side diagonal:\nIndex of minimal value that is multiple of that thing: [{0},{1}]", MinMax.minvalue1_index_i, MinMax.minvalue1_index_j);
        }
        struct MinMax
        {
            public int minvalue;
            public int maxvalue;
            public int minvalue_index_i;
            public int minvalue_index_j;
            public int maxvalue_index_i;
            public int maxvalue_index_j;
            public int maxvalue1;
            public int minvalue1;
            public int minvalue1_index_i;
            public int minvalue1_index_j;
            public int maxvalue1_index_i;
            public int maxvalue1_index_j;
        }
    }        
}