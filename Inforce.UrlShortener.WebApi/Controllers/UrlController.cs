using Inforce.UrlShortener.Abstraction.DTOs;
using Inforce.UrlShortener.Abstraction.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Inforce.UrlShortener.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UrlController : ControllerBase
    {
        private readonly IUrlService urlService;

        public UrlController(IUrlService urlService)
        {
            this.urlService = urlService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var urls = await this.urlService.GetAllAsync();
            return Ok(urls);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var url = await this.urlService.GetByIdAsync(id);
            if (url == null)
            {
                return NotFound($"URL with ID {id} not found.");
            }
            return Ok(url);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] UrlDto urlDto)
        {
            try
            {
                await this.urlService.AddAsync(urlDto);
                return CreatedAtAction(nameof(GetById), new { id = urlDto.Id }, urlDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UrlDto urlDto)
        {
            if (id != urlDto.Id)
            {
                return BadRequest("URL ID mismatch.");
            }

            try
            {
                await this.urlService.UpdateAsync(urlDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await this.urlService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
