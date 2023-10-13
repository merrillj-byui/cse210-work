using System.Collections;
using System.Reflection.Metadata.Ecma335;
using System.IO;

public class GoalManager
{
    private string _filename = "goals.txt";
    private string _username = "";
    private int _totalScore = 0;
    private List<Goal> _goals = new List<Goal>();


    public void Start()
    {
        // Create the main menu
        int[] options = {1,2,3,4,5,6};
        string menu = "\nMenu Options:\n" +
        "  1. Create New Goal\n" +
        "  2. List Goals\n" +
        "  3. Save Goals\n" +
        "  4. Load Goals\n" +
        "  5. Record Event\n" +
        "  6. Quit\n" +
        "Select a choice from the menu: ";

        int choice = 0;

        // Loop until the user chooses to exit 
        while (choice != 6)
        {
            int totalScore = TotalScore();
            int totalPossible = TotalPossible();

            // Show the total points the user has achieved
            Console.WriteLine($"\nYou have {totalScore} points out of a possible {totalPossible}.");

            // Display encouraging messages based on how close they are to achieving their goals
            if (totalScore > totalPossible)
            {
                Console.WriteLine($"Way to go, overachiever!");
            }
            else if (totalPossible == totalScore)
            {
                Console.WriteLine("Great work! It may be time to create some new goals.");
            }
            else if (totalPossible > 0 && (float)totalScore / (float)totalPossible < .1)
            {
                Console.WriteLine("Don't be discouraged by the large number of possible points. Hit the goals with big points or bonuses!");
            }
            else if (totalPossible > 0 && (float)totalScore / (float)totalPossible > .9)
            {
                Console.WriteLine("You are doing great! You're almost there.");
            }
            
            // Display the menu and get the user's chice
            choice = DisplayMenu(menu, options);
            
            // Based on users menu choice, call the appropriate function
            switch (choice)
            {
                case 1:
                    AddGoal();
                    break;
                case 2:
                    ListGoalDetails();
                    break;
                case 3:
                    SaveGoals();
                    break;
                case 4:
                    LoadGoals();
                    break;
                case 5:
                    RecordEvent();
                    break;
            }
        }
    }


    public int DisplayMenu(string menu, int[] options)
    {
        int choice = 0;

        // Display the menu on the console
        Console.Write(menu);

        // Loop until a valid choice is made
        do
        {
            // Get the user's input and test if it is a valid choice
            int.TryParse(Console.ReadLine(), out choice);
            if (!options.Contains(choice))
            {
                Console.Write("Please make a valid selection: ");
                choice = 0;
            }
        } while (choice == 0);

        return choice;
    }


    // Function prompts for, and returns a positive integer > 0
    public int GetNumber()
    {
        int number = 0;

        int.TryParse(Console.ReadLine(), out number);

        while (number < 1)
        {
            Console.Write("Please enter a valid integer for points: ");
            int.TryParse(Console.ReadLine(), out number);
        }

        return number;
    }


    public string GetFilename()
    {
        return _filename;
    }


    public void SetFilename(string filename)
    {
        _filename = filename;
    }


    public string AskNewFilename()
    {
        // Ask for a filename
        Console.Write($"What is the filename for the goal file (default=\"{GetFilename()}\")? ");
        string filename = Console.ReadLine();

        // If a filename was entered, make that the new default
        if (filename != "")
        {
            SetFilename(filename);
        }

        return GetFilename();
    }
    

    public string GetUsername()
    {
        return _username;
    }


    public void SetUsername(string username)
    {
        _username = username;
    }


    // Function loops through each goal in the list to calculate the total score, and returns that score
    public int TotalScore()
    {
        int totalScore = 0;
        foreach (Goal goal in _goals)
        {
            totalScore += goal.GetScore();
        }
        return totalScore;
    }


    public int TotalPossible()
    {
        int possibleScore = 0;
        foreach (Goal goal in _goals)
        {
            possibleScore += goal.GetPossibleScore();
        }
        return possibleScore;
    }


    public void DisplayPlayerInfo()
    {
        if (GetUsername() != "")
        {
            Console.Write($"Hello {GetUsername()}. ");
        }
        Console.WriteLine($"\nYou have {TotalScore()} points.\n");
    }


    public void ListGoalNames()
    {
        foreach (Goal goal in _goals)
        {
            Console.WriteLine(goal.GetName());
        }
    }


