﻿using MercadoPago.Resource.Payment;
using Microsoft.AspNetCore.Mvc;
using PixMicroservice.DAO;
using PixMicroservice.Events;
using PixMicroservice.Sagas;


[Route("api/[controller]")]
[ApiController]
public class PixTransactionsController : ControllerBase
{
    //private readonly PixContext _context;

    public PixTransactionsController(IPixContext context)
    {
        _context = context;
        
    }
   

    //private readonly PaymentSaga _paymentSaga;
    public readonly IPixContext _context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PixTransaction>>> GetPixTransactions(string ChavePix)
    {
        var transactions = await _context.GetAllTransactionsAsync(ChavePix);
        return Ok(transactions);
    }

    [HttpPost]
    public async Task<ActionResult<PixTransaction>> PostPixTransaction(PixTransaction transaction)
    {
        try
        {
            ML mL = new ML();
            var teste = mL.Pix(transaction);
            var qrcode = teste.PointOfInteraction.TransactionData.QrCodeBase64;
            transaction.Id = Guid.NewGuid().ToString();
            await _context.InsertTransactionAsync(transaction);
            return CreatedAtAction(nameof(GetPixTransactions), new { id = transaction.Id, QrCode = qrcode }, transaction);
        }
        catch (Exception ex)
        { 
            return BadRequest(ex.Message);
        }
    }
   


   


}
