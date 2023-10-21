/*
Media (base)
  ^
BookMedia
---------
Attributes
    _pages : int
Methods
    BookMedia(string)
    SetPages(int) : void
    GetPages() : int
    GetListing() : string <override>
    GetDetail() : string <override>
    GetInfoString() : string <override>
    GetDataDictionary() : Dictionary<string, object>
*/


using System.Reflection;
using System.Text;

public class BookMedia : Media
{
    private int _pages = 0;


    public BookMedia (string title) : base (title, "Book"){}

    public void SetPages(int pages)
    {
        _pages = pages;
    }

    public int GetPages()
    {
        return _pages;
    }

    public override string GetListing()
    {
        string listing = $"[{GetMediaType()}] {GetTitle()}";
        if (_pages > 0)
        {
            listing += $"  ({GetPages()} pages)";
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
        data.Add("pages", GetPages());

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