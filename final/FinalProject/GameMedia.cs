/*
Single responsibility: To represent a game as a media object.

Media (base)
  ^
GameMedia (Derived from Media)
------------------------------
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
    GetData() : MediaConvertObject
    SetData(MediaConvertObject) : void
*/


public class GameMedia : Media
{
    // Attributes
    private int _defaultMinAge = 0;
    private int _defaultMaxAge = 99;
    private int _minAge = 0;
    private int _maxAge = 99;


    // Constructors
    public GameMedia (string title) : base (title, "Game"){}


    // Setters and Getters
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


    // Other Methods
    public override string GetListing()
    // Returns a one-line string suitable for a displayed list
    {
        string listing = $"[{GetMediaType()}] {GetTitle()}";
        if (GetMinAge() == _defaultMinAge && GetMaxAge() == _defaultMaxAge) 
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
            listing += $" - LOANED OUT"; 
        }
        else 
        { 
            listing += $" - UNAVAILABLE: {GetLastNote()}"; 
        }
        return listing;
    }

    public override string GetDetail()
    // Returns a multi-line string with the details about the media
    {
        string details = $"Details for: {GetTitle()}\n";
        details += $"Media type: {GetMediaType()}\n";
        if (GetMinAge() >= _defaultMinAge || GetMaxAge() <= _defaultMaxAge) 
        { 
            details += $"Ages: {GetMinAge()} to {GetMaxAge()}\n"; 
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
    // Returns a string suitable for searches
    {
        return base.GetInfoString();
    }

    public override MediaConvertObject GetData()
    // Returns a public class used for JSON serializing and deserializing
    {
        // Populate a MediaConverterObject with data from this Game media
        Borrower borrower;
        MediaConvertObject data = new MediaConvertObject();
        data.loans = new List<LendingConvertObject>(){};
        data.mediaType = GetMediaType();
        data.title = GetTitle();
        data.acquireDate = GetAcquireDate();
        data.available = IsAvailable();
        data.minAge = GetMinAge();
        data.maxAge = GetMaxAge();
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
        SetMinAge((int)mediaData.minAge);
        SetMaxAge((int)mediaData.maxAge);
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