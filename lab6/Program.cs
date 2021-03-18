using System;
using System.IO;

namespace lab6
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            while(!exit)
            {
                string htmlCode = ""; 
                Console.WriteLine("\nChoose the command:\n1.control html code\n2.your html code\n3.exit");
                int command;
                bool parse = int.TryParse(Console.ReadLine(), out command);
                if(!parse)
                {
                    Console.WriteLine("Wrong command");
                    continue;
                }
                if(command == 1)
                {
                    htmlCode = @"
                    <html>
                    <head><title>Hello</title></head>
                    <body><p>This appears in the
                    <i>browser</i>.</p></body>
                    </html>";
                    Console.WriteLine($"Control code is:\n{htmlCode}");
                }
                else if(command == 2)
                {
                    Console.WriteLine("Enter your code:\n");
                    htmlCode = Console.ReadLine();
                }
                else if(command == 3)
                {
                    exit = true;
                    continue;
                }
                else
                {
                    Console.WriteLine("Wrong command");
                    continue;
                }
                string result = ProcessHTMLAnalysis(htmlCode);
                if(result == "0")
                {
                    Console.WriteLine("\nAnalysis passed successfully");
                }
                else
                {
                    Console.WriteLine($"\nAnalysis failed: {result}");
                }
            }    
        }
        static string ProcessHTMLAnalysis(string htmlCode)
        {
            Stack stack = new Stack();
            string buffer = "";
            bool ifBrOpen = false;
            bool toCheck = false;
            int counter = 0;
            for(int i = 0; i < htmlCode.Length; i ++)
            {
                if(htmlCode[i] == '<')
                {
                    ifBrOpen = true;
                    buffer += htmlCode[i];
                    i++;
                    while(i < htmlCode.Length && htmlCode[i] != '>')
                    {
                        if(htmlCode[i] == '/' && htmlCode[i-1] == '<')
                        {
                            toCheck = true;
                            buffer += htmlCode[i];
                        }
                        else if(htmlCode[i] == '/' && htmlCode[i-1] != '<')
                            return "wrong tag syntax";
                        else if(htmlCode[i] == '<')
                            return "wrong tag syntax";
                        else
                            buffer += htmlCode[i];
                        i++;
                    }
                }
                if(i < htmlCode.Length && htmlCode[i] == '>')
                {
                    ifBrOpen = false;
                    buffer += htmlCode[i];
                    if(toCheck)
                    {
                        bool isOk = CheckStack(buffer, stack);
                        if(isOk)
                        {
                            string deleted = stack.Pop();
                            Console.WriteLine($"Tag {deleted} was closed");
                            stack.Print();
                            buffer = "";
                            counter = 1;
                            toCheck = false;
                        }
                        else
                            return "wrong closer tag";
                    }
                    else
                    {
                        if(buffer.Length <= 2 || buffer.Contains(' '))
                            return "wrong tag";
                        stack.Push(buffer);
                        stack.Print();
                        buffer = "";
                    }
                }
            }
            if(ifBrOpen)
                return "wrong tag syntax";
            else if(!stack.IsEmpty())
                return "extra tag / tags";
            else if(counter == 0)
                return "there weren`t any html tags to check";
            return "0";
        }
        static bool CheckStack(string buffer, Stack stack)
        {
            if(stack.IsEmpty())
                return false;
            string topStack = stack.Peek().Substring(1);
            string current = buffer.Substring(2);
            if(topStack == current)
                return true;
            else
                return false;
        }
    }
    class Node
    {
        public Node next;
        public string data;
        public Node(string data)
        {
            this.data = data;
        }
    }
    class Stack
    {
        private Node top;
        private int size;
        public void Push(string data)
        {
            Node newNode = new Node(data);
            newNode.next = this.top;
            this.top = newNode;
            this.size ++;
        }
        public string Pop()
        {
            if (this.top == null)
                throw new Exception("Stack is empty");
            this.size --;
            Node tmp = this.top;
            if (this.top.next != null)
                this.top = this.top.next;
            else
                this.top = null;
            return tmp.data;
        }
        public string Peek()
        {
            if (this.top == null)
                throw new Exception("Stack is empty");
            return this.top.data;
        }
        public bool IsEmpty()
        {
            if(this.top == null)
                return true;  
            return false;
        }
        public void Print()
        {
            Console.WriteLine("\nCurrent stack: \n");
            Node current = this.top;
            if (this.top == null)
            {
                Console.WriteLine("Stack is empty");
                return;
            }
            int i = this.size;
            while (current != null)
            {
                Console.WriteLine($"{i}. {current.data}");
                current = current.next;
                i--;
            }
        }
    }
}