AddressLookUp - an API for IP/Domain Address LookUp. 

### Description

Endpoint: https://localhost:7085/api/addresslookup/8.8.8.8

Required Parameter : address - User has to send address (IP or Domain Name) for lookup. This is a required field.

Optional Parameter(query) : servicelist - User can send list of services they want to use for lookup. This is an optional field.

![image](https://user-images.githubusercontent.com/39909249/179764627-df55da82-5a91-4a92-b8af-76b56249524b.png)


### Implemented with Microservices - API Gateway Pattern

![image](https://user-images.githubusercontent.com/39909249/179696136-9594438f-3d86-4b54-a790-83352008c712.png)


1. AddressLookUp.Aggregator.API is an aggregated API which is calling multiple services underneath the hood for address lookup.
2. All the worker services are inside Services folder separately with their unit test project.
3. Common folder has all the reusable code.

Rule: If servicelist is empty, then sending response for default serviceslist `ping,reversedns,rdap,geoip`
otherwise, will send response for only asked serviceslist options.

### API Usage

https://localhost:7085/api/addresslookup/8.8.8.8?servicelist=ping%2Cdomain

`GET /api/lookup/{address}?servicelist=ping,domain` ->  Returns JSON from the specified set of services in the `servicelist` parameter.
If none is passed to servicelist, it will send response of default services `ping,reversedns,rdap,geoip`

**Parameter** - `address`

  - Type `{string}` - IP Address or Domain
	- Example: `8.8.8.8` or `google.com`

 **Parameter** - `servicelist`

  - Type `{string}` - Comma separated list of strings
    - Accepatable Values:    
      - `ping` - Ping
      - `geoip`- GeoIP
      - `rdap` - RDAP
      - `reversedns` - ReverseDNS
      - `domain`- Domain Availability
      
    - Example: `rdap,geoip`
	

### Building and running the App

### Technologies/Tools Used

- .NET 6
- Visual Studio 2022
- Docker Desktop
- xUnit with Moq

First, run microservices using docker

```shell
cd <project root dir>

docker-compose up --build
``` 

![image](https://user-images.githubusercontent.com/39909249/179767686-3991ccf9-11b3-4bc1-9bde-5115edc83584.png)


### Run AddressLookUp.Aggregator.Api 

As facade/aggregated api (AddressLookUp.Aggregator.Api) is not included in docker compose, it needs to be run separately.

```shell
cd <project root dir>
dotnet run --project AddressLookUp.Aggregator.Api
```

Now, API can be tested using this URl - https://localhost:7085/api/addresslookup/8.8.8.8

### To test API using Swagger

  - Make `AddressLookUp.Aggregator.Api` as a startup project and run it from VS 2022

  - The swagger URL will be - https://localhost:7085/swagger/index.html

  


 



 
