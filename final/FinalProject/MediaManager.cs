/*
MediaManager.cs
Attributes
    _filename : string
    _medias : List<Media>
    _borrowers : List<Borrower>
    _mainOptions : string[]
    _mediaTypes : string[]
    _movieMenuOptions : string[]
    _videoGameMenuOptions : string[]
    _mainMenu : Menu
    _mediaTypeMenu : Menu
    _movieRatingsMenu : Menu
    _videoGameRatingsMenu : Menu
Methods
    MediaManager() : void
    Run() : void
    ChoosMediaType() : string
    ListMedia(string, string) : int
    ListBorrowers() : int
    NewBorrower() : void
    NewMedia() : void
    LoanMedia() : void
    ReturnMedia() : void
    AnnotateMedia() : void
    LoadMedia() : void
    SaveMedia() : void
    DisplayDetails(int) : void
*/

using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;
using System.Text.Json;
using System.IO.Enumeration;
using System.Text.Json.Serialization;

public class MediaManager
{
    // Attributes
    private string _filename = "media.data";

    private List<Media> _medias = new List<Media>{};
    private List<Borrower> _borrowers = new List<Borrower>{};

    // Define the main menu attributes
    private string[] _mainMenuOptions = {"List Media", 
                                     "List Borrowers", 
                                     "New Media", 
                                     "Loan Media", 
                                     "Return Media", 
                                     "Annotate Media", 
                                     "Load from File", 
                                     "Save to File", 
                                     "Quit"};    
    private string[] _mediaTypes = {"Book", "Game", "Movie", "Music", "Video Game", "All"};
    private string[] _movieMenuOptions = {"G", "PG", "PG-13", "NC-17", "R", "Unrated", "Other"};
    private string[] _videoGameMenuOptions = {"E", "E10+", "T", "M", "A", "Unrated", "Other"};

    // Define menus
    private Menu _mainMenu;
    private Menu _mediaTypeMenu;
    private Menu _movieRatingsMenu;
    private Menu _videoGameRatingsMenu;


    // Constructors
    public MediaManager()
    { 
        // Instantiate menus
        _mainMenu = new Menu(_mainMenuOptions);
        _mediaTypeMenu = new Menu(_mediaTypes);
        _movieRatingsMenu = new Menu(_movieMenuOptions);
        _videoGameRatingsMenu  = new Menu(_videoGameMenuOptions);

    }


    // Methods
    public void Run()
    // The main execution, with the main menu handling
    {
        // Loop until the user chooses to quit
        string menuChoice = "";
        while (menuChoice != "Quit")
        {   
            int index;

            // Display the main menu and get the user's chosen option
            Console.Clear();
            _mainMenu.DisplayMenu();
            menuChoice = _mainMenu.GetOptionChoice();

            // Take action, based on the user's chosen option
            switch(menuChoice)
            {
                case "List Media":
                    index = ListMedia(ChooseMediaType());
                    if (index >= 0) 
                    { 
                        DisplayDetails(index); 
                        Console.Write("Press Enter to continue");
                    }
                    break;
                
                case "List Borrowers":
                    index = ListBorrowers();
                    if (index >= 0)
                    {
                        Console.Clear();
                        Console.WriteLine(_borrowers[index].GetMailLabel());
                        Console.Write("\nPress Enter to continue.");
                        Console.ReadLine();
                    }
                    break;

                case "New Media":
                    NewMedia();
                    break;

                case "Loan Media":
                    LoanMedia();
                    break;

                case "Return Media":
                    ReturnMedia();
                    break;

                case "Annotate Media":
                    AnnotateMedia();
                    break;

                case "Load from File":
                    LoadMedia();
                    break;

                case "Save to File":
                    SaveMedia();
                    break;                    
            }
        }
    }

    public string ChooseMediaType()
    // Lists the media types and returns the selected type
    {
        // Display a list of media types
        _mediaTypeMenu.DisplayMenu();

        // Return the selection
        return _mediaTypeMenu.GetOptionChoice();
    }

