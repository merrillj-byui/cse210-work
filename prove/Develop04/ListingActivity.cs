/*
ListingActivity class, child of Activity class, provides the functionality specific to the Listening activity.
+-----------------------------+
|           Activity          |
+-----------------------------+
              /\
              ||
+-----------------------------+
|       ListingActivity       |
+-----------------------------+
| _prompts : List<string>     |
| _items : List<string>       |
+-----------------------------+
| ListingActivity() : void    |
| DoListing() : void          |
+-----------------------------+

1. The activity should begin with the standard starting message and prompt for the duration that is used by all activities.
2. The description of this activity should be something like: "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area."
3. After the starting message, select a random prompt to show the user such as:
    - Who are people that you appreciate?
    - What are personal strengths of yours?
    - Who are people that you have helped this week?
    - When have you felt the Holy Ghost this month?
    - Who are some of your personal heroes?
4. After displaying the prompt, the program should give them a countdown of several seconds to begin thinking about the prompt. Then, it should prompt them to keep listing items.
5. The user lists as many items as they can until they they reach the duration specified by the user at the beginning.
6. The activity them displays back the number of items that were entered.
7. The activity should conclude with the standard finishing message for all activities.
*/

public class ListingActivity : Activity
{
    // Define private attributes
    private List<string> _prompts = new List<string>{
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heros?"};
    private List<string> responses = new List<string>{}; // list to allow user to type their response list

    // Constructors

    public ListingActivity() : base()
    // Constructor, sets the default name and description for this activity by using setters from the base class.
    {
        SetName("Listing Activity");
        SetDescription("This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.");
    }

    // Methods

    public void DoListing()
    // Function that does the primary work of this activity (used as callback function for menu option)
    {
        // Show the common opening, from the parent Activity class
        DisplayOpening();

        // Display the random activity prompt and tell the user to get ready, with a countdown
        Console.WriteLine("List as many responses as you can to the following prompt:");
        Console.WriteLine($"--- {_prompts[GetRandomListIndex(_prompts)]} ---");
        Console.Write("You may begin in: ");
        CountDown(10); // Using a 10 second countdown to give the user time to read
        Console.WriteLine();

        // Create timed operation to loop until the activity duration expires
        DateTime startTime = DateTime.Now;
        DateTime endTime = startTime.AddSeconds(GetDuration());
        DateTime now = DateTime.Now;

        // Loop for the specified activity duration
        while (now < endTime)
        {
            // Show the prompt
            Console.Write("> ");

            // Get the user's response and add it to the response list
            responses.Add(Console.ReadLine());

            // Update the current time variable
            now = DateTime.Now;
        }

        // Display the common closing, from the parent Activity class
        DisplayClosing();
    }
}