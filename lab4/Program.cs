using System;

namespace lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello!");
            DLList list = new DLList();
            while(true)
            {
                Console.WriteLine();
                Console.WriteLine(@"Commands:
                1. addFirst
                2. addLast
                3. addAtPosition
                4. deleteFirst
                5. deleteLast
                6. deleteAtPosition
                7. addAfterMinvalue
                8. deleteNodesAndCreateNewList
                9. printList
                10. exit"); 
                Console.Write("\nEnter the number of command:");
                string command = Console.ReadLine();
                Console.Clear();
                if(command == "1")
                {
                    Console.WriteLine("Enter the data:");
                    int data;
                    if(!int.TryParse(Console.ReadLine(), out data))
                    {
                        Console.WriteLine("Error:incorrect format of data, try once more");
                        continue;
                    }
                    list.AddFirst(data);
                    
                }
                else if(command == "2")
                {
                    Console.WriteLine("Enter the data:");
                    int data;
                    if(!int.TryParse(Console.ReadLine(), out data))
                    {
                        Console.WriteLine("Error:incorrect format of data, try once more");
                        continue;
                    }
                    list.AddLast(data);
                }
                else if(command == "3")
                {
                    Console.WriteLine("Enter the data:");
                    int data;
                    if(!int.TryParse(Console.ReadLine(), out data))
                    {
                        Console.WriteLine("Error:incorrect format of data, try once more");
                        continue;
                    }
                    Console.WriteLine("Enter the position:");
                    int position;
                    if(!int.TryParse(Console.ReadLine(), out position))
                    {
                        Console.WriteLine("Error:incorrect format of data, try once more");
                        continue;
                    }
                    list.AddAtPosition(data, position);
                }
                else if(command == "4")
                {
                    list.DeleteFirst();
                }
                else if(command == "5")
                {
                    list.DeleteLast();
                }
                else if(command == "6")
                {
                    Console.WriteLine("Enter the position:");
                    int position;
                    if(!int.TryParse(Console.ReadLine(), out position))
                    {
                        Console.WriteLine("Error:incorrect format of data, try once more");
                        continue;
                    }
                    list.DeleteAtPosition(position);
                }
                else if(command == "7")
                {
                    Console.WriteLine("Enter the data:");
                    int data;
                    if(!int.TryParse(Console.ReadLine(), out data))
                    {
                        Console.WriteLine("Error:incorrect format of data, try once more");
                        continue;
                    }
                    list.AddAfterMinvalue(data);
                }
                else if(command == "8")
                {
                    list.Task2();
                }
                else if(command == "9")
                {
                    list.Print();
                }
                else if(command == "10")
                    break;
                else
                    Console.WriteLine("Wrong command, try again");    
                if(command!="8" && command!="9")       
                    list.Print();        
            }
            Console.WriteLine("Goodbye!");
        }
    }
    class DLList
    {
        private Node minimal;
        private Node maximal;
        private Node tail;
        private int size;
        private class Node
        {
            public int data;
            public Node next;
            public Node prev;
            public Node(int data)
            {
                this.data = data;
            }
        }
        public void AddFirst(int data)
        {
            Node firstNode = new Node(data);

            if (tail == null)
            {
                tail = firstNode;
            }
            else
            {
                Node current = tail;
                while(current.prev!=null)
                {
                    current = current.prev;
                }
                current.prev = firstNode;
                firstNode.next = current;
            }
            size++;
        }
        public void AddLast(int data)
        {
            Node lastNode = new Node(data);
            if (tail == null)
            {
                tail = lastNode;
            }
            else
            {
                Node current = tail;
                current.next = lastNode;
                lastNode.prev = current;
                tail = lastNode;
            }
            size++;
        }
        public void AddAtPosition(int data, int pos)
        {
            if(pos<1 || size+1<pos)
            {
                Console.WriteLine("That position is not exists");
                return;
            }
            if(pos == 1)
                AddFirst(data);
            else if(pos == size+1)
                AddLast(data);
            else
            {
                Node current = tail;
                for(int counter = 0; counter <size - pos; counter++)
                    current =current.prev; 
                Node toAdd = new Node(data);
                current.prev.next = toAdd;
                toAdd.next = current;
                toAdd.prev = current.prev;
                current.prev = toAdd;
                size++;
            }    
        }
        public void DeleteFirst()
        {
            if(tail!=null)
            {
                if(tail.prev == null)
                    tail=null;
                else
                {
                    Node current = tail;
                    while(current.prev!=null)
                    {
                        current = current.prev;
                    }
                    current.next.prev = null;
                }
                size--;
            }
            else
            {
                Console.WriteLine("Our list is empty, we can`t execute this operation");
            }
        }
        public void DeleteLast()
        {
            if(tail!=null)
            {
                if(tail.prev == null)
                    tail = null;
                else
                {
                    tail = tail.prev;
                    tail.next=null;
                }
                size--;
            }
            else
            {
                Console.WriteLine("Our list is empty, we can`t execute this operation");
            }
        }
        public void DeleteAtPosition(int pos)
        {
            if(pos<1 || size<pos)
            {
                Console.WriteLine("That position is not exists, we can`t execute this operation");
                return;
            }
            if(pos == 1)
                DeleteFirst();
            else if(pos == size)
                DeleteLast();
            else
            {
                Node current = tail;
                for(int counter = 0; counter <size - pos; counter++)
                    current =current.prev;
                current.prev.next = current.next;
                current.next.prev = current.prev;
                size--;     
            }
        }
        public void AddAfterMinvalue(int data)
        {
            if(size == 0)
            {
                AddFirst(data);
                return;
            }
            int position = 1;
            int ssize = size/2;
            Node current = tail;
            double min = double.PositiveInfinity;
            for(int i = size; i>ssize; i--)
                current = current.prev;
            for(int i = ssize; i>=1; i--)
            {
                if(current.data<min)
                {
                    min = current.data;
                    position = i;
                }
                current = current.prev;
            }
            position +=1;
            AddAtPosition(data, position);
        }
        public void FindMinAndMax()
        {
            if (tail == null)
            {
                Console.WriteLine("the list is empty");
                return;
            }
            minimal = tail;
            maximal = tail;
            Node current = tail;
            for (int i = size; i > 0; i--)
            {
                if (current.data < minimal.data)
                {
                    minimal = current;
                }
                if (current.data > maximal.data)
                {
                    maximal = current;
                }
                current = current.prev;
            }
        }
        public void Task2()
        {
            SLList slList = new SLList();
            if(size == 0)
            {
                Console.WriteLine("Our list is empty, we can`t do that operation");
                return;
            }    
            FindMinAndMax();
            Node current = tail;
            while(current.prev!=null)
            {
                current = current.prev;
            }
            for(int i = 1; i<=size; i++)
            {
                if(current.data != maximal.data && current.data != minimal.data)
                {
                    slList.AddBeforeTail(current.data);
                    DeleteAtPosition(i);
                    i--;
                }
                current = current.next;
                Console.WriteLine("Process deleting..");
                Print();
                Console.WriteLine("Process adding..");
                slList.Print();
            }
            Console.WriteLine("Done!\n\n");
            Print();
            slList.Print();
        }
        public void Print()
        {
            Console.ForegroundColor = ConsoleColor.Red;

            if(size==0)
            {
                Console.WriteLine("Current list is empty");
                Console.ResetColor();
                return;
            }
            Console.Write("Current list: ");
            Node current = tail;
            while(current.prev!=null)
            {
                current = current.prev;
            }
            while (current != null)
            {
                Console.Write(current.data);
                if (current.next != null)
                    Console.Write(" <--> ");
                current = current.next;
            }

            Console.ResetColor();
            Console.WriteLine();
        }        
    }
    class SLList
    {
        public Node head;
        public int size;

        public class Node
        {
            public int data;
            public Node next;

            public Node(int data)
            {
                this.data = data;
            }
        }
        public void AddBeforeTail(int data)
        {
            Node current = head;
            Node beforetail = new Node(data);
            if (head == null)
            {
                head = beforetail;
                size++;
            }
            else if(size == 1)
            {
                Node tmp = head;
                head = beforetail;
                beforetail.next = tmp;
                size++;
            }
            else
            {
                while (current.next.next != null)
                {
                    current = current.next;
                }
                beforetail.next = current.next;
                current.next = beforetail;
                size++;
            }
            
        }
        public void Print()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            if(head == null)
            {
                Console.Write("Current singly linked list is empty.");
                Console.ResetColor();
                Console.WriteLine();
                return;
            }
            Console.Write("Current singly linked list: ");

            Node current = head;

            while (current != null)
            {
                Console.Write(current.data);
                if (current.next != null) Console.Write(" --> ");
                current = current.next;
            }
            Console.ResetColor();
            Console.WriteLine();
        }
    }  
}