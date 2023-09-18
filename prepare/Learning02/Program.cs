using System;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;

class Program
{
    static void Main(string[] args)
    {
        //Console.WriteLine("Hello Learning02 World!");

        Job job1 = new Job();
        job1._jobTitle = "Software Engineer";
        job1._company = "Microsoft";
        job1._startYear = "2019";
        job1._endYear = "2022";

        //Console.WriteLine($"Company: {job1._company}");
        //job1.Display();

        Job job2 = new Job();
        job2._jobTitle = "Manager";
        job2._company = "Apple";
        job2._startYear = "2022";
        job2._endYear = "2023";

        //Console.WriteLine($"Company: {job2._company}");
        //job2.Display();

        Resume resume = new Resume();
        resume._name = "Allison Rose";
        resume._jobs.Add(job1);
        resume._jobs.Add(job2);

        //Console.WriteLine($"{resume._jobs[0]._jobTitle}");

        resume.Display();
    }
}