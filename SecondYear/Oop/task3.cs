using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task3
{
    delegate void Violation(object o, ViolationEvent e);
    class ViolationEvent : EventArgs
    {
        public string info { get; set; }
        public ViolationEvent(string s)
        {
            info = s;
        }
    }
    class Driver
    {
        public string Name { get; set; }
        public int Speed { get; set; }
        public Driver(string n)
        {
            Name = n;
        }
        public event Violation CommitAppearance;
        public void createAppearance(string s)
        {
            if (CommitAppearance != null)
                CommitAppearance(this, new ViolationEvent(s));
        }
        public override string ToString()
        {
            return Name;
        }
        public override int GetHashCode()
        {
            return Speed;
        }
    }
    class Police
    {
        public void ToCatch(object o, ViolationEvent e)
        {
            Console.WriteLine(o + " you're caught for a reason" + e.info);
            if (o.GetHashCode() <= 10)
                Console.WriteLine("fine 100");
            else if (o.GetHashCode() <= 20)
                Console.WriteLine("fine 200");
            else
                Console.WriteLine("fine 500");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Driver driver1 = new Driver("Driver");
            Police police1 = new Police();
            driver1.CommitAppearance += police1.ToCatch;
            driver1.Speed = 15;
            driver1.createAppearance(" fine");
            driver1.Speed = 35;
            driver1.createAppearance(" fine");
            Console.ReadKey();
        }
    }
}
