using System.Net.Http.Json;
using NBomber.CSharp;

using var httpClient = new HttpClient();

var scenario = Scenario.Create("otus-load", async context =>
    {
        var id = await httpClient.PostAsync("http://arch.homework/api/User", JsonContent.Create("""
{    
  "username": "agesrhsgh",
  "firstName": "ssghdftghjdfyyjitring",
  "lastName": "fkdfyjtjrfth",
  "email": "hfjftjftj@gmsil.com",
  "phone": "8800553535"
}
"""));
        var response = await httpClient.GetAsync($"http://arch.homework/api/User?userId={id}");

        await httpClient.PutAsync("http://arch.homework/api/User", JsonContent.Create("""
{    
  "firstName": "4574jfgjfjfgjfgj",
  "lastName": "gfhjfghfgghkghjghjghj",
  "email": "hfjftjftj@gmsil.com",
  "phone": "880055353556346"
}
"""));

        await httpClient.DeleteAsync($"http://arch.homework/api/User?userId = {id}");

        return response.IsSuccessStatusCode
            ? Response.Ok()
            : Response.Fail();
    })
    .WithoutWarmUp()
    .WithLoadSimulations(
        Simulation.Inject(rate: 10,
            interval: TimeSpan.FromSeconds(1),
            during: TimeSpan.FromSeconds(30))
    );

NBomberRunner
    .RegisterScenarios(scenario)
    .Run();