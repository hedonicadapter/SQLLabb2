using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp2.Models;

[Keyless]
public partial class StoresEmployee
{
    public int? Employee { get; set; }

    public int? Store { get; set; }

    [ForeignKey("Employee")]
    public virtual Employee EmployeeNavigation { get; set; }

    [ForeignKey("Store")]
    public virtual Store StoreNavigation { get; set; }
}
