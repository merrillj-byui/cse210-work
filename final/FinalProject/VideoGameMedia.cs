/*
Single responsibility: to represent a video game as a media object.

Media (base)
  ^
VideoGameMedia (derived from Media)
-----------------------------------
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
    GetData() : MediaConvertObject
    SetData(MediaConvertObject) : void
*/



using System.Reflection;

public class VideoGameMedia : Media
{
    // Attributes
    private string _rating = "Other";
    private string[] _ratings = {"E", "E10+", "T", "M", "A", "Unrated", "Other"};

    public VideoGameMedia (string title, string rating) : base (title, "Video Game")
    {
        _rating = rating;
    }


    // Constructors
    public string[] GetAllRatings()
    {
        return _ratings;
    }


    // Setters and Getters
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


    // Other Methods
    public string GetAgeGuidance()
    // Returns the age guidance based on the rating
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
    // Returns a string that can be used in a displayed list
    {
        string listing = $"[{GetMediaType()}] {GetTitle()} ({GetRating()})";
        if (IsAvailable() && !IsOnLoan()) { listing += " - AVAILABLE"; }
        else if (IsOnLoan()) { listing += $" - LOANED OUT"; }
        else { listing += $" - UNAVAILABLE: {GetLastNote()}"; }
        return listing;
    }

    public override string GetDetail()
     // Returns a multi-line string containing details
   {
        string details = $"Details for: {GetTitle()}\n";
        details += $"Media type: {GetMediaType()}\n";
        details += $"Rating: {GetRating()} - {GetAgeGuidance()}\n";
        details += $"Acquired on: {GetAcquireDate().ToString("dddd, dd MMMM yyyy")}\n";
        if (IsOnLoan())
        {
            LendingRecord loan = GetLastLoan();
            Borrower borrower = GetLastBorrower();
            details += $"Status: On Loan to... \n{borrower.GetMailLabel()}\n";
            details += $"Phone: {borrower.GetPhone()}\n";
            details += $"Due date: {loan.GetDueDate().ToString("dddd, dd MMMM yyyy")}\n";
        }
        else if (IsAvailable()) { details += $"Status: AVAILABLE"; }
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
    // Returns a public class used for JSON serializing and deserializing
    {
        // Populate a MediaConverterObject with data from this Book media
        Borrower borrower;
        MediaConvertObject data = new MediaConvertObject();
        data.loans = new List<LendingConvertObject>(){};
        data.mediaType = GetMediaType();
        data.title = GetTitle();
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
        // Return the object ready for serialization
        return data;
    }    


    public override void SetData(MediaConvertObject mediaData)
    // Uses the MediaConvertObject to populate this media object
    {
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