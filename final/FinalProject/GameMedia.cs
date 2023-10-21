/*
Media (base)
  ^
GameMedia
---------
Attributes
    _defaultMinAge : int
    _defaultMaxAge : int
    _minAge : int
    _maxAge : int
Methods
    GameMedia(string)
    SetMinAge(int) : void
    GetMinAge() : int
    SetMaxAge(int) : void
    GetMaxAge() : int
    GetListing() : string <override>
    GetDetail() : string <override>
    GetInfoString() : string <override>
    GetDataDictionary() : Dictionary<string, object>
*/


public class GameMedia : Media
{
    private int _defaultMinAge = 0;
    private int _defaultMaxAge = 99;
    private int _minAge = 0;
    private int _maxAge = 99;

    public GameMedia (string title) : base (title, "Game"){}


    public void SetMinAge(int minAge)
    {
        _minAge = minAge;
    }


    public int GetMinAge()
    {
        return _minAge;
    }


    public void SetMaxAge(int maxAge)
    {
        _maxAge = maxAge;
    }


    public int GetMaxAge()
    {
        return _maxAge;
    }

    public override string GetListing()
    {
        string listing = $"[{GetMediaType()}] {GetTitle()}";
        if (_minAge == _defaultMinAge && _maxAge == _defaultMaxAge)
        {
            listing += $" (All age groups)";
        }
        else
        {
            listing += $" (Ages {GetMinAge()} to {GetMaxAge()})";
        }
        if (IsAvailable() && !IsOnLoan())
        {
            listing += " - AVAILABLE";
        }
        else if (IsOnLoan())
        {
            listing += $" - Due back: {GetLastLoan().GetDueDate().ToShortDateString()}";
        }
        else
        {
            listing += $" - Unavailable: {GetLastNote()}";
        }

        return listing;
    }

    public override string GetDetail()
    {
        return base.GetDetail();
    }

    public override string GetInfoString()
    {
        return base.GetInfoString();
    }


    public override Dictionary<string, object> GetDataDictionary()
    {
        Dictionary<string, object> data = new Dictionary<string, object>{};

        data.Add("mediaType", GetMediaType());
        data.Add("title", GetTitle());
        data.Add("genre", GetGenre());
        data.Add("publishDate", GetPublishDate());
        data.Add("acquireDate", GetAcquireDate());
        data.Add("available", IsAvailable());
        data.Add("minAge", GetMinAge());
        data.Add("maxAge", GetMaxAge());

        Borrower borrower;
        foreach (LendingRecord loan in GetLoans())
        {
            borrower = loan.GetBorrower();
            data.Add("borrower", new Dictionary<string, object> {
                        {"name", borrower.GetFullName()},
                        {"phone", borrower.GetPhone()},
                        {"email", borrower.GetEmail()},
                        {"streetAddress", borrower.GetStreetAddress()},
                        {"city", borrower.GetCity()},
                        {"state", borrower.GetState()},
                        {"zip", borrower.GetZip()}
                    } );
            data.Add("borrowedDate", loan.GetBorrowedDate());
            data.Add("returnedDate", loan.GetReturnedDate());
            data.Add("dueDate", loan.GetDueDate());
            data.Add("returned", loan.IsReturned());
        }
        data.Add("notes:", GetNotes());

        return data;
    }
}