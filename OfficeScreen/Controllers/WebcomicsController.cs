using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeScreen.Data;
using OfficeScreen.Models.Dtos;
using OfficeScreen.Models.Entities;
using OfficeScreen.Models.Helpers;

namespace OfficeScreen.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WebcomicsController : ControllerBase
    {
        private readonly ODDSApiContext _context;
        private readonly IMapper _mapper;

        public WebcomicsController(ODDSApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Webcomics
        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComicDto>>> Getwebcomics(int page = 1, int pageSize = 20)
        {
            var quary = _context.webcomics.AsQueryable();

            var totalItems = await quary.CountAsync();

            var items = await quary
                        .ProjectTo<ComicDto>(_mapper.ConfigurationProvider)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();

            var result = new PagedResult<ComicDto>
            {
                Items = items,
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
            };

            return Ok(result);
        }

        [HttpGet("random")]
        public async Task<ActionResult<ComicDto>> GetRandomwebcomic()
        {
            Random ran = new Random();
            var minId = await _context.webcomics.MinAsync(w => w.Id);
            var maxId = await _context.webcomics.MaxAsync(w => w.Id);

            var randomComic = ran.Next(minId, maxId);

            var comicDto = await _context.webcomics
                .Where(c => c.Id == randomComic)
                .ProjectTo<ComicDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (comicDto == null)
            {
                return NotFound(new ProblemDetails
                {
                    Title = "Comic not found",
                    Status = 404,
                    Instance = HttpContext.Request.Path
                });
            }

            return comicDto;
        }

        [HttpGet("latest")]
        public async Task<ActionResult<ComicDto>> GetLatestwebcomic()
        {
            var maxId = await _context.webcomics.MaxAsync(w => w.Id);

            var comicDto = await _context.webcomics
                .Where(c => c.Id == maxId)
                .ProjectTo<ComicDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (comicDto == null)
            {
                return NotFound(new ProblemDetails
                {
                    Title = "Comic not found",
                    Status = 404,
                    Instance = HttpContext.Request.Path
                });
            }

            return comicDto;
        }

        // GET: api/Webcomics/5
        //[Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ComicDto>> GetWebcomic(int id)
        {
            //var webcomic = await _context.webcomics.FindAsync(id);
            var comicDto = await _context.webcomics
                .Where(c => c.Id == id)
                .ProjectTo<ComicDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (comicDto == null)
            {
                return NotFound(new ProblemDetails
                {
                    Title = "Comic not found",
                    Detail = $"Comic with ID {id} was not found.",
                    Status = 404,
                    Instance = HttpContext.Request.Path
                });
            }

            return comicDto;
        }

        // POST: api/Webcomics
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ComicDto>> PostWebcomic(ComicDto comicDto)
        {
            //_context.webcomics.Add(webcomic);
            //await _context.SaveChangesAsync();
            var comic = _mapper.Map<Webcomic>(comicDto);
            _context.webcomics.Add(comic);
            var test = await _context.SaveChangesAsync();

            var respons = _mapper.Map<ComicDto>(comic);

            return CreatedAtAction("GetWebcomic", new { id = comic.Id }, comicDto);
        }

        // DELETE: api/Webcomics/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWebcomic(int id)
        {
            var webcomic = await _context.webcomics.FindAsync(id);
            if (webcomic == null)
            {
                return NotFound(new ProblemDetails
                {
                    Title = "Comic not found",
                    Detail = $"Comic with ID {id} was not found.",
                    Status = 404,
                    Instance = HttpContext.Request.Path
                });
            }

            _context.webcomics.Remove(webcomic);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WebcomicExists(int id)
        {
            return _context.webcomics.Any(e => e.Id == id);
        }
    }
}
