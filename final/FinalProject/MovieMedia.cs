/*
Single responsibility: to represent a movie (e.g. DVDs) as a media object

Media (base)
  ^
MovieMedia (derived from Media)
-------------------------------
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
    GetData() : MediaConvertObject
    SetData(MediaConvertObject) : void
*/


using System.Reflection;

public class MovieMedia : Media
{
    // Attributes
    private string _rating;
    private string[] _ratings = {"G", "PG", "PG-13", "R", "NC-17", "Unrated", "Other"};


    // Constructors
    public MovieMedia (string title, string rating) : base (title, "Movie")
    {
        _rating = rating;
    }


    // Setters and Getters
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


    // Other Methods
    public string GetAgeGuidance()
    // Based on the rating, returns the guidance for that rating
    {
        string ageGuidance = "Other";

        switch(GetRating())
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
    // Returns a string that can be used in a displayed list
    {
        string listing = $"[{GetMediaType()}] {GetTitle()} ({GetRating()})";
        if (IsAvailable() && !IsOnLoan()) 
        { 
            listing += " - AVAILABLE"; 
        }
        else if (IsOnLoan()) 
        { 
            listing += $" - LOANED OUT"; 
        }
        else 
        { 
            listing += $" - UNAVAILABLE: {GetLastNote()}"; 
        }
        return listing;
    }

    public override string GetDetail()
    // Returns a multi-line string containing details
    {
        string details = $"Details for: {GetTitle()}\n";
        details += $"Media type: {GetMediaType()}\n";
        details += $"Rated: {GetRating()} - {GetAgeGuidance()}\n";
        if (GetPublishDate() > DateTime.MinValue) 
        { 
            details += $"Released on: {GetPublishDate().ToString("MMMM yyyy")}\n"; 
        }
        details += $"Acquired on: {GetAcquireDate().ToString("dddd, dd MMMM yyyy")}\n";
        if (IsOnLoan())
        {
            LendingRecord loan = GetLastLoan();
            Borrower borrower = GetLastBorrower();
            details += $"Status: On Loan to... \n{borrower.GetMailLabel()}\n";
            details += $"Phone: {borrower.GetPhone()}\n";
            details += $"Due date: {loan.GetDueDate().ToString("dddd, dd MMMM yyyy")}\n";
        }
        else if (IsAvailable()) 
        { 
            details += $"Status: AVAILABLE"; 
        }
        else
        {
            details += $"Status: UNAVAILABLE\n";
            details += $"Last Note: {GetLastNote()}";
        }
        return details;
    }

    public override string GetInfoString()
    // Returns a single line string suitable for searching
    {
        string info = $"{GetTitle()}," + $"{GetRating()}," + $"{GetMediaType()}";
        return info;
    }


    public override MediaConvertObject GetData()
    // Uses the MediaConvertObject to populate this media object
    {
        Borrower borrower;
        MediaConvertObject data = new MediaConvertObject();
        data.loans = new List<LendingConvertObject>(){};
        data.mediaType = GetMediaType();
        data.title = GetTitle();
        data.publishDate = GetPublishDate();
        data.acquireDate = GetAcquireDate();
        data.available = IsAvailable();
        data.rating = GetRating();
        // Loop through the lending records and populate a list of LendingConvertObject
        foreach (LendingRecord loan in GetLoans())
        {
            borrower = loan.GetBorrower();
            LendingConvertObject loanConvert = new LendingConvertObject();
            BorrowerConvertObject borrowerConvert = new BorrowerConvertObject();
            borrowerConvert.firstName = borrower.GetFirstName();
            borrowerConvert.lastName = borrower.GetLastName();
            borrowerConvert.phone = borrower.GetPhone();
            borrowerConvert.email = borrower.GetEmail();
            borrowerConvert.streetAddress = borrower.GetStreetAddress();
            borrowerConvert.city = borrower.GetCity();
            borrowerConvert.state = borrower.GetState();
            borrowerConvert.zip = borrower.GetZip();
            loanConvert.borrower = borrowerConvert;
            loanConvert.borrowedDate = loan.GetBorrowedDate();
            loanConvert.returnedDate = loan.GetReturnedDate();
            loanConvert.dueDate = loan.GetDueDate();
            loanConvert.returned = loan.IsReturned();
            data.loans.Add(loanConvert);
        }
        data.notes = GetNotes();
        return data;
    }    


    public override void SetData(MediaConvertObject mediaData)
    // Uses the MediaConvertObject to populate this media object
    {
        SetPublishDate((DateTime)mediaData.publishDate);
        SetAcquireDate((DateTime)mediaData.acquireDate);
        SetAvailable((bool)mediaData.available);
        SetRating(mediaData.rating);
        if (mediaData.loans != null)
        {   
            foreach (LendingConvertObject loanConvert in mediaData.loans)
            {
                string fullName = $"{loanConvert.borrower.firstName} {loanConvert.borrower.lastName}";
                Borrower borrower = new Borrower(fullName);
                borrower.SetPhone(loanConvert.borrower.phone);
                borrower.SetEmail(loanConvert.borrower.email);
                borrower.SetStreetAddress(loanConvert.borrower.streetAddress);
                borrower.SetCity(loanConvert.borrower.city);
                borrower.SetState(loanConvert.borrower.state);
                borrower.SetZip(loanConvert.borrower.zip);
                LendingRecord loan = new LendingRecord(borrower);
                loan.SetBorrowedDate((DateTime)loanConvert.borrowedDate);
                loan.SetReturnedDate((DateTime)loanConvert.returnedDate);
                loan.SetDueDate((DateTime)loanConvert.returnedDate);
                loan.SetReturned((bool)loanConvert.returned);
                AddLoan(loan);

            }
        }
        SetNotes((List<string>)mediaData.notes);
    }
}