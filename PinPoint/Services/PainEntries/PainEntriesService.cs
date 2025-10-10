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
            var painEntries = await _context.PainEntries
                .Include(p => p.Symptom)
                .ToListAsync();
            
        return _mapper.Map<List<PainEntryReadOnlyVM>>(painEntries);
        }

        public async Task<T?> Get<T>(int id) where T : class
        {
            var painEntry = await _context.PainEntries
                .Include(q => q.Symptom)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (painEntry == null)
                return null;

            return _mapper.Map<T>(painEntry);
        }

        public async Task Edit(PainEntryEditVM model)
        {
            var painEntry = await _context.PainEntries.FindAsync(model.Id);
            if (painEntry != null)
            {
                _mapper.Map(model, painEntry);
                _context.Update(painEntry);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Create(PainEntryCreateVM model)
        {
            var painEntry = _mapper.Map<PainEntry>(model);
            _context.Add(painEntry);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(int id)
        {
            var painEntry = await _context.PainEntries.FindAsync(id);
            if (painEntry != null)
            {
                _context.PainEntries.Remove(painEntry);
                await _context.SaveChangesAsync();
            }
        }

        public bool PainEntryExists(int id)
        {
            return _context.PainEntries.Any(e => e.Id == id);
        }
    }
}
