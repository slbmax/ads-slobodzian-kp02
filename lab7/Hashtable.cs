using System; 

namespace lab7
{
    class Hashtable
    {
        private int _size;    
        private double _loadness;  
        private int _capacity; 
        private Entry[] table;
        public Hashtable()
        {
            this._capacity = 11;
            this._loadness = 0;
            this._size = 0;
            this.table = new Entry[this._capacity];
        }
        public void InsertEntry(Key key, Value value, AdditHashtable addTable, ref int index)
        {
            if(this._loadness > 0.6)
                Rehashing();
            int hash_index = getHash(key);
            for(int i = hash_index; i <= this._capacity; i++)
            {
                if(i == this._capacity) i = 0;
                if(table[i].key.firstName == key.firstName && table[i].key.lastName == key.lastName)
                {
                    CheckLimit(addTable,key,value,i);
                    addTable.RemovePatient(table[i].key, table[i].value);
                    table[i].value.familyDoctor = value.familyDoctor;
                    table[i].value.adress = value.adress;
                    addTable.InsertPatient(key, table[i].value);
                    break;
                }
                if(table[i].key.firstName == "DELETED" || table[i].key.firstName == null)
                {
                    Entry toCheck = findEntry(key);
                    if(toCheck.key.firstName!=null)
                        continue;
                    CheckLimit(addTable,key,value,i);
                    table[i].key = key;
                    table[i].value = value;
                    index++;
                    this._size ++;
                    this._loadness = (double)this._size / this._capacity;
                    addTable.InsertPatient(key, value);
                    break;
                }
            }
        }
        public bool RemoveEntry(Key key, AdditHashtable addTab)
        {
            int hash_index = getHash(key);    
            for(int i = hash_index; i <= this._capacity; i++)
            {
                if(i == this._capacity) i = 0;
                if(table[i].key.firstName == null)
                    return false;
                if(table[i].key.firstName == key.firstName && table[i].key.lastName == key.lastName)
                {
                    addTab.RemovePatient(table[i].key, table[i].value);
                    table[i].key.firstName = "DELETED";
                    table[i].key.lastName = null;
                    table[i].value.patientID =0;
                    table[i].value.adress = null;
                    table[i].value.familyDoctor = null;
                    this._size --;
                    this._loadness = (double)this._size / this._capacity;
                    break;
                }
            }
            return true;
        }
        private long HashCode(Key key)
        {
            long hash = 0;
            string hashable = key.firstName + key.lastName;
            for (int i = 0; i < hashable.Length; i++)
                hash += hashable[i] * (i + 1) * 89;
            return hash;
        }
        private int getHash(Key key)
        {
            return (int)(HashCode(key) % _capacity);
        }
        private void Rehashing()
        {
            Console.WriteLine("The table is loaded on more than 60%\nRehashing...");
            int oldCap = this._capacity;
            this._capacity *= 2;
            Entry[] newTab = new Entry[this._capacity];
            for(int i = 0; i < oldCap; i++)
            {
                if(table[i].key.firstName == null || table[i].key.firstName == "DELETED")
                    continue;
                int hash_index = getHash(table[i].key);
                for(int j = hash_index; j <= this._capacity; j++)
                {
                    if(j == this._capacity) j = 0;
                    if(newTab[j].key.firstName == null)
                    {
                        newTab[j] = table[i];
                        break;
                    }
                }
            }
            this._loadness = (double)this._size / this._capacity;
            table = newTab;
            Console.WriteLine("Succesfully");
        }
        public void Print()
        {
            if(this._size == 0)
            {
                Console.WriteLine("The table is empty yet.");
                return;
            }
            Console.WriteLine("-------------------------------------");
            for(int i = 0; i < this._capacity; i++)
            {
                if(table[i].key.firstName != null && table[i].key.firstName != "DELETED")
                {
                    int id = this.table[i].value.patientID;
                    string fn = this.table[i].key.firstName;
                    string ln = this.table[i].key.lastName;
                    string doc = this.table[i].value.familyDoctor;
                    string adrs = this.table[i].value.adress;
                    Console.WriteLine($"[{id}] {fn} {ln} [doc: {doc}] [adress: {adrs}]");
                }
                /* else
                {
                    Console.WriteLine("This field is null");
                } */
            }
            Console.WriteLine("-------------------------------------");
        }
        public Entry findEntry(Key key)
        {
            Entry nullEntry = new Entry();
            int hash_index = getHash(key);
            for(int i = hash_index; i <= this._capacity; i++)
            {
                if (i == this._capacity) i = 0;
                if(table[i].key.firstName == key.firstName && table[i].key.lastName == key.lastName)
                    return table[i];
                if(table[i].key.firstName == null)
                    break;
            }
            return nullEntry;
        }
        private void CheckLimit(AdditHashtable addTab,Key key, Value value, int i)
        {
            Entry2 doctor = addTab.FindDoctor(value.familyDoctor);
            if(doctor.doctor != null && doctor.patients.Count == 5)
            {
                if(table[i].value.familyDoctor == doctor.doctor)
                    return;
                else
                    throw new Exception("This doctor is currently unavailable: he already have 5 patients.");
            }
        }
    }
}