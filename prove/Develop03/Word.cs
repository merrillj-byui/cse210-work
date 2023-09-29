using System.Dynamic;

/*
  Class: Word

  +----------------------------+
  |          Word              |
  +----------------------------+
  |  _word : string            |
  |  _hidden " bool            |
  +----------------------------+
  |  Word() : void             |
  |  Word(string) : void       |
  |  SetWord(string) : void    |
  |  GetWord() : string        |
  |  GetDisplayWord() : string |
  |  Hide() : void             |
  |  Unhide() : void           |
  +----------------------------+
*/

public class Word
{
    private string _word;
    private bool _hidden;

    public Word()
    {
        _word = "";
        _hidden = false;
    } 

    public Word(string word)
    {
        _word = word;
        _hidden = false;
    }

    public void SetWord(string word)
    // Sets the word text
    {
        _word = word;
    }

    public string GetWord()
    // Returns the actual word, regardless if it is hidden
    {
        return _word;
    }

    public string GetDisplayWord()
    // Returns the word if unhidden, or underscores if hidden
    {
        // If it is hidden, return a string of underscores the same length as the word
        if (_hidden)
        {
            return new string('_', _word.Length);
        }
        // Otherwise, just return the word
        else
        {
            return _word;
        }
    }

    public void Hide()
    // Sets the hiddden state
    {
        _hidden = true;
    }

    public void Unhide()
    // Unsets the hidden state (make visible)
    {
        _hidden = false;
    }

    public bool isHidden()
    // Returns true if the word is hidden
    {
        return _hidden;
    }
}