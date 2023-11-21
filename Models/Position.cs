using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp2.Models;

[Index("Position1", Name = "UQ__Position__5A8B58B894A6F3AC", IsUnique = true)]
public partial class Position
{
    [Key]
    [Column("PositionID")]
    public int PositionId { get; set; }

    [Required]
    [Column("Position")]
    [StringLength(40)]
    [Unicode(false)]
    public string Position1 { get; set; }

    [InverseProperty("PositionNavigation")]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
