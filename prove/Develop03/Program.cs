using System;
using System.ComponentModel.Design;
using System.Net;
using System.Threading.Tasks.Dataflow;

class Program
{
    static void Main(string[] args)
    {

        string reference = "1 Thessalonians 5:5-6";
        string text = 
@"Ye are all the children of light, and the children of the day: we are not of the
night, nor of darkness.
Therefore let us not sleep, as do others; but let us watch and be sober.";

        Scripture scripture = new Scripture(reference, text);

        string response = "";
        int hiddenCount;
        int visibleCount;
        int hiddenWordNumber = 0;
        do
        {
            // Display the scripture
            Console.Clear();
            scripture.DisplayScripture();
            Console.WriteLine();

            // Display the statistics
            hiddenCount = scripture.CountHidden();
            visibleCount = scripture.CountVisible();
            if (hiddenWordNumber > 0)
            {
                Console.WriteLine($"Word {hiddenWordNumber} was just hidden.");
                if (hiddenCount == 1)
                {
                    Console.WriteLine($"{scripture.CountHidden()} word is hidden.");
                }
                else
                {
                    Console.WriteLine($"{scripture.CountHidden()} words are hidden.");
                }
            }
            if (visibleCount == 1)
            {
                Console.WriteLine($"{scripture.CountVisible()} word is visible.");
            }
            else
            {
                Console.WriteLine($"{scripture.CountVisible()} words are visible.");
            }

            // Display the prompt
            Console.WriteLine("\nPress Enter to continue, or type 'new', 'reset', or 'quit' to finish.");
            response = Console.ReadLine();

            // Handle the response to the prompt
            if (response == "reset")
            // Resets the scripture to not have any hidden words
            {
                scripture.UnhideAll();
                hiddenWordNumber = 0;
            }
            else if (response == "new")
            // Allows the user to enter their own scripture
            {
                Console.Write("Enter the scripture reference: ");
                reference = Console.ReadLine();
                Console.WriteLine("Enter the scripture (ends with a blank line):");
                string line;
                text = "";
                do{
                    line = Console.ReadLine();
                    text += line + " ";
                } while (!String.IsNullOrWhiteSpace(line));
                scripture = new Scripture(reference, text);
            }
            else if (visibleCount == 0)
            // If all the words have been made invisible, exit
            {
                break;
            }
            else
            // Otherwise, hide a random word in the scripture
            {
                hiddenWordNumber = scripture.HideRandomWord();
            }

        } while (response != "quit");
    }
}