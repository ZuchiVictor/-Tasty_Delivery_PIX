using MercadoPago.Resource.Payment;
using PixMicroservice.DAO;

namespace PixMicroservice.MercadoPago
{
    public interface IML
    {
        Payment Pix(PixTransaction pix);
    }
}
