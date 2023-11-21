using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp2.Models;

[Keyless]
public partial class GenresBook
{
    [Column("ISBN")]
    [StringLength(17)]
    [Unicode(false)]
    public string Isbn { get; set; }

    [Column("GenreID")]
    public int? GenreId { get; set; }

    [ForeignKey("GenreId")]
    public virtual Genre Genre { get; set; }

    [ForeignKey("Isbn")]
    public virtual Book IsbnNavigation { get; set; }
}
