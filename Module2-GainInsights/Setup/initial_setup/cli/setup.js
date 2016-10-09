var storage_account_sub_name = hdistr   //TODO: Ensure that this variable is populated the same way the JSON variable is. Same with the prefix variable
var prefix = 10     //TODO: Remove this hard-coding
var storage_account_name = storage_account_sub_name + prefix
var catalog_container_name=processeddata
var raw_logs_container = partsunlimited
var blob_name=blob_name

azure login

//TODO: Create Resource Groups

conn_string = azure storage account connectionstring show storage_account_name      //TODO: Not sure if you can run commands like this and copy the output. Please check


export AZURE_STORAGE_CONNECTION_STRING= conn_sring




// CREATE HDI Cluster

// Create SQL DW

// Pause SQL DW

// Upload Product Catalog file
echo "Creating the container..."
azure storage container create catalog_container_name

//Generate & Upload logs

