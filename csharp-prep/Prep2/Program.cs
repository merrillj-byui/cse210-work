using System;
using System.Runtime.CompilerServices;

class Program
{
    static void Main(string[] args)
    {
        //Console.WriteLine("Hello Prep2 World!");

        // Ask the user for their grade percentage
        Console.Write("What is the grade percentage? ");
        string response = Console.ReadLine();
        int grade = int.Parse(response);
        string letter;

        // Determine the letter grade
        if (grade >= 90)
        {
            //Console.WriteLine($"Your grade is A");
            letter = "A";
        }
        else if (grade >= 80)
        {
            //Console.WriteLine($"Your grade is B");
            letter = "B";
        }
        else if (grade >= 70)
        {
            //Console.WriteLine($"Your grade is C");
            letter = "C";
        }
        else if (grade >= 60)
        {
            //Console.WriteLine($"Your grade is D");
            letter = "D";
        }
        else
        {
            //Console.WriteLine($"Your grade is F");
            letter = "F";
        }

        // Determine the sign
        string sign = "";
        if (grade < 90 && grade >= 60 && grade % 10 >= 7)
        {
            sign = "+";
        }
        else if (grade >= 60 && grade % 10 < 3)
        {
            sign = "-";
        }

        // Print the grade letter
        Console.WriteLine($"Your grade is {letter}{sign}");

        
        // Show whether they passed the class
        if (grade >= 70)
        {
            Console.WriteLine("Congratulations on passing the class!");
        }
        else
        {
            Console.WriteLine("You didn't pass this time, but don't give up. You'll pass next time!");
        }
    }
}