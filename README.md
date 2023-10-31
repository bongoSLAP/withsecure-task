# CoinSight: WithSecure Tech Test

CoinSight is a .NET + Angular SPA that consumes the CoinGecko API to provide analysis on cryptocurrencies.

# Setup
There should be no setup needed. Simply run the .NET application in Rider or VS and it should run npm install (if running for the first time) and serve the frontend automatically.
In the case that this does not happen:
1. Navigate to **withsecure-task/WithSecure/WithSecure/Web** and run **npm install**
2. While still in the web folder, run **ng serve**

# Architecture

The backend has been separated into 4 projects:
- InfoScrape - Main application project, containing controllers, program.cs and appsettings.
- InfoScrape.Core - Domain layer project, containing models, enums and interfaces.
- InfoScrape.Handlers - Application layer project, contains handlers (business logic).
- InfoScrape.Services - Infrastructure layer project, contains http client for the CoinGecko API.

# Rate limiting

Because I am using CoinGecko for free, the API is very sensitively rate limited. If you spam requests even slightly in a short space of time (especially the 'Trend Reccomendations' functionality), you will be rate limited and you must wait a few minutes to continue.
