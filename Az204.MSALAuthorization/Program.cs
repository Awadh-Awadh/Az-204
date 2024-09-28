// See https://aka.ms/new-console-template for more information

using Az204.MSALAuthorization.Constants;
using Microsoft.Identity.Client;

Console.WriteLine("Hello, World!");
var app = PublicClientApplicationBuilder
    .Create(ApplicationConstants.ClientId)
    .WithAuthority(AzureCloudInstance.AzurePublic, ApplicationConstants.TenantId)
    .WithRedirectUri("http://localhost")
    .Build();
    
string[] scopes = ["user.read"];
AuthenticationResult result = await app.AcquireTokenInteractive(scopes).ExecuteAsync();
Console.WriteLine($"Token: \t {result.AccessToken}");