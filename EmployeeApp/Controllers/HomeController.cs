using EmployeeApp.Data;
using EmployeeApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


public class HomeController : Controller
{
    private readonly EmployeeContext _context;

    public HomeController(EmployeeContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Details(int id)
    {
        if (id == 0)
        {
            return NotFound();
        }

        var employee = await _context.Employees
            .FirstOrDefaultAsync(m => m.EmployeeID == id);

        if (employee == null)
        {
            return NotFound();
        }

        return View(employee);
    }


    // GET: Home/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var employee = await _context.Employees
            .FirstOrDefaultAsync(m => m.EmployeeID == id);
        if (employee == null)
        {
            return NotFound();
        }

        return View(employee);
    }

    // DELETE: Home/DeleteConfirmed/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null)
        {
            return NotFound();
        }


        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }


    // GET: Home/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var employee = await _context.Employees.FindAsync(id);
        if (employee == null)
        {
            return NotFound();
        }

        return View(employee);
    }

    // POST: Home/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("EmployeeID,FirstName,LastName,Email,Phone,HireDate")] Employee employee)
    {
        if (id != employee.EmployeeID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(employee);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(employee.EmployeeID))
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
        return View(employee);
    }

    private bool EmployeeExists(int id)
    {
        return _context.Employees.Any(e => e.EmployeeID == id);
    }
    // GET: Home/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Home/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("FirstName,LastName,Email,Phone,HireDate")] Employee employee)
    {
        if (ModelState.IsValid)
        {
            _context.Add(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(employee);
    }

    public async Task<IActionResult> Index()
    {
        var employees = await _context.Employees.ToListAsync();
        return View(employees);
    }
}
