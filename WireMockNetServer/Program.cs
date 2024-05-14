using WireMock.Matchers;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using WireMock.Settings;


// Server for Wiremock.NET
var server =WireMockServer.Start(new WireMockServerSettings
{
    Urls = [
        "http://localhost:8080"
    ],
    StartAdminInterface = true,
    ReadStaticMappings = true,
});


Console.WriteLine("Started WireMock.NET server");


//Create the sub
/*
 * 1. Path /test
 * 2. Request Type GET/POST/UPDATE
 * 3. Header
 * 4. Response
 */

//Stub - Test
server.Given(Request.Create().WithPath("/test").UsingGet())
    .RespondWith(Response.Create().WithBody("Welcome to Wiremock .NET Test")
    .WithHeader("Content-Type", "application/json"));

//Stub - Header
server.Given(Request.Create().WithPath("/headers").UsingGet())
    .RespondWith(Response.Create().WithBody("Welcome to Wiremock .NET Test for headers")
    .WithHeaders(new Dictionary<string, string>
    {
        {"Content-Type", "application/json"},
        {"Accept", "application/json"},
        {"Cache-Control", "no-cache" }
    })
    .WithStatusCode(System.Net.HttpStatusCode.Accepted)
    );


server.Given(Request.Create().WithPath("/headers").UsingGet())
    .RespondWith(Response.Create().WithBody("Headers with seconds stub call")
    .WithHeaders(new Dictionary<string, string>
    {
        {"Content-Type", "application/json"},
        {"Accept", "application/json"},
        {"Cache-Control", "no-cache" }
    })
    .WithStatusCode(System.Net.HttpStatusCode.Accepted)
    );



server.Given(Request.Create().WithPath("/headers").UsingPut())
    .RespondWith(Response.Create().WithBody("Welcome to Wiremock .NET Test for headers")
    .WithHeaders(new Dictionary<string, string>
    {
        {"Content-Type", "application/json"},
        {"Accept", "application/json"},
        {"Cache-Control", "no-cache" }
    })
    .WithStatusCode(System.Net.HttpStatusCode.Accepted)
    );

//Stub - ProductId
server.Given(Request.Create().WithPath(new RegexMatcher("/product/[0-9]+$")).UsingGet())
    .RespondWith(Response.Create().WithBody("Keyboard is the product")
    .WithHeaders(new Dictionary<string, string>
    {
        {"Content-Type", "application/json"},
        {"Accept", "application/json"},
        {"Cache-Control", "no-cache" }
    })
    .WithStatusCode(System.Net.HttpStatusCode.Accepted)
    );

//Stub - ProductId
server.Given(Request.Create().WithPath(new WildcardMatcher("/product/*")).UsingGet())
    .RespondWith(Response.Create().WithBody("This could be any product")
    .WithHeaders(new Dictionary<string, string>
    {
        {"Content-Type", "application/json"},
        {"Accept", "application/json"},
        {"Cache-Control", "no-cache" }
    })
    .WithStatusCode(System.Net.HttpStatusCode.Accepted)
    );

//Stub - login
server.Given(Request
    .Create()
    .WithPath("/login")
    .UsingGet()
    .WithHeader("Authorization", new WildcardMatcher("Bearer *")))
    .RespondWith(Response.Create().WithBody("Login Successful")
    .WithHeaders(new Dictionary<string, string>
    {
        {"Content-Type", "application/json"},
        {"Accept", "application/json"},
        {"Cache-Control", "no-cache" }
    })
    .WithStatusCode(System.Net.HttpStatusCode.Accepted)
    );

//Stub - login
server.Given(Request
    .Create()
    .WithPath("/login")
    .UsingGet()
    .WithHeader("Authorization", "*", MatchBehaviour.RejectOnMatch))
    .RespondWith(Response.Create().WithBody("Login UnSuccessful")
    .WithHeaders(new Dictionary<string, string>
    {
        {"Content-Type", "application/json"},
        {"Accept", "application/json"},
        {"Cache-Control", "no-cache" }
    })
    .WithStatusCode(System.Net.HttpStatusCode.Unauthorized)
    );



//Stub - GetAddress
server.Given(Request.Create().WithPath(new WildcardMatcher("/getAddress/*")).UsingGet())
    .RespondWith(Response.Create().WithBodyAsJson(new
    {
        Name = "Karthik",
        City = "Auckland",
        Country = "NZ"
    })
    .WithHeaders(new Dictionary<string, string>
    {
        {"Content-Type", "application/json"},
        {"Accept", "application/json"},
        {"Cache-Control", "no-cache" }
    })
    .WithStatusCode(System.Net.HttpStatusCode.Accepted)
    );


//Stub - GetAddress
server.Given(Request.Create().WithPath(new WildcardMatcher("/getAddressFromObject/*")).UsingGet())
    .RespondWith(Response.Create().WithBodyAsJson(new Address("Prashanth", "Chennai", "India"))
    .WithHeaders(new Dictionary<string, string>
    {
        {"Content-Type", "application/json"},
        {"Accept", "application/json"},
        {"Cache-Control", "no-cache" }
    })
    .WithStatusCode(System.Net.HttpStatusCode.Accepted)
    );


Console.WriteLine("All the Stub Mappings are bound");

Console.WriteLine("Hit any key to close the server !!!");

Console.ReadKey();

public record Address(string Name, string City, string Country);