using back_end.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_end.Utilidades
{
    public static class IQueryableExtencions
    {
        public static IQueryable<T> Paginar<T>(this IQueryable<T> queryable, PaginacionDTO paginacionDto)
        {
            return queryable
                .Skip((paginacionDto.pagina - 1) * paginacionDto.RegistrosPorPagina)
                .Take(paginacionDto.RegistrosPorPagina);
        }
    }
}
