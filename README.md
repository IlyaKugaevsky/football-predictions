# Football predictions 
Web application for organizing score prediction games and handling football statistics. There is a newer version of this application with API-based backend and modern JS framework on the client-side, see [football-predictions-v2](https://github.com/IlyaKugaevsky/football-predictions-v2).

## Tech stack
- ASP.NET MVC
- Microsoft SQL Server
- Entity Framework 
- Automapper
- Castle Windsor
- Bootstrap
- jQuery
- xUnit
- Moq

## Architecture overview
### Layers 
- Persistence (DbContext, data fetching strategies) 
- Core (models)
- Service (domain services)
- Web (composition root, controllers, viewmodels, views)
### Global patterns and design decisions
- MVC approach
- Dependency injection via Castle Windsor
- Anemic domain models with attributes to configure ORM "in-place"
- Most business logic in service layer, one service per domain model
- No repository pattern, direct calls to DbContext from service layer
- No ViewBag or ViewData, always create a ViewModel
- Client-side code reuse via partial views
### Testing
- Using xUnit and Moq frameworks
- Unit tests for each layer (in progress)
- Testing database operations by mocking DbContext
