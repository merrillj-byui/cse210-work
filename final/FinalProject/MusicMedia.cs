/*
Media (base)
  ^
MusicMedia
----------
Attributes
    _explicitLyrics : bool
    _artist : string
Methods
    MusicMedia(string, bool)
    SetExplicit(bool) : void
    UnsetExplicit() : void
    IsExplicit() : bool
    SetArtist(string) : void
    GetArtist() : string
    GetListing() : string <override>
    GetDetail() : string <override>
    GetInfoString() : string <override>
    GetData() : MediaConvertObject
    SetData(MediaConvertObject) : void
*/


using System.Reflection;

public class MusicMedia : Media
{
    // Attributes
    private bool _explicitLyrics = false;
    private string _artist = "";

    
    // Constructors
    public MusicMedia (string title, bool explicitLyrics) : base (title, "Music")
    {
        _explicitLyrics = explicitLyrics;
    }


    // Setters and Getters
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

    public void SetArtist(string artist)
    {
        _artist = artist;
    }

    public string GetArtist()
    {
        return _artist;
    }


    // Other Methods
    public override string GetListing()
    // Returns a string that can be used in a displayed list
    {
        string listing = $"[{GetMediaType()}] {GetTitle()}, by {GetArtist()}";
        listing += IsExplicit() ? " (EXPLICIT)" : " (clean)";
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
        if (_artist != "") 
        { 
            details += $"Artist: {_artist}\n"; 
        }
        if (GetPublishDate() > DateTime.MinValue) 
        { 
            details += $"Released on: {GetPublishDate()}\n"; 
        }
        details += $"Acquired on: {GetAcquireDate()}\n";
        if (IsOnLoan())
        {
            LendingRecord loan = GetLastLoan();
            Borrower borrower = GetLastBorrower();
            details += $"Status: On Loan to... \n{borrower.GetMailLabel()}\n";
            details += $"Phone: {borrower.GetPhone()}\n";
            details += $"Due date: {loan.GetDueDate()}\n";
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
        string info = $"{GetTitle()}," + $"{GetArtist()}," + $"{GetMediaType()}";
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
        data.explicitLyrics = IsExplicit();
        data.artist = GetArtist();
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
        SetArtist((string)mediaData.artist);
        SetAcquireDate((DateTime)mediaData.acquireDate);
        SetAvailable((bool)mediaData.available);
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