using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRODUCTO.entidades;

namespace PRODUCTO.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductoController : ControllerBase
{
    private readonly ApplicationDbContext dbContext;

    public ProductoController(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }


    [HttpPost("crearProducto")]
    public async Task<ActionResult> CrearTarjeta(Producto producto)
    {
        try
        {
            dbContext.Add(producto);
            await dbContext.SaveChangesAsync();
            return Ok(producto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("ConsultarProducto")]
    public async Task<IActionResult> Get()
    {
        try
        {
            var listaProducto = await dbContext.Productos.ToListAsync();
            return Ok(listaProducto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }
    [HttpPut("ActualizarProducto/{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Producto producto)
    {
        try
        {
            var existingProduct = await dbContext.Productos.FindAsync(id);

            if (existingProduct == null)
            {
                return NotFound(new { error = "Producto no encontrado" });
            }

            // Actualizar solo las propiedades necesarias
            existingProduct.Nombre = producto.Nombre;
            existingProduct.Categoria = producto.Categoria;
            existingProduct.Ubicacion = producto.Ubicacion;
            existingProduct.Precio = producto.Precio;

            dbContext.Update(existingProduct);
            await dbContext.SaveChangesAsync();

            return Ok(new { message = "El producto fue actualizado con éxito" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }


    [HttpDelete("EliminarProducto/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var tarjeta = await dbContext.Productos.FindAsync(id);
            if (tarjeta == null) { return NotFound(); }
            dbContext.Remove(tarjeta);
            await dbContext.SaveChangesAsync();
            return Ok(new { message = "el producto fue eliminado con exito" });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("ObtenerProducto/{id}")]
    public async Task<IActionResult> ObtenerProducto(int id)
    {
        try
        {
            var producto = await dbContext.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound(new { error = "Producto no encontrado" });
            }

            return Ok(producto);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

}
