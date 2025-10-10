using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PinPoint.Data;
using PinPoint.Models.Locations;
using PinPoint.Models.Triggers;
using PinPoint.Services.Locations;

namespace PinPoint.Services.Triggers
{
    public class TriggersService(ApplicationDbContext context, IMapper mapper) : ITriggersService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<List<TriggerReadOnlyVM>> GetAll()
        {
            var data = await _context.Triggers.ToListAsync();
            var viewData = _mapper.Map<List<TriggerReadOnlyVM>>(data);
            return viewData;
        }

        public async Task<T> Get<T>(int id) where T : class
        {
            var data = await _context.Triggers.FirstOrDefaultAsync(x => x.Id == id);
            if (data == null)
            {
                return null;
            }

            var viewData = _mapper.Map<T>(data);
            return viewData;
        }

        public async Task Edit(TriggerEditVM model)
        {
            var trigger = _mapper.Map<Trigger>(model);
            _context.Update(trigger);
            await _context.SaveChangesAsync();
        }

        public async Task Create(TriggerCreateVM model)
        {
            var trigger = _mapper.Map<Trigger>(model);
            _context.Add(trigger);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(int id)
        {
            var data = await _context.Triggers.FirstOrDefaultAsync(x => x.Id == id);
            if (data != null)
            {
                _context.Remove(data);
                await _context.SaveChangesAsync();
            }
        }

        public bool TriggerExists(int id)
        {
            return _context.Triggers.Any(e => e.Id == id);
        }

        public async Task<bool> CheckIfTriggerNameExistsAsync(string name)
        {
            var lowercaseName = name.ToLower();
            return await _context.Triggers.AnyAsync(q => q.Name.ToLower().Equals(lowercaseName));
        }

        public async Task<bool> CheckIfTriggerNameExistsForEditAsync(TriggerEditVM triggerEdit)
        {
            var lowercaseName = triggerEdit.Name.ToLower();
            return await _context.Triggers.AnyAsync(q => q.Name.ToLower().Equals(lowercaseName) && q.Id != triggerEdit.Id);
        }
    }
}
