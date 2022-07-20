
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
## Test Data
## Note: Please change id otherwise error will come
{
    "id": "e3d7d919-05267-23r-9f188-17dda09f900w22c",
    "branchId": "e819aaaa-2577-4d55-bc17-c1e1c2e2114d",
    "queueId": "830e1e00-7362-43a9-9d17-e6190f2c63055",
    "serviceId": "58668ef9-e4d0-4940-b802-4b18ad1864ba",
    "pk": "e819aaaa-2577-4d55-bc17-c1e1c2e2114d",
    "type": "queue",
    "requestTimeUTC": "2021-07-09T13:52:09.565Z",
    "sortPosition": 1,
    "ticketNumber": "10100",
    "preServiceQuestions": [
        {
            "questionId": "359a7619-dd9c-442a-83a7-795fc809d80c",
            "questionText": "Please enter your email",
            "answer": "mailto:rob@mailinator.com",
            "questionType": "EMAIL"
        },
        {
            "questionId": "3598877b-8be5-4ba1-b5b3-b69970bb35d6",
            "questionText": "Please enter your name",
            "answer": "Rob",
            "questionType": "TEXT"
        }
    ],
    "serviceQuestions": [],
    "requestTimeUTCString": "Fri, 09 Jul 2021 13:52:09 GMT",
    "companyId": "94acc0bd-6b1e-451f-9e59-67b493d97916",
    "requestSourceId": "",
    "requestSourceType": "MOBILE",
    "selectedLanguageId": "en",
    "mobileInterfaceId": "775ad93e-c1e3-4526-ae59-25495e8e4eef",
    "customerState": "WAITING",
    "groups": [],
    "_rid": "VkJiAIqpZSosAAAAAAAAAA==",
    "_self": "dbs/VkJiAA==/colls/VkJiAIqpZSo=/docs/VkJiAIqpZSosAAAAAAAAAA==/",
    "_etag": "\"f105d10c-0000-0700-0000-61b2578b0000\"",
    "_attachments": "attachments/",
    "estimatedServingTimeUTCString": "Tue, 26 Oct 2021 10:59:17 GMT",
    "actionHistory": [
        {
            "actionHistoryId": "8b06257b-d8fe-4907-b6f7-e2795ea7c62f",
            "action": "REQUEUED",
            "actionDateTimeUTC": "Tue, 26 Oct 2021 09:49:18 GMT",
            "triggerType": 3,
            "agentId": "507628be-4426-4014-b7e9-6f9a8a540f28"
        }
    ],
        "workflow":
        {
            "workFlowId": "82d7cc69-0035-4dc3-b3a0-88d47b24310f"
        },
    "_ts": 1639077771
}


***
To execute - either start in the debugger, or in the terminal window

    func host start

