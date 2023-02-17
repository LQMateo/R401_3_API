using Microsoft.EntityFrameworkCore;
using R401_3_API.Models;
using System.Linq;
using System.Text;

namespace R401_3_API
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Program.un_Deux();
            //Program.load_explicite();
            //Program.load_hatif();
            //Program.load_async();
            //Program.Exo2Q2();
            //Program.Exo2Q3();
            //Program.Exo2Q4();
            //Program.Exo2Q5();
            //Program.Exo2Q6();
            //Program.Exo2Q7();
            //Program.Exo2Q8();
            //Program.Exo2Q9();
        }


        static void un_Deux(){
            using (var ctx = new LequmaContext())
            {
                // Requete Select
                Film titanic = ctx.Films.First(f => f.Nom.Contains("Titanic"));

                //Modif entité 
                titanic.Description = "Un bateau échoué. Date : " + DateTime.Now;

                //Save modif
                int nbChanges = ctx.SaveChanges();

                Console.WriteLine("Nombre d'enregistrements modifiés ou ajoutés : "+nbChanges);
            }
            Console.ReadKey();
        }

        static void load_explicite()
        {
            //A la main 
             using (var ctx = new LequmaContext())
            {
                //Chargement de la catégorie Action
                Categorie categorieAction = ctx.Categories.First(c => c.Nom == "Action");
                Console.WriteLine("Categorie : " + categorieAction.Nom);
                Console.WriteLine("Films : ");

                //Chargement des films de la catégorie Action.
                foreach (var film in ctx.Films.Where(f => f.CategorieNavigation.Nom == categorieAction.Nom).ToList())
                {
                    Console.WriteLine(film.Nom);
                }
            }


            //Avec la collection
            using (var ctx = new LequmaContext())
            {
                Categorie categorieAction = ctx.Categories.First(c => c.Nom == "Action");
                Console.WriteLine("Categorie : " + categorieAction.Nom);
                //Chargement des films dans categorieAction
                ctx.Entry(categorieAction).Collection(c => c.Films).Load();
                Console.WriteLine("Films : ");
                foreach (var film in categorieAction.Films)
                {
                    Console.WriteLine(film.Nom);
                }
            }
        }

        static void load_hatif()
        {
            using (var ctx = new LequmaContext())
            {
                //Chargement de la catégorie Action et des films de cette catégorie
                Categorie categorieAction = ctx.Categories
                .Include(c => c.Films)
                .First(c => c.Nom == "Action");
                Console.WriteLine("Categorie : " + categorieAction.Nom);
                Console.WriteLine("Films : ");
                foreach (var film in categorieAction.Films)
                {
                    Console.WriteLine(film.Nom);
                }
            }
        }

        static void load_async()
        {
            using (var ctx = new LequmaContext())
            {
                //Chargement de la catégorie Action
                Categorie categorieAction = ctx.Categories.First(c => c.Nom == "Action");
                Console.WriteLine("Categorie : " + categorieAction.Nom);
                Console.WriteLine("Films : ");
                //Chargement des films de la catégorie Action.
                foreach (var film in categorieAction.Films) // lazy loading initiated
                {
                    Console.WriteLine(film.Nom);
                }
            }
        }

        //Afficher les emails des utilsiateurs
        static void Exo2Q2()
        {
            using (var ctx = new LequmaContext())
            {
                foreach (var user in ctx.Utilisateurs.ToList())
                {
                    Console.WriteLine(user.Email);
                }
            }
        }

        // Afficher tous les utilisateurs par login croissant
        static void Exo2Q3()
        {
            using (var ctx = new LequmaContext())
            {
                foreach (var user in ctx.Utilisateurs.OrderBy(u => u.Login).ToList())
                {
                    Console.WriteLine(user);
                }
            }
        }

        // Afficher les noms et id des films de la catégorie « Action ».
        static void Exo2Q4()
        {
            using (var ctx = new LequmaContext())
            {
                //Search id of categorie
                Categorie categorieAction = ctx.Categories.First(c => c.Nom == "Action");

                foreach (var film in ctx.Films.Where(f => f.Categorie == categorieAction.Id ).ToList())
                {
                    Console.WriteLine(film);
                }
            }
        }

        // Afficher le nombre de catégories.
        static void Exo2Q5()
        {
            using (var ctx = new LequmaContext())
            {
                Console.WriteLine(ctx.Categories.Count());                 
            }
        }


        //Afficher la note la plus basse dans la base.
        static void Exo2Q6()
        {
            using (var ctx = new LequmaContext())
            {
                Console.WriteLine(ctx.Avis.Min(a => a.Note)); 
            }
        }

        //Rechercher tous les films qui commencent par « le » (pas de respect de la casse => 14 résultats)
        static void Exo2Q7()
        {
            using (var ctx = new LequmaContext())
            {
                Console.WriteLine(ctx.Films.Where(f => f.Nom.Substring(0, 2).ToLower() == "le").Count() );             
            }
        }

        //Afficher la note moyenne du film « Pulp Fiction » (note : le nom du film ne devra pas être sensible à la casse).
        static void Exo2Q8()
        {
            using (var ctx = new LequmaContext())
            {
                //recherche le film
                Film filmsearch = ctx.Films.First(f => f.Nom.ToLower() == "pulp fiction");

                Console.WriteLine( ctx.Avis.Where(a => a.Film == filmsearch.Id).Average(a => a.Note) );             
            }
        }

        //Afficher l’utilisateur qui a mis la meilleure note dans la base (on pourra le faire en 2 instructions, mais essayer de le faire en une seule).
        static void Exo2Q9()
        {
            using (var ctx = new LequmaContext())
            {      
                Console.WriteLine(ctx.Utilisateurs.First(u => u.Id == ctx.Avis.First(a2 => a2.Note == ctx.Avis.Max(a => a.Note)).Utilisateur));
            }
        }
    }
}


