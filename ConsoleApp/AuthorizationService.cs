using OpenFga.Sdk.Client;
using OpenFga.Sdk.Client.Model;

namespace ConsoleApp;

public class AuthorizationService
{
    private readonly OpenFgaClient _openFgaClient;

    public AuthorizationService()
    {
        var configuration = new ClientConfiguration
        {
            ApiUrl = "http://127.0.0.1:8080",
            StoreId = "01JAW5WJJN1FGESES2NJ0SBHQC", // demo store, kan dus per klant zijn, per omgeving (tst/acc/prd) etc.,
            // AuthorizationModelId = "01JB9W953EY520875HH7WP5SR5", // revisie ID van het model -- leeg is de laatste
        };
        _openFgaClient = new OpenFgaClient(configuration);
    }

    public async Task<bool> Check(string user, string @object, string relation, CancellationToken cancellationToken = default)
    {
        var result = await _openFgaClient.Check(new ClientCheckRequest
        {
            User = user,
            Object = @object,
            Relation = relation,
        }, cancellationToken: cancellationToken);
        
        return result.Allowed ?? false;
    }

    public async Task Delete(string user, string @object, string relation, CancellationToken cancellationToken = default)
    {
        await _openFgaClient.DeleteTuples(
        [
            new ClientTupleKeyWithoutCondition
            {
                User = user,
                Object = @object,
                Relation = relation,
            }
        ], cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<string>> List(string user, string type, string relation, CancellationToken cancellationToken = default)
    {
        var result = await _openFgaClient.ListObjects(new ClientListObjectsRequest
        {
            User = user,
            Relation = relation,
            Type = type,
        }, cancellationToken: cancellationToken);

        return result.Objects;
    }

    public async Task Write(string user, string @object, string relation, CancellationToken cancellationToken = default)
    {
        await _openFgaClient.Write(new ClientWriteRequest
        {
            Writes = [
                new ClientTupleKey
                {
                    User = user,
                    Object = @object,
                    Relation = relation,
                }
            ]
        }, cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<string>> QueryRelations(string @object, string relation, CancellationToken cancellationToken = default)
    {
        var result = await _openFgaClient.Read(new ClientReadRequest
        {
            Object = @object,
            Relation = relation,
        }, cancellationToken: cancellationToken);

        return result.Tuples.Select(x => x.Key.User);
    }
}