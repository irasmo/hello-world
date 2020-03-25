using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics.CodeAnalysis;

namespace HW1pr
{

    public interface IPrintable
    {

        void Print();
    }

    public interface ICloneable
    {
        object Clone();
    }
   
    public class Computer : IPrintable, ICloneable
    {

        private string serial_numb;
        private string ip;
        private string producer;


        public Computer(string _serial_numb, string _ip, string _producer)
        {
            serial_numb = _serial_numb;
            ip = _ip;
            producer = _producer;

        }
        public Computer() : this("", "", "")
        {

        }

        public string Serial_numb
        {
            get { return serial_numb; }
            set { serial_numb = value; }

        }
        public string IP
        {
            get { return ip; }
            set { ip = value; }

        }
        public string Producer
        {
            get { return producer; }
            set { producer = value; }

        }
        public override string ToString()
        {
            return $"{serial_numb} {ip} {producer}";

        }

        virtual public void Print()
        {
            Console.WriteLine($"{serial_numb} {ip} {producer}");
        }
        virtual public object Clone()
        {
            return new Computer { Serial_numb = Serial_numb, IP = IP,  Producer=Producer};
        }
    }
    public class Personal : Computer, IPrintable, IComparable<Personal>, ICloneable
    {

        private string operation_syst;
        public string Operation_syst
        {
            get { return operation_syst; }
            set { operation_syst = value; }

        }
        public Personal() : base()
        {

        }
        public Personal(string _serial_numb, string _ip, string _producer, string _operation_syst) : base(_serial_numb, _ip, _producer)
        {
            operation_syst = _operation_syst;


        }
        public override string ToString()
        {
            return base.ToString() + $" {operation_syst}";

        }
        public override void Print()
        {
            Console.WriteLine($"{Serial_numb} {IP} {Producer} {operation_syst}");
        }

        public int CompareTo(Personal other)
        {
            int compare = Producer.CompareTo(other.Producer);
            if (compare == 0)
                return Serial_numb.CompareTo(other.Serial_numb);
            else
                return compare;
        }
        public override object Clone()
        {
            return new Personal { Serial_numb = Serial_numb, IP = IP, Producer = Producer, Operation_syst=Operation_syst };
        }
    }      
    public class Server : Computer, ICloneable
    {

        private int comp_num;
        public int Comp_num
        {
            get { return comp_num; }
            set { comp_num = value; }

        }

        public Server() : base()
        {

        }
        public Server(string _serial_numb, string _ip, string _producer, int _comp_num) : base(_serial_numb, _ip, _producer)
        {
            comp_num = _comp_num;
        }
        public override string ToString()
        {
            return base.ToString() + $" {comp_num}";

        }
        //public override void Print()
        //{
        //    Console.WriteLine($"{Serial_numb} {IP} {Producer} {comp_num}");
        //}
        public override object Clone()
        {
            return new  Server { Serial_numb = Serial_numb, IP = IP, Producer = Producer, Comp_num = Comp_num };
        }

    }
    class Serial_num_Comparer : IComparer<Server>
    {
        public int Compare(Server s1, Server s2)
        {
            return s1.Serial_numb.CompareTo(s2.Serial_numb);
        }
    }
    class Comp_num_Comparer : IComparer<Server>
    {
        public int Compare(Server s1, Server s2)
        {
            return s1.Comp_num.CompareTo(s2.Comp_num);
        }
    }
    class Ip_Comparer : IComparer<Server>
    {
        public int Compare(Server s1, Server s2)
        {
            return s1.IP.CompareTo(s2.IP);
        }
    }
    class Producer_Comparer : IComparer<Server>
    {
        public int Compare(Server s1, Server s2)
        {
            return s1.Producer.CompareTo(s2.Producer);
        }
    }

    class Comps : IEnumerable
    {

        public Computer[] mass = new Computer[4];        
        public IEnumerator GetEnumerator()
        {
            return mass.GetEnumerator();
        }
        public void add_new(string line)
        {
            string[] fields = line.Split();
            switch (fields[0])
            {
                case "1":
                    Computer p = new Computer(fields[2], fields[3], fields[4]);
                    mass[Int32.Parse(fields[1])] = p;
                    break;
                case "2":
                    Personal a = new Personal(fields[2], fields[3], fields[4], fields[5]);
                    mass[Int32.Parse(fields[1])] = a;
                    break;
                case "3":
                    Server b = new Server(fields[2], fields[3], fields[4], int.Parse(fields[5]));
                    mass[Int32.Parse(fields[1])] = b;
                    break;
                default:
                    throw new ArgumentException("not a computer");
            }

        }
    }
  
