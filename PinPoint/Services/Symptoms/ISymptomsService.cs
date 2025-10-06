using PinPoint.Models.Symptoms;

namespace PinPoint.Services.Symptoms
{
    public interface ISymptomsService
    {
        Task<bool> CheckIfSymptomNameExistsAsync(string name);
        Task<bool> CheckIfSymptomNameExistsForEditAsync(SymptomEditVM symptomEdit);
        Task Create(SymptomCreateVM model);
        Task Edit(SymptomEditVM model);
        Task<T?> Get<T>(int id) where T : class;
        Task<List<SymptomReadOnlyVM>> GetAll();
        Task Remove(int id);
        bool SymptomExists(int id);
    }
}