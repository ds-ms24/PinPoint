using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PinPoint.Data;
using PinPoint.Models.Locations;
using PinPoint.Models.PainEntries;
using PinPoint.Models.PainEntry;
using PinPoint.Models.Symptoms;
using PinPoint.Models.Triggers;
using PinPoint.Services.Locations;
using PinPoint.Services.PainEntries;
using PinPoint.Services.Symptoms;
using PinPoint.Services.Triggers;
using System.Threading.Tasks;

namespace PinPoint.Controllers
{
    public class PainEntriesController(IPainEntriesService painEntriesService, ISymptomsService symptomsService, ILocationsService locationsService, ITriggersService triggersService) : Controller
    {
        private readonly IPainEntriesService _painEntriesService = painEntriesService;
        private readonly ISymptomsService _symptomsService = symptomsService;
        private readonly ILocationsService _locationsService = locationsService;
        private readonly ITriggersService _triggersService = triggersService;

        // GET: PainEntries
        public async Task<IActionResult> Index()
        {
            var viewData = await _painEntriesService.GetAll();
            return View(viewData);
        }

        // GET: PainEntries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var painEntry = await _painEntriesService.Get<PainEntryDetailsVM>(id.Value);
            if (painEntry == null)
            {
                return NotFound();
            }

            return View(painEntry);
        }

        // GET: PainEntries/Create
        public async Task<IActionResult> Create()
        {   
            var model = new PainEntryCreateVM
            {
                EntryDate = DateOnly.FromDateTime(DateTime.Today),
                EntryTime = TimeOnly.FromDateTime(DateTime.Now)
            };

            await LoadDropdowns();
            return View(model);
        }

        // POST: PainEntries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PainEntryCreateVM painEntryCreate)
        {   
            // SYMPTOM
            if (!string.IsNullOrWhiteSpace(painEntryCreate.NewSymptomName))
            {
                // Check if symptom already exists
                var symptomExists = await _symptomsService.CheckIfSymptomNameExistsAsync(painEntryCreate.NewSymptomName);

                if (!symptomExists)
                {
                    // Create new symptom
                    var newSymptom = new SymptomCreateVM
                    {
                        Name = painEntryCreate.NewSymptomName
                    };

                    await _symptomsService.Create(newSymptom);

                    // Get ID of new symptom
                    var allSymptoms = await _symptomsService.GetAll();
                    var createdSymptom = allSymptoms.FirstOrDefault
                        (q => q.Name == painEntryCreate.NewSymptomName);

                    if (createdSymptom != null)
                    {
                        painEntryCreate.SymptomId = createdSymptom.Id;
                    }
                }
                else
                {
                    // If symptom exists, find ID and use
                    var existingSymptoms = await _symptomsService.GetAll();
                    var existingSymptom = existingSymptoms.FirstOrDefault
                        (q => q.Name == painEntryCreate.NewSymptomName);

                    if (existingSymptom != null)
                    {
                        painEntryCreate.SymptomId = existingSymptom.Id;
                    }
                }
            }

            // LOCATION
            if (!string.IsNullOrWhiteSpace(painEntryCreate.NewLocationName))
            {
                // Check if location already exists
                var locationExists = await _locationsService.CheckIfLocationNameExistsAsync(painEntryCreate.NewLocationName);

                if (!locationExists)
                {
                    // Create new location
                    var newLocation = new LocationCreateVM
                    {
                        Name = painEntryCreate.NewLocationName
                    };

                    await _locationsService.Create(newLocation);

                    // Get ID of new location
                    var allLocations = await _locationsService.GetAll();
                    var createdLocation = allLocations.FirstOrDefault
                        (q => q.Name == painEntryCreate.NewLocationName);

                    if (createdLocation != null)
                    {
                        painEntryCreate.LocationId = createdLocation.Id;
                    }
                }
                else
                {
                    // If location exists, find ID and use
                    var existingLocations = await _locationsService.GetAll();
                    var existingLocation = existingLocations.FirstOrDefault
                        (q => q.Name == painEntryCreate.NewLocationName);

                    if (existingLocation != null)
                    {
                        painEntryCreate.LocationId = existingLocation.Id;
                    }
                }
            }

            // TRIGGER
            if (!string.IsNullOrWhiteSpace(painEntryCreate.NewTriggerName))
            {
                // Check if symptom already exists
                var triggerExists = await _triggersService.CheckIfTriggerNameExistsAsync(painEntryCreate.NewTriggerName);

                if (!triggerExists)
                {
                    // Create new trigger
                    var newTrigger = new TriggerCreateVM
                    {
                        Name = painEntryCreate.NewTriggerName
                    };

                    await _triggersService.Create(newTrigger);

                    // Get ID of new trigger
                    var allTriggers = await _triggersService.GetAll();
                    var createdTrigger = allTriggers.FirstOrDefault
                        (q => q.Name == painEntryCreate.NewTriggerName);

                    if (createdTrigger != null)
                    {
                        painEntryCreate.TriggerId = createdTrigger.Id;
                    }
                }
                else
                {
                    // If symptom exists, find ID and use
                    var existingTriggers = await _triggersService.GetAll();
                    var existingTrigger = existingTriggers.FirstOrDefault
                        (q => q.Name == painEntryCreate.NewTriggerName);

                    if (existingTrigger != null)
                    {
                        painEntryCreate.TriggerId = existingTrigger.Id;
                    }
                }
            }

            if (ModelState.IsValid)
            {
                await _painEntriesService.Create(painEntryCreate);
                return RedirectToAction(nameof(Index));
            }
            
            await LoadDropdowns();
            return View(painEntryCreate);
        }

