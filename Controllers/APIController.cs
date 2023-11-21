using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CrudBack.Models;

namespace CrudBack.Controllers;

public class APIController : Controller{
    private readonly ILogger<APIController> _logger;
    private MyContext _context;

    public APIController(ILogger<APIController> logger, MyContext context){
        _logger = logger;
        _context = context;
    }

    // GET
    [HttpGet("api/usuarios")]
    public JsonResult APIUsuarios(){
        // List<Usuario> ListaUsuarios = _context.Usuarios.ToList();
        List<Usuario> ListaUsuarios = _context.Usuarios.ToList();
        return Json(ListaUsuarios);
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

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(){
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}