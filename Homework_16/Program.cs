using Npgsql;

namespace Homework_16
{
    class Program
    {
        private static void Main()
        {
            //SQL.GetAllFilms();
            //SQL.GetFilmsWithTitle("Ala");
            //SQL.GetFilmsWithRating("G");
            //SQL.GetFilmById(4);
            //SQL.GetFilmsLength(100);

            //SQL.GetAllActors();
            //SQL.GetActorsDistinct();
            //SQL.GetActorById(3);

            SQL.JoinActorAndFilm();
        }
    }
}