using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CDS_Models.DTOs;
using System;
using Microsoft.EntityFrameworkCore;
using CDS_DAL;
using CDS_Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;

namespace CDS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [SwaggerTag("TcUsd Management")]
    public class TcUsdController : ControllerBase
    {
        private readonly LogistContext _context;

        public TcUsdController(LogistContext context)
        {
            _context = context;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all TcUsds", Description = "Retrieves a list of all TcUsds.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<TcUsdDTO>>> GetTcUsds()
        {
            try
            {
                var tcUsds = await _context.TcUsd
                    .Select(t => new TcUsdDTO
                    {
                        IdTc = t.IdTc,
                        IdMda = t.IdMda,
                        FechaTc = t.FechaTc,
                        TipCam = t.TipCam
                    })
                    .ToListAsync();

                return Ok(tcUsds);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get a TcUsd by ID", Description = "Retrieves a specific TcUsd by its ID.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TcUsdDTO>> GetTcUsd(int id)
        {
            try
            {
                var tcUsd = await _context.TcUsd.FindAsync(id);

                if (tcUsd == null)
                {
                    return NotFound();
                }

                var tcUsdDTO = new TcUsdDTO
                {
                    IdTc = tcUsd.IdTc,
                    IdMda = tcUsd.IdMda,
                    FechaTc = tcUsd.FechaTc,
                    TipCam = tcUsd.TipCam
                };

                return Ok(tcUsdDTO);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new TcUsd", Description = "Creates a new TcUsd.")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TcUsdDTO>> PostTcUsd(TcUsdDTO tcUsdDTO)
        {
            try
            {
                var tcUsd = new TcUsd
                {
                    IdMda = tcUsdDTO.IdMda,
                    FechaTc = tcUsdDTO.FechaTc,
                    TipCam = tcUsdDTO.TipCam
                };

                _context.TcUsd.Add(tcUsd);
                await _context.SaveChangesAsync();

                tcUsdDTO.IdTc = tcUsd.IdTc; // Assuming Id is auto-generated
                
                return CreatedAtAction(nameof(GetTcUsd), new { id = tcUsdDTO.IdTc }, tcUsdDTO);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update a TcUsd", Description = "Updates an existing TcUsd.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutTcUsd(int id, TcUsdDTO tcUsdDTO)
        {
            try
            {
                if (id != tcUsdDTO.IdTc) // Changed from Id to IdTc
                {
                    return BadRequest();
                }

                var tcUsd = await _context.TcUsd.FindAsync(id); // Changed from _context.TcUsds to _context.TcUsd

                if (tcUsd == null)
                {
                    return NotFound();
                }

                tcUsd.IdMda = tcUsdDTO.IdMda;
                tcUsd.FechaTc = tcUsdDTO.FechaTc;
                tcUsd.TipCam = tcUsdDTO.TipCam;

                _context.Entry(tcUsd).State = EntityState.Modified; // This line is correct

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TcUsdExists(id)) // This line is correct, but the method TcUsdExists needs fixing
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a TcUsd", Description = "Deletes a TcUsd by its ID.")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTcUsd(int id)
        {
            try
            {
                var tcUsd = await _context.TcUsd.FindAsync(id); // Changed from _context.TcUsds to _context.TcUsd
                if (tcUsd == null)
                {
                    return NotFound();
                }

                _context.TcUsd.Remove(tcUsd); // Changed from _context.TcUsds to _context.TcUsd
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private bool TcUsdExists(int id)
        {
            return _context.TcUsd.Any(e => e.IdTc == id); // Changed from _context.TcUsds.Any(e => e.Id == id) to _context.TcUsd.Any(e => e.IdTc == id)
        }
    }
}
