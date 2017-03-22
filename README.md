# Microsoft Azure - Storage Queue Contents

## Overview
A PowerShell module written in C# to help manage the contents of an Azure Storage Queue.

## Installation
Copy the azureQueueStorage folder into a PowerShell module path.

## Commands
* Add-AzureStorageQueueContent
* Get-AzureStorageQueueContent
* Clear-AzureStorageQueueContent

## Commands
* Add-AzureStorageQueueContent
* Get-AzureStorageQueueContent
* Clear-AzureStorageQueueContent


## Examples
* Add-AzureStorageQueueContent
```PowerShell
Add-AzureStorageQueueContent -storageAccountName <acountname> -storageAccountKey <accountKey> -name queueName -message 'Queue Message'

Get-ChildItem | Add-AzureStorageQueueContent -storageAccountName <acountname> -storageAccountKey <accountKey> -name queueName
```

* Get-AzureStorageQueueContent
```PowerShell
Get-AzureStorageQueueContent -storageAccountName <acountname> -storageAccountKey <accountKey> -name queueName -verbose

Get-AzureStorageQueueContent -storageAccountName <acountname> -storageAccountKey <accountKey> -name queueName -peek -verbose
```

* Clear-AzureStorageQueueContent
```PowerShell
Clear-AzureStorageQueueContent -storageAccountName <acountname> -storageAccountKey <accountKey> -name queueName -verbose

Clear-AzureStorageQueueContent -storageAccountName <acountname> -storageAccountKey <accountKey> -name queueName -verbose -whatif
```

## Contributors
- Ben Taylor - ben@bentaylor.work