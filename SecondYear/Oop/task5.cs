using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task5
{
    static class R
    {
        static Random random = new Random();
        public static int rand(int a, int b)
        {
            return random.Next(a, b);
        }
    }

    class Player
    {
        public string Surname { get; set; }
        public int Age { get; set; }
        public int lvlmastery { get; set; }
        public Player(string s, int age)
        {
            Surname = s;
            Age = age;

            lvlmastery = R.rand(0, 100);
        }
    }
    class Team
    {
        public string Name { get; set; }
        public List<Player> players { get; }
        public float lvlmasterycomand { get; set; }
        public Trainer trainer { get; set; }
        public Team(string s, Trainer t)
        {
            Name = s;
            trainer = t;
            players = new List<Player>();
        }
        public void AddPlayer(Player player)
        {
            players.Add(player);
            for (int i = 0; i < players.Count; i++)
            {
                lvlmasterycomand += players[i].lvlmastery;
            }
            lvlmasterycomand *= trainer.luck;
        }
    }

    class MyAction : EventArgs
    {
        public string info { get; set; }
        public MyAction(string s)
        {
            info = s;
        }
    }
    delegate void Violation(object o, MyAction e);
    delegate void Goal(object o, MyAction e);

    class Game
    {
        public Team first { get; }
        public Team second { get; }
        public Referee Referee { get; }

        public event Violation MyActionViolation;
        public event Goal MyActionGoal;

        public void createViolation(string s)
        {
            if (MyActionViolation != null)
                MyActionViolation(this, new MyAction(s));
        }

        public void createGoal(string s)
        {
            if (MyActionGoal != null)
                MyActionGoal(this, new MyAction(s));
        }

        public Game(Team f, Team s, Referee r)
        {
            first = f;
            second = s;
            Referee = r;
        }
        public void Result()
        {

            double m1 = first.lvlmasterycomand / first.players.Count / 100f;
            double m2 = second.lvlmasterycomand / second.players.Count / 100f;
            switch (Referee.preferences)
            {
                case 1:
                    {
                        Console.WriteLine("real data");
                        Console.WriteLine($"skill {first.Name} = {first.lvlmasterycomand} , {second.Name} = {second.lvlmasterycomand}");
                        first.lvlmasterycomand += 50;

                        break;
                    }
                case 2:
                    {

                        Console.WriteLine("real data");
                        Console.WriteLine($"skill {first.Name} = {first.lvlmasterycomand} , {second.Name} = {second.lvlmasterycomand}");
                        second.lvlmasterycomand += 50;
                        break;

                    }
                default:
                    break;
            }
            m1 = Math.Round(m1, 1);
            m2 = Math.Round(m2, 1);
            if (m1 == m2)
            {
                Console.WriteLine($"no win");
            }
            else
            if (m1 > m2)
            {
                Console.WriteLine($"win {first.Name}");
            }
            else
            {
                Console.WriteLine($"win {second.Name}");
            }

            Console.WriteLine($"skill {first.Name} = {first.lvlmasterycomand} , {second.Name} = {second.lvlmasterycomand}");
        }

        public void Start()
        {
            try
            {
                if (first.lvlmasterycomand * 20 > 4000)
                {
                    throw new MyException(1);
                }
                if (second.lvlmasterycomand / R.rand(1, 2) < 200)
                {
                    throw new MyException(2);
                }
            }
            catch (MyException e )
            {
                Console.WriteLine(e.Message);
            }
            Result();
        }
    }
    class Trainer
    {
        public string Surname { get; set; }
        public float luck { get; set; }
        public Trainer(string s)
        {
            Surname = s;
            luck = R.rand(5, 15) / 10f;
        }
    }
    class Referee
    {
        public string Surname { get; set; }
        public int preferences { get; set; }
        public Referee(string s)
        {
            Surname = s;
            preferences = R.rand(0, 3);
        }
        public void ToCatch(object o, MyAction e)
        {

            Console.WriteLine("Event : " + e.info);
        }
    }
    class MyException : Exception
    {
        public int ErrorCode { get; }
        public MyException(int val)
        {
            ErrorCode = val;
        }
        public override string Message
        {
            get
            {
                switch (ErrorCode)
                {
                    case 1:
                        return "Turned off the lights";
                    case 2:
                        return "a tree grew";
                    case 3:
                        return "a naked man ran onto the field";
                    default:
                        return base.Message;
                }
            }
        }
    }
    class Viewers
    {
        public int Count { get; set; }
        public Viewers(int n)
        {
            Count = n;
        }
        public void ToCatch(object o, MyAction e)
        {

            if(Count > 50)
            {
                Console.WriteLine("Loud screams");
            }
            else
            {
                Console.WriteLine("FUUUUUUUUUUUUUUUUUUUUUUUUUUUUUU");
            }
            Console.WriteLine("Event : " + e.info);

        }
    }

    static class Program
    {
        public static void ListPlayers(this Team command)
        {
            var result = from player in command.players
                         orderby player.Surname ascending
                         select player;

            foreach (Player player in result)
                Console.WriteLine($"{player.Surname} : {player.Age}");
        }

        public static void OldPlayers(this Team command)
        {
            var result = from player in command.players
                         where player.Age > 30
                         select player;

            foreach (Player player in result)
                Console.WriteLine($"{player.Surname} : {player.Age}");
        }


        static void Main(string[] args)
        {
            Player p1 = new Player("Player 1", 20);
            Player p2 = new Player("Player 2", 40);
            Player p3 = new Player("Player 3", 25);
            Player p4 = new Player("Player 4", 33);

            Trainer trainer1 = new Trainer("bad boy");
            Trainer trainer2 = new Trainer("sad boy");

            Team com1 = new Team("baby dolls", trainer1);
            Team com2 = new Team("cutes", trainer2);

            Referee referee = new Referee("Referee");

            com1.AddPlayer(p1);
            com1.AddPlayer(p2);

            com2.AddPlayer(p3);
            com2.AddPlayer(p4);

            Viewers viewers = new Viewers(50);
            Game game = new Game(com1, com2, referee);

            game.MyActionGoal += referee.ToCatch;
            game.MyActionViolation += referee.ToCatch;
            game.MyActionGoal += viewers.ToCatch;

            game.createGoal("GOAL");
            game.createViolation("yellow card");

            game.Start();

            //Console.WriteLine();
            //Console.WriteLine();
            //Console.WriteLine();
            //Console.WriteLine();
            //com1.ListPlayers();
            //Console.WriteLine();
            //com2.ListPlayers();
            //Console.WriteLine();
            //com1.OldPlayers();
            //Console.WriteLine();
            //com2.OldPlayers();


            Console.ReadKey();


        }
    }
}
