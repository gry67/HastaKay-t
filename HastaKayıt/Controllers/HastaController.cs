using BusinessLayer.Abstract;
using BusinessLayer.Abstract;
using DataAccessLayer.CRUD;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace HastaKayıt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HastaController : ControllerBase 
    {
        private readonly IHastaService services;

        public HastaController(IHastaService _services)
        {
            services = _services;
        }

        [HttpPost]
        public async Task<IActionResult> HastaEkle([FromBody] Hasta hasta)
        {
            await services.CreateHastaServiceAsync(hasta);
            return Ok();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> HastaSorgulaByIdAsync([FromRoute(Name ="id")]int id)
        {
            var hasta =await services.HastaSorguByIdServiceAsync(id);
            if (hasta == null)
            {
                NotFound("Hasta Bulunamadı");
            }
            return Ok(hasta);
        }

        [HttpGet]
        public async Task<IActionResult> HastalarGetirAsync()
        {
            var hastalar = await services.HastalariGetirServiceAsync();
            string log = "log alındı controller";
            return Ok(hastalar);
        }

        [HttpPut]
        public async Task<IActionResult> HastaGuncelle([FromBody] Hasta hasta)
        {
            bool islem = await services.UpdateHastaServiceAsync(hasta);
            if (islem) 
            { 
                return Ok("Hasta Güncellendi"); 
            }else 
            { 
                return BadRequest("Hasta Güncellenemedi");
            }
        }
        
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> HastaDeleteAsync([FromRoute]int id)
        {
            bool kontrol=await services.HastaDeleteByIdServiceAsync(id);
            if (kontrol)
            {
                return Ok("Hasta Başarıyla silindi");
            }
            else { return BadRequest("İşlem Başarısız!"); }
        }

    }
}
