using System;

namespace lab3
{
    class Program
    {
        static void Main(string[] args)
        {   
            Console.Write("Enter the size of array:");
            int n;
            bool parse = int.TryParse(Console.ReadLine(), out n);
            if(!parse || n<1)
            {
                Console.WriteLine("Wrong data");
                Environment.Exit(0);
            }

            int[] array = new int[n];
            Random random = new Random();

            Console.WriteLine("Our main array:\n");

            for(int i = 0; i<n; i++)
            {
                array[i] = random.Next(-999, 1000);
                if(array[i]%2==0)
                Console.BackgroundColor =ConsoleColor.DarkRed;              
                Console.Write("{0}   ", array[i]);
                Console.ResetColor();
            }
                        
            SecondaryChangeofEven(array);

            int index_even = MainChangeOfEven(array);

            Sorting(array, index_even); 

            Console.WriteLine("\n\n\nSorted array:\n");

            for(int i = 0; i<array.Length; i++)
            {
                if(array[i]%2==0)
                Console.BackgroundColor =ConsoleColor.DarkRed;              
                Console.Write("{0,5}", array[i]);
                Console.ResetColor();
            }        
        }     
        static void SecondaryChangeofEven(int[] array)
        {
            int c = array.Length;
            for(int i = 0; i<c; i++)
            {
                if(array[i]%2==0)
                {
                    while(c>=i)
                    {
                        if(array[c-1]%2==0)
                        {
                            int buff = array[i];
                            array[i] = array[c-1];                
                            array[c-1] = buff;                
                            c--;
                            break;
                        }
                        c--;
                    }
                }
            }
        }
        static int MainChangeOfEven(int[] array)
        {
            int index_even = 0;
            for(int i = 0; i<array.Length; i++)
            {
                if(array[i]%2==0)
                {
                    int buff = array[i];
                    for(int k = i; k>index_even; k--)       
                        array[k] = array[k-1];
                    array[index_even] = buff;
                    index_even++;                   
                }
            }
            return index_even;
        }
        static void Sorting(int[] array, int index_even)
        {
            int gap = array.Length/2;
            while (gap>=1)
            {
                for(int i = gap; i<array.Length; i++)
                {                  
                    int current = array[i];
                    for(int j = i; j>=gap; j=j-gap)
                    {   
                        if(j-gap>=index_even)
                        {
                            if(array[j-gap]>current)            
                            {
                                int buff = array[j];
                                array[j] = array[j-gap];
                                array[j-gap] = buff;
                            }
                            else
                            break;
                        }    
                    }
                }
                gap=gap/2;
            }
        }
    }
}