    public int ListMedia(string type = "All", string searchString = "")
    // Lists the media, with search capability, paginated by 20, and returns selected media index
    {
        int index = 0; // index for the item that is displayed, or selected
        int count = 0; // Count of what is actually diaplayed, used for pagination
        string response = "";
        
        if (_medias.Count == 0 || !_mediaTypes.Contains(type))
        {
            Console.WriteLine("\nNo media to list. (returning)");
            Thread.Sleep(3000);
        }
        else
        {
            // Give an option to display media that matches a search string
            if (searchString == "")
            {   
                Console.Write("\nYou may enter a search string, or press Enter: ");
                searchString = Console.ReadLine();
            }

            // Loop through the media list
            foreach (Media media in _medias)
            {
                // If the media is the type we want to list and it matches the search string (if there is one)
                if ( (type == "All" || type == media.GetMediaType()) && 
                     (searchString == "" || media.GetInfoString().ToLower().Contains(searchString.ToLower())) )
                {
                    count++;
                    // About to display the first listing
                    if (count % 20 == 1)
                    {
                        Console.Clear();
                        Console.WriteLine("Listing up to 20 at a time...");
                    }   
                    // Display the listing
                    Console.WriteLine($"  {index + 1}. {media.GetListing()}");
                    // Just displayed the last listing in pagination or list
                    if (count % 20 == 0)
                    {
                        Console.Write("Enter a number, or press Enter to continue: ");
                        response = Console.ReadLine();
                        // If something was entered, break the loop
                        if (response != ""){ break; }
                    }
                }
               // Always increment the index, whether or not it matches the search string
                index ++;
            }
            if (count %20 != 0)
            {
                Console.Write("Enter a number, or press Enter to continue: ");
                response = Console.ReadLine();
            }
         }    
        if (response == ""){index = 0;}
        else {int.TryParse(response, out index);}
        return index - 1; // adjusting for 0-base index
    }

    public int ListBorrowers()
    // Displays a list of borrowers, paginated by 20, and returns the index of selected
    {
        int index = 0;
        int count = 0;
        string response = "";

        if (_borrowers.Count == 0)
        {
            NewBorrower();
            index = 1;
        }
        else
        {
            Console.Write("\nYou may enter a search string, or press Enter: ");
            string searchString = Console.ReadLine();

            foreach (Borrower borrower in _borrowers)
            {
                if (searchString == "" || borrower.GetFullName().ToLower().Contains(searchString.ToLower()))
                {
                    count++;
                    // If we are about to display the first line of pagination...
                    if (count % 20 == 1)
                    {
                        Console.Clear();
                        Console.WriteLine("Listing up to 20 at a time.\n");
                    }
                    // Display borrower information on  a single numbered line (number is index)
                    Console.WriteLine($"  {index + 1} {borrower.GetFullName()}, " +
                                      $"{borrower.GetStreetAddress()}, " +
                                      $"{borrower.GetCity()}, " +
                                      $"{borrower.GetState()}");
                    // If just displayed last line in pagination...
                    if (count %20 == 0)
                    {
                        Console.Write("Enter a number, or \"new\", or press Enter to continue. " );
                        response = Console.ReadLine();
                        if (response != ""){break;}
                    }
                }
                index++;
            }
            if (count %20 != 0)
            {
                Console.Write("Enter a number, or \"new\", or press Enter to continue. " );
                response = Console.ReadLine();
            }
            if (response == "new")
            {
                NewBorrower();
                index = _borrowers.Count;
            }
            else if (response == ""){index = 0;}
            else {int.TryParse(response, out index);}
        }
        return index - 1;
    }

