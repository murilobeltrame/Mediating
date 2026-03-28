# Mediating

Mediator became a very popular way to structure sofisitcated applications in combination of Command Query Resposability Separation. And Mediatr became the _de facto_ standard on how implement it.
This repo meant to explore alterantives to it implementing some of most common scenarios I currently work with while exploring code simplicity, testability and performance concerns.

## How to run

This is an Aspire Application, just run the `AppHost` project:
```sh
$ dotnet run --project env/AppHost
```
As any other Aspire Application, this project depends on [.NET SDK](https://dotnet.microsoft.com/en-us/download/dotnet/10.0) and a [supported Container Engine](https://aspire.dev/get-started/prerequisites/?apphost=csharp#install-an-oci-compliant-container-runtime).
