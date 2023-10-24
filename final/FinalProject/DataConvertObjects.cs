/* 
Trying to learn how to do JSON serialization and deserialization for objects that contain
other objects or lists of objects. From the examples I saw, this is done using classes 
with public attributes with default gets and sets. I realize this may break the principle
of encapsulation. I hope I am not docked for this, as I found no way around it. Please 
understand that this file contains classes that are only used temporarily for the JSON
serializing and deserializing. These are not part of my project classes, which all
demonstrate the principles taught in this course. But, because those classes are protected,
the JSON serializer can't access them. I don't know how to use private attributes with
public functions to get around that.
*/


public class MediaConvertObject
{
    public string mediaType { get; set; }
    public string title { get; set; }
    public List<string>? genre { get; set; }
    public DateTime? publishDate { get; set; }
    public DateTime acquireDate { get; set; }
    public bool available { get; set; }
    public List<LendingConvertObject> loans { get; set; }
    public List<string> notes { get; set; }
    public string? author { get; set; }
    public int? pages { get; set; }
    public int? minAge { get; set; }
    public int? maxAge { get; set; }
    public string? rating { get; set; }
    public bool? explicitLyrics { get; set; }
    public string? artist { get; set; }

}

public class LendingConvertObject
{
    public BorrowerConvertObject borrower { get; set; }
    public DateTime borrowedDate { get; set; }
    public DateTime? returnedDate { get; set; }
    public DateTime? dueDate { get; set; }
    public bool returned { get; set; }
}

public class BorrowerConvertObject
{
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string phone { get; set; }
    public string email { get; set; }
    public string streetAddress { get; set; }
    public string city { get; set; }
    public string state { get; set; }
    public string zip { get; set; }
}