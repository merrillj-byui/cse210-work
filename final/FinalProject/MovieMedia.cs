/*
Media (base)
  ^
MovieMedia
----------
Attributes
    _rating : string
    _ratings : string[]
Methods
    MovieMedia(string, string)
    GetAllRatings() : string[]
    SetRating(string) : void
    GetRating() : string
    GetAgeGuidance() : string
    GetListing() : string <override>
    GetDetail() : string <override>
    GetInfoString() : string <override>
    GetDataDictionary() : Dictionary<string, object>
*/


using System.Reflection;

public class MovieMedia : Media
{
    private string _mediaType = "Movie";
    private string _rating;
    private string[] _ratings = {"G", "PG", "PG-13", "R", "NC-17", "Unrated", "Other"};

    public MovieMedia (string title, string rating) : base (title, "Movie")
    {
        _rating = rating;
    }


    public string[] GetAllRatings()
    {
        return _ratings;
    }


    public void SetRating(string rating = "Other")
    {      
        if (_ratings.Contains(rating))
        {
            _rating = rating;
        }
    }


    public string GetRating()
    {
        return _rating;
    }

    public string GetAgeGuidance()
    {
        string ageGuidance = "Other";

        switch(_rating)
        { 
            case "G":
                ageGuidance = "General Audience: All Ages";
                break;
            case "PG":
                ageGuidance = "Parental Guidance Suggested: Some material may not be suitable for children";
                break;
            case "PG-13":
                ageGuidance = "Parents Strongly Cautioned: Some material may be inappropriate for children under 13";
                break;
            case "R":
                ageGuidance = "Restricted: Under 17 requires accompanying parent or adult guidance";
                break;
            case "NC-17":
                ageGuidance = "No one 17 and under admitted";
                break;
            case "Unrated":
                ageGuidance = "This movie is not rated. Unknown whether it may contain mature content";
                break;
            case "Other":
                ageGuidance = "This movie has an \"Other\" rating or rating is unknown.";
                break;
        }
        return ageGuidance;
    }

    public override string GetListing()
    {
        string listing = $"[{GetMediaType()}] {GetTitle()} ({GetRating()})";
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
        data.Add("rating", GetRating());

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