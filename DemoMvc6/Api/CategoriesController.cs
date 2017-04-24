namespace DemoMvc6.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Route("api/Categories")]
    public class CategoriesController : Controller
    {
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            using (var context = new DemoContext())
            {
                return Ok(await context.Categories.FirstOrDefaultAsync(x => x.Id == id));
            }
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            using (var context = new DemoContext())
            {
                return Ok(await context.Categories.ToListAsync());
            }
        }
    }
}