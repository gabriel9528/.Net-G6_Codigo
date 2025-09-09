using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataBaseFirst.Data;

[Table("Cliente")]
public partial class Cliente
{
    [Key]
    public int IdCliente { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string NombreCliente { get; set; } = null!;

    [StringLength(200)]
    [Unicode(false)]
    public string Cif { get; set; } = null!;

    [StringLength(200)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [StringLength(200)]
    [Unicode(false)]
    public string Direccion { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string Usuario { get; set; } = null!;

    [MaxLength(500)]
    public byte[] Password { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime FechaAlta { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaBaja { get; set; }

    [Column("descripcion")]
    [StringLength(200)]
    [Unicode(false)]
    public string? Descripcion { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string Genero { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? FechaNacimiento { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? ColumnaNueva { get; set; }
}
