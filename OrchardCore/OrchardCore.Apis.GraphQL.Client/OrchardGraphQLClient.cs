using System.Net.Http;

namespace OrchardCore.Apis.GraphQL.Client
{
    public class OrchardGraphQLClient
    {
        public OrchardGraphQLClient(HttpClient client)
        {
            Client = client;
        }

        public ContentResource Content => new ContentResource(Client);

        public HttpClient Client { get; }
    }
}
