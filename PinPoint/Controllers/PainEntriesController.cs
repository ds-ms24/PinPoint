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
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            var painEntries = await _painEntriesService.GetAll(searchString, sortOrder);

            var viewData = new PainEntryIndexVM
            {
                PainEntries = painEntries,
                CurrentFilter = searchString,
                CurrentSort = sortOrder
            };
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

            await LoadSelectList();
            return View(model);
        }

        // POST: PainEntries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PainEntryCreateVM painEntryCreate)
        {   
            // CHECK MAX 3 VALUE
            if (!string.IsNullOrWhiteSpace(painEntryCreate.NewSymptomName) && 
                (painEntryCreate.SymptomIds?.Count ?? 0) >= 3)
            {
                ModelState.AddModelError("NewSymptomName", "Cannot add new symptom: maximum of 3 already selected");
            }

            if (!string.IsNullOrWhiteSpace(painEntryCreate.NewLocationName) && 
                (painEntryCreate.LocationIds?.Count ?? 0) >= 3)
            {
                ModelState.AddModelError("NewLocationName", "Cannot add new location: maximum of 3 already selected");
            }

            if (!string.IsNullOrWhiteSpace(painEntryCreate.NewTriggerName) && 
                (painEntryCreate.TriggerIds?.Count ?? 0) >= 3)
            {
                ModelState.AddModelError("NewTriggerName", "Cannot add new trigger: maximum of 3 already selected");
            }

            // CREATE SYMPTOM UNDER 3 MAX
            if (!string.IsNullOrWhiteSpace(painEntryCreate.NewSymptomName) && 
                (painEntryCreate.SymptomIds?.Count ?? 0) < 3)  // Only create if under max
            {
                var newSymptomId = await CreateSymptomIfNotExists(painEntryCreate.NewSymptomName);
                if (newSymptomId.HasValue)
                {
                    painEntryCreate.SymptomIds.Add(newSymptomId.Value);
                }
                else
                {
                    ModelState.AddModelError("NewSymptomName", "Unable to create or find the symptom.");
                }
            }

            // CREATE LOCATION UNDER 3 MAX
            if (!string.IsNullOrWhiteSpace(painEntryCreate.NewLocationName) && 
                (painEntryCreate.LocationIds?.Count ?? 0) < 3)  // Only create if under max
            {
                var newLocationId = await CreateLocationIfNotExists(painEntryCreate.NewLocationName);
                if (newLocationId.HasValue)
                {
                    painEntryCreate.LocationIds.Add(newLocationId.Value);
                }
                else
                {
                    ModelState.AddModelError("NewLocationName", "Unable to create or find the location.");
                }
            }

            // CREATE TRIGGER UNDER 3 MAX
            if (!string.IsNullOrWhiteSpace(painEntryCreate.NewTriggerName) && 
                (painEntryCreate.TriggerIds?.Count ?? 0) < 3)  // Only create if under max
            {
                var newTriggerId = await CreateTriggerIfNotExists(painEntryCreate.NewTriggerName);
                if (newTriggerId.HasValue)
                {
                    painEntryCreate.TriggerIds.Add(newTriggerId.Value);
                }
                else
                {
                    ModelState.AddModelError("NewTriggerName", "Unable to create or find the trigger.");
                }
            }

            // VALIDATE SYMPTOM MIN VALUE
            if (painEntryCreate.SymptomIds == null || !painEntryCreate.SymptomIds.Any())
            {
                ModelState.AddModelError("SymptomIds", "You must select or create at least one symptom.");
            }

            // VALIDATE LOCATION MIN VALUE
            if (painEntryCreate.LocationIds == null || !painEntryCreate.LocationIds.Any())
            {
                ModelState.AddModelError("LocationIds", "You must select or create at least one location.");
            }

            // VALIDATE TRIGGER MIN VALUE
            if (painEntryCreate.TriggerIds == null || !painEntryCreate.TriggerIds.Any())
            {
                ModelState.AddModelError("TriggerIds", "You must select or create at least one trigger.");
            }

            if (ModelState.IsValid)
            {
                await _painEntriesService.Create(painEntryCreate);
                return RedirectToAction(nameof(Index));
            }
    
            await LoadSelectList();
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

            await LoadSelectList();
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
                return NotFound();

            painEntryEdit.EntryDate = DateOnly.FromDateTime(DateTime.Today);
            painEntryEdit.EntryTime = TimeOnly.FromDateTime(DateTime.Now);

            // CHECK FOR MAX 3 VALUE - CANNOT ADD NEW VALUE
            if (!string.IsNullOrWhiteSpace(painEntryEdit.NewSymptomName) && 
                (painEntryEdit.SymptomIds?.Count ?? 0) >= 3)
            {
                ModelState.AddModelError("NewSymptomName", "Cannot add new symptom: maximum of 3 already selected");
            }

            if (!string.IsNullOrWhiteSpace(painEntryEdit.NewLocationName) && 
                (painEntryEdit.LocationIds?.Count ?? 0) >= 3)
            {
                ModelState.AddModelError("NewLocationName", "Cannot add new location: maximum of 3 already selected");
            }

            if (!string.IsNullOrWhiteSpace(painEntryEdit.NewTriggerName) && 
                (painEntryEdit.TriggerIds?.Count ?? 0) >= 3)
            {
                ModelState.AddModelError("NewTriggerName", "Cannot add new trigger: maximum of 3 already selected");
            }

            // CREATE SYMPTOM UNDER 3 MAX
            if (!string.IsNullOrWhiteSpace(painEntryEdit.NewSymptomName) && 
                (painEntryEdit.SymptomIds?.Count ?? 0) < 3)  // Only create if under max
            {
                var newSymptomId = await CreateSymptomIfNotExists(painEntryEdit.NewSymptomName);
                if (newSymptomId.HasValue)
                {
                    painEntryEdit.SymptomIds.Add(newSymptomId.Value);
                }
                else
                {
                    ModelState.AddModelError("NewSymptomName", "Unable to create or find the symptom.");
                }
            }

            // VALIDATE SYMPTOM MIN VALUE
            if (painEntryEdit.SymptomIds == null || !painEntryEdit.SymptomIds.Any())
            {
                ModelState.AddModelError("SymptomIds", "You must select or create at least one symptom.");
            }

            // CREATE LOCATION UNDER 3 MAX
            if (!string.IsNullOrWhiteSpace(painEntryEdit.NewLocationName) && 
                (painEntryEdit.LocationIds?.Count ?? 0) < 3)  // Only create if under max
            {
                var newLocationId = await CreateLocationIfNotExists(painEntryEdit.NewLocationName);
                if (newLocationId.HasValue)
                {
                    painEntryEdit.LocationIds.Add(newLocationId.Value);
                }
                else
                {
                    ModelState.AddModelError("NewLocationName", "Unable to create or find the location.");
                }
            }

            // VALIDATE LOCATION MIN VALUE
            if (painEntryEdit.LocationIds == null || !painEntryEdit.LocationIds.Any())
            {
                ModelState.AddModelError("LocationIds", "You must select or create at least one location.");
            }

            // CREATE TRIGGER UNDER 3 MAX
            if (!string.IsNullOrWhiteSpace(painEntryEdit.NewTriggerName) && 
                (painEntryEdit.TriggerIds?.Count ?? 0) < 3)  // Only create if under max
            {
                var newTriggerId = await CreateTriggerIfNotExists(painEntryEdit.NewTriggerName);
                if (newTriggerId.HasValue)
                {
                    painEntryEdit.TriggerIds.Add(newTriggerId.Value);
                }
                else
                {
                    ModelState.AddModelError("NewTriggerName", "Unable to create or find the trigger.");
                }
            }

            // VALIDATE TRIGGER MIN VALUE
            if (painEntryEdit.TriggerIds == null || !painEntryEdit.TriggerIds.Any())
            {
                ModelState.AddModelError("TriggerIds", "You must select or create at least one trigger.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _painEntriesService.Edit(painEntryEdit);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    if (!_painEntriesService.PainEntryExists(painEntryEdit.Id))
                        return NotFound();
                    throw;
                }
            }

            await LoadSelectList();
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
        private async Task LoadSelectList()
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
        private async Task<int?> CreateSymptomIfNotExists(string name)
        {
            if (await _symptomsService.CheckIfSymptomNameExistsAsync(name))
            {
                var existing = (await _symptomsService.GetAll())
                    .FirstOrDefault(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
                return existing?.Id;
            }

            var newSymptom = new SymptomCreateVM { Name = name };
            await _symptomsService.Create(newSymptom);
            
            var created = (await _symptomsService.GetAll())
                .FirstOrDefault(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            return created?.Id;
        }
        private async Task<int?> CreateLocationIfNotExists(string name)
        {
            if (await _locationsService.CheckIfLocationNameExistsAsync(name))
            {
                var existing = (await _locationsService.GetAll())
                    .FirstOrDefault(l => l.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
                return existing?.Id;
            }

            var newLocation = new LocationCreateVM { Name = name };
            await _locationsService.Create(newLocation);
            
            var created = (await _locationsService.GetAll())
                .FirstOrDefault(l => l.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            return created?.Id;
        }
        private async Task<int?> CreateTriggerIfNotExists(string name)
        {
            if (await _triggersService.CheckIfTriggerNameExistsAsync(name))
            {
                var existing = (await _triggersService.GetAll())
                    .FirstOrDefault(t => t.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
                return existing?.Id;
            }

            var newTrigger = new TriggerCreateVM { Name = name };
            await _triggersService.Create(newTrigger);
            
            var created = (await _triggersService.GetAll())
                .FirstOrDefault(t => t.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            return created?.Id;
        }
    }
}
