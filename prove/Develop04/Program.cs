using System;
using System.Security.Cryptography.X509Certificates;

/*
1. Have a menu system to allow the user to choose an activity.
2. Each activity should start with a common starting message that provides the name of the activity, a description, and asks for and sets the duration of the activity in seconds. Then, it should tell the user to prepare to begin and pause for several seconds.
3. Each activity should end with a common ending message that tells the user they have done a good job, and pause and then tell them the activity they have completed and the length of time and pauses for several seconds before finishing.
4. Whenever the application pauses it should show some kind of animation to the user, such as a spinner, a countdown timer, or periods being displayed to the screen.
5. The interface for the program should remain generally true to the one shown in the video demo.
*/

class Program
{
    static void Main(string[] args)
    {
        bool exit = false;

        // Declare the objects for the three activities
        BreathingActivity opt1 = new BreathingActivity();
        ReflectionActivity opt2 = new ReflectionActivity();
        ListingActivity opt3 = new ListingActivity();

        // Create a function for the quit option
        void Quit()
        {
            Console.WriteLine($"Thank yourself for taking time for your own mindfulness.");
            exit = true;
        }

        // Create callback functions to execute when a menu option is selected
        MenuOptionCallback breathingCallback = new MenuOptionCallback(opt1.DoBreathing);
        MenuOptionCallback reflectionCallback = new MenuOptionCallback(opt2.DoReflection);
        MenuOptionCallback listingCallback = new MenuOptionCallback(opt3.DoListing);
        MenuOptionCallback quitCallback = new MenuOptionCallback(Quit);

        // Create the menu (object)
        Menu menu = new Menu();
        menu.SetHeader("Menu Options:");
        menu.AddOption("1", "Start breathing activity", breathingCallback);
        menu.AddOption("2", "Start reflecting activity", reflectionCallback);
        menu.AddOption("3", "Start listing activity", listingCallback);
        menu.AddOption("4", "Quit", quitCallback);
        menu.SetPrompt("Select a choice from the menu: ");

        // Loop until 'exit' is set to false by the Quit callback function
        while (!exit)
        {
            // Show the menu (prompt loops until a selection is made, and then a callback function is executed)
            menu.DisplayMenu();
        }
    }
}