
using System.Threading.Tasks;
using PixMicroservice.Context;
using PixMicroservice.DAO;
using PixMicroservice.MercadoPago;

namespace PixMicroservice.Sagas
{
    public class PixSaga
    {
        private readonly IPixContext _context;
        private readonly IML _mercadoPago;

        public PixSaga(IPixContext context, IML mercadoPago)
        {
            _context = context;
            _mercadoPago = mercadoPago;
        }

        public async Task<string> StartTransaction(PixTransaction transaction)
        {
            // Step 1: Save transaction to database
            await _context.SaveTransactionAsync(transaction);

            // Step 2: Process payment through MercadoPago
            var paymentResult = await _mercadoPago.ProcessPayment(transaction);

            // Step 3: Update transaction status based on payment result
            transaction.Status = paymentResult ? "Success" : "Failed";
            await _context.UpdateTransactionAsync(transaction);

            return transaction.Status;
        }
    }
}
