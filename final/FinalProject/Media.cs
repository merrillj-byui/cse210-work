/*
Single responsibility: To be the parent object to represent a generic media object.

Media <abstract> (not derived)
------------------------------
Attributes
    _mediaType : string
    _title : string
    _genre : List<string>
    _publishDate : DateTime
    _acquireDate : DateTime
    _available : bool
    _loans : List<LendingRecord>
    _notes : List<string>
Methods
    Media(string)
    GetMediaType() : string <abstract>
    SetTitle(string) : void
    GetTitle() : string
    AddGenre(string) : void
    GetGenre() : List<string>
    SetPublishDate(DateTime) : void
    GetPublishDate() : DateTime
    SetAcquireDate(DateTime) : void
    GetAcquireDate() : DateTime
    SetAvailable(bool) : void
    IsAvailable() : bool
    NewLoan(Borrower) : void
    Return() : void
    AddLoan(LendingRecord) : void
    GetLastLoan() : LendingRecord
    GetLoans() : List<LendingRecord>
    GetBorrower(int) : Borrower
    GetLastBorrower() : Borrower
    IsOnLoan : bool
    GetListing() : string <virtual>
    GetDetail() : string <virtual>
    GetInfoString() : string <virtual>
    GetdDataDictionary() : Dictionary<string, object>
    AddNote(string) : void
    GetLastNote() : string
    GetNotes() : string[]
*/

using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;

public abstract class Media
{
    // Attributes
    private string _mediaType;
    private string _title;
    private List<string> _genre = new List<string>{};
    private DateTime _publishDate;
    private DateTime _acquireDate;
    private bool _available;
    private List<LendingRecord> _loans = new List<LendingRecord>{};
    private List<string> _notes = new List<string>{};


    // Constructors
    public Media(string title, string mediaType)
    {
        _mediaType = mediaType;
        _title = title;
        _publishDate = DateTime.MinValue;
        _acquireDate = DateTime.Now;
        _available = true;
    }


    // Setters and Getters
    public  string GetMediaType()
    {
        return _mediaType;
    }

    public void SetTitle(string title)
    {
        _title = title;
    }

    public string GetTitle()
    {
        return _title;
    }

    public void AddGenre(string genre)
    {
        _genre.Add(genre);
    }

    public List<string> GetGenre()
    {
        return  _genre;
    }   
    
    public void SetPublishDate(DateTime publishDate)
    {
        _publishDate = publishDate;
    }   
    
    public DateTime GetPublishDate()
    {
        return _publishDate;
    }   
    
    public void SetAcquireDate(DateTime acquireDate)
    {
        _acquireDate = acquireDate;
    }
       
    public DateTime GetAcquireDate()
    {
        return _acquireDate;
    }

    public void SetAvailable(bool available)
    {
        _available = available;
    }


    // Other methods
    public bool IsAvailable()
    {
        return _available;
    }   
    
    public void NewLoan(Borrower borrower)
    {
        if (IsAvailable() && !IsOnLoan())
        {
            LendingRecord lendRec = new LendingRecord(borrower);
            _loans.Add(lendRec);
        }
    }
    
    public void Return()
    {
        if (IsOnLoan())
        {
            _loans[_loans.Count - 1].Return();
        }
    }

    public void AddLoan(LendingRecord loan)
    {
        _loans.Add(loan);
    }

    public LendingRecord GetLastLoan()
    {
        return _loans.Count > 0 ? _loans[_loans.Count - 1] : null;
    }

    public List<LendingRecord> GetLoans()
    {
        return _loans;
    }   
    
    public Borrower GetBorrower()
    {
        return !IsOnLoan() ? GetLastBorrower() : null;
    }    
    
    public Borrower GetLastBorrower()
    {
        return _loans[_loans.Count - 1].GetBorrower();
    }

     public bool IsOnLoan()
    {
        if (_loans.Count == 0)
        {
            return false;
        }
        else if (_loans[_loans.Count - 1].IsReturned())
        {
            return false;
        }
        else
        {
            return true;
        }
    }


    public virtual string GetListing()
    // Returns a string that can be used in a displayed list
    {
        string listing = $"[{GetMediaType()}] {GetTitle()}";
        if (GetPublishDate() != DateTime.MinValue)
        {
            listing += $" ({GetPublishDate().Year})";
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

    public virtual string GetDetail()
    // Returns a multi-line strin containing details
    {
        string details = $"Details for: {GetTitle()}\n";
        details += $"Media type: {GetMediaType()}\n";
        if (GetPublishDate() > DateTime.MinValue) 
        { 
            details += $"Published: {GetPublishDate().ToString("MMMM yyyy")}\n"; 
        }
        details += $"Acquired: {GetAcquireDate().ToString("dddd, dd MMMM yyyy")}\n";
        if (IsOnLoan())
        {
            Borrower borrower = GetLastBorrower();
            details += $"Status: On Loan to... \n{borrower.GetMailLabel()}\n";
            details += $"Phone: {borrower.GetPhone()}";
            details += $"Due date: {_loans[_loans.Count -1].GetDueDate().ToString("dddd, dd MMMM yyyy")}";
        }
        else if (IsAvailable())
        {
            details += $"Status: AVAILABLE";
        }
        else
        {
            details += $"Status: UNAVAILABLE";
            details += $"Last Note: ${GetLastNote()}";
        }
        return details;
    }

    public virtual string GetInfoString()
    // Returns a single line string suitable for searching
    {
        string info = $"{GetTitle()}," + $"{GetMediaType()}";
        return info;
    }


    public abstract MediaConvertObject GetData();

    public abstract void SetData(MediaConvertObject mediaData);
    
    
    public void AddNote(string note)
    {
        _notes.Add(note);
    }


    public string GetLastNote()
    {
        return _notes[_notes.Count -1];
    }


    public void SetNotes(List<string> notes)
    {
        _notes = notes;
    }
    
    
    public List<string> GetNotes()
    {
        return _notes;
    }
}