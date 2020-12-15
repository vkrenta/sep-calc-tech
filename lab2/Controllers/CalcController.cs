using System;
using System.Text.Json;
using System.Threading.Tasks;
using lab2.Models;
using Microsoft.AspNetCore.Mvc;

namespace lab2.Controllers {
    [ApiController]
    [Route("api/calc")]
    public class CalcController : ControllerBase {
        [HttpGet]
        [Route("function")]
        public ActionResult<string> CalcFunction() {

            var item = new CalcModel.FuncItem { id = CalcModel.funcId };

            Task.Run(() => {
                item.value = CalcModel.veryLongFunction();
                CalcModel.funcList.Add(item);
                Console.WriteLine("Finished " + item.id);
            });

            return Ok(new { message = $"Your function with id {item.id} are calculating" });
        }

        [HttpGet]
        [Route("result/id={id}")]
        public object ResultFunction([FromRoute] int id) {
            var item = CalcModel.funcList.Find(item => item.id == id);
            if (item == null) {
                return NotFound(new { message = "Function with this id not founded or not solved. Wait more" });
            }
            return Ok(new { calculations = item });

        }
    }

}