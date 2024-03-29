# TODO

---

## Break off `Teams` responsibilities from `StateController`

The `StateController` and its associated classes are bloated and are doing too much.

- [x] Create `TeamsService` class
- [x] move all calls to `Team` data into `TeamsService`
- [x] Create `TeamsController`
- [x] move all calls to `TeamsService` into `TeamsController`

---

## Consider not storing `Teams` in state

Instead, make calls to the db for all `Team` queries.

- [X] Add a check for teams.any() querying by season to replace the checks to state.SeasonIsLoaded().
- [X] Replace calls to state for `Teams` list with calls to db using `_state.season` as a parameter.
- [X] Remove depricated methods

---

## Move all state to the database

All persistant state needs to be held in the database. In order to preserve client state, and in leiu of creating a login system at this stage, a default user (id:1) should be created in order to persist state during refresh and accross browser sessions.

- [x] Create a userState db tabel and assosiated schema
- [x] Create userState db access repo and data service
- [x] Create userSate API controller
- [x] Reroute front end calls to *always* get data currently in the cache from the API
- [x] Remove the Cache and rename/remove all classes/methods referencing the current client cache system

---

## Make Table classes into Fluent Builder patterned objects

The Repo classes use a fluent builder pattern.

It seems appropriate to use this type of pattern in the Table classes.

Currently, their methods are exposed to the entire back end code base. as `internal static`

- [x] **Create a constructor declaring required objects**
- the db connection, etc.
- the class should no longer be `static`

- [x] **Make private fields to contain member data**
- use methods returning `this` to populate data

- [x] **Make `Get` methods that return the required types**
- methods executing queries return the propper type

---

## Change file structure of `Client` project

Project should be arranged by areas so that components that work together are found together.

- [x] Create an "Areas" directory
- [x] Create directories for "Home" and "Teams"
- [x] Move all apropriate files into "Areas/Home" and "Areas/Teams"
- [x] Remove the depricated file structure
  
 ---

## Add logging

The application should implement a logging client. This needs to be initiated before the code base grows much further

- [x] Add a logging service to the Client project
- [x] Add a logging service to the Server project
- [x] Add calls to the logger throughout the projects

---

## Create query objects

API calls using multiple parameters, particulary multiple parameters of the same `Type` should be packaged into some sort of query object

- [x] Create query objects in Shared project
- [x] Modify Controllers (Server) to require query objects instead of Tuple, int[], etc. for multiple parameters
- [x] Modify DataAccess classes (Client) to use query objects

---

## Remove `BlazoredModal`?

The `BlazoredModal` library might not be necessary if the `<modal>` html tag works

- [ ] Test using the `<modal/>` html element to create a modal in a test project
- [ ] If project is successful, implement this solution for the `SelectSeason` modal
- [ ] Remove all references to `BlazoredModal` in code
- [ ] Remove the `BlazoredModal` library reference

---

## Add Data Entry Validation

All data entry forms need to have validation checks and helpful error messages

- [ ] Season Select form needs validation
- [ ] Season Select form needs error message system

---

## Change Http handling

Passing the `HttpClient` object around the client project is an anti-pattern

- [ ] Rewatch [Nick's Post](https://www.youtube.com/watch?v=Z6Y2adsMnAA)
- [ ] Create an HttpClientFactory class
- [ ] Refactor all front end code to get `HttpClient` form `HttpClientFactrory`

---

## Add Scoped css

The current `.css` file is getting too big. Style settings should be put into separate scoped files for each element

- [ ] Uncomment line in `.../wwwroot/index.html` to allow for scoped `css`
- [ ] Move all style settings into new `.css` files created for each page/component

---

## Validate team schedule when all weeks are complete

When `_unscheduledGames == 0`, the team's schedule should be validated

- [x] Check 1 home and 1 away game for each division opponent
- [x] Check >= 8 Home games
- [x] Check >= 8 Away games
- [x] Check == 1 Bye Game
- [ ] Check <= 6 games vs opposite conf
- [ ] check >= 4 games vs opposite conf
- [ ] check <= 13 games vs own conference
- [ ] check >= 11 games vs own conference

---
