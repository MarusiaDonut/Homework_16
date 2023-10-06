using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_16
{
    internal class SQL
    {
        //table Film
        public static List<Film> GetAllFilms()
        {
            using (var conn = new NpgsqlConnection(Config.SqlConnectionString))
            {
                string sql = @"select film_id, title from film";
                var films = conn.Query<Film>(sql).ToList();
                films.ForEach(film => Console.WriteLine($"{film.Film_id} - {film.Title}"));
                return films;
            }
        }

        public static List<Film> GetFilmsWithTitle(string pattern)
        {
            using (var conn = new NpgsqlConnection(Config.SqlConnectionString))
            {
                string sql = $"select film_id, title, length, rating from film where title like '%" + pattern + "%'";
                var films =  conn.Query<Film>(sql, new { pattern }).ToList();
                films.ForEach(film => Console.WriteLine($"{film.Film_id} - {film.Title}"));
                return films;
            }
        }

        public static List<Film> GetFilmsWithRating(string pattern)
        {
            using (var conn = new NpgsqlConnection(Config.SqlConnectionString))
            {
                string sql = $"select count(rating) as count, rating from film where rating::text like '%" + pattern + "%' group by rating";
                var films = conn.Query<Film>(sql, new { pattern }).ToList();
                films.ForEach(film => Console.WriteLine($"{film.Count} - {film.Rating}"));
                return films;
            }
        }

        public static List<Film> GetFilmsLength(int length)
        {
            using (var conn = new NpgsqlConnection(Config.SqlConnectionString))
            {
                string sql = $"select title, description from film where length >" + length;
                var films = conn.Query<Film>(sql, new { length }).ToList();
                films.ForEach(film => Console.WriteLine($"{film.Title} - {film.Description}"));
                return films;
            }
        }

        public static Film GetFilmById(int id)
        {
            using (var conn = new NpgsqlConnection(Config.SqlConnectionString))
            {
                string sql = $"select film_id, title, length, rating from film where film_id = @id";
                var film = conn.QueryFirstOrDefault<Film>(sql, new { id });
                Console.WriteLine($"{film.Film_id} - {film.Title} - {film.Length} - {film.Rating}");
                return film;
            }
        }

        //table Actor
        public static List<Actor> GetAllActors()
        {
            using (var conn = new NpgsqlConnection(Config.SqlConnectionString))
            {
                string sql = @"select actor_id, first_name, last_name from actor";
                var actors = conn.Query<Actor>(sql).ToList();
                actors.ForEach(actor => Console.WriteLine($"{actor.Actor_id} - {actor.First_name} - {actor.Last_name}"));
                return actors;
            }
        }
        public static List<Actor> GetActorsDistinct()
        {
            using (var conn = new NpgsqlConnection(Config.SqlConnectionString))
            {
                string sql = @"select distinct first_name from actor";
                var actors = conn.Query<Actor>(sql).ToList();
                actors.ForEach(actor => Console.WriteLine($"{actor.First_name}"));
                return actors;
            }
        }

        public static Actor GetActorById(int id)
        {
            using (var conn = new NpgsqlConnection(Config.SqlConnectionString))
            {
                string sql = $"select actor_id, first_name, last_name from actor where actor_id = @id";
                var actor = conn.QueryFirstOrDefault<Actor>(sql, new { id });
                Console.WriteLine($"{actor.Actor_id} - {actor.First_name} - {actor.Last_name}");
                return actor;
            }
        }

        public static void JoinActorAndFilm()
        {
            using (var conn = new NpgsqlConnection(Config.SqlConnectionString))
            {

                string sql = $"SELECT actor.first_name, actor.last_name FROM actor " +
                    $"join film_actor on film_actor.actor_id = actor.actor_id " +
                    $"join film on film_actor.film_id = film.film_id " +
                    $"group by actor.first_name, actor.last_name " +
                    $"having count(film) < 20 " +
                    $"ORDER BY actor.last_name ASC";
                var actors = conn.Query<Actor>(sql).ToList();
                actors.ForEach(actor => Console.WriteLine($"{actor.First_name} - {actor.Last_name}"));

            }
        }

    }
}
