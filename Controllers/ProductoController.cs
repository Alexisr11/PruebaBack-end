using AutoMapper;
using back_end.DTOs;
using back_end.Entidades;
using back_end.Utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_end.Controllers
{
    [Route("api/productos")]
    [ApiController]
    public class ProductoController: ControllerBase
    {
        public readonly ILogger<ProductoController> logger;
        private readonly IMapper mapper;

        public AplicationDbContext Context { get; }

        public ProductoController(ILogger<ProductoController> logger,
            AplicationDbContext context, IMapper mapper)
        {
            this.logger = logger;
            Context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductoDTO>>> Get()
        {
            var queryable = Context.Producto.AsQueryable();
            await HttpContext.InsertarParametrosPaginacionCabecera(queryable);
            var productos = await queryable.OrderBy(x => x.nombre).ToListAsync();
            return mapper.Map<List<ProductoDTO>>(productos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductoDTO>> Get(int id)
        {
            var producto = await Context.Producto.FirstOrDefaultAsync(x => x.id == id);

            if (producto == null)
            {
                return NotFound();
            }

            return mapper.Map<ProductoDTO>(producto);
        }

        [HttpPost]
        public async Task<ActionResult> Post(CreacionProductoDTO creacionProductoDto)
        {
            var producto = mapper.Map<Producto>(creacionProductoDto);
            Context.Add(producto);
            await Context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] CreacionProductoDTO creacionProductoDTO)
        {
            var producto = await Context.Producto.FirstOrDefaultAsync(x => x.id == id);

            if (producto == null)
                return NotFound();

            producto = mapper.Map(creacionProductoDTO, producto);
            await Context.SaveChangesAsync();
            return NoContent();

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await Context.Producto.AnyAsync(x => x.id == id);

            if (existe == null)
            {
                return NotFound();
            }

            Context.Remove(new Producto() { id = id });
            await Context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet]
        [Route("producto-nombre/{nombrePro}")]
        public async Task<ActionResult<ProductoDTO>> GetPrroductoPorNombre(string nombrePro)
        {
            var producto = await Context.Producto.FirstOrDefaultAsync(x => x.nombre == nombrePro);

            if (producto == null)
            {
                return NotFound();
            }

            return mapper.Map<ProductoDTO>(producto);
        }
    }
}
