using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task1
{
    class Student : IComparable<Student>
    {
        public string Name { get; set; }

        public short Mark { get; set; }

        public Student(string name, short mark)
        {
            Name = name;
            Mark = mark;
        }

        public int CompareTo(Student other) => Mark == other.Mark ?
            Name.CompareTo(other.Name) : other.Mark.CompareTo(Mark);

        public override string ToString()
        {
            return String.Format(Name + " " + Mark);
        }

    }
    class Rating
    {
        private SortedSet<Student> listStudent;

        public Rating()
        {
            listStudent = new SortedSet<Student>();
        }

        public void Add(Student student)
        {
            listStudent.Add(student);
        }

        public void Print()
        {
            foreach (Student student in listStudent)
                Console.WriteLine($"{student.Name} : {student.Mark}");
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Rating rating = new Rating();
            rating.Add(new Student("Student 1", 80));
            rating.Add(new Student("Student 2", 85));
            rating.Add(new Student("Student 3", 85));
            rating.Add(new Student("Student 4", 75));
            rating.Add(new Student("Student 5", 94));
            rating.Add(new Student("Student 6", 85));
            rating.Print();
            Console.ReadKey();
        }
    }
}
