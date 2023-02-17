using Microsoft.EntityFrameworkCore;
using R401_3_API.Models.EntityFramework;
using System.Linq;

namespace R401_3_API
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Program.un_Deux();
            //Program.load_explicite();
            //Program.load_hatif();
            Program.load_async();
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
    }
}


