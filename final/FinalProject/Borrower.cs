/*
Borrower
--------
Attributes
    _firstName : string
    _lastName : string
    _phoneNumber : string
    _email : string
    _streetAddress : string
    _city : string
    _state : string
    _zip : string
Methods
    Borrower(string)
    Borrower(string, string)
    SetName(string, string) : void
    GetFullName() : string
    GetFirstName() : string
    GetLastName() : string
    GetFullNameLastFirst() : string
    SetPhone(string) : void
    GetPhone() : string
    SetEmail(string) : void
    GetEmail() : string
    SetStreetAddress(string): : void
    GetStreetAddress() : string
    SetCity(string) : void
    GetCity() : string
    SetState(string) : void
    GetState() : string
    SetZip(string) : void
    GetZip() : string
    GetFullAddress() : string
    GetMailLabel() : string
*/

public class Borrower
{
    // Attributes
    private string _firstName;
    private string _lastName;
    private string _phoneNumber;
    private string _email;
    private string _streetAddress;
    private string _city;
    private string _state;
    private string _zip;


    // Constructors
    public Borrower(string fullName)
    {
        string[] names = fullName.Split(" ");
        _firstName = names[0];
        _lastName = names[1];
    }

    public Borrower(string firstName, string lastName)
    {
        _firstName = firstName;
        _lastName = lastName;
    }


    // Setters and Getters
    public void SetName(string firstName, string lastName)
    {
        _firstName = firstName;
        _lastName = lastName;
    }

    public string GetFullName()
    {
        return $"{GetFirstName()} {GetLastName()}";
    }
    
    public string GetFirstName()
    {
        return _firstName;
    }

    public string GetLastName()
    {
        return _lastName;
    }
    
    public string GetFullNameLastFirst()
    {
        return $"{GetLastName()}, {GetFirstName()}";
    }

    public void SetPhone(string phoneNumber)
    {
        _phoneNumber = phoneNumber;
    }
    
    public string GetPhone()
    {
        return _phoneNumber;
    }

    public void SetEmail(string email)
    {
        _email = email;
    }

    public string GetEmail()
    {
        return _email;
    }
    
    public void SetStreetAddress(string streetAddress)
    {
        _streetAddress = streetAddress;
    }

    public string GetStreetAddress()
    {
        return _streetAddress;
    }    

    public void SetCity(string city)
    {
        _city = city;
    }

    public string GetCity()
    {
        return _city;
    }   

    public void SetState(string state)
    {
        _state = state;
    }    

    public string GetState()
    {
        return _state;
    }   

    public void SetZip(string zip)
    {
        _zip = zip;
    }    

    public string GetZip()
    {
        return _zip;
    }    

    // Other Methods
    public string GetFullAddress()
    {
        return $"{GetStreetAddress()}\n{GetCity()}, {GetState()} {GetZip()}";
    }
    
    public string GetMailLabel()
    {
        return $"{GetFullName()}\n{GetFullAddress()}";
    }

}