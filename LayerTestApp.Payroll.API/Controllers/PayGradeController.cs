using LayerTestApp.Common.Exceptions;
using LayerTestApp.Payroll.BAL.Models.DTOs.PayGradeDTOs;
using LayerTestApp.Payroll.BAL.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LayerTestApp.Payroll.API.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class PayGradeController : ControllerBase
    {

        private readonly IPayGradeService PayGradeService;

        public PayGradeController(IPayGradeService payGradeService)
        {
            PayGradeService = payGradeService;
        }

        // GET: api/<PayGradeController>/GetAllPayGrades
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllPayGrades()
        {
            var payGrades = await PayGradeService.GetAllAsync();
            return Ok(new APIResponse<IEnumerable<PayGradeResponseDTO>>(payGrades));
        }

        // GET api/<PayGradeController>/5
        [HttpGet("GetById/{id}")]

        public async Task<IActionResult> GetById(int id)
        {
            var payGrade = await PayGradeService.GetByIdAsync(id);
            return Ok(new APIResponse<PayGradeResponseDTO>(payGrade));
        }

        // POST api/<PayGradeController>
        [HttpPost]
        public bool Post([FromBody] string value)
        {

            throw new ResourceNotFoundException("LoggerTest");
        }

        // PUT api/<PayGradeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PayGradeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
