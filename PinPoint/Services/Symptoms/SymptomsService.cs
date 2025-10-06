using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PinPoint.Data;
using PinPoint.Models.Symptoms;

namespace PinPoint.Services.Symptoms;

public class SymptomsService(ApplicationDbContext context, IMapper mapper) : ISymptomsService
{
    private readonly ApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<List<SymptomReadOnlyVM>> GetAll()
    {
        var data = await _context.Symptoms.ToListAsync();
        var viewData = _mapper.Map<List<SymptomReadOnlyVM>>(data);
        return viewData;
    }

    public async Task<T?> Get<T>(int id) where T : class
    {
        var data = await _context.Symptoms.FirstOrDefaultAsync(x => x.Id == id);
        if (data == null)
        {
            return null;
        }

        var viewData = _mapper.Map<T>(data);
        return viewData;
    }

    public async Task Remove(int id)
    {
        var data = await _context.Symptoms.FirstOrDefaultAsync(x => x.Id == id);
        if (data != null)
        {
            _context.Remove(data);
            await _context.SaveChangesAsync();
        }
    }

    public async Task Edit(SymptomEditVM model)
    {
        var symptom = _mapper.Map<Symptom>(model);
        _context.Update(symptom);
        await _context.SaveChangesAsync();
    }
    public async Task Create(SymptomCreateVM model)
    {
        var symptom = _mapper.Map<Symptom>(model);
        _context.Add(symptom);
        await _context.SaveChangesAsync();
    }

    public bool SymptomExists(int id)
    {
        return _context.Symptoms.Any(e => e.Id == id);
    }
    public async Task<bool> CheckIfSymptomNameExistsAsync(string name)
    {
        var lowercaseName = name.ToLower();
        return await _context.Symptoms.AnyAsync(q => q.Name.ToLower().Equals(lowercaseName));
    }

    public async Task<bool> CheckIfSymptomNameExistsForEditAsync(SymptomEditVM symptomEdit)
    {
        var lowercaseName = symptomEdit.Name.ToLower();
        return await _context.Symptoms.AnyAsync(q => q.Name.ToLower().Equals(lowercaseName) && q.Id != symptomEdit.Id);
    }
}
