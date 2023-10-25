/*
Single responsibility: to represent a menu. This project uses multiple menus created from lists.

Menu (not derived)
------------------
Menu(string, string[], string)
Attributes
    _text : string
    _options : string[]
    _prompt : string
Methods
    Menu(string[], string)
    DisplayMenu() : void
    GetOptionChoice() : string
*/


public class Menu
{
    // Attributes
    private string _text;
    private string[] _options;
    private string _prompt;


    //Constructors
    public Menu(string[] options, string prompt="Enter your choice: ")
    {
        _options = options;
        _prompt = prompt;
        int count = 0;
        // Build the text that gets displayed
        foreach (string option in _options)
        {
            _text += $"  {++count}. {option}\n";
        }
    }


    // Methods
    public void DisplayMenu()
    // Displays the menu on the console
    {
        // Display the menu on the console
        Console.WriteLine(_text);
    }

    public string GetOptionChoice()
    // Prompts for a selection, and loops until a valid selection is entered
    {
        int choice = 0;
        while (choice < 1 || choice > _options.Count())
        {
            // Display the menu prompt
            Console.Write(_prompt);

            // Read the choice the user enters
            int.TryParse(Console.ReadLine(), out choice);
        }
        return _options[choice - 1];
    }
}