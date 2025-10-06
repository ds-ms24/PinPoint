using PinPoint.Models.Triggers;

namespace PinPoint.Services.Triggers
{
    public interface ITriggersService
    {
        Task<bool> CheckIfTriggerNameExistsAsync(string name);
        Task<bool> CheckIfTriggerNameExistsForEditAsync(TriggerEditVM triggerEdit);
        Task Create(TriggerCreateVM model);
        Task Edit(TriggerEditVM model);
        Task<T> Get<T>(int id) where T : class;
        Task<List<TriggerReadOnlyVM>> GetAll();
        bool TriggerExists(int id);
        Task Remove(int id);
    }
}