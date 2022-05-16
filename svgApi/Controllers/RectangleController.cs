using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using svgApi.Models;

namespace svgApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RectangleController : ControllerBase
    {
        private readonly RectangleContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private string _path;
        private JObject? _jsonObj;

        public RectangleController(RectangleContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            _path = Path.Combine(_hostingEnvironment.WebRootPath, "svg.json");

            string json = System.IO.File.ReadAllText(_path);
            _jsonObj = JsonConvert.DeserializeObject<JObject>(json);
        }

        // GET: api/Rectangle
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rectangle>>> GetRectangles()
        {
          if (_context.Rectangles == null)
          {
              return NotFound();
          }
            return await _context.Rectangles.ToListAsync();
        }

        // GET: api/Rectangle/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rectangle>> GetRectangle(int id)
        {
          if (_context.Rectangles == null)
          {
              return NotFound();
          }
            var rectangle = await _context.Rectangles.FindAsync(id);

            if (rectangle == null)
            {
                return NotFound();
            }

            return rectangle;
        }

        // PUT: api/Rectangle/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRectangle(int id, Rectangle rectangle)
        {
            if (id != rectangle.RectId)
            {
                return BadRequest();
            }

            _context.Entry(rectangle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                this.UpdateRectangleToFile(id, rectangle);

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RectangleExists(id))
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

        // POST: api/Rectangle
        [HttpPost]
        public async Task<ActionResult<Rectangle>> PostRectangle(Rectangle rectangle)
        {
          if (_context.Rectangles == null)
          {
              return Problem("Entity set 'RectangleContext.Rectangles'  is null.");
          }
            _context.Rectangles.Add(rectangle);
            await _context.SaveChangesAsync();

            this.CreateRectangleToFile(rectangle);

            return CreatedAtAction("GetRectangle", new { id = rectangle.RectId }, rectangle);
        }

        // DELETE: api/Rectangle/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRectangle(int id)
        {
            if (_context.Rectangles == null)
            {
                return NotFound();
            }
            var rectangle = await _context.Rectangles.FindAsync(id);
            if (rectangle == null)
            {
                return NotFound();
            }

            _context.Rectangles.Remove(rectangle);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RectangleExists(int id)
        {
            return (_context.Rectangles?.Any(e => e.RectId == id)).GetValueOrDefault();
        }

        // Save rectangle to json file
        private void CreateRectangleToFile(Rectangle rectangle)
        {
            var camelCaseSerializer = new JsonSerializer { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var updatedRectangle = JObject.FromObject(rectangle, camelCaseSerializer);

            if (_jsonObj == null)
            {
                return;
            }

            var rectanglesObj = _jsonObj["rectangle"];
            if (rectanglesObj != null)
            {
                JArray rectangles = (JArray)rectanglesObj;
                rectangles.Add(updatedRectangle);

                _jsonObj["rectangle"] = rectangles;
            }

            string output = JsonConvert.SerializeObject(_jsonObj, Formatting.Indented);
            System.IO.File.WriteAllText(_path, output);
            
        }

        // Update rectangle to json file
        private void UpdateRectangleToFile(int id, Rectangle rectangle)
        {
            var rectIdToken = JToken.FromObject(id);
            var updatedRectId = rectIdToken.Value<int>();

            var camelCaseSerializer = new JsonSerializer { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var updatedRectangle = JObject.FromObject(rectangle, camelCaseSerializer);

            if (_jsonObj == null)
            {
                return;
            }

            var rectanglesObj = _jsonObj["rectangle"];
            if (rectanglesObj == null)
            {
                return;
            }

            JArray rectangles = (JArray)rectanglesObj;

            var rectangleIndex = -1;

            foreach (JObject value in rectangles)
            {
                foreach (var property in value.Properties())
                {
                    if (property.Name == "rectId" && (int)property.Value == updatedRectId)
                    {
                        rectangleIndex = rectangles.IndexOf(value);
                    }
                }
            }

            if (rectangleIndex != -1)
            {
                rectangles[rectangleIndex] = updatedRectangle;
            }


            _jsonObj["rectangle"] = rectangles;
            

            string output = JsonConvert.SerializeObject(_jsonObj, Formatting.Indented);
            System.IO.File.WriteAllText(_path, output);
        }
    }
}
