using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PinPoint.Data;
using PinPoint.Models.Locations;
using PinPoint.Models.PainEntries;
using PinPoint.Models.PainEntry;
using PinPoint.Models.Symptoms;

namespace PinPoint.Services.Locations;

public class LocationsService(ApplicationDbContext context, IMapper mapper) : ILocationsService
{
    private readonly ApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<List<LocationReadOnlyVM>> GetAll()
    {
        var data = await _context.Locations.ToListAsync();
        var viewData = _mapper.Map<List<LocationReadOnlyVM>>(data);
        return viewData;
    }

    public async Task<T> Get<T>(int id) where T : class
    {
        var data = await _context.Locations.FirstOrDefaultAsync(x => x.Id == id);
        if (data == null)
        {
            return null;
        }

        var viewData = _mapper.Map<T>(data);
        return viewData;
    }

    public async Task Edit(LocationEditVM model)
    {
        var location = _mapper.Map<Location>(model);
        _context.Update(location);
        await _context.SaveChangesAsync();
    }

    public async Task Create(LocationCreateVM model)
    {
        var location = _mapper.Map<Location>(model);
        _context.Add(location);
        await _context.SaveChangesAsync();
    }

    public async Task Remove(int id)
    {
        var data = await _context.Locations.FirstOrDefaultAsync(x => x.Id == id);
        if (data != null)
        {
            _context.Remove(data);
            await _context.SaveChangesAsync();
        }
    }

    public bool LocationExists(int id)
    {
        return _context.Locations.Any(e => e.Id == id);
    }

    public async Task<bool> CheckIfLocationNameExistsAsync(string name)
    {
        var lowercaseName = name.ToLower();
        return await _context.Locations.AnyAsync(q => q.Name.ToLower().Equals(lowercaseName));
    }

    public async Task<bool> CheckIfLocationNameExistsForEditAsync(LocationEditVM locationEdit)
    {
        var lowercaseName = locationEdit.Name.ToLower();
        return await _context.Locations.AnyAsync(q => q.Name.ToLower().Equals(lowercaseName) && q.Id != locationEdit.Id);
    }
}
