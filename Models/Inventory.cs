using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp2.Models;

[PrimaryKey("StoreId", "Isbn")]
[Table("Inventory")]
public partial class Inventory
{
    [Key]
    [Column("StoreID")]
    public int StoreId { get; set; }

    [Key]
    [Column("ISBN")]
    [StringLength(17)]
    [Unicode(false)]
    public string Isbn { get; set; }

    public int Quantity { get; set; }

    [ForeignKey("Isbn")]
    [InverseProperty("Inventories")]
    public virtual Book IsbnNavigation { get; set; }

    [ForeignKey("StoreId")]
    [InverseProperty("Inventories")]
    public virtual Store Store { get; set; }
}
