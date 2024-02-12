using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;



internal class Program {
    static void Main(string[] args) {
        Boolean appRunning = true;
        MainMenu menu = new MainMenu();
        FileHandler files = new FileHandler();
        // List<Movie> movieList = new List<Movie>();
        MovieList movies = new MovieList();
        while (appRunning) {
            int count = 0;
            menu.DisplayMenu();
            int selection = int.Parse(Console.ReadLine());
            switch (selection) {
                case 1:
                    Console.WriteLine("\n\rAdd a Movie:");
                    movies.AddMovie();
                    break;
                case 2:
                    Console.WriteLine("\n\rDelete a Movie:");
                    movies.DeleteMovie();

                    break;
                case 3:
                    Console.Write("\n\r");
                    movies.ListMovies();    
                    break;
                case 4:
                    Console.WriteLine("\n\rLoad database");
                    movies = files.Load();
                    break;
                case 5:
                    Console.Write("\n\r");
                    //Console.WriteLine("\n\rSave database");
                    files.Save(movies);
                    Console.WriteLine("Database saved.");
                    break;
                case 6:
                    appRunning = false;
                    Console.Write("\n\r");
                    break;
                default:
                    Console.WriteLine("Unknown command");
                    break;

            }

        }
    }
}
[Serializable]
public class Movie {
    public String Name { get; set; }
    public int Length { get; set; }
    public int Year { get; set; }
    public Movie() { }
    public Movie(string name, int length, int year) {
        this.Name = name;
        this.Length = length;
        this.Year = year;
    }
    public void Display() {
        Console.WriteLine("" + Name + " (" + Year + "), " + Length + " minutes.  ");
    }
}
[Serializable]
public class MovieList {
    public List<Movie> movies = new List<Movie>();
    public MovieList() {

    }
    public void AddMovie() {
        String name = null;
        while (name == null) {
            Console.Write("Name:");
            name = Console.ReadLine();
        }
        int length = -1;
        while (length < 0) {
            Console.Write("Length (min):");
            length = int.Parse(Console.ReadLine());
        }
        int year = -1;
        while (year <= 0) {
            Console.Write("Year:");
            year = int.Parse(Console.ReadLine());
        }

        Movie newMovie = new Movie(name, length, year);
        movies.Add(newMovie);
    }
    public void DeleteMovie() {
        int count = 0;
        foreach (Movie aMovie in movies) {
            count++;
            Console.Write(count + ") ");
            aMovie.Display();

        }
        Console.Write("Input: ");
        int deleteChoice = int.Parse(Console.ReadLine());
        if (deleteChoice > 0 && deleteChoice <= count) {
            movies.RemoveAt(count - 1);
        }

    }
    public void ListMovies() {
        int count = 0;
        foreach (Movie movie in movies) {
            count++;
            Console.Write(count + ") ");
            movie.Display();
        }
    }
}
class MainMenu {
    public MainMenu() {
    }
    public void DisplayMenu() {
        Console.WriteLine("\n\r\n\rMovie Watchlog\n==============");
        //Console.WriteLine("==============");
        Console.WriteLine("1) Add a Movie");
        Console.WriteLine("2) Delete a Movie");
        Console.WriteLine("3) Show Report");
        Console.WriteLine("4) Load Database");
        Console.WriteLine("5) Save Database");
        Console.WriteLine("6) Quit");
        Console.Write("Input: ");
    }
    /*
    public Movie AddMovie() {
        Console.Write("Name:");
        string name = Console.ReadLine();
        Console.Write("Length (min):");
        int length = int.Parse(Console.ReadLine());
        Console.Write("Year:");
        int year = int.Parse(Console.ReadLine());
        Movie newMovie = new Movie(name, length, year);
        return newMovie;
    }
    */


}
class FileHandler {
    String name = "database.xml";
    public FileHandler() {


    }
    public void Save(MovieList movieList) {
        
        XmlSerializer SerializedFormat = new XmlSerializer(typeof(MovieList));
        using (StreamWriter sw = new StreamWriter(this.name)) {
            SerializedFormat.Serialize(sw, movieList);
            sw.Close();
        }
    }
    public MovieList Load() {
        MovieList LoadedMovies = new MovieList();
        if (File.Exists(this.name)) {
            XmlSerializer SerializedFormat = new XmlSerializer(typeof(MovieList));

            using (var reader = new StreamReader(this.name)) {
                LoadedMovies = (MovieList)SerializedFormat.Deserialize(reader);
            }
        }
        else {
            Console.WriteLine("No database available.");
        }
        

        return LoadedMovies;
    }
}