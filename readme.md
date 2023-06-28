# Frontend push

A repository to show how pushing data the frontend works by using server side events.

Tech stack:

* nextjs
* duende identity server <https://docs.duendesoftware.com/>
* aspnetcore
* signalr

## Folder Structure

* web: contains the web frontend build with nextjs and react
* backend: contains the apis build with apsnetcore
* identity: contains the identity server build with aspnetcore and duende identity server

## Quick start

* Ensure running postgres on localhost:5432, user: postgres, password: docker
* Run identity server migrations
  * `cd identity/`
  * `dotnet tool restore`
  * `cd src/IdentityServer`
  * `dotnet ef database update`
* Start Identity Server
* Start Api1
* Start Web frontend

## Hints to some code snippets

* [Backend - Initialization - Line 17 & Line 39-42 & Line 54](./backend/Backend.Api1/HostingExtensions.cs)
* [Backend - Hub](./backend/Backend.Api1/NotificationHub.cs)
* [Frontend - Added package line12](./web/package.json)
* [Frontend - Helper class](./web/src/pages/signalr/connection-signalR.ts)
* [Frontend - Connection setup](./web/src/pages/signalr/index.tsx)
