using System;

namespace task2
{
    abstract class abstractAboutMe
    {
        public string Name { get; set; }
        public abstract void AboutMe();
    }
    interface IFly
    {
        void Fly();
    }
    interface IMotor
    {
        void Motor();
    }
    interface IRun
    {
        void Run();
    }
    interface ISwim
    {
        void Swim();
    }

    class Boat : abstractAboutMe, ISwim, IMotor
    {
        public Boat()
        {
            Name = "Boat";
        }
        public override void AboutMe()
        {
            Console.WriteLine(Name);
            Motor();
            Swim();
        }
        public void Motor()
        {
            Console.WriteLine("Motor");
        }
        public void Swim()
        {
            Console.WriteLine("Swim");
        }
    }

    class Chicken : abstractAboutMe, IRun
    {
        public Chicken()
        {
            Name = "Chicken";
        }
        public override void AboutMe()
        {
            Console.WriteLine(Name);
            Run();
        }
        public void Run()
        {
            Console.WriteLine("Run");
        }
    }

    class Duck : abstractAboutMe, IFly, ISwim
    {
        public Duck()
        {
            Name = "Duck";
        }
        public override void AboutMe()
        {
            Console.WriteLine(Name);
            Fly();
            Swim();
        }
        public void Fly()
        {
            Console.WriteLine("Fly");
        }
        public void Swim()
        {
            Console.WriteLine("Swim");
        }
    }

    class Eagle : abstractAboutMe, IFly
    {
        public Eagle()
        {
            Name = "Eagle";
        }
        public override void AboutMe()
        {
            Console.WriteLine(Name);
            Fly();
        }
        public void Fly()
        {
            Console.WriteLine("Fly");
        }
    }

    class Plane : abstractAboutMe, IFly, IMotor
    {
        public Plane()
        {
            Name = "Plane";
        }
        public override void AboutMe()
        {
            Console.WriteLine(Name);
            Motor();
            Fly();
        }
        public void Fly()
        {
            Console.WriteLine("Fly");
        }
        public void Motor()
        {
            Console.WriteLine("Motor");
        }
    }

    class Rabbit : abstractAboutMe, IRun
    {
        public Rabbit()
        {
            Name = "Rabbit";
        }
        public override void AboutMe()
        {
            Console.WriteLine(Name);
            Run();
        }
        public void Run()
        {
            Console.WriteLine("Run");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Boat boat = new Boat();
            boat.AboutMe();
            Console.WriteLine();

            Chicken chicken = new Chicken();
            chicken.AboutMe();
            Console.WriteLine();


            Duck duck = new Duck();
            duck.AboutMe();
            Console.WriteLine();


            Eagle eagle = new Eagle();
            eagle.AboutMe();
            Console.WriteLine();


            Plane plane = new Plane();
            plane.AboutMe();
            Console.WriteLine();


            Rabbit rabbit = new Rabbit();
            rabbit.AboutMe();
            Console.WriteLine();

            Console.ReadKey();

            IFly[] fliesFly = { duck, eagle , plane };
            ISwim[] fliesSwim = { duck, boat };

            for (int i = 0; i < fliesFly.Length; i++)
            {
                abstractAboutMe upTemp = (abstractAboutMe)fliesFly[i];
                upTemp.AboutMe();
            }

            for (int i = 0; i < fliesSwim.Length; i++)
            {
                abstractAboutMe upTemp = (abstractAboutMe)fliesSwim[i];
                upTemp.AboutMe();
            }
           
            
            Console.ReadKey();

        }
    }
}
