using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PinPoint.Data;
using PinPoint.Models.Locations;
using PinPoint.Models.Symptoms;
using PinPoint.Services.Locations;

namespace PinPoint.Controllers
{   
    [Authorize(Roles ="Developer")]
    public class LocationsController(ILocationsService locationsService) : Controller
    {
        private readonly ILocationsService _locationsService = locationsService;
        private const string NameExistsValidationMessage = "This location already exists.";

        // GET: Locations
        public async Task<IActionResult> Index()
        {
            var viewData = await _locationsService.GetAll();
            return View(viewData);
        }

        // GET: Locations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _locationsService.Get<LocationReadOnlyVM>(id.Value);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // GET: Locations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Locations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LocationCreateVM locationCreate)
        {   
            if (await _locationsService.CheckIfLocationNameExistsAsync(locationCreate.Name))
            {
                ModelState.AddModelError(nameof(locationCreate.Name), NameExistsValidationMessage);
            }
            if (ModelState.IsValid)
            {
                await _locationsService.Create(locationCreate);
                return RedirectToAction(nameof(Index));
            }
            return View(locationCreate);
        }

        // GET: Locations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _locationsService.Get<LocationEditVM>(id.Value);
            if (location == null)
            {
                return NotFound();
            }
            return View(location);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LocationEditVM locationEdit)
        {
            if (id != locationEdit.Id)
            {
                return NotFound();
            }

            // Check if Location name exists
            if (await _locationsService.CheckIfLocationNameExistsForEditAsync(locationEdit))
            {
                ModelState.AddModelError(nameof(locationEdit.Name), NameExistsValidationMessage);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _locationsService.Edit(locationEdit);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! _locationsService.LocationExists(locationEdit.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(locationEdit);
        }

        // GET: Locations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _locationsService.Get<LocationReadOnlyVM>(id.Value);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _locationsService.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
