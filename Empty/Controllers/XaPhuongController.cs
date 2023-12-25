using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Empty.Models;
using Empty.Services;

namespace Empty.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class XaPhuongController : ControllerBase
    {
        private readonly XaPhuongService _xaPhuongService;

        public XaPhuongController(XaPhuongService xaPhuongService)
        {
            _xaPhuongService = xaPhuongService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _xaPhuongService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _xaPhuongService.GetByIdAsync(id));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(XaPhuong xaPhuong)
        {
            return Ok(await _xaPhuongService.CreateAsync(xaPhuong));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, XaPhuong xaPhuong)
        {
            if (id != xaPhuong.Id)
            {
                return BadRequest();
            }

            await _xaPhuongService.UpdateAsync(xaPhuong);

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _xaPhuongService.DeleteAsync(id);

            return NoContent();
        }
    }
}
