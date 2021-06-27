using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private static List<string> _Values = Enumerable.Range(1, 10)
            .Select(i => $"Value-{i}")
            .ToList();

        //public List<string> Get() => _Values;

        [HttpGet]
        public IActionResult Get() => Ok(_Values);

        [HttpGet("count")]
        public IActionResult GetCount() => Ok(_Values.Count);

        [HttpGet("{index}")]
        public ActionResult<string> GetByIndex(int index)
        {
            if (index < 0)
                return BadRequest();
            if (index >= _Values.Count)
                return NotFound();

            return _Values[index];
        }

        [HttpPost]
        [HttpPost("add")]
        public ActionResult Add(string str)
        {
            _Values.Add(str);
            return Ok();
            //return CreatedAtAction(nameof(GetByIndex), new { index = _Values.Count-1});
        }

        [HttpPut]
        [HttpPut("edit/{index}")]
        public ActionResult Replace(int index, string newStr)
        {
            if (index < 0)
                return BadRequest();
            if (index >= _Values.Count)
                return NotFound();

            _Values[index] = newStr;
            return Ok();
        }

        [HttpDelete("{index}")]
        public ActionResult Delete(int index)
        {
            if (index < 0)
                return BadRequest();
            if (index >= _Values.Count)
                return NotFound();

            _Values.RemoveAt(index);
            return Ok();
        }
    }
}
