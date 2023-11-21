using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp2.Models;

public partial class Book
{
    [Key]
    [Column("ISBN13")]
    [StringLength(17)]
    [Unicode(false)]
    public string Isbn13 { get; set; }

    [Required]
    [StringLength(255)]
    public string Title { get; set; }

    [Column("LanguageISO")]
    [StringLength(3)]
    [Unicode(false)]
    public string LanguageIso { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? Price { get; set; }

    [Column(TypeName = "date")]
    public DateTime? Published { get; set; }

    public int? Author { get; set; }

    [StringLength(255)]
    public string Description { get; set; }

    [ForeignKey("Author")]
    [InverseProperty("Books")]
    public virtual Author AuthorNavigation { get; set; }

    [InverseProperty("IsbnNavigation")]
    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
}
