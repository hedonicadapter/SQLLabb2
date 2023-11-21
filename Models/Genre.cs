using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp2.Models;

[Index("Genre1", Name = "UQ__Genres__F1410CF36090E39A", IsUnique = true)]
public partial class Genre
{
    [Key]
    [Column("GenreID")]
    public int GenreId { get; set; }

    [Required]
    [Column("Genre")]
    [StringLength(40)]
    [Unicode(false)]
    public string Genre1 { get; set; }
}
