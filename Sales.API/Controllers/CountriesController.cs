﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sales.API.Data;
using Sales.Shared.Entities;

namespace Sales.API.Controllers
{
	[ApiController]
	[Route("/api/countries")]
	public class CountriesController : ControllerBase
	{
		public readonly DataContext _context;

		public CountriesController(DataContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult> GetAsync()
		{
			return Ok(await _context.Countries.ToListAsync());
		}

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetAsync(int id)
        {	
			var country = await _context.Countries.FirstOrDefaultAsync(x => x.Id == id);

			if (country == null) 
			{
				return NotFound();
			}
			return Ok(country);
        }

        [HttpPost]
		public async Task<ActionResult> PostAsyns(Country country) 
		{
			_context.Add(country);
			await _context.SaveChangesAsync();
			return Ok(country);
		}

        [HttpPut]
        public async Task<ActionResult> PutAsyns(Country country)
        {
            _context.Update(country);
            await _context.SaveChangesAsync();
            return Ok(country);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var country = await _context.Countries.FirstOrDefaultAsync(x => x.Id == id);

            if (country == null)
            {
                return NotFound();
            }

            _context.Remove(country);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
