using Microsoft.AspNetCore.Mvc;
using PixMicroservice.DAO;

[Route("api/[controller]")]
[ApiController]
public class PixTransactionsController : ControllerBase
{
    private readonly PixContext _context;

    public PixTransactionsController(PixContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PixTransaction>>> GetPixTransactions()
    {
        var transactions = await _context.GetAllTransactionsAsync();
        return Ok(transactions);
    }

    [HttpPost]
    public async Task<ActionResult<PixTransaction>> PostPixTransaction(PixTransaction transaction)
    {
        transaction.Id = Guid.NewGuid().ToString();
        await _context.InsertTransactionAsync(transaction);
        return CreatedAtAction(nameof(GetPixTransactions), new { id = transaction.Id }, transaction);
    }

    // Outros métodos (PUT, DELETE) podem ser adicionados conforme necessário
}
