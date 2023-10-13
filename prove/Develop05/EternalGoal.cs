public class EternalGoal : Goal
{

    public EternalGoal(string name, string description, int pointValue) : base (name, description, pointValue){}


    public override int GetScore()
    {
        // the score is just the point value multiplied by the number of times it has been completed
        return GetCompletedCount() * GetPoints();
    }


    public override int GetPossibleScore()
    {
        //  We don't know how many times it may be possible to complete this goak, so just return the current score
        return GetScore();
    }


    public override void RecordEvent()
    {
        // Increment the completed count by one
        SetCompletedCount(GetCompletedCount() + 1);

        // Write a congratulatory message to the screen
        Console.WriteLine($"Congratulations! You have earned {GetScore()} points!"); 

        // Note, we don't ever mark this kind of goal as completed, never call SetCompleted()
        // But, I am counting the number of times the goal has been done, for scoring purposes
    }


    public override string GetDetailsString()
    {
         string detailsString;
         // Since this is a goal that will never be completed, start with a box with a dot in it if task done at least once (in progress indicator)
         detailsString = GetCompletedCount() > 0 ? "[•] " : "[ ] ";

         // Append the name, description, and points value
         detailsString += $"{GetName()} ({GetDescription()}) {GetPoints()}pts";

         // Append how many times it has been completed
         detailsString += $" -- completed {GetCompletedCount()} times";

         return detailsString;
    }


    public override string GetStringRepresentation()
    {
        string dateString = GetCompletedDate().ToString();
        string stringRepresentation = "EternalGoal:" +
                                      $"{GetName()}," +
                                      $"{GetDescription()}," +
                                      $"{GetPoints()}," +
                                      $"{GetCompletedCount()}";
        return stringRepresentation;
    }
}