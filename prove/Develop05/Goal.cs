using System.Runtime.InteropServices;

public abstract class Goal
{
    private DateTime _createdDate;
    private string _name;
    private string _description;
    private int _points;
    private bool _complete = false;
    private int _completedCount = 0;
    private DateTime? _completedDate = null; // nullable DateTime


    public Goal(string name, string description, int points)
    {
        _createdDate = DateTime.Now;
        _name = name;
        _description = description;
        _points = points;
    }


    public DateTime GetCreatedDate()
    {
        return _createdDate;
    }


    public void SetName(string name)
    {
        _name = name;
    }


    public string GetName()
    {
        return _name;
    }


    public void SetDescription(string description)
    {
        _description = description;
    }


    public string GetDescription()
    {
        return _description;
    }


    public void SetPoints(int points)
    {
        _points = points;
    }


    public int GetPoints()
    {
        return _points;
    }


    public virtual void SetCompleted()
    {
        _completedDate = DateTime.Now;
        _complete = true;
    }


    public virtual void SetCompletedCount(int count)
    {
        _completedCount = count;
    }


    public virtual int GetCompletedCount()
    {
        return _completedCount;
    }


    public virtual void SetCompletedWithDate(DateTime date)
    {
        _completedDate = date;
        _complete = true;
    }
    

    public virtual bool IsCompleted()
    {
        return _complete;
    }


    public DateTime? GetCompletedDate()
    {
        return _completedDate;
    }


    public abstract int GetScore();

    public abstract int GetPossibleScore();

    public abstract void RecordEvent();

    public abstract string GetDetailsString();

    public abstract string GetStringRepresentation();
}