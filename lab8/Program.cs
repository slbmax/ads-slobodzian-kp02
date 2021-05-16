using System;

namespace lab8
{
    class Program
    {
        static void Main(string[] args)
        {
            RBTree tree = new RBTree();
            while(true)
            {
                Console.WriteLine("1. Control adding (control)\n2. Dynamic adding (dynamic)\n3. Exit (exit)");
                string command = Console.ReadLine();
                if(command == "control") {ControlAdding(tree); break;}
                else if(command == "dynamic")
                {
                    while(true)
                    {
                        Console.WriteLine("\nEnter integer key to insert,'p' to print tree and 'e' to exit");
                        string input = Console.ReadLine();
                        if(input == "p"){ tree.PrintTree();}
                        else if(input == "e") break;
                        else
                        {
                            int num = 0;
                            if(!int.TryParse(input, out num))
                            {
                                Console.WriteLine("Error: input key should be integer");
                                continue;
                            }
                            try{tree.Insert(num); 
                            Console.WriteLine($"Inserting '{num}': successfull");}
                            catch(Exception ex) {Console.WriteLine(ex.Message);}
                        }
                    }
                    break;
                }
                else if(command == "exit") break;
                else{Console.WriteLine("Invalid command, please, try again");}
            }
        }
        static void ControlAdding(RBTree tree)
        {
            Console.WriteLine("Inserting node  8:");
            tree.Insert(8);
            tree.PrintTree();
            System.Threading.Thread.Sleep(2000);
            Console.WriteLine("Inserting node  4:");
            tree.Insert(4);
            tree.PrintTree();
            System.Threading.Thread.Sleep(3000);
            Console.WriteLine("Inserting node  7:");
            tree.Insert(7);
            System.Threading.Thread.Sleep(4000);
            tree.PrintTree();
            System.Threading.Thread.Sleep(4000);
            Console.WriteLine("Inserting node  3:");
            tree.Insert(3);
            System.Threading.Thread.Sleep(4000);
            tree.PrintTree();
            System.Threading.Thread.Sleep(4000);
            Console.WriteLine("Inserting node  2:");
            tree.Insert(2);
            System.Threading.Thread.Sleep(4000);
            tree.PrintTree();
            System.Threading.Thread.Sleep(4000);
            Console.WriteLine("Inserting node  11:");
            tree.Insert(11);
            System.Threading.Thread.Sleep(4000);
            tree.PrintTree();
            System.Threading.Thread.Sleep(4000);
            Console.WriteLine("Inserting node  9:");
            tree.Insert(9);
            System.Threading.Thread.Sleep(4000);
            tree.PrintTree();
            System.Threading.Thread.Sleep(4000);
            Console.WriteLine("Inserting node  12:");
            tree.Insert(12);
            System.Threading.Thread.Sleep(4000);
            tree.PrintTree();
            System.Threading.Thread.Sleep(4000);
            Console.WriteLine("Inserting node  13:");
            tree.Insert(13);
            System.Threading.Thread.Sleep(4000);
            tree.PrintTree();
        }
    }
    class Node
    {
        public string color;
        public int data;
        public Node[] child; // 0 -- left / 1 -- right
        public Node(int data)
        {
            this.data = data;
            this.color = "r";
            this.child = new Node[2];
        }
    }
    class RBTree
    {
        public Node root;
        public void Insert(int data)
        {
            if(root == null)
                root = new Node(data);
            else if(isExists(root, data)) throw new Exception($"Error: node '{data}' is already in the tree");
            else
            {
                Node temporaryRoot = new Node(0);
                Node grand =null; // grandparent
                Node par = null; // parent
                Node curr = null; // current
                int direction = 0; //direction means left or right child
                int lastDir = 0;
                Node greatGrand = temporaryRoot;// great-grandparent
                greatGrand.child[1] = root;
                curr = greatGrand.child[1];
                while(true)
                {
                    if(curr == null) {curr = new Node(data); par.child[direction] = curr;} // <-insertion
                    else if(IsRed(curr.child[0]) && IsRed(curr.child[1]))
                    {
                        Console.WriteLine($"\nParent '{curr.data}' is black and his children '{curr.child[0].data}'"
                        + $" and '{curr.child[1].data}' is red:");
                        Console.WriteLine("    Solution -- perform inverting their colors");
                        curr.color = "r"; 
                        curr.child[0].color = "b";
                        curr.child[1].color = "b";
                    }
                    if(IsRed(par) && IsRed(curr))
                    {
                        int dir2 = 0; // left or right node we will work with
                        if(greatGrand.child[1] == grand) dir2 = 1;
                        int dirToRotate = lastDir == 1 ? 0 : 1;
                        if(curr == par.child[lastDir])
                        {
                            if(lastDir == 1)
                                Console.WriteLine($"Right-right violation of grandparent '{greatGrand.child[dir2].data}',"
                                + $" parent '{par.data}' and child '{curr.data}'\n    Solution -- single left rotation");
                            else
                                Console.WriteLine($"Left-left violation of grandparent '{greatGrand.child[dir2].data}',"
                                + $" parent '{par.data}' and child '{curr.data}'\n    Solution -- single right rotation");
                            greatGrand.child[dir2] = RotateSingle(grand, dirToRotate);
                        }
                        else
                        {
                            if(lastDir == 1)
                                Console.WriteLine($"Right-left violation of grandparent '{greatGrand.child[dir2].data}',"
                                + $" parent '{par.data}' and child '{curr.data}'\n    Solution -- double rotation");
                            else
                                Console.WriteLine($"Left-right violation of grandparent '{greatGrand.child[dir2].data}',"
                                + $" parent '{par.data}' and child '{curr.data}'\n    Solution -- double rotation");
                            greatGrand.child[dir2] = RotateDouble(grand, dirToRotate);
                        }
                    }
                    if(curr.data == data) {break;} //<--quit loop
                    lastDir = direction;
                    direction = data > curr.data ? 1 : 0;
                    if(grand != null) {greatGrand = grand;}
                    grand = par;
                    par = curr;
                    curr = curr.child[direction];
                }
                root = temporaryRoot.child[1];
            }
            if(root.color == "r")
            {
                Console.WriteLine($"Root '{root.data}' is red: paint the root black");
                root.color = "b";
            }
        }
        private bool isExists(Node root, int data)
        {
            if(root == null)
                return false;
            if(root.data == data)
                return true;
            bool fromLeft = isExists(root.child[0], data);
            if(fromLeft) return true;
            return isExists(root.child[1], data);
        }
        public void PrintTree()
        {
            if(root == null)
            {
                Console.WriteLine("The tree is empty");
                return;
            }
            if(root.color == "b")
                Console.ForegroundColor = ConsoleColor.Black;
            else
                Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine("Root:{0}", root.data);
            PrintChildren(root, "");
            Console.ResetColor();
        }
        private void PrintChildren(Node root, string whiteSpace)
        {
            if(root.child[0] != null)
            {
                Console.Write(whiteSpace);
                if(root.child[0].color == "b")
                    Console.ForegroundColor = ConsoleColor.Black;
                else
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"`---L:{root.child[0].data}");
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                string newWhiteSpace = whiteSpace + "|   ";
                PrintChildren(root.child[0], newWhiteSpace);
            }
            if(root.child[1] != null)
            {
                Console.Write(whiteSpace );
                if(root.child[1].color == "b")
                    Console.ForegroundColor = ConsoleColor.Black;
                else
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"`---R:{root.child[1].data}");
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                string newWhiteSpace = whiteSpace + "    ";
                PrintChildren(root.child[1], newWhiteSpace);
            }     
        } 
        private Node RotateSingle(Node root, int rotateDir)
        {
            /* For instance: if left-left imbalance ---  perform right rotation --> rotateDir == 1(right);
               New root will be: root.left (left = 0), so we should invert our direction - "anotherDir" */
            int anotherDir =0;
            if(rotateDir == 0) anotherDir = 1;
            Node newR = root.child[anotherDir];
            root.child[anotherDir] = newR.child[rotateDir];
            newR.child[rotateDir] = root;
            root.color = "r";
            newR.color = "b";
            return newR;
        }
        private Node RotateDouble(Node root, int rotateDir)
        {
            int anotherDir =0;
            if(rotateDir == 0) anotherDir = 1;
            root.child[anotherDir] = RotateSingle(root.child[anotherDir], anotherDir); 
            return RotateSingle(root,rotateDir);
        }
        private bool IsRed(Node node)
        {
            bool isNull = node != null;
            if(!isNull)
                return false;
            return node.color == "r";
        }
    }
}