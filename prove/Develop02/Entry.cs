
// Class for an individual journal entry
using System.Diagnostics.Contracts;
using System.Security.Cryptography.X509Certificates;

public class Entry
{
    //Attributes
    string _prompt;
    string _entry;
    string _datestamp;

    public Entry(string prompt, string entry, string date = "now")
    {
        _prompt = prompt;
        _entry = entry;
        if (date == "now")
        {
            // If no date is given or is "now" then set current date
            DateTime currentTime = DateTime.Now;
            string defaultDate = currentTime.ToShortDateString();
            _datestamp = defaultDate;
        }
        else
        {
            // Otherwise, set the date to the date that was passed in
            _datestamp = date;
        }
    }

    public void Display()
    {
        // Write the journal entry to the console
        Console.WriteLine($"Date: {_datestamp} - Prompt: {_prompt}");
        Console.WriteLine(_entry);
    }

    public string[] GetEntry()
    {
        // Returns the journal entry as a string[]
        string[] entryFields = new string[3];
        entryFields[0] = _datestamp;
        entryFields[1] = _prompt;
        entryFields[2] = _entry;
        return entryFields;
    }

}