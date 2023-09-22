using System;


/*  Exceeds:
    * Menu object is generic, allowing for any menu to be passed in with a 
      query string, and will loop until valid selection is made.
    * File string delimiter is quote-comma-quote, which is what Excel seems
      to use when the string contains a comma. Tried to handle that.
    * Prompt object automatically saves the default prompt list to a file if 
      the file doesn't exist, and reads the file if it does exist.
    * Prompt object allows for new prompts to be added. Every time one is
      added, the prompt file is saved.
    
*/
class Program
{
    static void Main(string[] args)
    {
        //Console.WriteLine("Hello Develop02 World!");

        // Define menu variables and instantiate Menu
        string menuText = "Please select one of the following choices:\n" +
                            "1. Write\n" +
                            "2. Display\n" +
                            "3. Load\n" +
                            "4. Save\n" +
                            "5. Quit\n" +
                            "--------\n" +
                            "6. Display Prompts\n" +
                            "7. Add New Prompt\n";
        string menuQuery = "What would you like to do? ";
        string[] menuOptions = new string[] {"1", "2", "3", "4","5","6","7"};
        Menu menu = new Menu(menuText, menuQuery, menuOptions);

        // Define prompt variables and instantiate Prompts
        List<string> promptDefaults = new List<string>
        {
           "Who was the most interesting person I interacted with today?",
           "What was the best part of my day?",
           "How did I see the hand of the Lord in my life today?",
           "What was the strongest emotion I felt today?",
           "If I had one thing I could do over today, what would it be?",
           "For what are you most grateful for today?",
           "What was the most important thing you did today?",
           "What service did you provide to whom today?"
        };
        // This file stores the list of prompts, and starts with the default list (above)
        const string promptFile = "journal_prompts.txt";
        Prompts journalPrompts = new Prompts(promptDefaults, promptFile);

        // Instantiate Journal
        Journal journal = new Journal();

        // Display welcome message
        Console.WriteLine("Welcome to the Journal Program!");

        string menuChoice;

        // Loop through menu actions
        do
        {
            // Display the menu and get the choice
            menuChoice = menu.Display();

            // Take action according to the menu choice
            switch (menuChoice)
            {
                case "1":
                    string prompt = journalPrompts.GetPrompt();
                    Console.Write($"{prompt}\n> ");
                    string response = Console.ReadLine();
                    journal.AddEntry(prompt, response);
                    break;
                case "2":
                    journal.Display();
                    break;
                case "3":
                    journal.ReadFile();
                    break;
                case "4":
                    journal.WriteFile();
                    break;
                case "6":
                    journalPrompts.Display();
                    break;
                case "7":
                    journalPrompts.AddPrompt();
                    break;
            }
        } while (menuChoice != "5");
    }
}