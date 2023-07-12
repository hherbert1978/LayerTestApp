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

        #region "Get"

        // GET: api/<PayGradeController>/GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllPayGrades()
        {
            var payGrades = await PayGradeService.GetAllAsync();
            return new OkObjectResult(new APIResponse<IEnumerable<PayGradeResponseDTO>>(payGrades));
        }

        // GET: api/<PayGradeController>/GetAllActive
        [HttpGet("GetAllActive")]
        public async Task<IActionResult> GetAllActivePayGrades()
        {
            var payGrades = await PayGradeService.GetAllActiveAsync();
            return new OkObjectResult(new APIResponse<IEnumerable<PayGradeResponseDTO>>(payGrades));
        }

        // GET api/<PayGradeController>/GetById/5
        [HttpGet("GetById/{id}")]

        public async Task<IActionResult> GetById(int id)
        {
            var payGrade = await PayGradeService.GetByIdAsync(id);
            return new OkObjectResult(new APIResponse<PayGradeResponseDTO>(payGrade));
        }

        // GET api/<PayGradeController>/GetByName/Test des PayGrade
        [HttpGet("GetByName/{name}")]

        public async Task<IActionResult> GetByName(string name)
        {
            var payGrade = await PayGradeService.GetByNameAsync(name);
            return new OkObjectResult(new APIResponse<PayGradeResponseDTO>(payGrade));
        }

        // GET api/<PayGradeController>/GetByName/Test des PayGrade
        [HttpGet("GetIdByName/{name}")]

        public async Task<IActionResult> GetIdByName(string name)
        {
            var payGradeId = await PayGradeService.GetIdByNameAsync(name);
            return new OkObjectResult(new APIResponse<int>(payGradeId));
        }

        #endregion

        // Post: api/<PayGradeController>/Create
        [HttpPost("Create")]
        public async Task<IActionResult> CreatePayGrade(CreatePayGradeDTO createPayGradeDTO)
        {
            var createResponse = await PayGradeService.CreateAsync(createPayGradeDTO);
            return new OkObjectResult(new APIResponse<PayGradeResponseDTO>(createResponse));
        }

        // PUT api/<PayGradeController>/Update
        [HttpPut("Update")]
        public async Task<IActionResult> UpdatePayGrade(UpdatePayGradeDTO updatePayGradeDTO)
        {
            var updateResponse = await PayGradeService.UpdateAsync(updatePayGradeDTO);
            return new OkObjectResult(new APIResponse<PayGradeResponseDTO>(updateResponse));
        }

        // DELETE api/<PayGradeController>/Delete
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeletePayGrade(DeletePayGradeDTO deletePayGradeDTO)
        {
            var deleteResponse = await PayGradeService.DeleteAsync(deletePayGradeDTO);
            return new OkObjectResult(new APIResponse<PayGradeDeleteResponseDTO>(deleteResponse));
        }
    }
}
