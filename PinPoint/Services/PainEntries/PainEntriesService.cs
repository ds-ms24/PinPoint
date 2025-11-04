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

        public async Task<List<PainEntryReadOnlyVM>> GetAll(string? searchString = null, string? sortOrder = null)
        {
            var query = _context.PainEntries
                .Include(q => q.PainEntrySymptoms)
                    .ThenInclude(q => q.Symptom)
                .Include(q => q.PainEntryLocations)
                    .ThenInclude(q => q.Location)
                .Include(q => q.PainEntryTriggers)
                    .ThenInclude(q => q.Trigger)
                .AsQueryable();

            // SEARCH FILTER
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(q =>
                    q.PainDescription!.ToUpper().Contains(searchString.ToUpper()) ||
                    q.ActivitiesBeforePain!.ToUpper().Contains(searchString.ToUpper()) ||
                    q.ReliefMethodsTried!.ToUpper().Contains(searchString.ToUpper()) ||
                    (q.AdditionalNotes != null && q.AdditionalNotes.ToUpper().Contains(searchString.ToUpper())) ||
                    q.PainEntrySymptoms.Any(q => q.Symptom.Name.ToUpper().Contains(searchString.ToUpper())) ||
                    q.PainEntryLocations.Any(q => q.Location.Name.ToUpper().Contains(searchString.ToUpper())) ||
                    q.PainEntryTriggers.Any(q => q.Trigger.Name.ToUpper().Contains(searchString.ToUpper()))
                );
            }

            // SORTING
            query = sortOrder switch
            {
                "date_desc" => query.OrderByDescending(q => q.EntryDate).ThenByDescending(q => q.EntryTime),
                "time" => query.OrderBy(q => q.EntryTime),
                "time_desc" => query.OrderByDescending(q => q.EntryTime),
                "intensity" => query.OrderBy(q => q.PainIntensity),
                "intensity_desc" => query.OrderByDescending(q => q.PainIntensity),
                "description" => query.OrderBy(q => q.PainDescription),
                "description_desc" => query.OrderByDescending(q => q.PainDescription),
                "duration" => query.OrderBy(q => q.DurationMinutes),
                "duration_desc" => query.OrderByDescending(q => q.DurationMinutes),
                "activities" => query.OrderBy(q => q.ActivitiesBeforePain),
                "activities_desc" => query.OrderByDescending(q => q.ActivitiesBeforePain),
                "relief_methods" => query.OrderBy(q => q.ReliefMethodsTried),
                "relief_methods_desc" => query.OrderByDescending(q => q.ReliefMethodsTried),
                "relief_effectiveness" => query.OrderBy(q => q.ReliefEffectiveness),
                "relief_effectiveness_desc" => query.OrderByDescending(q => q.ReliefEffectiveness),
                "notes" => query.OrderBy(q => q.AdditionalNotes),
                "notes_desc" => query.OrderByDescending(q => q.AdditionalNotes),
                _ => query.OrderBy(q => q.EntryDate).ThenBy(q => q.EntryTime), 
            };

            var painEntries = await query.ToListAsync();
            var viewData = _mapper.Map<List<PainEntryReadOnlyVM>>(painEntries);

            // Get all pending delete request pain entry IDs
            var pendingDeleteRequestIds = await _context.DeleteRequests
                .Where(q => q.Status == DeleteRequestStatusEnum.Pending)
                .Select(q => q.PainEntryId)
                .ToListAsync();
    
            // Mark pain entries that have pending delete requests
            foreach (var x in viewData)
            {
                x.PendingDeleteRequest = pendingDeleteRequestIds.Contains(x.Id);
            }
    
            return viewData;
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
