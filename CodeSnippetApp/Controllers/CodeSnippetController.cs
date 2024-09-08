using CodeSnippetApp.Data;
using CodeSnippetApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeSnippetApp.Controllers;

public class CodeSnippetController : Controller
{
    private readonly ApplicationDbContext _db;

    public CodeSnippetController(ApplicationDbContext db)
    {
        _db = db;
    }

    // Index: Displays all code snippets
    public IActionResult Index()
    {
        IEnumerable<CodeSnippet> objCodeSnippetList = _db.CodeSnippets;
        return View(objCodeSnippetList);
    }

    // Create GET: Displays form to add a new code snippet
    public IActionResult Create()
    {
        return View();
    }

    // Create POST: Handles form submission for new code snippet
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CodeSnippet obj)
    {
        if (ModelState.IsValid)
        {
            obj.CreatedDate = DateTime.Now; // Ensure CreatedDate is set
            obj.UpdatedDate = DateTime.Now; // Initially set UpdatedDate
            _db.CodeSnippets.Add(obj);
            _db.SaveChanges();
            TempData["success"] = "Code Snippet Saved Successfully";
            return RedirectToAction("Index");
        }
        return View(obj);
    }

    // View GET: Displays a specific code snippet for viewing/editing
    public IActionResult Views(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        var snippetFromDb = _db.CodeSnippets.Find(id);
        if (snippetFromDb == null)
        {
            return NotFound();
        }
        return View(snippetFromDb);
    }

    // View POST: Handles form submission for updating a code snippet
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Views(CodeSnippet obj)
    {
        if (ModelState.IsValid)
        {
            // Retrieve the original snippet from the database to preserve the CreatedDate
            var originalSnippet = _db.CodeSnippets.AsNoTracking().FirstOrDefault(x => x.Id == obj.Id);
            if (originalSnippet != null)
            {
                obj.CreatedDate = originalSnippet.CreatedDate; // Preserve the original CreatedDate
            }

            obj.UpdatedDate = DateTime.Now; // Update the UpdatedDate to the current time
            _db.CodeSnippets.Update(obj);
            _db.SaveChanges();
            TempData["success"] = "Code Snippet Updated Successfully";
            return RedirectToAction("Index");
        }
        return View(obj);
    }


    // Delete GET: Displays confirmation page for deleting a code snippet
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        var snippetFromDb = _db.CodeSnippets.Find(id);
        if (snippetFromDb == null)
        {
            return NotFound();
        }
        return View(snippetFromDb);
    }

    // Delete POST: Handles deletion of the code snippet
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePOST(int? id)
    {
        var obj = _db.CodeSnippets.Find(id);
        if (obj == null)
        {
            return NotFound();
        }

        _db.CodeSnippets.Remove(obj);
        _db.SaveChanges();
        TempData["success"] = "Code Snippet Deleted Successfully";
        return RedirectToAction("Index");
    }
}