    class Program
    {
        
        static void Main(string[] args)
        {
            string path = @"D:\C#\HW1pr\TextFile1.txt";
            if (File.Exists(path) == false)
            {
                Console.WriteLine("file doesn`t exist");
            }
            else
            {
                List<Computer> computers = new List<Computer>();
                
                string[] lis = File.ReadAllLines(path);
                
                foreach(string line in lis)
                {
                    string[] fields = line.Split();
                    switch (fields[0])
                    {
                        case "1":
                            Computer p = new Computer(fields[1], fields[2], fields[3]);
                            computers.Add(p);
                            break;
                        case "2":
                            Personal a = new Personal(fields[1], fields[2], fields[3], fields[4]);
                            computers.Add(a);
                            break;
                        case "3":
                            Server b = new Server(fields[1], fields[2], fields[3], int.Parse(fields[4]));
                            computers.Add(b);
                            break;
                    }
                }
                Console.WriteLine("from file: ");
                foreach (Computer comp in computers)
                {
                    Console.WriteLine(comp);
                }
                var mySortedList = computers.OrderBy(x => x.Serial_numb);
                Console.WriteLine("sorted list: ");
                foreach (var comp in mySortedList)
                {
                    Console.WriteLine(comp);
                }
                Console.WriteLine("IPrintable: ");
                IPrintable x = new Server("P678RTY", "152.789.4.6", "USA", 11);
                x.Print();
                Personal[] personals = new Personal[3];
                personals[0] =new Personal("T121GHJ","121.232.4.2","USA"," MacOs");               
                personals[1] = new Personal("K221YTJ", "241.212.4.3", "China", "Windows7");
                personals[2] = new Personal("A131GRW", "324.652.2.2", "Netherlands", " Linux");
                Array.Sort(personals);
                Console.WriteLine("sorted array of personal computers by producer using IComparable: ");
                foreach (Personal comp in personals)
                {
                    Console.WriteLine(comp);
                }

                Server[] servers = new Server[3];
                servers[0] = new Server("K321OPJ", "421.132.4.2", "Brutain", 11);
                servers[1] = new Server("R451YTP", "291.342.5.3", "USA", 5);
                servers[2] = new Server("B136WRS", "904.651.1.0", "Netherlands", 9);
                Console.WriteLine("Comparer by serial number: ");
                Array.Sort(servers, new Serial_num_Comparer());
                foreach (Computer comp in servers)
                {
                    Console.WriteLine(comp);
                }
                Console.WriteLine("Comparer by ip: ");
                Array.Sort(servers, new Ip_Comparer());
                foreach (Computer comp in servers)
                {
                    Console.WriteLine(comp);
                }
                Console.WriteLine("Comparer by producer: ");
                Array.Sort(servers, new Producer_Comparer());
                foreach (Computer comp in servers)
                {
                    Console.WriteLine(comp);
                }
                Console.WriteLine("Comparer by computer number: ");
                Array.Sort(servers, new Comp_num_Comparer());
                foreach (Computer comp in servers)
                {
                    Console.WriteLine(comp);
                }                  
                Computer c1 = new Computer { Serial_numb = "B136WRS", IP = "904.651.1.0", Producer = "Netherlands "};
                Computer c2 = (Computer)c1.Clone();
                c2.IP= "789.432.2.7";
                Personal p1 = new Personal {Serial_numb = "C133THS",IP = "304.781.1.2", Producer = "France", Operation_syst ="Linux" };
                Personal p2 = (Personal)p1.Clone();
                p2.Producer = "USA";
                Server s1 = new Server { Serial_numb = "G567HFS", IP = "564.897.3.5", Producer = "Netherlands", Comp_num = 5 };
                Server s2 = (Server)s1.Clone();
                s2.Comp_num = 3;
                Console.WriteLine("IClonable: ");
                Console.WriteLine(c2);
                Console.WriteLine(p2);
                Console.WriteLine(s2);
                Comps mass = new Comps();
                
                mass.add_new("2 0 E456FGH 123.234.6.5 USA Linux");
                mass.add_new("2 1 R456YUI 256.789.4.6 China Windows10");
                mass.add_new("3 2 Y678HJK 789.543.7.2 China 7");
                mass.add_new("3 3 I789PIY 654.345.2.1 Netherlands 3");
                Console.WriteLine("IEnumerable:");
                IEnumerator ie = mass.GetEnumerator(); 
                while (ie.MoveNext())   
                {
                    Computer item = (Computer)ie.Current;     
                    Console.WriteLine(item);
                }
                ie.Reset();


            }

        }
    }
}

