using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp2.Models;

public partial class City
{
    [Key]
    [Column("City")]
    [StringLength(40)]
    public string City1 { get; set; }

    [StringLength(40)]
    public string Country { get; set; }

    [InverseProperty("CityNavigation")]
    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    [ForeignKey("Country")]
    [InverseProperty("Cities")]
    public virtual Country CountryNavigation { get; set; }
}
