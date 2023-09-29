/*
  Class: Scripture

  +-------------------------------------------+
  |              Scripture                    |
  +-------------------------------------------+
  |  _words : List<Word>                      |
  |  _reference : Reference                   |
  +-------------------------------------------+
  |  Scripture(string, Reference) : void      |
  |  HideRandomWord() : int                   |
  |  UnhideAll() : void                       |
  |  CountHidden() : int                      |
  |  CountVisible() : int                     |
  |  GetScripture() : string                  |
  |  DisplayScripture() : void                |
  +-------------------------------------------+
*/

public class Scripture
{
    private List<Word> _words = new List<Word>();
    private Reference _reference;

    public Scripture(string reference, string text)
    {
        _reference = new Reference(reference);
        string[] wordlist = text.Split();
        foreach (string word in wordlist)
        {
            _words.Add(new Word(word));
        }
        Console.WriteLine();
    }

    public int HideRandomWord()
    // Marks a random word in the scripture as hidden
    {
        // To be sure we don't randomly select a word that is already hidden, build
        // a list of words that are not hidden to randomly select
        List<int> visibleWords = new List<int>();
        for (int i=0; i < _words.Count; i++)
        {
            if (!_words[i].isHidden())
            {
                visibleWords.Add(i);
            }
        }
        // Test whether all the words are already hidden (the list of unhidden word indexes is empty)
        if (visibleWords.Count > 0)
        {
            // Select a random word from the words that are not yet hidden
            var rand = new Random();
            int index = visibleWords[rand.Next(0, visibleWords.Count - 1)];
            _words[index].Hide();
            // Return the word number (1 based) so the caller can know which word was hidden
            return index + 1;
        }
        else
        {
            // Otherwise, just return 0, which tells the caller that all words are already hidden
            return 0;
        }
    }

    public void UnhideAll()
    // Mark all the words as not hidden
    {
        foreach (Word word in _words)
        {
            word.Unhide();
        }
    }

    public int CountHidden()
    // Returns a count of how many of the words are hidden.
    {
        int count = 0;
        // loop through the list and count the number of not hidden words
        for (int i=0; i < _words.Count; i++)
        {
            if (_words[i].isHidden())
            {
                count++;
            }
        }
        //  return the count of not hidden words
        return count;
    }

    public int CountVisible()
    // Returns a count of how many of the words are not yet hidden
    {
        int count = CountHidden();
        return _words.Count - count;
    }
    
    public string GetScripture()
    // Returns a string consisting of the reference and scripture text
    {
        string text = "[" + _reference.GetReference() + "] ";
        foreach (Word word in _words)
        {
            text += word.GetDisplayWord() + " ";
        }
        return text;
    }

    public void DisplayScripture()
    // Displays the scripture to the console
    { 
        string text = GetScripture();
        Console.WriteLine(text);
    }
}