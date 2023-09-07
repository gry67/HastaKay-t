using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using DataAccessLayer.CRUD;
using DataAccessLayer.Models;

namespace BusinessLayer.Concrete
{
    public class HastaService : IHastaService
    {
        //dependency injection
        private readonly crud crud;
        private readonly ILoggerService logger;
        public HastaService(crud _crud, ILoggerService _logger)
        {
            crud = _crud;
            logger = _logger;
        }
        //end


        public async Task<Hasta> CreateHastaServiceAsync(Hasta hasta)
        {
            bool kontrol =await crud.HastaCreateAsync(hasta);
            if (kontrol)
            {
                logger.LogInfo("Hasta kaydı oluşturuldu");
                return hasta;
            }
            else
            {
                logger.LogError("Hasta kaydı oluşturulamadı");
                return null;
            }
        }

        public async Task<Hasta> HastaSorguByIdServiceAsync(int id)
        {
            Hasta hasta = await crud.HastaReadByIdAsync(id);
            logger.LogInfo($"Bu id bilgisi ile kayıt sorgulandı: {id}");
            return hasta;
        }

        public async Task<List<Hasta>> HastalariGetirServiceAsync()
        {
            List<Hasta> hastalar = await crud.HastaReadAsync();
            logger.LogInfo("Bütün kayıtlar listelendi");
            return hastalar;
        }

        public async Task<bool> UpdateHastaServiceAsync(Hasta hasta)
        {
            bool islem_sonucu = await crud.HastaUpdateAsync(hasta);
            
            if (islem_sonucu) 
            {
                logger.LogInfo($"Bu id'ye sahip hasta güncellendi: {hasta.HastaId}");
                return true; 
            } else 
            {
                logger.LogError($"Bu id'ye sahip hasta güncellenemedi: {hasta.HastaId}");
                return false; 
            }
        }
    
        public async Task<bool> HastaDeleteServiceAsync(Hasta hasta)
        {
            var sonuc = crud.HastaReadByIdAsync(hasta.HastaId);
            if (sonuc != null) 
            {
                await crud.HastaDeleteAsync(hasta);
                logger.LogInfo($"Bu hasta silindi: id={hasta.HastaId} isim={hasta.HastaAdi}");
                return true;
            }
            else
            {
                logger.LogError($"kayıtlarda olmayan bir hasta silinmeye çalışıldı. Bilgiler: {hasta.HastaId} {hasta.HastaAdi}");
                return false;
            }
        }

        public async Task<bool> HastaDeleteByIdServiceAsync(int id)
        {
            bool sonuc=await crud.HastaDeleteByIdAsync(id);
            if (sonuc)
            {
                logger.LogInfo($"Bu hasta id'ye göre silindi: id={id}");
                return true;
            }else
            {
                logger.LogError($"Bu hasta silinemedi: id={id}");
                return false;
            }
            
        }     
    }
}
