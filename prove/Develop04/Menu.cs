/*
+------------------------------------------------------+
|                        MenuOption                    |
+------------------------------------------------------+
                             /\
                             ||
+------------------------------------------------------+
|                           Menu                       |
+------------------------------------------------------+
| _options : Dictionary<string, MenuOption>            |
| _header : string                                     |
| _prompt : string                                     |
+------------------------------------------------------+
| AddOption(string, string, MenuOptionCallback) : void |
| SetOption(string, string) : void                     |
| GetOption(string) : void                             |
| SetHeader(string) : void                             |
| GetHeader() : string                                 |
| SetPrompt(string) : void                             |
| GetPrompt() : string                                 |
| GetMenu() : string                                   |
| DisplayMenu() : void                                 |
+------------------------------------------------------+
*/

using System.Runtime.InteropServices;

public class Menu
{
    // Define private attributes
    private Dictionary<string, MenuOption> _options = new Dictionary<string, MenuOption>{};
    private string _header = "Menu Options; ";
    private string _prompt = "Select an option: ";

    // Constructors

    public void AddOption(string key, string text, MenuOptionCallback callback)
    {
        _options.Add(key, new MenuOption(key, text, callback));
    }

    // Methods

    public void SetOption(string key, string text, MenuOptionCallback callback)
    // Function to set the menu option text and callback for an option associated with the key
    {
        if (_options.ContainsKey(key))
        {
            _options[key].SetMenuOptionText(text);
            _options[key].SetMenuOptionCallback(callback);
        }    
    }

    public string GetOption(string key)
    // Getter for the menu text based on the key provided
    {
        // If the key is valid, return the text for that menu option
        if (_options.ContainsKey(key))
        {
            return _options[key].GetMenuOptionText();
        }
        // Otherwise return an empty string
        else
        {
            return "";
        }    
    }

    public void SetHeader(string header)
    // Setter for the _header attribute
    {
        _header = header;
    }

    public string GetHeader()
    // Getter for the _header attribute
    {
        return _header;
    }

    public void SetPrompt(string prompt)
    // Setter for the _prompt attribute
    {
        _prompt = prompt;
    }

    public string GetPrompt()
    // Getter for the _prompt attribute
    {
        return _prompt;
    }

    public string GetMenu()
    // Function constructs the menu text in a single string and returns it
    {
        // Add the header to the menu string
        string menu = $"\n{_header}";

        // Loop through the list of menu options
        foreach (KeyValuePair<string, MenuOption> kvp in _options)
        {
            // Get the menu option from the key-value pair
            MenuOption option = kvp.Value;
            // Add the key and menu option text to the menu string
            menu += $"{kvp.Key}. {option.GetMenuOptionText()}";
        }

        // Add the menu prompt to the menu string
        menu += $"\n{_prompt}";

        return menu;
    }   

    public void DisplayMenu()
    // Function displays the menu and loops for a valid choice to be entered
    {
        string choice = "";

        // Display the menu to the console
        Console.Clear();
        Console.WriteLine($"\n{_header}");
        foreach (KeyValuePair<string, MenuOption> kvp in _options)
        {
            MenuOption option = kvp.Value;
            Console.WriteLine($"{kvp.Key}. {option.GetMenuOptionText()}");
        }
        Console.WriteLine();

        // Loop until a valid selection is made
        while (!_options.ContainsKey(choice))
        {
            // Display the prompt
            Console.Write($"{_prompt}");

            // Read the user's input
            choice = Console.ReadLine();
        }

        // Once a valid selection is made, call the callback function for that option
        if (_options.ContainsKey(choice))
        {
            _options[choice].RunMenuOption();
        }
    }   
}