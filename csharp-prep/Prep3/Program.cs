using System;

class Program
{
    static void Main(string[] args)
    {
        //Console.WriteLine("Hello Prep3 World!");

        // Declare variables
        string response;
        int magic, guess, count = 0;
        Random random = new Random();

        /*
        // Ask the user for a magic number.
        Console.Write("What is the magic number? ");
        response = Console.ReadLine();
        magic = int.Parse(response);
        */

        // Loop so the user can play again if they answer 'yes'
        do{

            // Set a magic number between 1 and 100
            magic = random.Next(1, 101);

            // Loop until the magic number is guessed
            do
            {
                // Ask the user for a guess.
                Console.Write("What is your guess? ");
                response = Console.ReadLine();
                guess = int.Parse(response);
                ++count;

                // Tell the user to guess higher, lower, or if they guessed it
                if (guess < magic)
                {
                    Console.WriteLine("Higher");
                }
                else if (guess > magic)
                {
                    Console.WriteLine("Lower");
                }
                else
                {
                    Console.WriteLine("You guessed it!");
                }
            } while (guess != magic);
            Console.WriteLine($"You guessed the magic number in {count} guesses.");

            // Ask if the user wants to play again
            Console.Write("Would you like to play again (yes/no)? ");
            response = Console.ReadLine();

        } while (response == "yes");
    }
}