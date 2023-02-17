using Microsoft.EntityFrameworkCore;
using R401_3_API.Models;
using System.Linq;
using System.Runtime.Intrinsics.X86;
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

            //Exo 2.3
            //addUser();
            //Program.updateFilm();
            //addAvis();
            addtwofilms();
        }

        static void addUser()
        {
            using (var ctx = new LequmaContext())
            {
                var user = new Utilisateur()
                {
                    Login = "_etamo_",
                    Pwd = "12345678",
                    Email = "mateo.lq74@hds.com"
                };
                ctx.Utilisateurs.Add(user);
                ctx.SaveChanges();

            }
        }

        //Update film
        static void updateFilm()
        {
            using (var ctx = new LequmaContext())
            {
                //Search film
                Film film = ctx.Films.First(f => f.Nom.ToLower() == "L'armee des douze singes".ToLower() );
                film.Description = "bah jsp";
                ctx.SaveChanges();
            }
        }

        static void addtwofilms()
        {
            using (var ctx = new LequmaContext())
            {
                Categorie categoriedrame = ctx.Categories.First(c => c.Nom.ToLower() == "Drame".ToLower());


                var film1 = new Film()
                {Id = 50010,
                    Nom = "Avaater1",
                    Description = "Je sais pas",
                    Categorie = categoriedrame.Id
                };

                ctx.Films.Add(film1);
                ctx.SaveChanges();

                var film2 = new Film()
                {Id = 50020,
                    Nom = "Avaatar2",
                    Description = "Je sais pas",
                    Categorie = categoriedrame.Id
                };

                ctx.Films.Add(film2);
                ctx.SaveChanges();

                //Bien penser à ajouter dans la bd 
                /*ctx.Films.AddRange(film1, film2);                
                ctx.SaveChanges();*/

            }
                
        }

        //Add avi
        static void addAvis()
        {
            using (var ctx = new LequmaContext())
            {
                Film film = ctx.Films.First(f => f.Nom.ToLower() == "L'armee des douze singes".ToLower());
                Utilisateur user = ctx.Utilisateurs.First(u => u.Id== 1);

                var avi = new Avi()
                {
                    Film = film.Id,
                    Utilisateur = user.Id,
                    Avis = "trop jsp je l'ai pas vu",
                    Note = (decimal)4.50
                };

                ctx.Avis.Add(avi);
                ctx.SaveChanges();
            }
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


