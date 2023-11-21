using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp2.Models;

public partial class Employee
{
    [Key]
    [Column("EmployeeID")]
    public int EmployeeId { get; set; }

    [StringLength(40)]
    public string Firstname { get; set; }

    [StringLength(40)]
    public string Lastname { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string Phone { get; set; }

    public long? Salary { get; set; }

    public int? Position { get; set; }

    [ForeignKey("Position")]
    [InverseProperty("Employees")]
    public virtual Position PositionNavigation { get; set; }
}
