using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PinPoint.Data;
using PinPoint.Models.PainEntries;
using PinPoint.Models.PainEntry;
using PinPoint.Models.Symptoms;

namespace PinPoint.Services.PainEntries
{
    public class PainEntriesService(ApplicationDbContext context, IMapper mapper) : IPainEntriesService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<List<PainEntryReadOnlyVM>> GetAll()
        {
            var data = await _context.PainEntries.ToListAsync();
            var viewData = _mapper.Map<List<PainEntryReadOnlyVM>>(data);
            return viewData;
        }

        public async Task<T> Get<T>(int id) where T : class
        {
            var data = await _context.PainEntries.FirstOrDefaultAsync(x => x.Id == id);
            if (data == null)
            {
                return null;
            }

            var viewData = _mapper.Map<T>(data);
            return viewData;
        }

        public async Task Edit(PainEntryEditVM model)
        {
            var painEntry = _mapper.Map<PainEntry>(model);
            _context.Update(painEntry);
            await _context.SaveChangesAsync();
        }

        public async Task Create(PainEntryCreateVM model)
        {
            var painEntry = _mapper.Map<PainEntry>(model);
            _context.Add(painEntry);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(int id)
        {
            var data = await _context.PainEntries.FirstOrDefaultAsync(x => x.Id == id);
            if (data != null)
            {
                _context.Remove(data);
                await _context.SaveChangesAsync();
            }
        }

        public bool PainEntryExists(int id)
        {
            return _context.PainEntries.Any(e => e.Id == id);
        }
    }
}
