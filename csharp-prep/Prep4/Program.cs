using System;
using System.Collections.Generic;

class Program

{
    static void Main(string[] args)
    {
        //Console.WriteLine("Hello Prep4 World!");

        // Declare variables
        List<int> numbers = new List<int>();
        string response;
        int newNumber = -1, total = 0, maximum = 0, minimum = 0;
        float average;

        Console.WriteLine("Enter a list of numbers, type 0 when finished.");

        do
        {
            Console.Write("Enter number: ");
            response = Console.ReadLine();
            newNumber = int.Parse(response);
            if (newNumber != 0)
            {
                numbers.Add(newNumber);
            }
        } while (newNumber != 0);

        // Calculate the total and find the largest number
        maximum = numbers[0];
        foreach (int number in numbers)
        {
            total += number;
            if (number > maximum)
            {
                maximum = number;
            }
        }

        // Calculate the average
        average = (float)total / numbers.Count;

        // Find the minimum number
        minimum = maximum;
        foreach (int number in numbers)
        {
            if (number < minimum && number > 0)
            {
                minimum = number;
            }
        }

        Console.WriteLine($"The sum is: {total}");
        Console.WriteLine($"The average is: {average}");
        Console.WriteLine($"The largest number is: {maximum}");
        Console.WriteLine($"The smallest number is: {minimum}");

    }
}