using System;
using System.Collections.Generic;
using System.Linq;

namespace task4
{
    class Worker : IComparable<Worker>
    {
        public string Surname { get; set; }
        public int Age { get; set; }
        public List<Project> Projects { get; }
        public Worker(string name, int age)
        {
            Projects = new List<Project>();
            Surname = name;
            Age = age;
        }
        public void AddProject(Project project)
        {
            Projects.Add(new Project(project.Name, project.DateStart));
        }
        public override int GetHashCode()
        {
            return Age;
        }
        public override string ToString()
        {
            return Surname + " " + Age.ToString();
        }
        public int CompareTo(Worker other)
        {
            return GetHashCode();
        }
    }

    class Project
    {
        public string Name { get; set; }
        public DateTime DateStart { get; set; }
        public List<Worker> Workers { get; }
        public Project(string name, DateTime date)
        {
            Workers = new List<Worker>();
            Name = name;
            DateStart = date;
        }
        public void AddWoker(Worker worker)
        {
            worker.AddProject(this);
            Workers.Add(new Worker(worker.Surname, worker.Age));
        }
        public override string ToString()
        {
            return Name + " " + DateStart.ToString();
        }
    }

    class Company
    {
        public string Name { get; set; }
        public List<Worker> workersAll { get; }
        public List<Project> projectsAll { get; }
        public Company()
        {
            workersAll = new List<Worker>();
            projectsAll = new List<Project>();
        }
        public void Addproj(Project proj)
        {
            projectsAll.Add(proj);
        }
        public void AddWorker(Worker worker)
        {
            workersAll.Add(worker);
        }
    }

    static class Program
    {
        public static void WorkersOnTwoProjects(this Company company)
        {
            var result = from worker in company.workersAll
                         orderby worker.Surname descending
                         select worker;
            foreach (Worker worker in result)
                Console.WriteLine($"{worker.Surname} : {worker.Age}");
        }

        public static void ListProjects(this Company company)
        {
            DateTime dateTime = new DateTime(2019, 4, 12);
            var result = from project in company.projectsAll
                         where project.DateStart < dateTime
                         orderby project.DateStart ascending
                         select project;
            foreach (Project project in result)
                Console.WriteLine($"{project.DateStart} : {project.Name}");
        }

        public static void OldWorkerNewProject(this Company company)
        {
            DateTime dateTime = new DateTime(2019, 12, 12);
            var result = from project in company.projectsAll
                         where dateTime.Subtract(project.DateStart).Days > 365
                         && project.Workers.Exists(x=> x.Age < 30)
                         select project;
            foreach (Project project in result)
                Console.WriteLine($"{project.DateStart} : {project.Name}");
        }

        public static void SurnameOldWorkerOneProject(this Company company)
        {
            var result = from worker in company.workersAll
                         where worker.Projects.Exists(x=>x.DateStart.Year == 2019)
                         && worker.Projects.Count == 1 
                         select worker;
            Worker Max = result.Max();
            Console.WriteLine($"{Max.Surname} : {Max.Age}");
        }

        static void Main(string[] args)
        {

            Company comp = new Company();

            Worker worker1 = new Worker("Worker 1", 20);
            Worker worker2 = new Worker("Worker 2", 40);
            Worker worker3 = new Worker("Worker 3", 25);
            Worker worker4 = new Worker("Worker 4", 23);
            Worker worker5 = new Worker("Worker 5", 29);
            Worker worker6 = new Worker("Worker 5", 33);
            Worker worker7 = new Worker("Worker 6", 18);


            Project proj1 = new Project("Telegram", new DateTime(2009, 10, 5));
            Project proj2 = new Project("Mobile game", new DateTime(2019, 5, 5));
            Project proj3 = new Project("Sales website", new DateTime(2019, 5, 15));

            proj1.AddWoker(worker1);
            proj2.AddWoker(worker1);
            proj3.AddWoker(worker1);


            proj2.AddWoker(worker2);
            proj2.AddWoker(worker3);


            proj3.AddWoker(worker3);



            comp.Addproj(proj2);
            comp.Addproj(proj1);
            comp.Addproj(proj3);

            comp.AddWorker(worker1);
            comp.AddWorker(worker2);
            comp.AddWorker(worker3);

            //comp.WorkersOnTwoProjects();
            //comp.ListProjects();
            //comp.OldWorkerNewProject();
            //comp.SurnameOldWorkerOneProject();
            Console.ReadKey();
        }
    }
}
