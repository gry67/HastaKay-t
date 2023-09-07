using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using DataAccessLayer.Repositories;

namespace DataAccessLayer.CRUD
{
    public class crud
    {
        private readonly RepositoryContex context;
        public crud(RepositoryContex _context)
        {
            context = _context;
        }

        public async Task<bool> HastaCreateAsync(Hasta hasta)
        {
            try
            {
                await context.Hastalar.AddAsync(hasta);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }


        public async Task<List<Hasta>> HastaReadAsync()
        {
            try
            {
                List<Hasta> hastalar = await context.Hastalar.ToListAsync();
                return hastalar;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


        public async Task<Hasta> HastaReadByIdAsync(int id)
        {
            var hasta = await context.Hastalar.FindAsync(id);
            return hasta;
        }

        public async Task<bool> HastaUpdateAsync(Hasta guncelHasta)
        {
            try
            {
                var mevcuthasta = await context.Hastalar.FindAsync(guncelHasta.HastaId);

                if (mevcuthasta != null)
                {
                    mevcuthasta.HastaAdi = guncelHasta.HastaAdi;
                    mevcuthasta.HastaSoyadi = guncelHasta.HastaSoyadi;
                    mevcuthasta.HastaKimlikNo = guncelHasta.HastaKimlikNo;

                    await context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }


        public async Task<bool> HastaDeleteAsync(Hasta silinecekHasta)
        {
            try
            {
                Hasta mevcutHasta = await context.Hastalar.FindAsync(silinecekHasta.HastaId);

                if (mevcutHasta != null)
                {
                    context.Hastalar.Remove(mevcutHasta);
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }



        public async Task<bool> HastaDeleteByIdAsync(int id)
        {
            try
            {
                Hasta mevcutHasta = await context.Hastalar.FindAsync(id);

                if (mevcutHasta != null)
                {
                    context.Hastalar.Remove(mevcutHasta);
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

    }
}
