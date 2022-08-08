using API_Task_08_08.DAL;
using API_Task_08_08.DTOs;
using API_Task_08_08.DTOs.Book;
using API_Task_08_08.DTOs.Category;
using API_Task_08_08.Model;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Task_08_08.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CategoriesController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAll(int page = 1, string search = null)
        {
            var query = _context.Categories.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(c => c.Name.Contains(search));
            }
            List<Category> categories = query.Include(c=>c.Books).Skip((page - 1) * 4).Take(4).ToList();
            ListDto<CategoryItemDto> listDto = new ListDto<CategoryItemDto>
            {
                ListItemDtos = _mapper.Map<List<CategoryItemDto>>(categories),
                TotalCount = categories.Count()
            };
            return Ok(listDto);
        }
        [HttpGet("get/{id}")]
        public IActionResult Get(int id)
        {
            if (id == 0) return StatusCode(StatusCodes.Status404NotFound);
            Category category = _context.Categories.Include(b => b.Books).FirstOrDefault(c => c.Id == id);
            if (category == null) return StatusCode(StatusCodes.Status404NotFound);
            CategoryGetDto dto = _mapper.Map<CategoryGetDto>(category);
            return Ok(dto);
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create(CategoryPostDto dto)
        {
            if (dto == null) return StatusCode(StatusCodes.Status404NotFound);
            if (_context.Categories.Any(b => b.Name == dto.Name)) return BadRequest();
            Category category = _mapper.Map<Category>(dto);
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return StatusCode(201, new { Id = category.Id, book = dto });
        }
        [HttpPut("update/{id}")]
        public IActionResult Update(int id, CategoryPostDto category)
        {
            if (id == 0) return StatusCode(StatusCodes.Status404NotFound);
            Category existed = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (existed == null) return StatusCode(StatusCodes.Status404NotFound);
            if (_context.Categories.Any(b => b.Name == category.Name)) return BadRequest();
            _context.Entry(existed).CurrentValues.SetValues(category);
            _context.SaveChanges();
            return Ok(existed);
        }
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0) return StatusCode(StatusCodes.Status404NotFound);
            Category category = _context.Categories.Include(c=>c.Books).FirstOrDefault(c => c.Id == id);
            if (category == null) return StatusCode(StatusCodes.Status404NotFound);
            if (category.Books!=null||category.Books.Count>0)
            {
              _context.Books.RemoveRange(category.Books);  
            }
            _context.Remove(category);
            _context.SaveChanges();
            return NoContent();
        }
        [HttpPatch("change/{id}")]
        public IActionResult ChangeName(int id, string newname)
        {
            if (id == 0) return StatusCode(StatusCodes.Status404NotFound);
            if (string.IsNullOrEmpty(newname)) return BadRequest();
            Category existed = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (existed == null) return StatusCode(StatusCodes.Status404NotFound);
            if (_context.Categories.Any(b => b.Name == newname)) return BadRequest();
            existed.Name = newname;
            _context.SaveChanges();
            return NoContent();
        }
    }
}
