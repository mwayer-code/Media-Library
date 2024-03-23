using NLog;

// See https://aka.ms/new-console-template for more information
string path = Directory.GetCurrentDirectory() + "\\nlog.config";

// create instance of Logger
var logger = LogManager.LoadConfiguration(path).GetCurrentClassLogger();
logger.Info("Program started");

string scrubbedFile = FileScrubber.ScrubMovies("movies.csv");
logger.Info(scrubbedFile);
MovieFile movieFile = new MovieFile(scrubbedFile);

string choice = "";
while (choice != "q")
{
    Console.WriteLine("1) Add Movie");
    Console.WriteLine("2) Display All Movies");
    Console.WriteLine("q) Quit");
    choice = Console.ReadLine();

    if (choice == "1")
    {
        Movie movie = new Movie();
        Console.WriteLine("Enter movie title");
        movie.title = Console.ReadLine();
        Console.WriteLine("Enter movie genres (action|comedy|etc)");
        string genres = Console.ReadLine();
        movie.genres = genres.Split('|').ToList();
        Console.WriteLine("Enter movie director");
        movie.director = Console.ReadLine();
        Console.WriteLine("Enter movie run time (h:m:s)");
        movie.runningTime = TimeSpan.Parse(Console.ReadLine());
        movieFile.AddMovie(movie);
    }
    else if (choice == "2")
    {
        foreach (Movie m in movieFile.Movies)
        {
            Console.WriteLine(m.Display());
        }
    }
}

logger.Info("Program ended");