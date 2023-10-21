/*
MediaManager.cs
Attributes
    _medias : List<Media>
    _borrowers : List<Borrower>
    _mainMenu : string
    _mainOptions : string[]
    _mainQuit : string
    _mediaTypes : string[]
Methods
    Run() : void
    ChoosMedia() : string
    ListMedia(string, string) : int
    ListBorrowers() : int
    NewMedia() : void
    LoanMedia() : void
    ReturnMedia() : void
    LoadMedia() : void
    SaveMedia() : void
*/

using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;
using System.Text.Json;
using System.IO.Enumeration;

public class MediaManager
{
    private List<Media> _medias = new List<Media>{};
    private List<Borrower> _borrowers = new List<Borrower>{};

    // Define the main menu attributes
    private string _mainMenu = "Media Manager\n" +
                        "-------------\n" +
                        "  1. List Media\n" +
                        "  2. List Borrowers\n" +
                        "  3. New Media\n" +
                        "  4. Loan Media\n" +
                        "  5. Return Media\n" +
                        "  6. Load\n" +
                        "  7. Save\n" +
                        "Type 'quit' to exit\n";
    private string[] _mainOptions = {"1", "2", "3", "4", "5", "6", "7", "quit"};
    private string _mainQuit = "quit";
    
    // private string _mediaMenu = "Choose a media type:\n" +
    //                     "1. Book\n" +
    //                     "2, Game\n" +
    //                     "3. Movie\n" +
    //                     "4. Music/CD\n" +
    //                     "5. Video Game\n";
    // private string[] _mediaOptions = {"1", "2", "3", "4", "5", ""};
    private string[] _mediaTypes = {"Book", "Game", "Movie", "Music", "Video Game", "All"};

    public void Run()
    {
        // Define the main menu
        Menu mainMenu = new Menu(_mainMenu, _mainOptions);

        // Loop until the user chooses to quit
        string menuChoice = "";
        while (menuChoice != _mainQuit)
        {   
            int index;

            // Display the main menu and get the user's chosen option
            Console.Clear();
            mainMenu.DisplayMenu();
            menuChoice = mainMenu.GetOptionChoice();

            // Take action, based on the user's chosen option
            switch(menuChoice)
            {
                // List Media
                case "1":
                    if (_medias.Count > 0)
                    {   
                        index = ListMedia(ChooseMediaType());
                        if (index > 0) { DisplayDetails(index); }
                    }
                    else
                    {
                        Console.WriteLine("\nNothing to list");
                        Thread.Sleep(3000);
                    }
                    break;
                
                // List Borrowers
                case "2":
                    if (_borrowers.Count > 0)
                    {
                        ListBorrowers();
                    }
                    else
                    {
                        Console.WriteLine("\nNo borrowers to list");
                        Thread.Sleep(3000);
                    }
                    break;

                // New Media
                case "3":
                    NewMedia();
                    break;

                // Loan Media
                case "4":
                    LoanMedia();
                    break;

                // Return Media                
                case "5":
                    ReturnMedia();
                    break;

                // Load Media
                case "6":
                    LoadMedia();
                    break;

                // Save Media
                case "7":
                    SaveMedia();
                    break;
            }
        }
    }


    public string ChooseMediaType()
    {
        // Define the media selection menu
        // Menu mediaTypeMenu = new Menu(_mediaMenu, _mediaOptions);

        // Display a list of media types
        int typeIndex = 0;
        Console.WriteLine("\nAvailable media types:");
        foreach (string mediaType in _mediaTypes)
        {
            Console.WriteLine($"  {++typeIndex}. {_mediaTypes[typeIndex - 1]}");
        }

        // Get the user's selection
        string choice = "";
        do
        {
            Console.Write("Choose a media type: ");
            choice = Console.ReadLine();
            int.TryParse(choice, out typeIndex);
        } while (typeIndex < 1 || typeIndex > _mediaTypes.Length);

        return _mediaTypes[typeIndex - 1];
    }

    
    public int ListMedia(string type = "All", string searchString = "")
    {
        if (!_mediaTypes.Contains(type)) {type = "All";}

        if (searchString == "")
        {   
            Console.Write("You may enter a search string, or press Enter: ");
            searchString = Console.ReadLine();
        }

        Console.Clear();
        //Console.WriteLine($"Listing of {type} media, up to 10 at a time.\n");
        int index = 0;
        int count = 0;
        string response = "";
        string detail = "";
        
        foreach (Media media in _medias)
        {
            if (type == "All" || type == media.GetMediaType())
            {
                detail = media.GetInfoString();
                if (detail.Contains(searchString) || searchString == "")
                {
                    count++;
                    if (count == 1)
                    {
                        Console.Clear();
                        Console.WriteLine("Listing up to 20 at a time:");
                    }
                    else if (count % 20 == 1)
                    {
                        Console.Clear();
                        Console.WriteLine("Listing continues...up to 20 at a time.");
                    }   

                    Console.WriteLine($"  {index + 1}. {media.GetListing()}");

                    if (count % 20 == 0)
                    {
                        Console.Write("Press Enter to continue, or enter a number, or enter 'q' to quit: ");
                        response = Console.ReadLine();
                        if (response != ""){ break; }
                    }
                }
            }
            index ++;
        }
        if (count == 0)
        {
            Console.WriteLine("\nNothing to list.");
            Thread.Sleep(3000);
        }
        else if (count % 20 != 0)
        {
            Console.Write("Press Enter to continue, or enter a number: ");
            response = Console.ReadLine();
        }
        
        int.TryParse(response, out int retval);
        index = retval - 1;
        return index;
    }


