using System;
using System.ComponentModel.DataAnnotations;

// Class for handling program menu
// This is a flexible menu object that can be used with different menu content and queries
// Practice using Array
public class Menu
{
    // Declare variables
    string _menuText;
    string _menuPrompt;
    string[] _menuOptions = new string[0];

    // Menu constructor accepts menu text, prompt text, and an array of options
    public Menu(string text, string query, string[] options)
    {
        // initialize variables
        _menuText = text;
        _menuPrompt = query;
        // Create a copy of the menu options
        Array.Resize(ref _menuOptions, options.Length);
        Array.Copy(options, 0, _menuOptions, 0, options.Length);
    }

    // Display function shows menu, loops at the query until a valid selection is made
    public string Display()
    {
        // Variables
        string choice;

        // Display the menu text
        Console.Write($"\n{_menuText}");

        // Loop until a valid selection is made
        do
        {
            // Display the string to query for a choice
            Console.Write(_menuPrompt);
            choice = Console.ReadLine();

        } while (_menuOptions.Contains(choice) == false);

        Console.WriteLine();

        return choice;
    }
}