using ParcialWebApi.Models;

namespace ParcialWebApi.Repositories
{
    public interface ICriptomonedaRepository
    {
        List<Criptomoneda> GetAll();
        List<Criptomoneda> GetBy(string cat);
        bool Create(Criptomoneda oCriptomoneda);
        bool Update(string simbolo, double valorActual, DateTime ultimaActualizacion);
        bool Delete(int id);
    }
}
