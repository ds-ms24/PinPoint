using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PinPoint.Data;
using PinPoint.Migrations;
using PinPoint.Models.DeleteRequests;

namespace PinPoint.Services.DeleteRequests
{
    public class DeleteRequestsService : IDeleteRequestsService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public DeleteRequestsService(ApplicationDbContext context, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<List<DeleteRequestReadOnlyVM>> GetAllPending()
        {
            var requests = await _context.DeleteRequests
                .Include(q => q.PainEntry)
                    .ThenInclude(q => q.PainEntrySymptoms)
                        .ThenInclude(q => q.Symptom)
                .Include(q => q.PainEntry)
                    .ThenInclude(q => q.PainEntryLocations)
                        .ThenInclude(q => q.Location)
                .Include(q => q.PainEntry)
                    .ThenInclude(q => q.PainEntryTriggers)
                        .ThenInclude(q => q.Trigger)
                .Where(q => q.Status == DeleteRequestStatusEnum.Pending)
                .OrderBy(q => q.RequestedDate)
                .ToListAsync();

            var viewModels = new List<DeleteRequestReadOnlyVM>();
            
            foreach (var request in requests)
            {
                var viewData = _mapper.Map<DeleteRequestReadOnlyVM>(request);
                var user = await _userManager.FindByIdAsync(request.RequestedByUserId);
                viewData.RequestedByUserEmail = user?.Email;
                viewModels.Add(viewData);
            }
            
            return viewModels;
        }

        public async Task<List<DeleteRequestReadOnlyVM>> GetUserRequests(string userId)
        {
            var requests = await _context.DeleteRequests
                .Include(q => q.PainEntry)
                .Where(q => q.RequestedByUserId == userId)
                .OrderByDescending(q => q.RequestedDate)
                .ToListAsync();

            var viewModels = new List<DeleteRequestReadOnlyVM>();
            
            foreach (var request in requests)
            {
                var viewData = _mapper.Map<DeleteRequestReadOnlyVM>(request);
                
                if (request.ReviewedByUserId != null)
                {
                    var reviewer = await _userManager.FindByIdAsync(request.ReviewedByUserId);
                    viewData.ReviewedByUserEmail = reviewer?.Email;
                }
                
                viewModels.Add(viewData);
            }
            
            return viewModels;
        }

        public async Task Create(DeleteRequestCreateVM model, string userId)
        {   
            var deleteRequest = new DeleteRequest
            {
                PainEntryId = model.PainEntryId,
                RequestedByUserId = userId,
                Reason = model.Reason,
                RequestedDate = DateOnly.FromDateTime(DateTime.Now),
                RequestedTime = TimeOnly.FromDateTime(DateTime.Now),
                Status = DeleteRequestStatusEnum.Pending
            };

            _context.DeleteRequests.Add(deleteRequest);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Approve(int id, string reviewerId, string? notes)
        {
            var request = await _context.DeleteRequests
                .Include(q => q.PainEntry)
                .FirstOrDefaultAsync(q => q.Id == id);

            if (request == null || request.Status != DeleteRequestStatusEnum.Pending)
                return false;

            request.Status = DeleteRequestStatusEnum.Approved;
            request.ReviewedByUserId = reviewerId;
            request.ReviewedDate = DateTime.UtcNow;
            request.ReviewerNotes = notes;

            // Delete the pain entry
            if (request.PainEntry != null)
            {
                _context.PainEntries.Remove(request.PainEntry);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Reject(int id, string reviewerId, string? notes)
        {
            var request = await _context.DeleteRequests
                .FirstOrDefaultAsync(q => q.Id == id);

            if (request == null || request.Status != DeleteRequestStatusEnum.Pending)
                return false;

            request.Status = DeleteRequestStatusEnum.Rejected;
            request.ReviewedByUserId = reviewerId;
            request.ReviewedDate = DateTime.UtcNow;
            request.ReviewerNotes = notes;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AlreadyRequested(int painEntryId)
        {
            return await _context.DeleteRequests
                .AnyAsync(q => q.PainEntryId == painEntryId && q.Status == DeleteRequestStatusEnum.Pending);
        }

        public async Task<bool> Cancel(int id, string userId)
        {
            var request = await _context.DeleteRequests
                .FirstOrDefaultAsync(q => q.Id == id && q.RequestedByUserId == userId);

            if (request == null || request.Status != DeleteRequestStatusEnum.Pending)
                return false;

            _context.DeleteRequests.Remove(request);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
