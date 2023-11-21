using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp2.Models;

public partial class Address
{
    [Key]
    [Column("AddressID")]
    public int AddressId { get; set; }

    [StringLength(40)]
    public string City { get; set; }

    [StringLength(255)]
    public string Street { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string ZipCode { get; set; }

    [ForeignKey("City")]
    [InverseProperty("Addresses")]
    public virtual City CityNavigation { get; set; }

    [InverseProperty("AddressNavigation")]
    public virtual ICollection<Store> Stores { get; set; } = new List<Store>();
}
