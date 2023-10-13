public class SimpleGoal : Goal
{
    public SimpleGoal(string name, string description, int pointValue) : base (name, description, pointValue){}


    // public override int GetScore()
    // {
    //     // The score is just the point value if it has been completed, otherwise 0
    //     return IsCompleted() ? GetPoints() : 0;
    // }


    public override int GetPossibleScore()
    {
        return GetPoints();
    }


    public override void RecordEvent()
    {
        if (!IsCompleted())
        {
            // Mark the goal as completed
            SetCompleted();

            // Write a congrattulatory message to the screen
            Console.WriteLine($"Congratulations! You have earned {GetPoints()} points!"); 
        }
        else
        {
            Console.WriteLine($"This goal was already completed on: {GetCompletedDate()}");
        }
    }


    public override string GetDetailsString()
    {
         string detailsString;
         // If completed, start with a checked box, elxe an empty box
         detailsString = IsCompleted() ? "[X] " : "[ ] ";

         // Add the name, description, and point value
         detailsString += $"{GetName()} ({GetDescription()}) {GetPoints()}pts";

         // If completed, append that it was completed on a given date, or state that it is incomplete
         detailsString += IsCompleted() ? $" -- completed on {GetCompletedDate()}" : " -- incomplete";

         return detailsString;
    }


    public override string GetStringRepresentation()
    {
        string dateString = GetCompletedDate().ToString();
        string stringRepresentation = "SimpleGoal:" +
                                      $"{GetName()}," +
                                      $"{GetDescription()}," +
                                      $"{GetPoints()}," +
                                      $"{IsCompleted().ToString()}," +
                                      $"{dateString}";
        return stringRepresentation;
    }
}