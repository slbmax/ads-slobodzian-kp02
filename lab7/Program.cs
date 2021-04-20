using System;
using System.Collections.Generic;

namespace lab7
{
    class Program
    {
        static void Main(string[] args)
        {
            AdditHashtable addTable = new AdditHashtable();
            Hashtable table = new Hashtable();
            int id = 35900;
            bool exit = false;
            Console.WriteLine("Control example?(yes/no)");
            string control = Console.ReadLine();
            switch (control)
            {
                case "yes":
                    Control(table, ref id, addTable);
                    break;
                case "no":
                    break;
                default:
                    Console.WriteLine("Incorrect input.");
                    break;
            }
            while(!exit)
            {
                Console.WriteLine(@"Command:
                1. InsertPatient
                2. RemovePatient
                3. FindPatient
                4. PrintTable
                5. FindFamilyDoctorPatients
                6. Exit");
                string command = Console.ReadLine();
                switch (command)
                {
                    case "1":
                        Console.WriteLine("Enter your first and last name, adress and family doctor that you want to choose. " +   
                        "\nThe order of the parameters is required." +
                        "\nExample:\nMax,Slobodzian,Mudrogo,Marinich");
                        string input = Console.ReadLine();
                        string result = ProcessInsertion(table, input,ref id, addTable);
                        Console.WriteLine(result);
                        break;
                    case "2":
                        Console.WriteLine("Enter the patient`s first and last name to delete it from table." + 
                        "\nThe order of the parameters is required." +
                        "\nExample:\nMax,Slobodzian");
                        string toRemove = Console.ReadLine();
                        string resultRemove = ProcessRemoving(table, toRemove, addTable); 
                        Console.WriteLine(resultRemove);
                        break;
                    case "3":
                        Console.WriteLine("Enter the patient`s first and last name to find data about him." + 
                        "\nThe order of the parameters is required." +
                        "\nExample:\nMax,Slobodzian");
                        string toFind = Console.ReadLine();
                        string resultFind = ProcessFind(table, toFind);
                        if(resultFind!="")
                            Console.WriteLine(resultFind);
                        break;
                    case "4":
                        table.Print(); // При перевірці краще використовувати метод addTable.Print(), він набагато зручніший
                                       // Для цього розкоментуйте в файлі AdditHashtable.cs відповідний блок
                        break;
                    case "5":
                        Console.WriteLine("Enter the name of a doctor:");
                        string doc = Console.ReadLine();
                        string resultPatients = ProcessFindFamilyDoctorPatients(addTable, doc);
                        if(resultPatients!="")
                            Console.WriteLine(resultPatients);
                        break;
                    case "6":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid command");
                        break;
                }
            }
        }
        static string ProcessInsertion(Hashtable table, string input,ref int id, AdditHashtable addTable)
        {
            string[] arguments = input.Split(",");
            if(arguments.Length != 4)
                return "Error: incorrect amount of arguments";
            string name = CheckNames(arguments[0]);
            string surname = CheckNames(arguments[1]);
            string ptadress = CheckNames(arguments[2]);
            string doctor = CheckNames(arguments[3]);
            if(name == "ERROR" || surname == "ERROR" || ptadress == "ERROR" || doctor == "ERROR")
                return "Error: there are incorrect characters in the input.";
            Key key = new Key{
                firstName = name,
                lastName = surname
            };
            Value value = new Value{
                patientID = id,
                adress = ptadress,
                familyDoctor = doctor
            };
            try{
                table.InsertEntry(key, value, addTable, ref id);
            }
            catch(Exception ex){
                Console.WriteLine(ex.Message);
                Console.WriteLine("Searching for available doctor...");
                string avDoc = addTable.FindAvailableDoctor();
                if(avDoc == null)
                {
                    Console.WriteLine("All doctors are unavailable. Please, enter the name of a new doctor.");
                    value.familyDoctor = DocName(addTable, key, table);
                }
                else
                {
                    Console.WriteLine($"Available doctor: {avDoc}. Do you want to choose it? (yes/no)");
                    while(true)
                    {
                        string command = Console.ReadLine();
                        if(command == "yes")
                        {
                            value.familyDoctor = avDoc;
                            break;
                        }
                        else if( command == "no")
                        {
                            Console.WriteLine("Please, enter the name of a new doctor.");
                            value.familyDoctor = DocName(addTable, key, table);
                            break;
                        }
                        else
                            Console.WriteLine("Incorrect input. Please, try again");
                    }
                }
                table.InsertEntry(key, value, addTable, ref id);
            }
            return "Patient was inserted/renewed successfully";
        }
        static string ProcessRemoving(Hashtable table, string input, AdditHashtable addTable)
        {
            string[] arguments = input.Split(",");
            if(arguments.Length != 2)
                return "Error: incorrect amount of arguments";
            string name = CheckNames(arguments[0]);
            string surname = CheckNames(arguments[1]);
            if(name == "ERROR" || surname == "ERROR")
                return "Error: there are incorrect characters in the input.";
            Key key = new Key{
                firstName = name,
                lastName = surname
            };
            bool deleted = table.RemoveEntry(key, addTable);
            if(deleted)
                return "Removing successfull";
            else    
                return "Patient wasn`t found. Please, check the input";
        }
        static string ProcessFind(Hashtable table, string input)
        {
            string[] arguments = input.Split(",");
            if(arguments.Length != 2)
                return "Error: incorrect amount of arguments";
            string name = CheckNames(arguments[0]);
            string surname = CheckNames(arguments[1]);
            if(name == "ERROR" || surname == "ERROR")
                return "Error: there are incorrect characters in the input.";
            Key key = new Key{
                firstName = name,
                lastName = surname
            };
            Entry found = table.findEntry(key);
            if(found.key.firstName == null)
                return "Patient wasn`t found. Please, check the input";
            else
            {
                Console.WriteLine("Patient was found:");
                Console.WriteLine("[{0}] {1} {2} [doc.{3}][adress:{4}]", 
                found.value.patientID, found.key.firstName, found.key.lastName,
                found.value.familyDoctor, found.value.adress);
            }
            return"";
        }
        static string ProcessFindFamilyDoctorPatients(AdditHashtable table, string doc)
        {
            string doctor = CheckNames(doc);
            if(doctor == "ERROR")
                return "Error: there are incorrect characters in the input.";
            Entry2 patients = table.FindDoctor(doctor);
            if(patients.doctor == null)
                return "Doctor wasn`t found. Please, check the input";
            if(patients.patients.Count == 0)
                return $"Doc.{doctor} do not have any patients yet";
            else{
                Console.WriteLine($"Doc.{doctor}:");
                Patient[] array = new Patient[patients.patients.Count];
                patients.patients.CopyTo(array);
                for(int j = 0; j < array.Length; j++)
                {
                    string fn = array[j].firstName;
                    string ln = array[j].lastName;
                    string adress = array[j].adress;
                    int id = array[j].patientID;
                    Console.WriteLine($"Patient№{j+1}: [{id}] {fn} {ln} [{adress}]");
                }
            }
            return "";
        }
        static void Control(Hashtable table, ref int id, AdditHashtable addTable)
        {
            string[] names = new string[]{
                "Max", "Lera", "Andriy", "Maria", "Dmytro", "Olga", "Jack", "Sem", "Din", "Tom", "Dan","Din","Din"
            };
            string[] surnames = new string[]{
                "Slobodzian", "Hrushka", "Pich", "Repin", "Slack", "Trod", "Reiz", "Locki", "Krot", "Top", "Lofi","Slack", "Repin"
            };
            string[] adress = new string[]{
                "Mudrogo", "Kotlovanska", "Richna","Odesskaya", "Mudrogo", "Kotlovanska", "Richna","Odesskaya",
                 "Mudrogo", "Kotlovanska", "Mudrogo","Mudrogo","Mudrogo"
            };
            string[] doctors = new string[]{
                "Marinina", "Myronchuk", "Serpeninov"
            };
            Key key = new Key();
            Value value = new Value();
            for(int i = 0; i < 13; i++)
            {
                key.firstName = names[i];
                key.lastName = surnames[i];
                value.patientID = id;
                value.adress = adress[i];
                if(i < 5)
                    value.familyDoctor = doctors[0];
                else if(i < 8)
                    value.familyDoctor = doctors[1];
                else    
                    value.familyDoctor = doctors[2];
                table.InsertEntry(key, value, addTable, ref id);
            }
            table.Print();
            System.Threading.Thread.Sleep(2500);
            Console.WriteLine("Command:Insert\nLolo,Selec,Mudrogo,Makson");
            table.InsertEntry(new Key{firstName="Lolo", lastName="Selec"},
                 new Value{patientID=id,familyDoctor="Makson",adress="Mudrogo"},addTable, ref id);
            Console.WriteLine("Patient was inserted/renewed succesfully");
            System.Threading.Thread.Sleep(2500);
            Console.WriteLine("Command:Insert\nIvan,Selec,Mudrogo,Marinina");
            Entry2 doc = addTable.FindDoctor("Marinina");
            try{
                table.InsertEntry(new Key{firstName="Ivan", lastName="Selec"},
                 new Value{patientID=id,familyDoctor="Marinina",adress="Mudrogo"},addTable, ref id);
                Console.WriteLine("Patient was inserted succesfully");
            }
            catch (Exception ex){
                Console.WriteLine(ex.Message);
                Console.WriteLine("Searching for available doctor...");
                System.Threading.Thread.Sleep(1000);
                string avDoc = addTable.FindAvailableDoctor();
                Console.WriteLine($"Available doctor: {avDoc}. Do you want to choose it? (yes/no)");
                System.Threading.Thread.Sleep(2000);
                Console.WriteLine("yes");
                table.InsertEntry(new Key{firstName="Ivan", lastName="Selec"},
                 new Value{patientID=id,familyDoctor=avDoc,adress="Mudrogo"},addTable, ref id);
                Console.WriteLine("Patient was inserted succesfully");
            }
            System.Threading.Thread.Sleep(2800);
            Console.WriteLine("\nCommand:Find\nLolo,Selec");
            Console.WriteLine(ProcessFind(table,"Lolo,Selec"));
            System.Threading.Thread.Sleep(2800);
            Console.WriteLine("Command:Remove\nLolo,Selec");
            Console.WriteLine(ProcessRemoving(table, "Lolo,Selec", addTable));
            System.Threading.Thread.Sleep(2800);
            Console.WriteLine("\nCommand:Find\nLolo,Selec");
            Console.WriteLine(ProcessFind(table,"Lolo,Selec"));
            System.Threading.Thread.Sleep(2800);
            Console.WriteLine("\nCommand:Remove\nLolo,Selec");
            Console.WriteLine(ProcessRemoving(table, "Lolo,Selec", addTable));
            System.Threading.Thread.Sleep(2800);
            Console.WriteLine("\nCommand:Print");
            table.Print();
            System.Threading.Thread.Sleep(2800);
            Console.WriteLine("\nCommand:FindFamilyDoctorPatients\nMarinina");
            Console.WriteLine(ProcessFindFamilyDoctorPatients(addTable, "Marinina"));
        }
        static string CheckNames(string input)
        {
            string correct = "";
            for(int i = 0; i < input.Length; i++)
            {
                if(!char.IsLetter(input[i]))
                    return "ERROR";
                if(i == 0)
                    correct += char.ToUpper(input[i]);
                else
                    correct += char.ToLower(input[i]);
            }
            return correct;
        }
        static string DocName(AdditHashtable addTable, Key key, Hashtable table)
        {
            while(true)
            {
                string inputDoc = Console.ReadLine();
                string newDoc = CheckNames(inputDoc);
                if(newDoc == "ERROR")
                {
                    Console.WriteLine("Incorrect characters. Please, try again");
                    continue;
                }
                Entry2 doc = addTable.FindDoctor(newDoc);
                if(doc.doctor != null && doc.patients.Count == 5)
                {
                    Entry entity = table.findEntry(key);
                    if(entity.value.familyDoctor == newDoc)
                        return newDoc;
                    Console.WriteLine("This doctor is currently unavailable. Please, choose another doc.");
                    continue;
                }
                else
                    return newDoc;
            }
        }
    }
    struct Key
    {
        public string firstName;
        public string lastName;
    }
    struct Value
    {
        public int patientID;
        public string familyDoctor;
        public string adress;
    }
    struct Entry
    {
        public Key key;
        public Value value;
    }
    struct Patient
    {
        public int patientID;
        public string firstName;
        public string lastName;
        public string adress;
    }
    struct Entry2
    {
        public List<Patient> patients;
        public string doctor;
    }  
}