    public void NewBorrower()
    // Creates a new borrower and adds it to the list of borrowers
    {
        Console.Write("Enter the borrower's first name: ");
        string firstName = Console.ReadLine();
        Console.Write("Enter the borrower's first name: ");
        string lastName = Console.ReadLine();
        Borrower borrower = new Borrower(firstName, lastName);
        Console.Write("Street Address: ");
        borrower.SetStreetAddress(Console.ReadLine());
        Console.Write("City: ");
        borrower.SetCity(Console.ReadLine());
        Console.Write("State: ");
        borrower.SetState(Console.ReadLine());
        Console.Write("Zip: ");
        borrower.SetZip(Console.ReadLine());
        Console.Write("Phone: ");
        borrower.SetPhone(Console.ReadLine());
        Console.Write("Email: ");
        borrower.SetEmail(Console.ReadLine());
        _borrowers.Add(borrower);
    }

    public void NewMedia()
    // Creates new media and adds it to the list of media
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
                    DateTime bookDate;
                    string author;
                    int pages;
                    Console.Write($"Who authored {title}? ");
                    author = Console.ReadLine();
                    Console.Write($"How many pages? ");
                    int.TryParse(Console.ReadLine(), out pages);
                    Console.Write($"When was {title} published (month and day)? ");
                    DateTime.TryParse(Console.ReadLine(), out bookDate);
                    BookMedia book = new BookMedia(title);
                    book.SetAuthor(author);
                    book.SetPages(pages);
                    book.SetPublishDate(bookDate);
                    _medias.Add(book);
                    break;

                case "Game":
                    _medias.Add(new GameMedia(title));
                    break;

                case "Movie":
                    DateTime movieDate;
                    string rating;
                    Console.WriteLine("What is the movie rated:");
                    _movieRatingsMenu.DisplayMenu();
                    rating = _movieRatingsMenu.GetOptionChoice();
                    Console.Write($"When was {title} released (month and day)? ");
                    DateTime.TryParse(Console.ReadLine(), out movieDate);
                    MovieMedia movie = new MovieMedia(title, rating);
                    movie.SetPublishDate(movieDate);
                    _medias.Add(movie);
                    break;

                case "Music":
                    DateTime musicDate;
                    bool explicitLyrics;
                    string artist;
                    Console.Write($"Does {title} contain explicit lyrics (y/n)? ");
                    string[] choices = {"y", "Y", "yes", "Yes", "YES"};
                    string choice = Console.ReadLine();
                    explicitLyrics = choices.Contains(choice) ? true : false;
                    Console.Write($"Who is the artist? ");
                    artist = Console.ReadLine();
                    Console.Write($"When was {title} released (month and day)? ");
                    DateTime.TryParse(Console.ReadLine(), out musicDate);
                    MusicMedia music = new MusicMedia(title, explicitLyrics);
                    music.SetArtist(artist);
                    _medias.Add(music);
                    break;

