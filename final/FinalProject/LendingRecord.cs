/*
LendingRecord
-------------
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
    GetReturnedDate() : DateTime
    SetDueDate(DateTime) : void
    GetDueDate() : DateTime
    Return() : void
    IsReturned() : bool
*/


using Microsoft.VisualBasic;

public class LendingRecord
{
    private Borrower _borrower;
    private DateTime _borrowedDate;
    private DateTime _returnedDate;
    private DateTime _dueDate = DateTime.Now.AddDays(14);
    private bool _returned = false;


    public LendingRecord(Borrower borrower)
    {
        _borrower = borrower;
    }

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
    
    
    public void Return()
    {
        _returnedDate = DateTime.Now;
        _returned = true;
    }
   
   
    public bool IsReturned()
    {
        return _returned;
    }

}