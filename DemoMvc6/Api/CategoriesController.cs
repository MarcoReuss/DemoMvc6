namespace DemoMvc6.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using DemoMvc6.Domain;
    using DemoMvc6.Dto;

    [Route("api/Categories")]
    public class CategoriesController : Controller
    {
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            using (var context = new DemoContext())
            {
                return Ok(await context.Categories.Where(x=>x.Id == id).Select(x=>new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsActive = x.IsActive
                }).FirstOrDefaultAsync());
            }
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            using (var context = new DemoContext())
            {
                return Ok(await context.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsActive = x.IsActive
                }).ToListAsync());
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post([FromBody]CategoryDto model)
        {
            using (var context = new DemoContext())
            {
                context.Categories.Add(new Category
                {
                    Name = model.Name,
                    IsActive = model.IsActive
                });

                return Ok(await context.SaveChangesAsync());
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]CategoryDto model)
        {
            using (var context = new DemoContext())
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id.Equals(id));

                if (category == null) return NotFound();

                category.Name = model.Name;

                category.IsActive = model.IsActive;

                return Ok(await context.SaveChangesAsync());
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            using (var context = new DemoContext())
            {
                var category = await context.Categories.FindAsync(id);

                if (category == null) return NotFound();

                context.Entry(category).State = EntityState.Deleted;

                return Ok(await context.SaveChangesAsync());
            }
        }
    }
}