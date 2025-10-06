using PinPoint.Models.PainEntries;
using PinPoint.Models.PainEntry;

namespace PinPoint.Services.PainEntries
{
    public interface IPainEntriesService
    {
        Task Create(PainEntryCreateVM model);
        Task Edit(PainEntryEditVM model);
        Task<T> Get<T>(int id) where T : class;
        Task<List<PainEntryReadOnlyVM>> GetAll();
        Task Remove(int id);
        bool PainEntryExists(int id);
    }
}