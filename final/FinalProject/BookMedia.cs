/*
Media (base)
  ^
BookMedia
---------
Attributes
    _author : string
    _pages : int
Methods
    BookMedia(string)
    SetAuthor(string) : void
    GetAuthor() : string
    SetPages(int) : void
    GetPages() : int
    GetListing() : string <override>
    GetDetail() : string <override>
    GetInfoString() : string <override>
    GetData() : MediaConvertObject
    SetData(MediaConvertObject : void
*/


using System.Net;
using System.Reflection;
using System.Text;

public class BookMedia : Media
{
    // Attributes
    private string _author = "";
    private int _pages = 0;


    // Constructors
    public BookMedia (string title) : base (title, "Book"){}


    // Setters and Getters
    public void SetAuthor(string author)
    {
        _author = author;
    }

    public string GetAuthor()
    {
        return _author;
    }

    public void SetPages(int pages)
    {
        _pages = pages;
    }

    public int GetPages()
    {
        return _pages;
    }


    // Other Methods
    public override string GetListing()
    // Returns a string that can be used in a displayed list
    {
        string listing = $"[{GetMediaType()}] {GetTitle()}";
        listing += (GetAuthor() == "") ? "" : $" by {GetAuthor()}"; 
        listing += (GetPages() <= 0) ? "" : $"  ({GetPages()} pages)"; 
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
        details += (GetAuthor() == "") ? "" : $"Author: {GetAuthor()}\n"; 
        details += (GetPages() <= 0) ? "" : $"Pages: {GetPages()}\n"; 
        details += (GetPublishDate() == DateTime.MinValue) ? "" : $"Published on: {GetPublishDate()}\n"; 
        details += $"Acquired on: {GetAcquireDate()}\n";
        if (IsOnLoan())
        {
            LendingRecord loan = GetLastLoan();
            Borrower borrower = GetLastBorrower();
            details += $"Status: On Loan to... \n{borrower.GetMailLabel()}'\n";
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
        string info = $"{GetTitle()}, {GetAuthor()}, {GetMediaType()}";
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
        data.publishDate = GetPublishDate();
        data.acquireDate = GetAcquireDate();
        data.available = IsAvailable();
        data.author = GetAuthor();
        data.pages = GetPages();
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
        SetPublishDate((DateTime)mediaData.publishDate);
        SetAcquireDate((DateTime)mediaData.acquireDate);
        SetAvailable((bool)mediaData.available);
        SetAuthor((string)mediaData.author);
        SetPages((int)mediaData.pages);
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