                case "Video Game":
                    Console.WriteLine("What is the ESRB rating: ?");
                    _videoGameRatingsMenu.DisplayMenu();
                    string videoGameRating = _videoGameRatingsMenu.GetOptionChoice();
                    _medias.Add(new VideoGameMedia(title, videoGameRating));
                    break;
            }
        }
    }

    public void LoanMedia()
    // Allows user to select media from a list and a list of borrowers, and create a loan record
    {
        string type = "";
        int mediaIndex = -1;
        int borrowerIndex = -1;

        Console.WriteLine("\nLet's find the media you want to loan.");
        Console.WriteLine("\nChoose the media type you are loaning out."); 
        mediaIndex = ListMedia(ChooseMediaType());

        if (mediaIndex < 0)
        {
            Console.WriteLine("Nothing selected. (returning)");
            Thread.Sleep(3000);
        }
        else
        {
            Console.WriteLine("\nLet's enter who will be borrowing it.");
            borrowerIndex = ListBorrowers();

            if (borrowerIndex < 0)
            {
                Console.WriteLine("Nothing selected. (returning)");
                Thread.Sleep(3000);
            }
            else
            {
                _medias[mediaIndex].NewLoan(_borrowers[borrowerIndex]);
            }            
        }
    }

    public void ReturnMedia()
    // Marks the media as returned
    {
        int mediaIndex = -1;

        Console.WriteLine("Let's find the media that is being returned.");
        mediaIndex = ListMedia(ChooseMediaType());

        if (mediaIndex < 0)
        {
            Console.WriteLine("Nothing Selected. (returning)");
            Thread.Sleep(3000);
        }
        else
        {
            _medias[mediaIndex].Return();
        }
    }

    public void AnnotateMedia()
    // Adds a note to the media, and allows it to be marked available or not
    {
        int index = ListMedia(ChooseMediaType());
        Console.WriteLine("Enter the note: ");
        string note = Console.ReadLine();
        _medias[index].AddNote(note);
        if (_medias[index].IsAvailable())
        {
            Console.Write("Would you like to toggle this UNAVAILABLE (y/n)? ");
            string response = Console.ReadLine().ToLower();
            if (response == "y")
            {
                _medias[index].SetAvailable(false);
            }
        }
        else
        {
            Console.Write("Would you like to toggle this AVAILABLE (y/n)? ");
            string response = Console.ReadLine().ToLower();
            if (response == "y")
            {
                _medias[index].SetAvailable(true);
            }
        }
    }

    public void LoadMedia()
    // Loads the media from the media file, deserializing JSON for each line
    {
        string[] lines = System.IO.File.ReadAllLines(_filename);
        _medias.Clear();
        foreach (string line in lines)
        {
            //Console.WriteLine(line);
            MediaConvertObject data = DeserializeMediaData(line);
            switch (data.mediaType)
            {
                case "Book":
                    BookMedia book = new BookMedia(data.title);
                    book.SetData(data);
                    _medias.Add(book);
                    break;
                case "Game":
                    GameMedia game = new GameMedia(data.title);
                    game.SetData(data);
                    _medias.Add(game);
                    break;

                case "Movie":
                    MovieMedia movie = new MovieMedia(data.title, data.rating);
                    movie.SetData(data);
                    _medias.Add(movie);
                    break;
                case "Music":
                    MusicMedia music = new MusicMedia(data.title, (bool)data.explicitLyrics);
                    music.SetData(data);
                    _medias.Add(music);
                    break;
                case "Video Game":
                    VideoGameMedia vidgame = new VideoGameMedia(data.title, data.rating);
                    vidgame.SetData(data);
                    _medias.Add(vidgame);
                    break;
            }
        }
        // Populate list of borrowers from lending records that were just restored
        _borrowers.Clear();
        foreach (Media media in _medias)
        {
            foreach (LendingRecord loan in media.GetLoans())
            {
                _borrowers.Add(loan.GetBorrower());
            }
        }
        Console.WriteLine("\nData loaded. (returning)");
        Thread.Sleep(3000);
    }

    public void SaveMedia()
    // Serializes the data and saves it to the media file
    {
        using (StreamWriter outFile = new StreamWriter(_filename))
        {
            for (int i = 0; i < _medias.Count; i++)
            {
                //outFile.WriteLine(_medias[i].GetData());
                outFile.WriteLine(SerializeMediaData(_medias[i].GetData()));
            }
        }
        Console.WriteLine("\nData saved. (returning)");
        Thread.Sleep(3000);
    }

    private MediaConvertObject? DeserializeMediaData(string json)
    // JSON deserializer
    {   
        MediaConvertObject mediaData = JsonSerializer.Deserialize<MediaConvertObject>(json, new JsonSerializerOptions() {PropertyNameCaseInsensitive = true});
        return mediaData;
    }

    private string SerializeMediaData(MediaConvertObject mediaData)
    // JSON serializer
    {
        string json = JsonSerializer.Serialize<MediaConvertObject>(mediaData);
        return json;
    }

    public void DisplayDetails(int index)
    // Displays the details about media at this index
    {
        Console.Clear();
        Console.WriteLine(_medias[index].GetDetail());
        Console.Write("\nPress Enter to continue.");
        Console.ReadLine();
    }
}