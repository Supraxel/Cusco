---
sidebar_position: 2
---

# Contribute

## Set up your development environment

Before contributing to Cusco, you need to setup your development environment.

- Install [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
- Install [Node.js](https://nodejs.org/en/) then run `npm install`

## Serve documentation locally

The documentation is built with [Docusaurus](https://docusaurus.io/).  
You can find the documentation source in `/apps/docs` and start a local docs server with:

```sh
nx serve docs
```

## Create a new library project

- Run `nx g @nx-dotnet/core:lib ProjectName` to generate a new library.
  - The NX project will be named in a kebab case fashion (i.e. `project-name`). Subsequent NX commands will have to refer to project name in this casing.
  - The `*.csproj` file will be named `Cusco.ProjectName.csproj`.
- Run `dotnet sln Cusco.sln add libs/project-name/Cusco.ProjectName.csproj` to add the new project to the solution file.

## Build

Run `nx build project-name` to build the project. The build artifacts will be stored in the `dist/` directory. Use the `--prod` flag for a `Release` build.

## Running unit tests

If a test project exists for the project  `nx test project-name-test` to execute the unit tests.

Run `nx affected:test` to execute the unit tests affected by a change.

## Dependency graph

Run `nx graph` to see a diagram of the dependencies of your projects.
