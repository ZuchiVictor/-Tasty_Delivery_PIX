
using Microsoft.AspNetCore.Mvc;
using PixMicroservice.DAO;
using PixMicroservice.Sagas;

namespace PixMicroservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PixTransactionsController : ControllerBase
    {
        private readonly PixSaga _pixSaga;

        public PixTransactionsController(PixSaga pixSaga)
        {
            _pixSaga = pixSaga;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] PixTransaction transaction)
        {
            var result = await _pixSaga.StartTransaction(transaction);
            if (result == "Success")
            {
                return Ok(transaction);
            }
            else
            {
                return BadRequest("Transaction failed");
            }
        }
    }
}
