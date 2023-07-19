using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjectManeger.Models;

[Table("employees")]
public partial class Employee
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("code")]
    [StringLength(100)]
    public string? Code { get; set; }

    [Column("name")]
    [StringLength(50)]
    public string? Name { get; set; }

    [Column("gender")]
    [StringLength(10)]
    public string? Gender { get; set; }

    [Column("eamil")]
    [StringLength(100)]
    public string? Eamil { get; set; }

    [Column("mobileno")]
    [StringLength(50)]
    public string? Mobileno { get; set; }

    [Column("password")]
    [StringLength(100)]
    public string? Password { get; set; }

    [InverseProperty("Manager")]
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    [InverseProperty("Employe")]
    public virtual ICollection<Projecttask> Projecttasks { get; set; } = new List<Projecttask>();
}
