#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace CrudBack.Models;

public class Usuario{
    [Key]
    public int UsuarioId {get;set;}

    public string Rut {get; set;}

    public string Nombre {get;set;}

    public string Apellido {get; set;}

    public int Telefono {get; set;}

    public string Correo {get; set;}
}