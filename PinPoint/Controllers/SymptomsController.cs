using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PinPoint.Models.Symptoms;
using PinPoint.Services.Symptoms;

namespace PinPoint.Controllers
{
    [Authorize(Roles ="Developer")]
    public class SymptomsController(ISymptomsService symptomsService) : Controller
    {
        private readonly ISymptomsService _symptomsService = symptomsService;
        private const string NameExistsValidationMessage = "This symptom already exists.";

        // GET: Symptoms
        public async Task<IActionResult> Index()
        {
            var viewData = await _symptomsService.GetAll();
            return View(viewData);
        }

        // GET: Symptoms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var symptom = await _symptomsService.Get<SymptomReadOnlyVM>(id.Value);
            if (symptom == null)
            {
                return NotFound();
            }

            return View(symptom);
        }

        // GET: Symptoms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Symptoms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SymptomCreateVM symptomCreate)
        {
            if (await _symptomsService.CheckIfSymptomNameExistsAsync(symptomCreate.Name))
            {
                ModelState.AddModelError(nameof(symptomCreate.Name), NameExistsValidationMessage);
            }

            if (ModelState.IsValid)
            {
                await _symptomsService.Create(symptomCreate);
                return RedirectToAction(nameof(Index));
            }
            return View(symptomCreate);
        }

        // GET: Symptoms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var symptom = await _symptomsService.Get<SymptomEditVM>(id.Value);
            if (symptom == null)
            {
                return NotFound();
            }
            
            return View(symptom);
        }

        // POST: Symptoms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SymptomEditVM symptomEdit)
        {
            if (id != symptomEdit.Id)
            {
                return NotFound();
            }

            // Check if Symptom name exists
            if (await _symptomsService.CheckIfSymptomNameExistsForEditAsync(symptomEdit))
            {
                ModelState.AddModelError(nameof(symptomEdit.Name), NameExistsValidationMessage);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _symptomsService.Edit(symptomEdit);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! _symptomsService.SymptomExists(symptomEdit.Id))
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
            return View(symptomEdit);
        }

        // GET: Symptoms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var symptom = await _symptomsService.Get<SymptomReadOnlyVM>(id.Value);
            if (symptom == null)
            {
                return NotFound();
            }
            return View(symptom);
        }

        // POST: Symptoms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _symptomsService.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
