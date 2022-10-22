![An alpaca picture](.extras/readme_banner.png)

# Cusco

![GitHub last commit](https://img.shields.io/github/last-commit/Supraxel/Cusco?style=flat-square)
![CI status](https://img.shields.io/circleci/build/github/Supraxel/Cusco/main?style=flat-square)
![Platforms](https://img.shields.io/badge/platforms-.NET%207%20|%20Unity%202022-lightgrey?style=flat-square)
![License](https://img.shields.io/badge/license-MIT-lightgrey?style=flat-square)

Cusco is a set of game agnostic C# modules. Initially developed for _Project Lima_, we decided to open-source them
so they can benefit to both the community and _Project Lima_ itself.

They target either .NET 7 or .NET Standard 2.1 (Unity 2022+ compatible).

## Getting started

You can find the Cusco modules on [NuGet](https://www.nuget.org/packages?q=Cusco).

Here's a non-exhaustive list of the various modules available in Cusco
- Cusco.Dispatch: A multi-threading and event-based I/O framework inspired by [Grand Central Dispatch](https://apple.github.io/swift-corelibs-libdispatch/)
- Cusco.Pathfinding: A _Bring-your-own-graph_ implementation of pathfinding algorithms
- Cusco.ReactiveX: A [ReactiveX](https://reactivex.io/) implementation, built for Cusco.Dispatch

Discover all the packages in the [libs](https://github.com/Supraxel/Cusco/tree/main/libs) folder.

## Project structure

This project uses [NX](https://nx.dev/) with the [NX Dotnet](https://www.nx-dotnet.com/) plugin.

If you're not familiar with NX or the [Monorepo](https://monorepo.tools/) philosophy, it might be a good idea to check
their [documentation](https://nx.dev/getting-started/intro) first 🙂

## Where to start ?

Run the following commands to install the development dependencies and show the documentation.

```
npm install
npm serve docs
```

---

Banner photo by <a href="https://unsplash.com/@andycusco">Andy Salazar</a>
