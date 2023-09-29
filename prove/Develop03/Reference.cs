
/*
  Class: Reference

  +----------------------------------------------+
  |                   Reference                  |
  +----------------------------------------------+
  |  _book : string                              |
  |  _chapter : int                              |
  |  _firstVerse: int                            |
  |  _lastVerse : int                            |
  +----------------------------------------------+
  |  Reference(string)                           |
  |  Reference(string, int, int, int) : void     |
  |  SetReference(string, int, int, int) : void  |
  |  GetReference() : string                     |
  +----------------------------------------------+
*/

public class Reference
{
    private string _book;
    private int _chapter;
    private int _firstVerse;
    private int _lastVerse;

    public Reference(string book, int chapter, int firstVerse, int lastVerse=0)
    {
        SetReference(book, chapter, firstVerse, lastVerse);
    }

    public Reference(string reference)
    {
        // These variables need to be parsed from the string before the class attributes are set
        string book = "";
        int num = 0;
        int chapter = 0;
        int firstVerse = 0;
        int lastVerse = 0;
        int index = 0;

        // Split the reference into an array
        string[] refParts = reference.Split(' ', ':', '-');

        // Determine the book and advance the index to the chapter
        if (int.TryParse(refParts[index], out num))
        // If the first field is a number, it must be a book number so grab that and the second field
        {
            book = refParts[index] + " " + refParts[index + 1];
            index += 2;
        }
        // Otherwise, just grab the first field as the book
        else{
            book = refParts[index++];
        }
        
        // If subsequent fields are not numeric, they must be part of the book name so grab them too
        while (!int.TryParse(refParts[index], out num))
        {
            book += " " + refParts[index++];
        }
        
        // The next field is the chapter
        int.TryParse(refParts[index++], out chapter);

        // The next field is the starting verse
        int.TryParse(refParts[index++], out firstVerse);

        // If there is another field, it is the last verse
        if (index < refParts.Length)
        {
            int.TryParse(refParts[index], out lastVerse);
        }

        SetReference(book, chapter, firstVerse, lastVerse);
    }

    public void SetReference(string book, int chapter, int firstVerse, int lastVerse=0)
    // Setter for Reference
    {
        _book = book;
        _chapter = chapter;
        _firstVerse = firstVerse;
        _lastVerse = lastVerse;
 
    }

    public string GetReference()
    // Getter for Reference
    {
        // If the last verse is greater than 0, return the with the last verse number
        if (_lastVerse > 0)
        {
            return $"{_book} {_chapter}:{_firstVerse}-{_lastVerse}";
        }
        // Otherwise, return with just the first verse number (single verse reference)
        else
        {
            return $"{_book} {_chapter}:{_firstVerse}";
        }
    }
}