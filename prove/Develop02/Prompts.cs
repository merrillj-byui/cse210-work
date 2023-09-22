using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

// Class for handling the list of journal prompts
// Practice using List and File
public class Prompts
{
    List<string> _promptList = new List<string>{};
    string _filename;

    public Prompts(List<string> defaultList, string filename)
    {
        _promptList = defaultList;
        _filename = filename;
        // If the prompt file exists, read it instead of using the default list
        if (System.IO.File.Exists(_filename))
        {
            ReadFile();
        }
        // Else (file doesn't exist), write the default list to the file
        else
        {
            WriteFile();
        }
    }

    public void Display(){
        foreach(string prompt in _promptList)
        {
            Console.WriteLine(prompt);
        }
    }

    // Function to return a random prompt from the list
    public string GetPrompt()
    {
        // Get an index that is random between 0 and length of prompt list
        var rand = new Random();
        int index = rand.Next(0, _promptList.Count());

        return _promptList[index];
    }

    //Function to write the journal prompts to a file.
    void WriteFile(){
        using (StreamWriter file = new StreamWriter(_filename))
        {
            foreach (string  line in _promptList)
            {
                file.WriteLine(line);
            }
        }
    }

    // Function to read the journal prompts from a file.
    void ReadFile()
    {
        string[] file = System.IO.File.ReadAllLines(_filename);
        // Clear the current prompt list before reading in the new one
        _promptList.RemoveRange(0, _promptList.Count);

        // Read the prompts into the prompt list
        foreach (string line in file)
        {
            _promptList.Add(line);
        }
    }

    // Function to add a new prompt to the prompt list and write the list to a file
    public void AddPrompt()
    {
        // Ask the user for the new prompt
        Console.Write("What is the prompt you would like to add?\n> ");
        string line = Console.ReadLine();

        // Add the new prompt to the list
        _promptList.Add(line);

        // Write the list to the prompt file for safe keeping
        WriteFile();
    }
}