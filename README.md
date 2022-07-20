
# Welcome to QTrac_Azure_Functions



***
## To run

> Needs a local.settings.json

{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "AzurewebJobsDashboard": "UseDevelopmentStorage=true",
    "COSMOS_ENDPOINT": "https://cosmos-qtrac-global-dev.documents.azure.com:443/",
    "COSMOS_KEY": "4wvXSRXUOUG37UeEIZ24KTmuXBM4JTj54ZjvRB9SzqTlpc5Ybq7S4OY0FU1w1XwZIa5JzivA1flbMDxdtiVB1w==",
    "COSMOS_DATABASE": "qtrac",
    "AzureSignalRConnectionString": "Endpoint=https://sigr-qtrac-westus-dev.service.signalr.net;AccessKey=auCQAoBLhG4LXKpAcCeR9X2ojier08MxpSXxBGpq0d4=;Version=1.0;",
    "COSMOS_CONNECTION_STRING": "AccountEndpoint=https://cosmos-qtrac-global-dev.documents.azure.com:443/;AccountKey=4wvXSRXUOUG37UeEIZ24KTmuXBM4JTj54ZjvRB9SzqTlpc5Ybq7S4OY0FU1w1XwZIa5JzivA1flbMDxdtiVB1w==;",
    "COSMOS_WAITING_CUSTOMERS_QUEUES_CONTAINER": "waiting-customers-queues",
    "COSMOS_COMPANY_CONFIGURATIONS_CONTAINER": "company-configurations",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet"
  }
}


***
To execute - either start in the debugger, or in the terminal window

    func host start

