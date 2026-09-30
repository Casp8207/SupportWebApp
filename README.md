# SupportWebApp

## Formål

Formålet med dette projekt er at lave en simpel webapp, hvor man kan oprette og se supporthenvendelser.

Webappen er lavet med **Blazor** og bruger **Azure Cosmos DB** til at gemme supporthenvendelserne. En henvendelse indeholder blandt andet navn, kategori, besked og dato/tidspunkt.

Projektet er lavet som en del af Cloud Computing, hvor fokus er på at få en webapp til at kommunikere med en cloud-database.

## Teknologier

Vi har brugt:

* C#
* .NET
* Blazor Web App
* Azure Cosmos DB
* Microsoft.Azure.Cosmos
* Git og GitHub

## Cosmos DB

Vores Cosmos DB er bygget op med følgende:

* **Database:** `IBasSupportDB`
* **Container:** `ibassupport`
* **Partition key:** `/category`

### Opret en ny Cosmos DB

Hvis databasen skal oprettes fra bunden, kan det gøres med Azure CLI.

Først sættes nogle variabler:

```powershell
$env:RESGRP="IBasSupportRG"
$env:DBACCOUNT="ibas-db-account-XXXX"
$env:DATABASE="IBasSupportDB"
$env:CONTAINER="ibassupport"
$env:LOCATION="germanywestcentral"
```

Opret Resource Group:

```powershell
az group create --name $env:RESGRP --location $env:LOCATION
```

Opret Cosmos DB-account:

```powershell
az cosmosdb create `
  --name $env:DBACCOUNT `
  --resource-group $env:RESGRP `
  --locations regionName=$env:LOCATION `
  --enable-free-tier true
```

Opret databasen:

```powershell
az cosmosdb sql database create `
  --account-name $env:DBACCOUNT `
  --resource-group $env:RESGRP `
  --name $env:DATABASE
```

Opret containeren med `/category` som partition key:

```powershell
az cosmosdb sql container create `
  --account-name $env:DBACCOUNT `
  --resource-group $env:RESGRP `
  --database-name $env:DATABASE `
  --name $env:CONTAINER `
  --partition-key-path "/category"
```

Connection stringen kan hentes med:

```powershell
az cosmosdb keys list `
  --name $env:DBACCOUNT `
  --resource-group $env:RESGRP `
  --type connection-strings
```

Connection stringen skal ikke lægges direkte i projektet eller på GitHub. Vi bruger i stedet **User Secrets** til at gemme den:

```powershell
dotnet user-secrets set "ConnectionString:CosmosDB" "DIN_CONNECTION_STRING"
```

## Status

### Det har vi nået

Vi har fået lavet:

* Oprettet Blazor WebApp-projektet.
* Oprettet en model til supporthenvendelser.
* Oprettet forbindelse til Azure Cosmos DB.
* Oprettet Cosmos DB-service.
* Oprettet en side hvor man kan oprette supporthenvendelser.
* Oprettet en side hvor man kan se supporthenvendelser.
* Fået data gemt i Cosmos DB.
* Fået data hentet fra Cosmos DB.
* Tilføjet navigation mellem siderne.
* Brugt User Secrets til connection string.
* Oprettet GitHub repository og pushed projektet.

Vi har også testet, at man kan oprette en supporthenvendelse og se den igen fra Cosmos DB.

### Det mangler

Der er stadig nogle ting, som kunne gøres bedre:

* Bedre validering af input.
* Mulighed for at redigere eller slette henvendelser.
* Mulighed for at søge og filtrere i henvendelser.
* Bedre fejlhåndtering.
* Et mere gennemarbejdet design.

### Næste trin

Det næste trin vil være at teste løsningen lidt mere grundigt og sikre, at alle funktionerne virker som de skal.

Derefter kunne vi arbejde videre med blandt andet bedre validering, søgning og filtrering af supporthenvendelser. På længere sigt kunne webappen også deployes til Azure, så den ikke kun kører lokalt.