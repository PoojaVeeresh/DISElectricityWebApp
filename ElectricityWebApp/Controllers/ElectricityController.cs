using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ElectricityWebApp.DataAccess;
using ElectricityWebApp.Models;

namespace ElectricityWebApp.Controllers
{
    public class ElectricityController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ElectricityController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> ElectricityDetails(string id,string countryname)
        {
            TempData["country"] = countryname;
            return View(await _context.electricityDetails.Where(c=>c.CountryID==id).ToListAsync());
        }

        
        // GET: Electricity
        public async Task<IActionResult> Index()
        {
            return View(await _context.countryData.ToListAsync());
        }

        
        // GET: Electricity/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Electricity/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,CountryName,CityName")] CountryData countryData)
        {
            if (ModelState.IsValid)
            {
                countryData.ID = Guid.NewGuid();
                _context.Add(countryData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(countryData);
        }

        // GET: Electricity/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var countryData = await _context.countryData.FindAsync(id);
            if (countryData == null)
            {
                return NotFound();
            }
            return View(countryData);
        }

        //for electricity details
        public async Task<IActionResult> EditElectricity(Guid? id,string country)
        {
            TempData["country"] = country;

            if (id == null)
            {
                return NotFound();
            }

            var electrictyData = await _context.electricityDetails.FindAsync(id);
            if (electrictyData == null)
            {
                return NotFound();
            }
            return View(electrictyData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditElectricity(Guid id, [Bind("ID,CountryID,UnitsConsumption,Year")] ElectricityDetails electricityDetails)
        {
            if (id != electricityDetails.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(electricityDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ElectricityDataExists(electricityDetails.ID))
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
            return View(electricityDetails);
        }

        // POST: Electricity/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,CountryName,CityName")] CountryData countryData)
        {
            if (id != countryData.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(countryData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountryDataExists(countryData.ID))
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
            return View(countryData);
        }
        public async Task<IActionResult> DeleteElectricity(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var elecData = await _context.electricityDetails
                .FirstOrDefaultAsync(m => m.ID == id);
            if (elecData == null)
            {
                return NotFound();
            }

            return View(elecData);
        }

        // POST: Electricity/DeleteElectricity/5
        [HttpPost, ActionName("DeleteElectricity")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteElectricityConfirmed(Guid id)
        {
            var elecData = await _context.electricityDetails.FindAsync(id);
            _context.electricityDetails.Remove(elecData);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Electricity/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var countryData = await _context.countryData
                .FirstOrDefaultAsync(m => m.ID == id);
            if (countryData == null)
            {
                return NotFound();
            }

            return View(countryData);
        }

        // POST: Electricity/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var countryData = await _context.countryData.FindAsync(id);
            _context.countryData.Remove(countryData);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CountryDataExists(Guid id)
        {
            return _context.countryData.Any(e => e.ID == id);
        }
        private bool ElectricityDataExists(Guid id)
        {
            return _context.electricityDetails.Any(e => e.ID == id);
        }

        
        //add electricity details
        public IActionResult AddDetails(string id,string Country)
        {
            TempData["country"] = Country;
            TempData["ID"] = id;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDetails([Bind("ID,CountryID,Year,UnitsConsumption")] ElectricityDetails electricityDetails)
        {
            if (ModelState.IsValid)
            {
                electricityDetails.ID = Guid.NewGuid();
                _context.Add(electricityDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(electricityDetails);
        }

    }
}
