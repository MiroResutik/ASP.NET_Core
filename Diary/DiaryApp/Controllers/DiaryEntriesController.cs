using DiaryApp.Data;
using DiaryApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DiaryApp.Controllers
{
    public class DiaryEntriesController : Controller
    {
        private readonly ApplicationDbContext _db;

        // Dependency Injection - injecting ApplicationDbContext
        public DiaryEntriesController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<DiaryEntry> objDiaryEntryList = _db.DiaryEntries.ToList();

            return View(objDiaryEntryList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(DiaryEntry obj)
        {
            // Server side validation
            if (obj != null && obj.Title.Length < 3)
            {
                ModelState.AddModelError("Title", "Title too short!!! (Server side validation)");
            }
            if (ModelState.IsValid) 
            {
                // Add object  Diary Entries to database
                _db.DiaryEntries.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(obj);

        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            DiaryEntry? diaryEntry = _db.DiaryEntries.Find(id);

            if (diaryEntry == null)
            {
                return NotFound();
            }

            return View(diaryEntry);
        }

        [HttpPost]
        public IActionResult Edit(DiaryEntry obj)
        {
            if (obj != null && obj.Title.Length < 3)
            {
                ModelState.AddModelError("Title", "Title too short");
            }
            if (ModelState.IsValid)
            {
                _db.DiaryEntries.Update(obj);  // Update the diary entry in the database
                _db.SaveChanges();// Saves the changes to the database
                return RedirectToAction("Index");
            }

            return View(obj);

        }
        [HttpGet]
        // csharp
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var entry = _db.DiaryEntries.Find(id);
            if (entry == null) return NotFound();

            _db.DiaryEntries.Remove(entry);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(DiaryEntry obj)
        {
            if (obj != null && obj.Title.Length < 3)
            {
                ModelState.AddModelError("Title", "Title too short");
            }
            if (ModelState.IsValid)
            {
                _db.DiaryEntries.Remove(obj);  // Update the diary entry in the database
                _db.SaveChanges();// Saves the changes to the database
                return RedirectToAction("Index");
            }

            return View(obj);

        }
    }
}
