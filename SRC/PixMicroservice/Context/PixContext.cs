using Couchbase.KeyValue;
using Couchbase;
using PixMicroservice.DAO;
using Couchbase.Query;
using Newtonsoft.Json;
using System.Transactions;

public class PixContext : IPixContext
{
    private readonly ICouchbaseCollection _collection;
    private readonly ICluster _cluster;

    public PixContext(IConfiguration config)
    {
        var options = new ClusterOptions
        {
            UserName = config["Couchbase:Username"],
            Password = config["Couchbase:Password"]
        };
        _cluster = Cluster.ConnectAsync(config["Couchbase:ConnectionString"], options).Result;
        var bucket = _cluster.BucketAsync(config["Couchbase:Bucket"]).Result;
       
        _collection = bucket.DefaultCollection();
    }

    public async Task<List<PixTransaction>> GetAllTransactionsAsync(string chavepix)
    {
        List<PixTransaction> pix = new List<PixTransaction>();
        // Define your N1QL query
        var query1 = "SELECT * FROM `PixBucket` WHERE pixKey = $fieldValue";

        // Define query options, including any parameters
        var queryOptions = new QueryOptions()
            .Parameter(
        "fieldValue", chavepix);


        var result1 = await _cluster.QueryAsync<PixBucketContainer>(query1, queryOptions).Result.Rows.ToListAsync();
       

        foreach (var row in result1)
        {
            pix.Add(row.PixBucket);

        }

        // Execute the query
        //var result = await _cluster.QueryAsync<PixTransaction>(query1, queryOptions);

        

        //var query = await _collection.Scope.QueryAsync<PixBucketContainer>("SELECT * FROM `PixBucket`").Result.Rows.ToListAsync();

        return pix;
    }

    public async Task InsertTransactionAsync(PixTransaction transaction)
    {
        await _collection.UpsertAsync(transaction.Id, transaction);
    }


}


