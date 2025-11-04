using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PinPoint.Migrations;
using PinPoint.Models.DeleteRequests;
using PinPoint.Services.DeleteRequests;

namespace PinPoint.Controllers;

[Authorize]
public class DeleteRequestsController : Controller
{
    private readonly IDeleteRequestsService _deleteRequestsService;
    private readonly UserManager<ApplicationUser> _userManager;

    public DeleteRequestsController(IDeleteRequestsService deleteRequestsService, UserManager<ApplicationUser> userManager)
    {
        _deleteRequestsService = deleteRequestsService;
        _userManager = userManager;
    }

    // GET: DeleteRequests (For Employees and above)
    [Authorize(Roles = "Employee,Manager,Developer")]
    public async Task<IActionResult> Index(string? message, string? messageType)
    {
        if (!string.IsNullOrEmpty(message))
        {
            ViewData["Message"] = message;
            ViewData["MessageType"] = messageType ?? "info";
        }

        var requests = await _deleteRequestsService.GetAllPending();
        return View(requests);
    }

    // GET: DeleteRequests/MyRequests (For Patients)
    [Authorize(Roles = "Patient")]
    public async Task<IActionResult> MyRequests(string? message, string? messageType)
    {
        if (!string.IsNullOrEmpty(message))
        {
            ViewData["Message"] = message;
            ViewData["MessageType"] = messageType ?? "info";
        }

        var userId = _userManager.GetUserId(User);
        var requests = await _deleteRequestsService.GetUserRequests(userId!);
        return View(requests);
    }

    // GET: DeleteRequests/Create
    [Authorize(Roles = "Patient")]
    public IActionResult Create(int painEntryId)
    {
        var model = new DeleteRequestCreateVM { PainEntryId = painEntryId };
        return View(model);
    }

    // POST: DeleteRequests/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Patient")]
    public async Task<IActionResult> Create(DeleteRequestCreateVM model)
    {
        if (await _deleteRequestsService.AlreadyRequested(model.PainEntryId))
        {
            ViewData["Message"] = "A delete request for this pain entry is already pending.";
            ViewData["MessageType"] = "warning";
            return View(model);
        }

        if (ModelState.IsValid)
        {
            var userId = _userManager.GetUserId(User);
            await _deleteRequestsService.Create(model, userId!);
            
            return RedirectToAction("MyRequests", new 
            { 
                message = "Delete request submitted successfully.", 
                messageType = "success" 
            });
        }

        return View(model);
    }

    // POST: DeleteRequests/Approve
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Employee,Manager,Developer")]
    public async Task<IActionResult> Approve(int id, string? notes)
    {
        var reviewerId = _userManager.GetUserId(User);
        var success = await _deleteRequestsService.Approve(id, reviewerId!, notes);

        if (success)
        {
            return RedirectToAction(nameof(Index), new 
            { 
                message = "Delete request approved and pain entry deleted.", 
                messageType = "success" 
            });
        }

        return RedirectToAction(nameof(Index), new 
        { 
            message = "Unable to approve delete request.", 
            messageType = "danger" 
        });
    }

    // POST: DeleteRequests/Reject
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Employee,Manager,Developer")]
    public async Task<IActionResult> Reject(int id, string? notes)
    {
        var reviewerId = _userManager.GetUserId(User);
        var success = await _deleteRequestsService.Reject(id, reviewerId!, notes);

        if (success)
        {
            return RedirectToAction(nameof(Index), new 
            { 
                message = "Delete request rejected.", 
                messageType = "success" 
            });
        }

        return RedirectToAction(nameof(Index), new 
        { 
            message = "Unable to reject delete request.", 
            messageType = "danger" 
        });
    }

    // POST: DeleteRequests/Cancel
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Patient")]
    public async Task<IActionResult> Cancel(int id)
    {
        var userId = _userManager.GetUserId(User);
        var success = await _deleteRequestsService.Cancel(id, userId!);

        if (success)
        {
            return RedirectToAction("MyRequests", new 
            { 
                message = "Delete request cancelled successfully.", 
                messageType = "success" 
            });
        }

        return RedirectToAction("MyRequests", new 
        { 
            message = "Unable to cancel delete request.", 
            messageType = "danger" 
        });
    }
}
