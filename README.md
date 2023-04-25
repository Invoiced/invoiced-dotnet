# invoiced-dotnet

This repository contains the .NET client library for the [Invoiced](https://invoiced.com) API.

You can find detailed API documentation along with .NET code snippets [here](https://www.invoiced.com/resources/docs/api/?csharp).

[![CI](https://github.com/Invoiced/invoiced-dotnet/actions/workflows/ci.yml/badge.svg)](https://github.com/Invoiced/invoiced-dotnet/actions/workflows/ci.yml)
[![NuGet](https://img.shields.io/nuget/v/Invoiced.svg)](https://www.nuget.org/packages/Invoiced/)

## Usage

First, you must instantiate a new client

```c#
using Invoiced;
 ...
var connection = new Connection("{YOUR_API_KEY}",Invoiced.Environment.sandbox);
```

Then, API calls can be made like this:
```c#
# retrieve invoice
var invoice = connection.NewInvoice().retrieve(1);

# mark as paid
var payment = connection.NewPayment();
payment.Amount = invoice.Balance;
payment.Method = "check";
payment.AppliedTo = new[]
{
    new PaymentItem {Type = "invoice", Amount = 100, Invoice = invoice.Id}
};
payment.Create();
```

## Developing

The test suite can be ran with `dotnet test`

## Deployment

Follow these steps to publish a package to NuGet:

```
dotnet pack invoicedapi -c Release --output nuget
cd nuget
nuget push Invoiced.X.X.X.nupkg <apikey> -Source https://api.nuget.org/v3/index.json
```