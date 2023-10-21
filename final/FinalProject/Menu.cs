/*
Menu.cs
-------
Menu(string, string[], string)
Attributes
    _text : string
    _options : string[]
    _prompt : string
Methods
    Menu(string, string[], string)
    DisplayMenu() : void
    GetOptionChoice() : string
*/


public class Menu
{
    private string _text;
    private string[] _options;
    private string _prompt;

    public Menu(string text, string[] options, string prompt="Enter your choice: ")
    {
        _text = text;
        _options = options;
        _prompt = prompt;
    }


    public void DisplayMenu()
    {
        // Display the menu on the console
        Console.WriteLine(_text);
    }

    public string GetOptionChoice()
    {
        string choice = null;

        while (!_options.Contains(choice))
        {
            // Display the menu prompt
            Console.Write(_prompt);

            // Read the choice the user enters
            choice = Console.ReadLine();

            // If the choice isn't valid, clear it and loop
            if (!_options.Contains(choice))
            {
                choice = null;
            }
        }

        return choice;
    }
}