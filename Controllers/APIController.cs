using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CrudBack.Models;
using System.Net;

namespace CrudBack.Controllers;

public class APIController : Controller{
    private readonly ILogger<APIController> _logger;
    private MyContext _context;

    public APIController(ILogger<APIController> logger, MyContext context){
        _logger = logger;
        _context = context;
    }

    [HttpGet("api/usuarios")]
    public JsonResult APIUsuarios([FromQuery(Name = "rut")] string rutParam){
        List<Usuario> listaUsuarios;

        if (string.IsNullOrEmpty(rutParam)){
            listaUsuarios = _context.Usuarios.ToList();
        }
        else{
            listaUsuarios = _context.Usuarios.Where(u => u.Rut.Contains(rutParam)).ToList();
            if(listaUsuarios.Count == 0){
                listaUsuarios = _context.Usuarios.ToList();
                return Json(listaUsuarios, HttpStatusCode.NotFound);
            }
        }
        return Json(listaUsuarios);
    }

    [HttpGet("api/usuario/{id}")]
    public JsonResult APIUsuarioId(int id){
        Usuario? usuario = _context.Usuarios.FirstOrDefault(u => u.UsuarioId == id);
        if(usuario != null){
            return Json(usuario);
        }else{
            return Json("Usuario no encontrado");
        }
    }

    // POST
    [HttpPost("api/usuario/agregar")]
    public async Task<ActionResult> CrearUsuario([FromBody] Usuario usuario){
        if (usuario == null){
            return BadRequest();
        }

        await _context.Usuarios.AddAsync(usuario);
        await _context.SaveChangesAsync();

        return Ok();
    }
    
    // PUT
    [HttpPut("api/usuario/actualizar")]
    public async Task<ActionResult> ActualizarUsuario([FromBody] Usuario usuario){
        if (usuario == null){
            return BadRequest();
        }

        var existingUser = await _context.Usuarios.FindAsync(usuario.UsuarioId);
        if (existingUser == null){
            return NotFound();
        }

        existingUser.Rut = usuario.Rut;
        existingUser.Nombre = usuario.Nombre;
        existingUser.Apellido = usuario.Apellido;
        existingUser.Correo = usuario.Correo;
        existingUser.Telefono = usuario.Telefono;

        await _context.SaveChangesAsync();

        return Ok();
    }

    // DELETE
    [HttpDelete("api/usuario/eliminar/{id}")]
    public async Task<ActionResult> EliminarUsuario(int id){
        var existingUser = await _context.Usuarios.FindAsync(id);

        if (existingUser == null){
            return NotFound();
        }

        _context.Usuarios.Remove(existingUser);
        await _context.SaveChangesAsync();

        return Ok();
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(){
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}