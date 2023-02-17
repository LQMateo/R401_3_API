using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace R401_3_API.Models;

[Table("categorie", Schema = "r41_3")]
public partial class Categorie
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("nom")]
    [StringLength(50)]
    public string Nom { get; set; } = null!;

    [Column("description")]
    public string? Description { get; set; }

    [InverseProperty("CategorieNavigation")]
    private ICollection<Film> films;

    public virtual ICollection<Film> Films
    {
        get
        {
            return _lazyLoader.Load(this, ref films);
        }
        set { films = value; }
    }


    //Pour la chargement différé
    private ILazyLoader _lazyLoader;
    public Categorie(ILazyLoader lazyLoader)
    {
        _lazyLoader = lazyLoader;
    }
}
