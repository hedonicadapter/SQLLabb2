using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp2.Models;

public partial class Country
{
    [Key]
    [Column("Country")]
    [StringLength(40)]
    public string Country1 { get; set; }

    [InverseProperty("CountryNavigation")]
    public virtual ICollection<City> Cities { get; set; } = new List<City>();
}
