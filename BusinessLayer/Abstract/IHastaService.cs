using DataAccessLayer.Models;

namespace BusinessLayer.Abstract
{
    public interface IHastaService
    {
        Task<Hasta> CreateHastaServiceAsync(Hasta hasta);
        Task<Hasta> HastaSorguByIdServiceAsync(int id);
        Task<List<Hasta>> HastalariGetirServiceAsync();
        Task<bool> UpdateHastaServiceAsync(Hasta hasta);
        Task<bool> HastaDeleteServiceAsync(Hasta hasta);
        Task<bool> HastaDeleteByIdServiceAsync(int id);
    }
}