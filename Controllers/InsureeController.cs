using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

public class HomeController : Controller
{
    private readonly InsuranceContext _context;

    public HomeController(InsuranceContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Insuree insuree)
    {
        if (ModelState.IsValid)
        {
            decimal quote = 50;

            if (insuree.Age <= 18)
                quote += 100;
            else if (insuree.Age >= 19 && insuree.Age <= 25)
                quote += 50;
            else
                quote += 25;

            if (insuree.CarYear < 2000)
                quote += 25;
            else if (insuree.CarYear > 2015)
                quote += 25;

            if (insuree.CarMake == "Porsche")
            {
                quote += 25;

                if (insuree.CarModel == "911 Carrera")
                    quote += 25;
            }

            quote += insuree.SpeedingTickets * 10;

            if (insuree.HasDUI)
                quote += quote * 0.25;

            if (insuree.IsFullCoverage)
                quote += quote * 0.5;

            insuree.Quote = quote;

            _context.Insurees.Add(insuree);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        return View(insuree);
    }

    public IActionResult Admin()
    {
        var allQuotes = _context.Insurees
            .Select(i => new { i.FirstName, i.LastName, i.Email, i.Quote })
            .ToList();

        return View(allQuotes);
    }
}
