using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace R401_3_API.Models;

[Table("film", Schema = "r41_3")]
public partial class Film
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("nom")]
    [StringLength(50)]
    public string Nom { get; set; } = null!;

    [Column("description")]
    public string? Description { get; set; }

    [Column("categorie")]
    public int Categorie { get; set; }

    [InverseProperty("FilmNavigation")]
    public virtual ICollection<Avi> Avis { get; } = new List<Avi>();

    [ForeignKey("Categorie")]
    [InverseProperty("Films")]
    public virtual Categorie CategorieNavigation { get; set; } = null!;
}
