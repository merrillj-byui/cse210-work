/*
Media (base)
  ^
VideoGameMedia
--------------
Attributes
    _rating : string
    _ratings : string[]
Methods
    VideoGameMedia(string, string) :  void
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

public class VideoGameMedia : Media
{
    private string _mediaType = "Video Game";
    private string _rating = "Other";
    private string[] _ratings = {"E", "E10+", "T", "M", "A", "Unrated", "Other"};

    public VideoGameMedia (string title, string rating) : base (title, "Video Game")
    {
        _rating = rating;
    }


    public string[] GetAllRatings()
    {
        return _ratings;
    }


    public void SetRating(string rating)
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
            case "E":
                ageGuidance = "Everyone";
                break;
            case "E10+":
                ageGuidance = "Everyone 10+: everyone 10 years old or older";
                break;
            case "T":
                ageGuidance = "Teen: suitable for ages 13 and up";
                break;
            case "M":
                ageGuidance = "Mature 17+: suitable for ages 17 and up";
                break;
            case "A":
                ageGuidance = "Adults Only 18+: suitable only for adults ages 18 and up";
                break;
            case "Unrated":
                ageGuidance = "This video game is not rated. Unknown whether it may contain mature content";
                break;
            case "Other":
                ageGuidance = "This video game has an \"Other\" rating or rating is unknown.";
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