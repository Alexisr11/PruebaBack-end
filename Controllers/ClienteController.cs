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
    [Route("api/clientes")]
    [ApiController]
    public class ClienteController: ControllerBase
    {
        public readonly ILogger<ClienteController> logger;
        private readonly IMapper mapper;

        public AplicationDbContext Context { get; }

        public ClienteController(ILogger<ClienteController> logger,
            AplicationDbContext context, IMapper mapper)
        {
            this.logger = logger;
            Context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ClienteDTO>>> Get()
        {
            var queryable = Context.Cliente.AsQueryable();
            await HttpContext.InsertarParametrosPaginacionCabecera(queryable);
            var clientes = await queryable.OrderBy(x => x.nombre).ToListAsync();
            return mapper.Map<List<ClienteDTO>>(clientes);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ClienteDTO>> Get(int id)
        {
            var cliente = await Context.Cliente.FirstOrDefaultAsync(x => x.id == id);

            if (cliente == null)
            {
                return NotFound();
            }

            return mapper.Map<ClienteDTO>(cliente);
        }

        [HttpPost]
        public async Task<ActionResult> Post(CreacionClienteDTO creacionClienteDto)
        {
            var cliente = mapper.Map<Cliente>(creacionClienteDto);
            Context.Add(cliente);
            await Context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] CreacionClienteDTO creacionClienteDTO)
        {
            var cliente = await Context.Cliente.FirstOrDefaultAsync(x => x.id == id);

            if (cliente == null)
                return NotFound();

            cliente = mapper.Map(creacionClienteDTO, cliente);
            await Context.SaveChangesAsync();
            return NoContent();

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await Context.Cliente.AnyAsync(x => x.id == id);

            if (existe == null)
            {
                return NotFound();
            }

            Context.Remove(new Cliente() { id = id });
            await Context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet]
        [Route("cliente-cedula/{cedula}")]
        public async Task<ActionResult<ClienteDTO>> GetClientePorCedula(string cedula)
        {
            var cliente = await Context.Cliente.FirstOrDefaultAsync(x => x.cedula == cedula);

            if (cliente == null)
            {
                return NotFound();
            }

            return mapper.Map<ClienteDTO>(cliente);
        }
    }
}