    public void ListGoalDetails()
    {
        int count = 1;

        Console.WriteLine();
        foreach (Goal goal in _goals)
        {
            Console.WriteLine($"{count++}. {goal.GetDetailsString()}");
        }
    }


    public void AddGoal()
    {
        // Prepare the menu for adding a new goal
        int[] options = {1,2,3};
        string menu = "\nThe types of Goals are:\n" +
        "  1. Simple Goal\n" +
        "  2. Eternal Goal\n" +
        "  3. Checklist Goal\n" +
        "Which type of goal would you like to create? ";

        // Display the menu and get the option from the user
        int choice = DisplayMenu(menu, options);

        // Ask for the common info we will need to instantiate a new goal
        Console.Write("What is the name of your goal? ");
        string name = Console.ReadLine();
        Console.Write("What is a short description of it? ");
        string description = Console.ReadLine();
        Console.Write("What is the amount of points associated with this goal? ");
        int points = GetNumber();

        // Add the chosen goal type            
        switch (choice)
        {
            case 1:
                _goals.Add(new SimpleGoal(name, description, points));
                break;
            case 2:
                _goals.Add(new EternalGoal(name, description, points));
                break;
            case 3:
                // For the Checklist Goal, we need a little more information
                Console.Write("How many times does this goal need to be accomplished for a bonus? ");
                int target = GetNumber();
                Console.Write("What is the bonus for accomplishing it  that many times? ");
                int bonus = GetNumber();
                _goals.Add(new ChecklistGoal(name, description, points, target, bonus));
                break;
        }
    }


    public void RecordEvent()
    {
        int count = 0;

        // Prepare teh menu, which lists each goal
        string menu = "The goals are:\n";
        int[] options = new int[]{};
        foreach (Goal goal in _goals)
        {
            menu += $"{++count}. {goal.GetName()}\n";
            options = options.Append(count).ToArray();
        }
        menu += "Which goal did you accomplish? ";

        // Display the menu and get the user's choice
        int choice = DisplayMenu(menu, options);

        // Record an event on the goal the user chose
        _goals[choice - 1].RecordEvent();

        // Display the new number of total points
        Console.WriteLine($"You now have {TotalScore()} points.");
    }


    public void SaveGoals()
    {
        string filename = AskNewFilename();

        using (StreamWriter outFile = new StreamWriter(filename))
        {
            //file.WriteLine(TotalScore());  //I've chosen not to put the total score in the file, but to have it be calculated

            // Loop through the goals and write each to the file
            foreach (Goal goal in _goals)
            {
                outFile.WriteLine(goal.GetStringRepresentation());
            }
        }
    }
    

    public void LoadGoals()
    {
        string filename = AskNewFilename();

        string[] lines = System.IO.File.ReadAllLines(filename);

        foreach (string line in lines)
        {
            // Separate the goal type from the goal data
            string[] splits = line.Split(":", 2);

            // Split out the goal data
            string[] data = splits[1].Split(",");

            // Based on the goal type, add the goal to the goal list
            switch (splits[0])
            {
                case "SimpleGoal":
                    _goals.Add(new SimpleGoal(data[0], data[1], int.Parse(data[2])));
                    // If the goal is completed, set the goal as completed on the given date
                    if (data[3] == "True")
                    {
                        DateTime date = DateTime.Parse(data[4]);
                        _goals[_goals.Count - 1].SetCompletedWithDate(date);
                    }
                    break;

                case "EternalGoal":
                    _goals.Add(new EternalGoal(data[0], data[1], int.Parse(data[2])));
                    // Add the number of times the goal was completed
                    _goals[_goals.Count - 1].SetCompletedCount(int.Parse(data[3]));
                    break;

                case "ChecklistGoal":
                    _goals.Add(new ChecklistGoal(data[0], data[1], int.Parse(data[2]), int.Parse(data[3]), int.Parse(data[4])));
                    // Add the number of times the goal was completed
                    _goals[_goals.Count -1].SetCompletedCount(int.Parse(data[5]));
                    // If the goal was completed (target met), set the goal as completed on the given date
                    if (data[6] == "True")
                    {
                        DateTime date = DateTime.Parse(data[7]);
                        _goals[_goals.Count -1].SetCompletedWithDate(date);
                    }
                    break;
            }
        }
    }
}