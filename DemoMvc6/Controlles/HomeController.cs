namespace DemoMvc6.Controlles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using DemoMvc6.ViewModel;
    using Microsoft.EntityFrameworkCore;

    public class HomeController : Controller
    {
        
        public async Task<IActionResult> Index()
        {
            using (var context = new DemoContext())
            {
                IEnumerable<CategoryViewModel> categories = await context.Categories.Select(x => new CategoryViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsActive = x.IsActive
                }).ToListAsync();

                return View(categories);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("CreateOrEdit", new CategoryViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var context = new DemoContext())
                {
                    context.Categories.Add(new Domain.Category
                    {
                        Name = model.Name,
                        IsActive = model.IsActive
                    });

                    await context.SaveChangesAsync();
                }
                
                return RedirectToAction("Index");
            }

            return View("CreateOrEdit", model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            using (var context = new DemoContext())
            {
                var category = await context.Categories.FindAsync(id);

                return View("CreateOrEdit", new CategoryViewModel
                {
                    Id = category.Id,
                    Name = category.Name,
                    IsActive = category.IsActive
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                using (var context = new DemoContext())
                {
                    var category = await context.Categories.FindAsync(model.Id);

                    category.Name = model.Name;

                    category.IsActive = model.IsActive;

                    await context.SaveChangesAsync();
                }

                return RedirectToAction("Index");
            }

            return View("CreateOrEdit", model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            using (var context = new DemoContext())
            {
                var category =  await context.Categories.FindAsync(id);

                if (category == null) return NotFound();

                context.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;

                await context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}