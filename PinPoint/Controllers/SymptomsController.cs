using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PinPoint.Data;
using PinPoint.Models.Symptoms;

namespace PinPoint.Controllers
{
    public class SymptomsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SymptomsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            this._mapper = mapper;
        }

        // GET: Symptoms
        public async Task<IActionResult> Index()
        {
            var data = await _context.Symptoms.ToListAsync();
            var viewData = _mapper.Map<List<SymptomReadOnlyVM>>(data);
            return View(viewData);
        }

        // GET: Symptoms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var symptom = await _context.Symptoms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (symptom == null)
            {
                return NotFound();
            }

            var viewData = _mapper.Map<SymptomReadOnlyVM>(symptom);
            return View(viewData);
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
            // Check if Symptom name exists
            if (await CheckIfSymptomNameExistsAsync(symptomCreate.Name))
            {
                ModelState.AddModelError(nameof(symptomCreate.Name), 
                    "This symptom already exists.");
            }

            if (ModelState.IsValid)
            {
                var symptom = _mapper.Map<Symptom>(symptomCreate);
                _context.Add(symptom);
                await _context.SaveChangesAsync();
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

            var symptom = await _context.Symptoms.FindAsync(id);
            if (symptom == null)
            {
                return NotFound();
            }
            var viewData = _mapper.Map<SymptomEditVM>(symptom);
            return View(viewData);
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

            if (ModelState.IsValid)
            {
                try
                {
                    var symptom = _mapper.Map<Symptom>(symptomEdit);
                    _context.Update(symptom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SymptomExists(symptomEdit.Id))
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

            var symptom = await _context.Symptoms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (symptom == null)
            {
                return NotFound();
            }

            var viewData = _mapper.Map<SymptomReadOnlyVM>(symptom);
            return View(viewData);
        }

        // POST: Symptoms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var symptom = await _context.Symptoms.FindAsync(id);
            if (symptom != null)
            {
                _context.Symptoms.Remove(symptom);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SymptomExists(int id)
        {
            return _context.Symptoms.Any(e => e.Id == id);
        }
        private async Task<bool> CheckIfSymptomNameExistsAsync(string name)
        {
            var lowercaseName = name.ToLower();
            return await _context.Symptoms.AnyAsync(q => q.Name.ToLower().Equals(lowercaseName));
        }
    }
}