        // GET: PainEntries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var painEntry = await _painEntriesService.Get<PainEntryEditVM>(id.Value);
            if (painEntry == null)
            {
                return NotFound();
            }

            await LoadDropdowns();
            return View(painEntry);
        }

        // POST: PainEntries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PainEntryEditVM painEntryEdit)
        {
            if (id != painEntryEdit.Id)
            {
                return NotFound();
            }

            // SYMPTOM
            if(!string.IsNullOrWhiteSpace(painEntryEdit.NewSymptomName))
            {
                var symptomExists = await _symptomsService.CheckIfSymptomNameExistsAsync(painEntryEdit.NewSymptomName);

                if (!symptomExists)
                {
                    var newSymptom = new SymptomCreateVM
                    {
                        Name = painEntryEdit.NewSymptomName
                    };

                    await _symptomsService.Create(newSymptom);

                    var allSymptoms = await _symptomsService.GetAll();
                    var createdSymptom = allSymptoms.FirstOrDefault(q => q.Name == painEntryEdit.NewSymptomName);

                    if (createdSymptom != null)
                    {
                        painEntryEdit.SymptomId = createdSymptom.Id;
                    }
                }
            }

            // LOCATION
            if(!string.IsNullOrWhiteSpace(painEntryEdit.NewLocationName))
            {
                var locationExists = await _locationsService.CheckIfLocationNameExistsAsync(painEntryEdit.NewLocationName);

                if (!locationExists)
                {
                    var newLocation = new LocationCreateVM
                    {
                        Name = painEntryEdit.NewLocationName
                    };

                    await _locationsService.Create(newLocation);

                    var allLocations = await _locationsService.GetAll();
                    var createdLocation = allLocations.FirstOrDefault(q => q.Name == painEntryEdit.NewLocationName);

                    if (createdLocation != null)
                    {
                        painEntryEdit.LocationId = createdLocation.Id;
                    }
                }
            }

            // TRIGGER
            if(!string.IsNullOrWhiteSpace(painEntryEdit.NewTriggerName))
            {
                var triggerExists = await _triggersService.CheckIfTriggerNameExistsAsync(painEntryEdit.NewTriggerName);

                if (!triggerExists)
                {
                    var newTrigger = new TriggerCreateVM
                    {
                        Name = painEntryEdit.NewTriggerName
                    };

                    await _triggersService.Create(newTrigger);

                    var allTriggers = await _triggersService.GetAll();
                    var createdTrigger = allTriggers.FirstOrDefault(q => q.Name == painEntryEdit.NewTriggerName);

                    if (createdTrigger != null)
                    {
                        painEntryEdit.TriggerId = createdTrigger.Id;
                    }
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _painEntriesService.Edit(painEntryEdit);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! _painEntriesService.PainEntryExists(painEntryEdit.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            await LoadDropdowns();
            return View(painEntryEdit);
        }

        // GET: PainEntries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var painEntry = await _painEntriesService.Get<PainEntryReadOnlyVM>(id.Value);
            if (painEntry == null)
            {
                return NotFound();
            }

            return View(painEntry);
        }

        // POST: PainEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _painEntriesService.Remove(id);
            return RedirectToAction(nameof(Index));
        }
        private async Task LoadDropdowns()
        {   
            // SYMPTOMS
            var symptoms = await _symptomsService.GetAll();

            var symptomSelectList = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "Choose a symptom or create new below" }
            };

            symptomSelectList.AddRange(symptoms.Select(q => new SelectListItem
            {
                Value = q.Id.ToString(),
                Text = q.Name
            }));

            ViewBag.Symptoms = symptomSelectList;

            // LOCATIONS
            var locations = await _locationsService.GetAll();

            var locationSelectList = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "Choose a location or create new below" }
            };

            locationSelectList.AddRange(locations.Select(q => new SelectListItem
            {
                Value = q.Id.ToString(),
                Text = q.Name
            }));

            ViewBag.Locations = locationSelectList;

            // TRIGGERS
            var triggers = await _triggersService.GetAll();

            var triggerSelectList = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "Choose a trigger or create new below" }
            };

            triggerSelectList.AddRange(triggers.Select(q => new SelectListItem
            {
                Value = q.Id.ToString(),
                Text = q.Name
            }));

            ViewBag.Triggers = triggerSelectList;
        }
    }
}
