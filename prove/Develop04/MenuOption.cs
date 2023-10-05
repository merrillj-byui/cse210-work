/*
+-------------------------------------------------------+
|                      MenuOption                       |
+-------------------------------------------------------+
| _text : string                                        |
| _key : string                                         |
| _callback : MenuOptionCallback                        |
+-------------------------------------------------------+
| MenuOption(string, string, MenuOptionCallback) : void |
| GetMenuOptionText() : string                          |
| SetMenuOptionText(string) : void                      |
| GetMenuOptionKey() : string                           |
| SetMenuOptionKey(string) : void                       |
| SetMenuOptionCallback(MenuOptionCallback) : void      |
| RunMenuOption() : void                                |
+-------------------------------------------------------+
*/

using System.Reflection.Metadata.Ecma335;

// Callback delagate definition
public delegate void MenuOptionCallback();

public class MenuOption
{
    // Define private attributes
    private string _text = "";
    private string _key = "";
    private MenuOptionCallback _callback;

    // Constructors

    public MenuOption(string key, string text, MenuOptionCallback callback)
    {
        SetMenuOptionKey(key);
        SetMenuOptionText(text);
        SetMenuOptionCallback(callback);
    }

    // Methods

    public string GetMenuOptionText()
    // Getter for _text attribute
    {
        return _text;
    }

    public void SetMenuOptionText(string text)
    // Setter for _text attribute
    {
        _text = text;
    }

    public string GetMenuOptionKey()
    // Getter for _key attribute
    {
        return _key;
    }

    public void SetMenuOptionKey(string key)
    // Setter for _key attribute
    {
        _key = key;
    }

    public void SetMenuOptionCallback(MenuOptionCallback callback)
    // Setter for callback function to execute when this option is selected
    {
        _callback = callback;
    }

    public void RunMenuOption()
    // Function to execute the callback function (which function is stored privately)
    {
        _callback();
    }
}