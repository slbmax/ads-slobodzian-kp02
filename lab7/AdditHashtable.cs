using System;
using System.Collections.Generic;


namespace lab7
{
    class AdditHashtable
    {
        private int _size;
        private double _loadness;
        private int _capacity;
        private Entry2[] table;
        public AdditHashtable()
        {
            this._capacity = 11;
            this._loadness = 0;
            this._size = 0;
            this.table = new Entry2[this._capacity];
        }
        public void InsertPatient(Key key, Value value)
        {
            if(this._loadness > 0.6)
                Rehashing();
            int hash_index = getHash(value.familyDoctor);
            Entry2 doctor = FindDoctor(value.familyDoctor);
            bool isDocHere = doctor.doctor!=null;
            Patient patient = new Patient
            {
                patientID = value.patientID,
                firstName = key.firstName,
                lastName = key.lastName,
                adress = value.adress
            };
            for(int i = hash_index; i <= this._capacity; i++)
            {
                if(i == this._capacity) i = 0;
                if(isDocHere)
                {
                    if(table[i].doctor == value.familyDoctor)
                    {
                        table[i].patients.Add(patient);
                        break;
                    }
                }
                else
                {
                    if(table[i].doctor == null)
                    {
                        table[i].doctor = value.familyDoctor;
                        table[i].patients = new List<Patient>();
                        table[i].patients.Add(patient);
                        this._size ++;
                        this._loadness = (double)this._size / this._capacity;
                        break;
                    }
                } 
            }
        }
        public void RemovePatient(Key key, Value value)
        {
            Patient patient = new Patient
            {
                patientID = value.patientID,
                firstName = key.firstName,
                lastName = key.lastName,
                adress = value.adress
            };
            int hash_index = getHash(value.familyDoctor);
            for(int i = hash_index; i <= this._capacity; i++)
            {
                if(i == this._capacity) i = 0;
                if(table[i].doctor == value.familyDoctor)
                {
                    table[i].patients.Remove(patient);
                    /* if(hashtable[i].patient.Count == 0)
                    {
                        hashtable[i].doctor = null;
                        this._size --;
                        this._loadness = (double)this._size / this._capacity;
                    } */
                    //Доктор залишається без пацієнтів, але це корисно коли шукається вільний доктор
                    //При виведенні пацієнтів цього доктора вкажеться, що він вільний/не має пацієнтів
                    break;
                }
            }
        }
        private long HashCode(string key)
        {
            long hash = 0;
            for (int i = 0; i < key.Length; i++)
                hash += key[i] * (i + 1) * 89;
            return hash;
        }
        private int getHash(string key)
        {
            return (int)(HashCode(key) % this._capacity);
        }
        public Entry2 FindDoctor(string doctor)
        {
            Entry2 nullEntry = new Entry2();
            int hash_index = getHash(doctor);
            for(int i = hash_index; i <= this._capacity; i++)
            {
                if (i == this._capacity) i = 0;
                if(table[i].doctor == doctor)
                    return table[i];
                if(table[i].doctor == null)
                    break;
            }
            return nullEntry;
        }
        public string FindAvailableDoctor()
        {
            for(int i = 0; i < this._capacity; i++)
            {
                if(table[i].doctor !=null && table[i].patients.Count < 5)
                    return table[i].doctor;
            }
            return null;
        }
        private void Rehashing()
        {
            int oldCap = this._capacity;
            this._capacity *= 2;
            Entry2[] newTab = new Entry2[this._capacity];
            for(int i = 0; i < oldCap; i++)
            {
                if(table[i].doctor == null)
                    continue;
                int hash_index = getHash(table[i].doctor);
                for(int j = hash_index; j <= this._capacity; j++)
                {
                    if(j == this._capacity) j = 0;
                    if(newTab[j].doctor == null)
                    {
                        newTab[j] = table[i];
                        break;
                    }
                }
            }
            this._loadness = (double)this._size / this._capacity;
            table = newTab;
        }
        /* public void Print()          //Для перевірки ця таблиця набагато зручніша
        {
            Console.WriteLine("--------------------");
            for(int i = 0; i < this._capacity; i++)
            {
                if(table[i].doctor != null && table[i].patients.Count != 0)
                {
                    Console.WriteLine($"Doc.{table[i].doctor}");
                    Patient[] array = new Patient[table[i].patients.Count];
                    table[i].patients.CopyTo(array);
                    for(int j = 0; j < array.Length; j++)
                    {
                        string fn = array[j].firstName;
                        string ln = array[j].lastName;
                        string adress = array[j].adress;
                        int id = array[j].patientID;
                        Console.WriteLine($"Patient№{j+1}: [{id}] {fn} {ln} [{adress}]");
                    }
                }
                else if(table[i].doctor != null && table[i].patients.Count == 0)
                {
                    Console.WriteLine($"Doc.{table[i].doctor}");
                    Console.WriteLine("No patients yet.");
                }
            }
            Console.WriteLine("--------------------");
        } */
    }
}