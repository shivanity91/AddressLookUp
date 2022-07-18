AddressLookUp - an API for IP/Domain Address LookUp. 

### Description

Endpoint: https://localhost:7085/api/lookup/8.8.8.8

Required Parameter : address - User has to send address for lookup. This is a required field.

Optional Parameter(query) : servicelist - User can send list of services they want to use for lookup. This is an optional field.

### Implemented with Microservices - API Gateway Pattern

AddressLookUp is an aggregated API which is calling multiple services underneath the hood for address lookup.

Rule: If servicelist is empty, then sending response for default serviceslist `ping,reversedns,rdap,geoip`
otherwise, will send response for only asked serviceslist options.

### API Usage

`GET /api/lookup/{address}?servicelist=` ->  Returns JSON from the specified set of services in the `servicelist` parameter.
If none is passed to servicelist, it will send response of default services `ping,reversedns,rdap,geoip`

**Parameter** - `address`

  - Type `{string}` - IP Address or Domain
	- Example: `8.8.8.8` or `google.com`

 **Parameter** - `servicelist`

  - Type `{string}` - Comma separated list of strings
    - Accepatable Values:
      - `rdap` - RDAP
      - `reversedns` - ReverseDNS
      - `ping` - Ping
      - `geoip`- GeoIP

    - Example: `rdap,geoip`
	

### Building and running the App

### Prerequisites

- .NET 6
- Visual Studio 2022
- Docker Desktop

First, run microservices using docker

```shell
cd <project root dir>

docker-compose up --build
``` 

### Run AddressLookUp.Api 

Since facade api (AddressLookUp.Api) is not included in docker compose, it needs to be run separately.

```shell
cd <project root dir>
dotnet run --project AddressLookUp.Api
```

Now, API can be tested using this URl - https://localhost:7085/api/lookup/8.8.8.8

### To test API using Swagger

  - Make `AddressLookUp.Api` as a startup project and run it from VS 2022

  - The swagger URL will be - https://localhost:7085/swagger/index.html

  


 



 
