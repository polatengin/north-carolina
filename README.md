# North Carolina Project

## Prerequisites

- Dotnet 8
- Azure Subscription

## Setup

Create a new project

```bash
dotnet new console
```

Add the following NuGet packages

```bash
dotnet add package Azure.Storage.Blobs
```

Create a new Azure Resource Group

```bash
az group create --name rg-north-carolina --location westus
```

Create a new Azure Storage Account

```bash
az storage account create --name northcarolinastorage --resource-group rg-north-carolina --location westus --sku Standard_LRS
```

Get the Azure Storage Account Connection String

```bash
az storage account show-connection-string --name northcarolinastorage --resource-group rg-north-carolina --query connectionString --output tsv
```

Run the project

```bash
dotnet run
```
