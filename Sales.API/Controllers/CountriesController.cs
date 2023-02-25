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
			return Ok(await _context.Countries
				.Include(x => x.States)
				.ToListAsync());
		}

        [HttpGet("full")]
        public async Task<ActionResult> GetFullAsync()
        {
            return Ok(await _context.Countries
                .Include(x => x.States!)
				.ThenInclude(x => x.Cities)
                .ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetAsync(int id)
        {
			var country = await _context.Countries
				.Include(x => x.States!)
				.ThenInclude(x => x.Cities)
				.FirstOrDefaultAsync(x => x.Id == id);

			if (country == null) 
			{
				return NotFound();
			}
			return Ok(country);
        }

        [HttpPost]
		public async Task<ActionResult> PostAsyns(Country country) 
		{
			try
			{
				_context.Add(country);
				await _context.SaveChangesAsync();
				return Ok(country);
			}
			catch (DbUpdateException dbUpdateException)
			{
				if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
				{
					return BadRequest("Ya existe un país con el mismo nombre.");
				}

				return BadRequest(dbUpdateException.Message);
			}
			catch (Exception ex)
			{
                return BadRequest(ex.Message);
            }
		}

        [HttpPut]
        public async Task<ActionResult> PutAsyns(Country country)
        {
			try
			{
				_context.Update(country);
				await _context.SaveChangesAsync();
				return Ok(country);
			}
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe un país con el mismo nombre.");
                }

                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
