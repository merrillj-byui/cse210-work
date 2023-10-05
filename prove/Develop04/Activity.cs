/*
+---------------------------------------+
|               Activity                |
+---------------------------------------+
| _name : string                        |
| _welcome : string                     |
| _description : string                 |
| _duration : int                       |
| _prepMsg : string                     |
| _doneMsg : string                     |
+---------------------------------------+
| Activity(string, string) : void       |
| Activity() : void                     |
| SetName(string) : void                |
| GetName() : string                    |
| SetDescription(string) : void         |
| GetDescription() : string             |
| AskDuration() : void                  |
| GetDuration() : int                   |
| SetPrepMsg(string) : void             |
| GetPrepMsg() : string                 |
| SetDoneMsg(string) : void             |
| GetDoneMsg(string) : void             |
| PauseSpinner(int) : void              |
| DisplayOpening() : void               |
| DisplayClosing() : void               |
| GetRandomListIndex(List<string>) : int|
| CountDown(int) : void                 |
+---------------------------------------+
*/

using System.ComponentModel;
using System.Threading.Tasks.Dataflow;

public class Activity
{
    // Define private attributes
    private string _name;
    private string _description;
    private int _duration = 60;
    private string _prepMsg = "Get ready...";
    private string _doneMsg = "Well done!!";
    private string[] spinner = {"|", "/", "-", "\\"}; // The array of spinner characters


    // Constructors

    public Activity(string name, string description)
    {
        SetName(name); 
        SetDescription(description);
    }

    public Activity()
    {
        _name = "Activity";
        _description = "Perform some activity, such as meditation.";
    }

    // Methods

    public void SetName(string name)
    // Setter for the _name attribute
    {
        _name = name;
    }

    public string GetName()
    // Getter for the _name attribute
    {
        return _name;
    }

    public void SetDescription(string description)
    // Setter for the _description attribute
    {
        _description = description;
    }

    public string GetDescription()
    // Getter for the _description attribute
    {
        return _description;
    }

    public void AskDuration()
    // Asks the user for a duration for the activity, and sets the _duration attribute
    {
        int duration;

        // Ask for how long to perform the activity
        Console.Write($"How long, in seconds, would you like for your session (default {_duration})? ");

        // Loop until a valid integer is passed, or Enter is pressed without any other input (default)
        do
        {
            // Read the user's response
            string temp = Console.ReadLine();

            // If the user hit Enter with no other input, use the current _duration as default
            if (temp == "")
            {
                duration = _duration;
            }
            // Otherwise, try to parse the input into a variable
            else
            {
                Int32.TryParse(temp, out duration);
            }

            // If the duration is not greater than 0, prompt user to enter a valid duration and loop
            if (duration <= 0)
            {
                Console.Write("Please enter a valid number of seconds (positive integer). ");
            }

        } while (duration <= 0);

        // Set the _duration attribute
        _duration = duration;
    }

    public int GetDuration()
    // Getter for the _duration attribute
    {
        return _duration;
    }

    public void SetPrepMsg(string prepMsg)
    // Setter for the _prepMsg attribute
    {
        _prepMsg = prepMsg;
    }

    public string GetPrepMsg()
    // Getter for the _prepMsg attribute
    {
        return _prepMsg;
    }

    public void SetDoneMsg(string doneMsg)
    // Setter for the _doneMsg attribute
    {
        _doneMsg = doneMsg;
    }

    public string GetDoneMsg()
    // Getter for the _doneMsg attribute
    {
        return _doneMsg;
    }

    public void PauseSpinner(int timer)
    // Function to present a spinner that displayes for the specified timer (seconds)
    {
        int index = 0; // Index into the array of spinner characters

        // Create timed operation to loop until the activity duration expires
        DateTime startTime = DateTime.Now;
        DateTime endTime = startTime.AddSeconds(timer);
        DateTime now = DateTime.Now;

        // Display the initial string of blank characters
        Console.Write("   ");

        // Loop through the index of spinner characters
        while (now < endTime)
        {
            // Delete the previously displayed spinner, and display the new spinner character
            Console.Write($"\b\b\b   \b\b\b {spinner[index]} ");

            // increment the index, looping through 1..4
            index = ++index % 4;

            // Sleep for .2 seconds (I like a little faster spinner)
            Thread.Sleep(200);

            // Update the current time variable
            now = DateTime.Now;
        }

        // Wipe out the spinner
        Console.WriteLine("\b\b\b   \b\b\b");
    }

    public void DisplayOpening()
    // Function that displays the standard opening messages for all activities
    {
        // Display the standard opening messages for all activities
        Console.Clear();
        Console.WriteLine($"\nWelcome to the {_name}.\n");
        Console.WriteLine($"{_description}\n");

        // Request the desired activity duration, and save it to _duration attribute
        AskDuration();

        // Display the rest of the standard opening messages
        Console.Clear();
        Console.WriteLine(_prepMsg);

        // Display a spinner and pause to give the user time to read
        PauseSpinner(6);
    }

    public void DisplayClosing()
    // Function that displays the standard closing messages for all activities
    {
        Console.WriteLine($"\n{_doneMsg}");
        Console.WriteLine($"\nYou have completed another {GetDuration()} seconds of the {GetName()}.");

        // Display a spinner and pause to give the user time to read
        PauseSpinner(6);
    }

    public int GetRandomListIndex(List<string> list)
    // Function to return a random index for a List of strings
    {
        Random rand = new Random();
        int index = rand.Next(list.Count);
        return index;
    }

    public void CountDown(int counter)
    // Function to display an integer countdown (multiple digits supported)
    {
        string clearString; // Used to create a string that backspaces over the counter, replaces it with spaces, and backspaces over it again
        string indexString; // Convert string to an index so we know it's character length so we know how many characters to mask over

        // Count down from the counter that was given
        for (int i = counter; i > 0; i--)
        {
            // Convert the index to a string so we can determine its character length
            indexString = i.ToString();

            // Show the counter index
            Console.Write(i);

            // Sleep  for 1  second
            Thread.Sleep(1000);

            // Create the string to backspace over the counter, replace it with spaces, and backspace again
            clearString = new string('\b', indexString.Length);
            clearString += new string(' ', indexString.Length);
            clearString += new string('\b', indexString.Length);

            // Clear the counter that was displayed
            Console.Write(clearString);
        }
    }
}