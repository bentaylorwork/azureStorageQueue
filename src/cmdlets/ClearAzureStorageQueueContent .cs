namespace PowerShell.Storage.Queue
{
    using System;
    using System.Management.Automation;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Queue;

    [Cmdlet(VerbsCommon.Clear, "AzureStorageQueueContent", SupportsShouldProcess = true)]
    public class ClearAzureStorageQueueContent : Cmdlet
    {
        [Parameter(Mandatory = true,
                   Position = 0,
                   HelpMessage = "The name of the storage account."
        )]
        public string storageaccountName { get; set; }
        [Parameter(Mandatory = true,
                   Position = 1,
                   HelpMessage = "The account key for the storage account."
        )]
        public string storageaccountkey { get; set; }
        [Parameter(Mandatory = true,
                   Position = 2,
                   HelpMessage = "The name of the storage queue."
        )]
        public string name { get; set; }

        private CloudQueue queue;

        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            WriteVerbose("Creating Connection String");
            string connectionString = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", storageaccountName, storageaccountkey);

            try
            {
                WriteVerbose("Connecting to the Azure Queue Service.");
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
                CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
                queue = queueClient.GetQueueReference(name);
            }
            catch
            {
                ParameterBindingException error = new ParameterBindingException("There was a problem connecting to the Azure Storage Queue service.");
                ErrorRecord errorRecord = new ErrorRecord(error, null, ErrorCategory.ResourceUnavailable, '1');
                ThrowTerminatingError(errorRecord);
            }

            if (!queue.Exists())
            {
                ParameterBindingException error = new ParameterBindingException("The storage queue does not exist.");
                ErrorRecord errorRecord = new ErrorRecord(error, null, ErrorCategory.ResourceUnavailable, '1');
                ThrowTerminatingError(errorRecord);
            }
        }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            try
            {
                if (ShouldProcess(string.Format("{0} ({1})", "Clearing Queue", name)))
                {
                    WriteVerbose("Clearing messages from Azure Storage Queue.");
                    queue.Clear();
                }
            }
            catch (InvalidOperationException ex)
            {
                ErrorRecord errorRecord = new ErrorRecord(ex, null, ErrorCategory.ResourceUnavailable, '1');
                ThrowTerminatingError(errorRecord);
            }
        }
    }
}
