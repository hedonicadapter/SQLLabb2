using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp2.Models;

public partial class Author
{
    [Key]
    [Column("AuthorID")]
    public int AuthorId { get; set; }

    [StringLength(40)]
    public string Firstname { get; set; }

    [StringLength(40)]
    public string Lastname { get; set; }

    [Column(TypeName = "date")]
    public DateTime? Birthday { get; set; }

    [InverseProperty("AuthorNavigation")]
    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
