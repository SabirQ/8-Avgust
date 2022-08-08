using API_Task_08_08.DAL;
using API_Task_08_08.DTOs;
using API_Task_08_08.DTOs.Book;
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
    public class BooksController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public BooksController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll(int page = 1, string search = null)
        {
            var query = _context.Books.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(c => c.Name.Contains(search));
            }
            List<Book> books = query.Include(b => b.Category).ThenInclude(c=>c.Books).Skip((page - 1) * 4).Take(4).ToList();
            ListDto<BookItemDto> listDto = new ListDto<BookItemDto>
            {
                ListItemDtos = _mapper.Map<List<BookItemDto>>(books),
                TotalCount = books.Count()
            };
            return Ok(listDto);
        }
        [HttpGet("get/{id}")]
        public IActionResult Get(int id)
        {
            if (id == 0) return StatusCode(StatusCodes.Status404NotFound);
            Book book = _context.Books.Include(b=>b.Category).ThenInclude(c => c.Books).FirstOrDefault(c => c.Id == id);
            if (book == null) return StatusCode(StatusCodes.Status404NotFound);
            BookGetDto dto = _mapper.Map<BookGetDto>(book);
            return Ok(dto);
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create(BookPostDto dto)
        {
            if (dto == null) return StatusCode(StatusCodes.Status404NotFound);
            if (_context.Books.Any(b => b.Name == dto.Name)) return BadRequest();
            if (!_context.Categories.Any(c => c.Id == dto.CategoryId))return BadRequest();
            Book book = _mapper.Map<Book>(dto);
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return StatusCode(201, new { Id = book.Id, book = dto });
        }
        [HttpPut("update/{id}")]
        public IActionResult Update(int id, BookPostDto book)
        {
            if (id == 0) return StatusCode(StatusCodes.Status404NotFound);
            Book existed = _context.Books.FirstOrDefault(c => c.Id == id);
            if (existed == null) return StatusCode(StatusCodes.Status404NotFound);
            if (_context.Books.Any(b => b.Name == book.Name)) return BadRequest();
            if (!_context.Categories.Any(c => c.Id == book.CategoryId)) return BadRequest();
            _context.Entry(existed).CurrentValues.SetValues(book);
            _context.SaveChanges();
            return Ok(existed);
        }
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0) return StatusCode(StatusCodes.Status404NotFound);
            Book book = _context.Books.FirstOrDefault(c => c.Id == id);
            if (book == null) return StatusCode(StatusCodes.Status404NotFound);

            _context.Remove(book);
            _context.SaveChanges();
            return NoContent();
        }
        [HttpPatch("change/{id}")]
        public IActionResult ChangeName(int id, string newname)
        {
            if (id == 0) return StatusCode(StatusCodes.Status404NotFound);
            if (string.IsNullOrEmpty(newname)) return BadRequest();
            Book existed = _context.Books.FirstOrDefault(c => c.Id == id);
            if (existed == null) return StatusCode(StatusCodes.Status404NotFound);
            if (_context.Books.Any(b => b.Name == newname)) return BadRequest();
            existed.Name = newname;
            _context.SaveChanges();
            return NoContent();
        }
    }
}
