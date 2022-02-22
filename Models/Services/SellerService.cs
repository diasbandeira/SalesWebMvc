using SalesWebMvc.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Models.Services.Exceptions;

namespace SalesWebMvc.Models.Services
{
    public class SellerService
    {
        private SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;        
        }

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        public async Task InsertAsync(Seller seller)
        {
            _context.Add(seller);
            await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var obj = await _context.Seller.FindAsync(id);
                _context.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException("Não foi possivel excluir por que existem vendas associadas a(o) vendedor(a)");
            }
        }

        public async Task UpdateAsync(Seller seller)
        {
            bool exist = await _context.Seller.AnyAsync(x => x.Id == seller.Id);


            if (!exist)
            {
                throw new NotFoundException("Id not Found");
            }
            try
            {
                _context.Update(seller);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }

    }
}
