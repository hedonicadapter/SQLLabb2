using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp2.Models;

public partial class Store
{
    [Key]
    [Column("StoreID")]
    public int StoreId { get; set; }

    [Required]
    [StringLength(40)]
    public string StoreName { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string Phone { get; set; }

    public int? Address { get; set; }

    [ForeignKey("Address")]
    [InverseProperty("Stores")]
    public virtual Address AddressNavigation { get; set; }

    [InverseProperty("Store")]
    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
}
