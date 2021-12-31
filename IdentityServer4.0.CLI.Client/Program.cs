using System.Text.Json.Nodes;
using IdentityModel.Client;

var client = new HttpClient();
var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");

if (disco.IsError)
{
    Console.WriteLine(disco.Error);
    return;
}

var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest()
{
    Address = disco.TokenEndpoint,
    ClientId = "IdentityServer4CliClient",
    ClientSecret = "secret",
    Scope = "IdentityServer4Provider",
});

if (tokenResponse.IsError)
{
    Console.WriteLine(tokenResponse.Error);
    return;
}

Console.WriteLine(tokenResponse.Json);

var apiClient = new HttpClient();
apiClient.SetBearerToken(tokenResponse.AccessToken);

var response = await apiClient.GetAsync("https://localhost:6001/identity");

if (!response.IsSuccessStatusCode)
{
    Console.WriteLine(response.StatusCode);
    Console.WriteLine(await response.Content.ReadAsStringAsync());
}
else
{
    var content = await response.Content.ReadAsStringAsync();
    Console.WriteLine(JsonArray.Parse(content));
}

Console.ReadLine();