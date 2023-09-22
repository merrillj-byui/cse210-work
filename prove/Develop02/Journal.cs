
// Class to handle journal as a whole, which is a list of journal entries
public class Journal
{
    // Attributes
    List<Entry> _entries = new List<Entry>();

    public void ReadFile()
    {
        Console.Write("What is the filename? ");
        string filename = Console.ReadLine();

        if (File.Exists(filename))
        {
            string[] file = System.IO.File.ReadAllLines(filename);
            _entries.RemoveRange(0, _entries.Count);
            foreach (string line in file)
            {
                // Strings in the file are all quoted and delineated by commas
                // So, splitting by \",\" removes all but first and last quotes

                // Split by \",\"
                string[] fields = line.Split("\",\"");

                // Remove first quote in first string
                string date = fields[0].Remove(0, 1);

                string prompt = fields[1];

                // Remove last quote in last string
                string entry = fields[2].Remove(fields[2].Length -1, 1);

                // Create the new entry and add it to the entries list
                Entry newEntry = new Entry(prompt, entry, date);
                _entries.Add(newEntry);
            }
        }
        else
        {
            Console.WriteLine($"Error: {filename} does not yet exist. Try writing it first.");
        }
    }

    public void WriteFile()
    {
        Console.Write("What is the filename? ");
        string filename = Console.ReadLine();

        if (_entries.Count > 0)
        {
            string[] entryFields = new string[0];
            using (StreamWriter file = new StreamWriter(filename))
            {
                foreach (Entry entry in _entries)
                {
                    // Get the entry data into a string array
                    entryFields = entry.GetEntry();
                    // Write the data to file, with \",\" as delimiter
                    file.WriteLine($"\"{entryFields[0]}\",\"{entryFields[1]}\",\"{entryFields[2]}\"");
                }
            }
        }
        else
        {
            Console.WriteLine("Error: Journal is empty. Not writing empty journal to disk.");
        }
    }

    public void AddEntry(string prompt, string text)
    {
        // Create new entry
        Entry newEntry = new Entry(prompt, text);
        // Add it to the list of entries
        _entries.Add(newEntry);
    }

    public void Display()
    {
        if (_entries.Count > 0)
        {
            foreach (Entry entry in _entries)
            {
                // Get the entry data into a string array
                string[] entryFields = entry.GetEntry();
                // Write the journal entry to the console
                Console.WriteLine($"Date: {entryFields[0]} - Prompt: {entryFields[1]}");
                Console.WriteLine($"{entryFields[2]}\n");
            }
        }
        else{
            Console.WriteLine("Error: Journal is empty. Nothing to display.");
        }
    }
}