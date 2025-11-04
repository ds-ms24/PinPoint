using PinPoint.Models.DeleteRequests;

namespace PinPoint.Services.DeleteRequests
{
    public interface IDeleteRequestsService
    {
        Task<List<DeleteRequestReadOnlyVM>> GetAllPending();
        Task<List<DeleteRequestReadOnlyVM>> GetUserRequests(string userId);
        Task Create(DeleteRequestCreateVM model, string userId);
        Task<bool> Approve(int id, string reviewerId, string? notes);
        Task<bool> Reject(int id, string reviewerId, string? notes);
        Task<bool> AlreadyRequested(int painEntryId);

        Task<bool> Cancel(int id, string userId);
    }
}
