﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjectManeger.Models;

[Table("projecttask")]
public partial class Projecttask
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("projectid")]
    public int? Projectid { get; set; }

    [Column("moduleid")]
    public int? Moduleid { get; set; }

    [Column("task")]
    [StringLength(100)]
    public string? Task { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Column("employeid")]
    public int? Employeid { get; set; }

    [Column("startdate", TypeName = "date")]
    public DateTime? Startdate { get; set; }

    [Column("starttime")]
    [StringLength(100)]
    public string? Starttime { get; set; }

    [Column("enddate", TypeName = "date")]
    public DateTime? Enddate { get; set; }

    [Column("endtime")]
    [StringLength(100)]
    public string? Endtime { get; set; }

    [Column("duration")]
    public int? Duration { get; set; }

    [Column("status")]
    [StringLength(100)]
    public string? Status { get; set; }

    [ForeignKey("Employeid")]
    [InverseProperty("Projecttasks")]
    public virtual Employee? Employe { get; set; }

    [ForeignKey("Moduleid")]
    [InverseProperty("Projecttasks")]
    public virtual Projectmodule? Module { get; set; }

    [ForeignKey("Projectid")]
    [InverseProperty("Projecttasks")]
    public virtual Project? Project { get; set; }
}
