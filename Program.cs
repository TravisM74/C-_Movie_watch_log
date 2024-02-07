using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;



internal class Program {
    static void Main(string[] args) {
        Boolean appRunning = true;
        MainMenu menu = new MainMenu();
        FileHandler files = new FileHandler();  
        List<Movie> movieList = new List<Movie>();
        while (appRunning) {
            int count = 0;
            menu.DisplayMenu();
            int selection = int.Parse(Console.ReadLine());
            switch (selection) {
                case 1:
                    Console.WriteLine("\n\rAdd a Movie:");
                    Movie movie = menu.AddMovie();
                    movieList.Add(movie);
                    break;
                case 2:
                    Console.WriteLine("\n\rDelete a Movie:");
                    count = 0;
                    foreach (Movie aMovie in movieList) {
                        count++;
                        Console.Write(count + ") ");
                        aMovie.Display();
                        
                    }
                    Console.Write("Input: ");
                    int deleteChoice = int.Parse(Console.ReadLine());
                    if (deleteChoice > 0 && deleteChoice <= count) {
                        movieList.RemoveAt(count-1);
                    }

                    break;
                case 3:
                    Console.Write("\n\r");
                    count = 0;
                    int totalTime = 0;
                    foreach (Movie aMovie in movieList) {
                        aMovie.Display();
                        count++;
                        totalTime += aMovie.Length;
                    }
                    Console.Write("\n\rTotal of " + count + " movies and " + totalTime + " minutes.\n");
                    break;
                case 4:
                    Console.WriteLine("\n\rLoad database");
                    movieList = files.Load();
                    break;
                case 5:
                    Console.Write("\n\r");
                    //Console.WriteLine("\n\rSave database");
                    files.Save(movieList);
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


}
class FileHandler {
     String name = "database.xml";
    public FileHandler() {
       

    }
    public void Save(List<Movie> movieList) {
        XmlSerializer SerializedFormat = new XmlSerializer(typeof(List<Movie>));
        using (StreamWriter sw = new StreamWriter(this.name)) 
        {
            SerializedFormat.Serialize(sw, movieList);
            sw.Close();
        }
    }
    public List<Movie> Load() {
        List<Movie>LoadedMovies = new List<Movie>();
        if (File.Exists(this.name)) {
            XmlSerializer SerializedFormat = new XmlSerializer(typeof(List<Movie>));  
           
            using (var reader = new StreamReader(this.name)) {
                LoadedMovies = (List<Movie>) SerializedFormat.Deserialize(reader);
            }
        } else {
            Console.WriteLine("No database available.");
        }


        return LoadedMovies;    
    }
}