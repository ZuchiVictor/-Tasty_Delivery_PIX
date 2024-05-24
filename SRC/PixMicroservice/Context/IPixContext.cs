using PixMicroservice.DAO;

public interface IPixContext
{
    Task<List<PixTransaction>> GetAllTransactionsAsync(string chavepix);
    Task InsertTransactionAsync(PixTransaction transaction);
}