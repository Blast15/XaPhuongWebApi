using Empty.Datas;
using Empty.Models;
using Microsoft.EntityFrameworkCore;

namespace Empty.Services
{
    public class XaPhuongService
    {
        private readonly XaPhuongContext _context;

        public XaPhuongService(XaPhuongContext context)
        {
            _context = context;
        }

        public async Task<List<XaPhuong>> GetAllAsync()
        {
            return await _context.XaPhuongs.ToListAsync();
        }

        public async Task<XaPhuong> GetByIdAsync(int id)
        {
            return await _context.XaPhuongs.FindAsync(id);
        }

        public async Task<XaPhuong> CreateAsync(XaPhuong xaPhuong)
        {
            _context.XaPhuongs.Add(xaPhuong);
            await _context.SaveChangesAsync();
            return xaPhuong;
        }

        public async Task UpdateAsync(XaPhuong xaPhuong)
        {
            _context.Entry(xaPhuong).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var xaPhuong = await _context.XaPhuongs.FindAsync(id);
            if (xaPhuong != null)
            {
                _context.XaPhuongs.Remove(xaPhuong);
                await _context.SaveChangesAsync();
            }
        }
    }
}
