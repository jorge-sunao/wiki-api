# Wiki API
> Web API for managing and editing wiki articles built with C# .NET 6.


## Table of Contents
* [General Info](#general-information)
* [Built With](#built-with)
* [Features](#features)
* [Setting Up Redis in Docker](#setting-up-redis-in-docker)
* [Testing It](#testing-it)


## General Information
* Architecture based on three layers
	- Core: This layer will contain interfaces, entities, abstractios and business logics. It has no dependencies on external influence.
	- Infrastructure: This layer will contain classes for accessing external resources such as file systems and database. These classes should be based on interfaces defined within the application layer. Depends on Core,
	- API: This layer is a Web API that which depends on Core.
* Application of Mediator Pattern to decrease the dependence and coupling between objects.


## Built With
- .NET 6
- C# 10
- MediatR
- AutoMapper
- SQL Server
- FluentValidation
- Dapper
- Serilog
- Redis
- xUnit
- Moq


## Features
* Initial data seeding on startup
* Basic CRUD using Web API .NET 6
* Clean Architecture
* Logging
* Distributed Caching / Response Caching


## Setting Up Redis in Docker

1. Pull docker Redis image from docker hub (`docker pull redis`)
2. Run Redis images by mapping Redis port to a local system port (`docker run --name wikiapirediscache -p 5003:6379 -d redis`)
3. Start the container (`docker start wikiapirediscache`)


## Testing It

Open the project using your IDE of choice for .NET projects, build it, and run it.
The Swagger default start page should be loaded in the browser displaying the following two controllers:
* Articles
	- GET ​/api​/Articles: Retrieve all articles
	- GET ​/api​/Articles​/{id}​: Retrieve the details of the specified article
	- POST ​/api​/Articles: Create a new article
	- PUT ​/api​/Articles​: Update an existing article
	- DELETE ​/api​/Articles​/{id}: Delete an existing article
* Sources
	- GET ​/api​/Sources: Retrieve all sources
	- GET ​/api​/Sources​/{id}​: Retrieve the details of the specified source
	- POST ​/api​/Sources: Create a new source
	- PUT ​/api​/Sources​: Update an existing source
	- DELETE ​/api​/Sources​/{id}: Delete an existing source