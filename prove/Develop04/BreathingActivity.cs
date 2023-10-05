/*
+-----------------------------+
|           Activity          |
+-----------------------------+
              /\
              ||
+-----------------------------+
|      BreathingActivity      |
+-----------------------------+
| _breathInMsg : string       |
| _breathOutMsg : string      |
| _breathSeconds : int        |
+-----------------------------+
| BreatheIn() : void          |
| BreatheOut() : void         |
| DisplayCount() : void       |
| DoBreathing() : void        |
+-----------------------------+

1. The activity should begin with the standard starting message and prompt for the duration that is used by all activities.
2. The description of this activity should be something like: "This activity will help you relax by walking your through breathing in and out slowly. Clear your mind and focus on your breathing."
3. After the starting message, the user is shown a series of messages alternating between "Breathe in..." and "Breathe out..."
4. After each message, the program should pause for several seconds and show a countdown.
5. It should continue until it has reached the number of seconds the user specified for the duration.
6. The activity should conclude with the standard finishing message for all activities.

*/

public class BreathingActivity : Activity
{
    // Define private attributes
    private string _breatheInMsg = "Breathe in...";
    private string _breatheOutMsg = "Now breathe out...";
    private int _breatheSeconds = 10;

    // Constructors
    
    public BreathingActivity() : base()
    {
        SetName("Breating Activity");
        SetDescription("This activity will help you relax by walking your through breathing in and out slowly. Clear your mind and focus on your breathing.");
    }

    // Methods

    public void BreatheIn()
    // Function displays a count up prompt to the specified breath length
    {
        Console.Write($"\n{_breatheInMsg}");

        // Loop from 1 to the specified breath length
        for (int i = 1; i <= _breatheSeconds; i++)
        {
            // Display the breath seconds count indicator
            DisplayCount(i, '>');
        }

        Console.WriteLine();
    }

    public void BreatheOut()
    // Function displays a count down prompt to the specified breath length
    {
        Console.Write($"{_breatheOutMsg}");

        // Loop from the specified breath length down to 1
        for (int i = _breatheSeconds; i > 0; i--)
        {

            // Display the breath seconds count indicator
            DisplayCount(i, '<');
        }
        Console.WriteLine("\n");
    }

    public void DisplayCount(int i, char indicator)
    // This is different than the Activity CountDown(). This shows a line of dots equivalent to the count, and the count number, sleeps, then wipes out that displayed text.
    {
        // A string of '.' equivalent to the count concatenated with the count number
        string countStr = new string(indicator, i) + i;

        // A string that will backspace over the whole countStr, replace it with ' ', and backspace again
        string clearStr = new string('\b', countStr.Length);
        clearStr += new string(' ', countStr.Length);
        clearStr += new string('\b', countStr.Length);

        // Display the count string, sleep for 1 second, then wipe it out
        Console.Write(countStr);
        Thread.Sleep(1000);
        Console.Write(clearStr);
    }

    public void DoBreathing()
    // Function that does the primary work of this activity (used as callback function for menu option)
    {
        // Show the common opening, from the parent Activity class
        DisplayOpening();

        // Create timed operation to loop until the activity duration expires
        DateTime startTime = DateTime.Now;
        DateTime endTime = startTime.AddSeconds(GetDuration());
        DateTime now = DateTime.Now;

        // Loop for the specified activity duration
        while (now < endTime)
        {
            // Show the prompt to breathe in, with the countdown display
            BreatheIn();

            // Show the prompt to breath out, with the countdown display
            BreatheOut();

            // Update the current time variable
            now = DateTime.Now;
        } 

        // Display the common closing, from the parent Activity class
        DisplayClosing();
    }
}