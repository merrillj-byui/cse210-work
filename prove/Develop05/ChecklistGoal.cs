using System.Reflection.Metadata.Ecma335;

public class ChecklistGoal : Goal
{
    private int _target = 0;
    private int _bonus = 0;


    public ChecklistGoal(string name, string description, int pointValue, int target, int bonus) : base (name, description, pointValue)
    {
        _target = target;
        _bonus = bonus;
    }


    public override int GetScore()
    {
        // The base score is just the point value multiplied by the number of times completed
        int score = GetCompletedCount() * GetPoints();

        // If the target was met, also add the bonus points
        if (GetCompletedCount() >= GetTarget())
        {
            score += _bonus;
        }

        return score;
    }


    public override int GetPossibleScore()
    {
        return GetTarget() * GetPoints() + GetBonus();
    }


    public void SetTarget(int target)
    {
        _target = target;
    }


    public int GetTarget()
    {
        return _target;
    }


    public void SetBonus(int bonus)
    {
        _bonus = bonus;
    }


    public int GetBonus()
    {
        return _bonus;
    }


    public override void RecordEvent()
    {
        // Increment the number of times completed
        SetCompletedCount(GetCompletedCount() + 1); // purposely allowing completion count to go beyond target (bonus only scored once)

        // If the target was met, mark the goal completed, but the user is still allowed to keep completing even after meeting the target
        if (GetCompletedCount() >= _target)
        {
            SetCompleted();
        }

        // Display a congratulatory message
        Console.WriteLine($"Congratulations! You have earned {GetScore()} points!"); 
    }


    public override string GetDetailsString()
    {
         string detailsString;

         // If the goal is completed (target met) start with a checked box, otherwise an empty box
         detailsString = IsCompleted() ? "[X] " : "[ ] ";

         // Append the name, description, and point value
         detailsString += $"{GetName()} ({GetDescription()}) {GetPoints()}pts(+{GetBonus()})";

         // Append the number of times this goal has been completed, and the target
         detailsString += $" -- Currently completed: {GetCompletedCount()}/{GetTarget()}";

         // Append the completed date if completed
         if (IsCompleted())
         {
            detailsString += $" ({GetCompletedDate()})";
         }

         return detailsString;
    }


    public override string GetStringRepresentation()
    {
        string dateString = GetCompletedDate().ToString();
        string stringRepresentation = "ChecklistGoal:" +
                                      $"{GetName()}," +
                                      $"{GetDescription()}," +
                                      $"{GetPoints()}," +
                                      $"{_target}," +
                                      $"{_bonus}," +
                                      $"{GetCompletedCount()}," +
                                      $"{IsCompleted().ToString()}," +
                                      $"{dateString}";
        return stringRepresentation;
    }
}