using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PinPoint.Data;
using PinPoint.Models.PainEntries;
using PinPoint.Models.PainEntry;
using PinPoint.Models.Symptoms;
using PinPoint.Services.PainEntries;
using PinPoint.Services.Symptoms;

namespace PinPoint.Controllers
{
    public class PainEntriesController(IPainEntriesService painEntriesService) : Controller
    {
        private readonly IPainEntriesService _painEntriesService = painEntriesService;

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

            var painEntry = await _painEntriesService.Get<PainEntryReadOnlyVM>(id.Value);
            if (painEntry == null)
            {
                return NotFound();
            }

            return View(painEntry);
        }

        // GET: PainEntries/Create
        public IActionResult Create()
        {   
            var model = new PainEntryCreateVM
            {
                EntryDate = DateOnly.FromDateTime(DateTime.Today),
                EntryTime = TimeOnly.FromDateTime(DateTime.Now)
            };
            return View(model);
        }

        // POST: PainEntries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PainEntryCreateVM painEntryCreate)
        {

            if (ModelState.IsValid)
            {
                await _painEntriesService.Create(painEntryCreate);
                return RedirectToAction(nameof(Index));
            }
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

            if (ModelState.IsValid)
            {
                try
                {
                    await _painEntriesService.Edit(painEntryEdit);
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
                return RedirectToAction(nameof(Index));
            }
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

        
    }
}
