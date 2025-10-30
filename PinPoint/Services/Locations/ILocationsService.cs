using PinPoint.Models.Locations;

namespace PinPoint.Services.Locations
{
    public interface ILocationsService
    {
        Task<bool> CheckIfLocationNameExistsAsync(string name);
        Task<bool> CheckIfLocationNameExistsForEditAsync(LocationEditVM locationEdit);
        Task Create(LocationCreateVM model);
        Task Edit(LocationEditVM model);
        Task<T> Get<T>(int id) where T : class;
        Task<List<LocationReadOnlyVM>> GetAll();
        bool LocationExists(int id);
        Task Remove(int id);
        Task<bool> IsLocationInUse(int locationId);
    }
}