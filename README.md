## ItalianoPizzeria - A Basic Pizza Builder app using .NET Core and Angular
-------

This application lets you build a basic Pizza by adding the Doughtype and ingredients. This project is a POC for testing how to build an application with Angular, Bootstrap and .NET Core backend.

## Motivation
-------

This project is to show a user a simple basic SPA app using Angular , Bootstrap and .NET core 3.1 and the OpenAPI specification (Swagger)

## Build
-----

The backend for the project is built using .NET Core 3.1 (C#) as the programming language.

The front end using Angular 10 with Bootstrap. 

The project could be further extended to be deployed into any cloud platform by adding build infrastructure. 

The Swagger OpenAPI specification makes it easier to understand the backend contracts and also add the corresponding models in the FrontEnd Angular project for making the development easier using stronger types (in this case Typescript)


## Code Style
-------
The coding style for the backend has been following a clean code practice using Dependency Injection , Automapper for mapping the Business objects to the entities, an In memory Database for easier testing , unit testing to test the backend code. CORS configuration had to be configured to make the cross domain (frontend domain) support the backend domain.


## Screenshots
------

<a href="https://www.dropbox.com/s/137hoo9s45km6vq/getAllPizzas.png?dl=0" target="_blank"> Get All Pizzas</a>

<a href="https://www.dropbox.com/s/cag0dvzofspcgxk/pizza_details.png?dl=0" target="_blank"> Get Pizza Details </a> 

<a href="https://www.dropbox.com/s/pg7l889ul3t97vz/edit_and_delete_pizza.png?dl=0" target="_blank"> Edit and Delete Pizza </a> 

<a href="https://www.dropbox.com/s/jsqu1hpj8d79mh5/swagger.png?dl=0" target="_blank"> Swagger </a> 

## Frameworks and other packages Used
------
.NET Core 3.1 , Angular 10, Swagger Generator, EF Core (3.1.8) , EF Core In Memory DB (3.1.8), Automapper (10.0.0) , Bootstrap (4.5.2)

## Installation
------
Used nuget package manager to install .NET core dependencies

Used npm to install Angular dependencies


## How To Use
------
The backend can be run from .NET CLI using (dotnet run) command or from an IDE(I used JetBrains Rider and its convinient from the UI to Run and debug the program)

The backend tests can be run using .NET CLI (dotnet test) or through an IDE using the test explorer

The FrontEnd Angular is run using (ng serve --o) command

## Other links and resources used for the project
------
I used the resources from the project statement/task that was given to me . Following are the links for reference and the problem statement.


Here are the basic frameworks and techniques I would suggest for your demo project:
- Backend: ASP.NET Core WebApi


o EntityFramework Core with InMemory Database: ( https://exceptionnotfound.net/ef-core-inmemory-asp-net-core-store-database/ )


▪ InMemory because its more portable and a real database system is not needed.

o Automapper to map EF model objects to DataTransferObjects ( https://www.c-sharpcorner.com/article/integrate-automapper-in-net-core-web-api2/ )

o OpenAPI Specification for the WebApi

o Use NSwag.AspNetCore (supports Swagger Spec 2.0 and OpenAPI 3.0. https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-nswag?view=aspnetcore-3.1&tabs=visual-studio )

o Optional Bonus: Use Serilog for logging all actions in the controllers (https://nblumhardt.com/2019/10/serilog-in-aspnetcore-3/ )

- Frontend: Angular 9 or 10

o Api Clients with openapi-generator ( https://javaeeblog.wordpress.com/2019/07/18/openapi-generator/ )

o Use Angular Material Design Components ( https://material.angular.io/guide/getting-started ) OR any other WebUI Framework like
Bootstrap

o Optional Bonus : Use Ngrx to handle the state of the Angular App (https://www.youtube.com/watch?v=9P5DTlg9oLc )


▪ Comment: Only if you really have a lot of time and want to learn advanced techniques. Redux/Ngrx is an absolute overkill for a small demo project but we use it for NexOpt.

Example idea for the demo project:

Our customer is a small pizzeria and they want to manage their pizza variations (Margherita, Tonno, Provenciale...). A pizza has the following properties:

	• -  Name
	• -  Pizza Dough Type: (New York Style, Neapolitan, Sicilian)
	• -  IsCalzone: (yes or no)
	• -  List of Ingredients: (A subset of a predefined fixed list with whatever you can imagine.)
	• The customer wants to create, update and delete these variations. 

***********

Credits
-----

I give credits to STEINBAUER Engineering for giving me the opportunity to perform this project task.
