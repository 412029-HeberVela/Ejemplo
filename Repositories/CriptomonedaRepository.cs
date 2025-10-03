using Microsoft.EntityFrameworkCore;
using ParcialWebApi.Models;

namespace ParcialWebApi.Repositories
{
    public class CriptomonedaRepository : ICriptomonedaRepository
    {
        private CriptoContext _context;
        public CriptomonedaRepository(CriptoContext context)
        {
            _context = context;
        }
        public bool Create(Criptomoneda oCriptomoneda)
        {
            _context.Criptomonedas.Add(oCriptomoneda);
            return _context.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var criptomoneda = _context.Criptomonedas.Find(id);
            if (criptomoneda != null && criptomoneda.Estado == "H")
            {
                criptomoneda.Estado = "NH";
                return _context.SaveChanges() > 0;
            }
            return false;
        }

        public List<Criptomoneda> GetAll()
        {
            return _context.Criptomonedas.ToList();
        }

        public List<Criptomoneda> GetBy(string cat)
        {
            var limite = DateTime.Now.AddDays(-1);

            return _context.Criptomonedas
                .Include(p => p.CategoriaNavigation)
                .Where(p => p.CategoriaNavigation != null &&
                            p.CategoriaNavigation.Nombre == cat &&
                            p.UltimaActualizacion >= limite) // filtro agregado
                .ToList();
        }

        public bool Update(string simbolo, double valorActual, DateTime ultimaActualizacion)
        {
            var criptomoneda = _context.Criptomonedas
                                       .FirstOrDefault(c => c.Simbolo == simbolo);

            if (criptomoneda != null)
            {
                // Validación: fecha no puede tener más de 1 día de diferencia
                if ((DateTime.Now.Date - ultimaActualizacion.Date).TotalDays <= 1)
                {
                    criptomoneda.ValorActual = valorActual;
                    criptomoneda.UltimaActualizacion = ultimaActualizacion;

                    return _context.SaveChanges() > 0;
                }
            }
            return false;
        }
    }
}
