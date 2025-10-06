using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PinPoint.Data;
using PinPoint.Models.Symptoms;
using PinPoint.Models.Triggers;
using PinPoint.Services.Symptoms;
using PinPoint.Services.Triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PinPoint.Controllers
{   
    [Authorize(Roles ="Developer")]
    public class TriggersController(ITriggersService triggersService) : Controller
    {
        private readonly ITriggersService _triggersService = triggersService;
        private const string NameExistsValidationMessage = "This trigger already exists.";

        // GET: Triggers
        public async Task<IActionResult> Index()
        {
            var viewData = await _triggersService.GetAll();
            return View(viewData); 
        }

        // GET: Triggers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trigger = await _triggersService.Get<TriggerReadOnlyVM>(id.Value);
            if (trigger == null)
            {
                return NotFound();
            }

            return View(trigger);
        }

        // GET: Triggers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Triggers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TriggerCreateVM triggerCreate)
        {   
            if (await _triggersService.CheckIfTriggerNameExistsAsync(triggerCreate.Name))
            {
                ModelState.AddModelError(nameof(triggerCreate.Name), NameExistsValidationMessage);
            }

            if (ModelState.IsValid)
            {
                await _triggersService.Create(triggerCreate);
                return RedirectToAction(nameof(Index));
            }
            return View(triggerCreate);
        }

        // GET: Triggers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trigger = await _triggersService.Get<SymptomEditVM>(id.Value);
            if (trigger == null)
            {
                return NotFound();
            }
            return View(trigger);
        }

        // POST: Triggers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TriggerEditVM triggerEdit)
        {
            if (id != triggerEdit.Id)
            {
                return NotFound();
            }

            // Check if Trigger name exists
            if (await _triggersService.CheckIfTriggerNameExistsForEditAsync(triggerEdit))
            {
                ModelState.AddModelError(nameof(triggerEdit.Name), NameExistsValidationMessage);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _triggersService.Edit(triggerEdit);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! _triggersService.TriggerExists(triggerEdit.Id))
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
            return View(triggerEdit);
        }

        // GET: Triggers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trigger = await _triggersService.Get<TriggerReadOnlyVM>(id.Value);
            if (trigger == null)
            {
                return NotFound();
            }

            return View(trigger);
        }

        // POST: Triggers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _triggersService.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
