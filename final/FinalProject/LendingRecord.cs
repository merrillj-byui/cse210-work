/*
Single responsibility: To represent a loan to a borrower (not derived).

LendingRecord (not derived)
---------------------------
Attributes
    _borrower : Borrower
    _borrowedDate : DateTime
    _returnedDate : DateTime
    _dueDate : DateTime
    _returned : bool
Methods
    LendingRecord(Borrower)
    SetBorrower(Borrower) : void
    GetBorrower() : Borrower
    SetBorrowedDate(DateTime) : void
    GetBorrowedDate() : DateTime
    SetReturnedDate() : DateTime
    GetReturnedDate() : DateTime
    SetDueDate(DateTime) : void
    GetDueDate() : DateTime
    Return() : void
    IsReturned() : bool
*/

using Microsoft.VisualBasic;

public class LendingRecord
{
    // Attributes
    private Borrower _borrower;
    private DateTime _borrowedDate;
    private DateTime _returnedDate;
    private DateTime _dueDate = DateTime.Now.AddDays(14);
    private bool _returned = false;


    // Constructors
    public LendingRecord(Borrower borrower)
    {
        _borrower = borrower;
        _dueDate = DateTime.Now.AddDays(14);
    }


    // Setters and Getters
    public void SetBorrower(Borrower borrower)
    {
        _borrower = borrower;
    }
    
    public Borrower GetBorrower()
    {
        return _borrower;
    }
    
    public void SetBorrowedDate(DateTime date)
    {
        _borrowedDate = date;
    }  
    
    public DateTime GetBorrowedDate()
    {
        return _borrowedDate;
    }

    public void SetReturnedDate(DateTime date)
    {
        _returnedDate = date;
    }

    public DateTime GetReturnedDate()
    {
        return _returnedDate;
    }
        
    public void SetDueDate(DateTime date)
    {
        _dueDate = date;
    }    
    
    public DateTime GetDueDate()
    {
        return _dueDate;
    }

    public void SetReturned(bool returned)
    {
        _returned = returned;
    }
   
       
    // Other methods
    public void Return()
    // Marks the media returned with today's date
    {
        _returnedDate = DateTime.Now;
        _returned = true;
    }

    public bool IsReturned()
    // returns whether the media is returned (true / false)
    {
        return _returned;
    }
}