using System;

class Program
{
    static void Main(string[] args)
    {
        //Console.WriteLine("Hello Prep5 World!");


        // Call each of the functions, saving the return values, and passing them as necessary
        DisplayWelcome();
        string name = PromptUserName();
        int number = PromptUserNumber();
        int square = SquareNumber(number);
        DisplayResults(name, square);

    }
    
    // Function to display the welcome message
    static void DisplayWelcome()
    {
        Console.WriteLine("Welcome to the Program!");
    }

    // Function to ask for and return the users name as a string
    static string PromptUserName()
    {
        Console.Write("Please enter yor name: ");
        string name = Console.ReadLine();
        return name;
    }

    // Function that asks for and returns the user's favorite number (integer)
    static int PromptUserNumber()
    {
        Console.Write("Please enter your favorite number: ");
        string response = Console.ReadLine();
        return int.Parse(response);
    }

    // Function that accepts an integer and returns the square of that number
    static int SquareNumber(int number)
    {
        int square = number * number;
        return square;
    }

    // Function that accepts theuser's name and squared number, and displays them
    static void DisplayResults(string name, int square)
    {
        Console.WriteLine($"{name}, the square of your number is {square}");
    } 

}