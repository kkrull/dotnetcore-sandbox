# Identity Server 4 Quick Start

Going through the [Identity Server QuickStart](http://docs.identityserver.io/en/release/quickstarts/1_client_credentials.html).

There are several moving parts:

* An `IdentityServer` project that runs its own web app on `http://localhost:5000`, 
  which performs authentication and authorization.
* An `Api` project that runs its own web app on `http://localhost:5001`,
  which has the REST endpoints you want to get to.
* A console-based `Client` project to get at all the stuff in the API, providing the necessary means of authentication.
* A browser-based `MvcClient` project that does OpenID Connect workflows on `http://localhost:5002`.


## IdentityServer

This server issues and validates tokens.


```bash
# Server
$ dotnet run #Starts server on http://localhost:5000

# Client
$ curl 'http://localhost:5000/.well-known/openid-configuration' | jq
```


## Api

An API server where users do stuff.  Stuff needs to be authorized for authenticated users, using a bearer token.

```bash
# Server (start after IdentityServer)
$ dotnet run #Starts on http://localhost:5001

# Unauthorized client
$ curl -v 'http://localhost:5001/identity' # => 401 Unauthorized
```


## Client

A client that

* Gets the discovery document to find the access token URL
* Requests an access token using a known client, secret, and API

`IdentityServer` has to be running.

```bash
$ dotnet run --project Client | jq
  {
    "access_token": "eyJhbGciOiJSUzI1NiIsImtpZCI6ImU5NzUzZjRjZWFhMzc0NDc0N2E4NjRkYjE0NmNiMjBlIiwidHlwIjoiSldUIn0.eyJuYmYiOjE1MzI2NTkzNzEsImV4cCI6MTUzMjY2Mjk3MSwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MDAwIiwiYXVkIjpbImh0dHA6Ly9sb2NhbGhvc3Q6NTAwMC9yZXNvdXJjZXMiLCJhcGkxIl0sImNsaWVudF9pZCI6ImNsaWVudCIsInNjb3BlIjpbImFwaTEiXX0.jR-IPm4nkMqyrHT8wAmFmZUKhHa1-cNetAkCvvVh_CJK8ELuXefeJIA42_hdTSh9QKLEQTgQ4hw7lrD1DHNSdjOcIPfv0ZqeW1814pzTuf1BWHUrCBfoVPSPblyhZoRq4oCxSX0qjR6znR24dZkfnO4tAmePqZG0AHt7gYHmZlPp8OjwVRgjKYmDXgn3jmkGLyTQ9-kGwWbKRgRz5H-u34Aa40JnOeZKS65Xf6XAVuZAVW53fQMkECrJ1LIShrmiKgmLPrKRqHp2lV1iXcfytB3yhulMmYrn8PYYQiGpMXmh3rdzMclbUoo_8nBMAGNq28QyeqLEXHfoWVetAERHYg",
    "expires_in": 3600,
    "token_type": "Bearer"
  }
```

Take that to [jwt.io][https://jwt.io] to decode it.


## MvcClient

```bash
$ dotnet run --project MvcClient
```