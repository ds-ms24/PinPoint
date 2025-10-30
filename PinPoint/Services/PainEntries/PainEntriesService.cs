using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PinPoint.Data;
using PinPoint.Models.PainEntries;
using PinPoint.Models.PainEntry;
using PinPoint.Models.Symptoms;
using PinPoint.Services.Symptoms;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PinPoint.Services.PainEntries
{
    public class PainEntriesService(ApplicationDbContext context, IMapper mapper) : IPainEntriesService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<List<PainEntryReadOnlyVM>> GetAll()
        {
            var painEntries = await _context.PainEntries
                .Include(q => q.PainEntrySymptoms)
                    .ThenInclude(q => q.Symptom)
                .Include(q => q.PainEntryLocations)
                    .ThenInclude(q => q.Location)
                .Include(q => q.PainEntryTriggers)
                    .ThenInclude(q => q.Trigger)
                .ToListAsync();
            
        return _mapper.Map<List<PainEntryReadOnlyVM>>(painEntries);
        }

        public async Task<PainEntryEditVM> GetForEdit(int? id)
        {
            var painEntry = await _context.PainEntries
                .Include(q => q.PainEntrySymptoms)
                    .ThenInclude(q => q.Symptom)
                .Include(q => q.PainEntryLocations)
                    .ThenInclude(q => q.Location)
                .Include(q => q.PainEntryTriggers)
                    .ThenInclude(q => q.Trigger)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (painEntry == null)
            {
                return null;
            }
            
        return _mapper.Map<PainEntryEditVM>(painEntry);
        }

        public async Task<T?> Get<T>(int id) where T : class
        {
            var painEntry = await _context.PainEntries
                .Include(q => q.PainEntrySymptoms)
                    .ThenInclude(q => q.Symptom)
                .Include(q => q.PainEntryLocations)
                    .ThenInclude(q => q.Location)
                .Include(q => q.PainEntryTriggers)
                    .ThenInclude(q => q.Trigger)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (painEntry == null)
                return null;

            return _mapper.Map<T>(painEntry);
        }

        public async Task Edit(PainEntryEditVM model)
        {
            var painEntry = await _context.PainEntries
                .Include(q => q.PainEntrySymptoms)
                .Include(q => q.PainEntryLocations)
                .Include(q => q.PainEntryTriggers)
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (painEntry == null) 
                return;

            _mapper.Map(model, painEntry);

            painEntry.PainEntrySymptoms.Clear();
            painEntry.PainEntrySymptoms = (model.SymptomIds ?? new List<int>())
                .Distinct()
                .Select(symptomId => new PainEntrySymptom 
                { 
                    PainEntryId = painEntry.Id,
                    SymptomId = symptomId 
                })
                .ToList();

            painEntry.PainEntryLocations.Clear();
            painEntry.PainEntryLocations = (model.LocationIds ?? new List<int>())
                .Distinct()
                .Select(locationId => new PainEntryLocation 
                { 
                    PainEntryId = painEntry.Id,
                    LocationId = locationId 
                })
                .ToList();

            painEntry.PainEntryTriggers.Clear();
            painEntry.PainEntryTriggers = (model.TriggerIds ?? new List<int>())
                .Distinct()
                .Select(triggerId => new PainEntryTrigger 
                { 
                    PainEntryId = painEntry.Id,
                    TriggerId = triggerId 
                })
                .ToList();


            await _context.SaveChangesAsync();
        }

        public async Task Create(PainEntryCreateVM model)
        {
            var painEntry = _mapper.Map<PainEntry>(model);

            painEntry.PainEntrySymptoms = (model.SymptomIds ?? new List<int>())
                .Distinct()
                .Select(symptomId => new PainEntrySymptom 
                { 
                    SymptomId = symptomId 
                })
                .ToList();

            painEntry.PainEntryLocations = (model.LocationIds ?? new List<int>())
                .Distinct()
                .Select(locationId => new PainEntryLocation 
                { 
                    LocationId = locationId 
                })
                .ToList();

            painEntry.PainEntryTriggers = (model.TriggerIds ?? new List<int>())
                .Distinct()
                .Select(triggerId => new PainEntryTrigger 
                {   
                    TriggerId = triggerId   
                })
                .ToList();
            
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