    public int ListBorrowers()
    {
        int index = 0;

        // foreach 

        return index;
    }

    public void NewMedia()
    {
        string type = "";
        
        type = ChooseMediaType();

        if (type == "")
        {
            Console.WriteLine("\nNo type selected. Returning.");
            Thread.Sleep(4000);
        }
        else
        {
            Console.Write($"\nEnter the title of the new {type}: ");
            string title = ""; 
            
            while (title == "") {title = Console.ReadLine();}

            switch(type)
            {
                case "Book":
                    BookMedia book = new BookMedia(title);
                    DateTime bookDate;
                    Console.Write($"When was {book.GetTitle()} published (month and day)? ");
                    DateTime.TryParse(Console.ReadLine(), out bookDate);
                    book.SetPublishDate(bookDate);
                    _medias.Add(book);
                    break;

                case "Game":
                    _medias.Add(new GameMedia(title));
                    break;

                case "Movie":
                    string movieMenuText = "What is the movie rating: G, PG, PG-13, NC-17, R, Unrated, or Other?";
                    string[] movieMenuOptions = {"G", "PG", "PG-13", "NC-17", "R", "Unrated", "Other"};
                    string movieMenuPrompt = "Enter Movie Rating: ";
                    Menu movieRatingsMenu = new Menu(movieMenuText, movieMenuOptions, movieMenuPrompt);
                    movieRatingsMenu.DisplayMenu();
                    string movieRating = movieRatingsMenu.GetOptionChoice();
                    _medias.Add(new MovieMedia(title, movieRating));
                    break;

                case "Music":
                    Console.Write($"Does {title} contain explicit lyrics (y/n)? ");
                    string[] choices = {"y", "Y", "yes", "Yes", "YES"};
                    string choice = Console.ReadLine();
                    bool explicitLyrics = choices.Contains(choice) ? true : false;
                    _medias.Add(new MusicMedia(title, explicitLyrics));
                    break;

                case "Video Game":
                    string videoGameMenuText = "What is the ESRB rating: E, E10+, T, M, A, Unrated, or Other?";
                    string[] videoGameMenuOptions = {"E", "E10+", "T", "M", "A", "Unrated", "Other"};
                    string videoGameMenuPrompt = "Enter ESRB Rating: ";
                    Menu videoGameRatingsMenu = new Menu(videoGameMenuText, videoGameMenuOptions, videoGameMenuPrompt);
                    videoGameRatingsMenu.DisplayMenu();
                    string videoGameRating = videoGameRatingsMenu.GetOptionChoice();
                    _medias.Add(new VideoGameMedia(title, videoGameRating));
                    break;
            }
        }
    }


    public void LoanMedia()
    {
        Console.WriteLine("Choose the media type you are loaning out.");
        string type = "";
        int selection = 0;
        
        type = ChooseMediaType();

        if (type == "")
        {
            Console.WriteLine("No type selected. Returning.");
            Thread.Sleep(4000);
        }
        else
        {            
            Console.Write("If you would like to include a search string, enter it here: ");
            string searchString = Console.ReadLine();
            Console.WriteLine("Select the item you would like to loan.");
            selection = ListMedia(type, searchString);
        }
        // Select a borrower
        // Create a lending record for that borrower and attach to the media's lending list
    }


    public void ReturnMedia()
    {

    }


    public void LoadMedia()
    {

    }


    public void SaveMedia()
    {
        Dictionary<string, object>[] data = new Dictionary<string, object>[_medias.Count];

        for (int i = 0; i < _medias.Count; i++)
        {
            data[i] = _medias[i].GetDataDictionary();
        }
        string filename = "media.json";
        string jsonString = JsonSerializer.Serialize(data);

        using (StreamWriter outFile = new StreamWriter(filename))
        {
            outFile.WriteLine(jsonString);
        }
        //Console.Write(jsonString);
        //Console.ReadLine();
    }

    public void DisplayDetails(int index)
    {
        Console.Clear();
        Console.WriteLine(_medias[index].GetDetail());
        Console.Write("\nPress Enter to continue.");
        Console.ReadLine();
    }
}