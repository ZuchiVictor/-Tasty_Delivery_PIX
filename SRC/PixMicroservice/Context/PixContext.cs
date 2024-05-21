using Couchbase.KeyValue;
using Couchbase;
using PixMicroservice.DAO;

public class PixContext
{
    private readonly ICouchbaseCollection _collection;

    public PixContext(IConfiguration config)
    {
        var options = new ClusterOptions
        {
            UserName = config["Couchbase:Username"],
            Password = config["Couchbase:Password"]
        };
        var cluster = Cluster.ConnectAsync(config["Couchbase:ConnectionString"], options).Result;
        var bucket = cluster.BucketAsync(config["Couchbase:Bucket"]).Result;
        _collection = bucket.DefaultCollection();
    }

    public async Task<List<PixTransaction>> GetAllTransactionsAsync()
    {
        var query = await _collection.Scope.QueryAsync<PixTransaction>("SELECT * FROM `PixBucket`");
        return await query.Rows.ToListAsync();
    }

    public async Task InsertTransactionAsync(PixTransaction transaction)
    {
        await _collection.UpsertAsync(transaction.Id, transaction);
    }
}