/*
Media (base)
  ^
MusicMedia
----------
Attributes
    _explicitLyrics : bool
Methods
    MusicMedia(string, bool)
    SetExplicit(bool) : void
    IsExplicit() : bool
    GetListing() : string <override>
    GetDetail() : string <override>
    GetInfoString() : string <override>
    GetDataDictionary() : Dictionary<string, object>
*/


using System.Reflection;

public class MusicMedia : Media
{
    private string _mediaType = "Music";
    private bool _explicitLyrics = false;

    
    public MusicMedia (string title, bool explicitLyrics) : base (title, "Music")
    {
        _explicitLyrics = explicitLyrics;
    }


    public void SetExplicit()
    {
        _explicitLyrics = true;
    }


    public void UnsetExplicit()
    {
        _explicitLyrics = false;
    }


    public bool IsExplicit()
    {
        return _explicitLyrics;
    }


    public override string GetListing()
    {
        string listing = $"[{GetMediaType()}] {GetTitle()}";
        listing += IsExplicit() ? " (EXPLICIT)" : " (clean)";
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
        data.Add("explicitLyrics", IsExplicit());

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