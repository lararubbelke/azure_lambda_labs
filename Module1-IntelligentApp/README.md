<a name="HOLTop"></a>
# Intelligent Application #

---

<a name="Overview"></a>
## Overview ##

In this module you will build an eCommerce web site for an automotive parts supplier called "Parts Unlimited".

For the web site you will also might want to track the user interactions (such as products views, additions to the cart, etc.) for further analysis. The challenge here is to process millions of events from concurrent users connected from different devices across the globe. With **Azure Event Hubs** or **Azure IoT Hub** you can process large amounts of event data from connected devices and applications. These are managed services that ingest events with elastic scale to accommodate to  variable load profiles and the spikes caused by intermittent connectivity.

After you collect data into IoT Hubs, you can store the data using a storage cluster or transform it using a real-time analytics provider. **Azure Stream Analytics** is integrated out-of-the-box with Azure IoT Hubs to ingest millions of events per second. Stream Analytics processes ingested events in real-time, comparing multiple streams or comparing streams with historical values and models. It detects anomalies, transforms incoming data, triggers an alert when a specific error or condition appears in the stream, and displays this real-time data in your dashboard. For this scenario, you will use **Stream Analytics** to process and spool data to Blob Storage and Power BI.

<a name="Objectives"></a>
### Objectives ###
In this module, you'll:

- Create an **IoT Hub** and integrate it into a Web App
- Use **Stream Analytics** to process data in near-realtime and spool data to **Blob Storage** and **Power BI**
- Create a sample **Power BI** dashboard

<a name="Prerequisites"></a>
### Prerequisites ###

The following is required to complete this module:

- [Visual Studio Community 2015][1] or greater
- [ASP.NET Core 1.0][2]
- [Microsoft Azure Storage Explorer][3] or any other tool to manage Azure Storage
- [Service Bus Explorer][4]

[1]: https://www.visualstudio.com/products/visual-studio-community-vs
[2]: https://docs.asp.net/en/latest/getting-started/installing-on-windows.html#install-asp-net-5-with-visual-studio
[3]: http://storageexplorer.com/
[4]: https://github.com/paolosalvatori/ServiceBusExplorer


<a name="Exercises"></a>
## Exercises ##
This module includes the following exercises:

1. [Creating and integrating Event Hubs](#Exercise1)
1. [Using Stream Analytics to process your data](#Exercise3)
1. [Visualizing your data with Power BI](#Exercise4)

Estimated time to complete this module: **60 minutes**

> **Note:** When you first start Visual Studio, you must select one of the predefined settings collections. Each predefined collection is designed to match a particular development style and determines window layouts, editor behavior, IntelliSense code snippets, and dialog box options. The procedures in this module describe the actions necessary to accomplish a given task in Visual Studio when using the **General Development Settings** collection. If you choose a different settings collection for your development environment, there may be differences in the steps that you should take into account.

> ![General Development Settings](Images/vs-general-development.png?raw=true "General Development Settings")

> _General Development Settings_


<a name="Exercise1"></a>
### Exercise 1: Creating and integrating IoT Hubs ###

Azure IoT Hubs is an event processing service that provides event and telemetry ingress to the cloud at massive scale, with low latency and high reliability. This service, used with other downstream services, is particularly useful in application instrumentation, user experience or workflow processing, and Internet of Things (IoT) scenarios.
In this exercise, you will use Azure IoT Hubs to track the user behavior in your retail website when viewing a product and also when adding it to the cart.

<a name="Ex1Task1"></a>
#### Task 1 - Creating the Event Hub ####

In this task, you will create the Event Hub that will be used in the following tasks.

1. Log on to the [Azure classic portal](https://manage.windowsazure.com/), and click **NEW** at the bottom of the screen.

1. Click App Services, then **Service Bus**, then **Event Hub**, then **Quick Create**.

1. Type a name for your Event Hub, e.g. eventhubdatamodule, select your desired region, and then click **Create a new Event Hub**.

	![Creating the Event Hub](Images/creating-event-hub.png?raw=true "Creating the Event Hub")

	_Creating the Event Hub_

1. Click the namespace you just created (usually _event hub name_-ns).

1. Click the **Event Hubs** tab at the top of the page, and then click the Event Hub you just created.

1. Click the **Configure** tab at the top, add a new policy named **SendRule** with _Send_ rights, add another policy called **ReceiveRule** with _Manage, Send, Listen_ rights, and then click **Save**.

	![Creating the shared access policies](Images/creating-event-hub-policies.png?raw=true "Creating the shared access policies")

	_Creating the shared access policies_

	We created 2 Shared Access Policies and 2 Shared Access Signatures (or tokens) based off those policies. The signatures will be used to authenticate. If we had a variety of devices, we could create a different token for each device. 

1. Click the **Dashboard** tab at the top of the page, and then click **Connection Information**. Notice the tokens are part of the connection strings. Take note of the two connection strings; you'll need them later in this module.

1. Your Event Hub is now created, and you have the connection strings you need to send and receive events.


<a name="Ex1Task2"></a>
#### Task 2 - Configuring and starting event generator application ####

In this task, you'll set up and run a console application that will randomly create and send events - such as add, view, checkout and remove - to your Event Hub. Later in this module, you'll visualize these events in Power BI.

1. Open in Visual Studio the **PartsUnlimitedDataGen.sln** solution located at **Source / Ex3 / Begin** folder.

1. Replace the **eventHubName** and  **eventHubConnectionString** values in **Program.cs** with the connection string and name of the **SendRule** from your Event Hub .

1. Build the solution to trigger the download of required NuGet packages.

1. Run the application.

	![Generating events](Images/events-generator.png?raw=true "Generating events")

	_Generating events_

<a name="Ex1Task3"></a>
#### Task 3 - Verifying the website events in IoT Hubs ####

In this task, you'll verify that the events are being sent to your Event Hub.

1. In the [Azure classic portal](https://manage.windowsazure.com/), go to **Service Bus** and select the one you created before.

1. Click the **CONNECTION INFORMATION** button below and copy the **RootManageSharedAccessKey**; you'll need it to connect via the ServiceBusExplorer in the next step.

	![Getting the service bus RootManageSharedAccessKey](Images/service-bus-connection.png?raw=true "Getting the service bus RootManageSharedAccessKey")

	_Getting the service bus RootManageSharedAccessKey_

1. Run **Service Bus Explorer**.

1. Click **File** -> **Connect**, select _Enter connection string_ from the Service Bus Namespaces list and paste the **RootManageSharedAccessKey** you copied before.

	![Connect to a Service Bus Namespace](Images/service-bus-enter-connection.png?raw=true "Connect to a Service Bus Namespace")

	_Connect to a Service Bus Namespace_

1. Go to **Event Hubs** -> **eventhubdatamodule** -> **Consumer Groups** -> **$Default** -> **Partitions** -> **00** and right click to "Create Partition Listener".

	![Creating a Partition Listener](Images/creating-partition-listener.png?raw=true "Creating a Partition Listener")

	_Creating a Partition Listener_

1. Go to the **Events** tab and click **Start**.

	![Viewing the events list](Images/viewing-events-list.png?raw=true "Viewing the events list")

	_Viewing the events list_

<a name="Exercise2"></a>
### Exercise 2: Using Stream Analytics to process your data ###

Now that we have a stream of events, you'll set up a Stream Analytics job to analyze these events in real-time.
Azure Stream Analytics (ASA) is a fully managed, cost effective real-time event processing engine that helps to unlock deep insights from data. Stream Analytics makes it easy to set up real-time analytic computations on data streaming from devices, sensors, web sites, social media, applications, infrastructure systems, and more.


<a name="Ex2Task1"></a>
#### Task 1 - Creating Stream Analytics job ####

In this task, you'll set up a Stream Analytics job to analyze the events in real-time.

1. In the [Azure portal](https://portal.azure.com/), click **New** > **Data + Analytics** > **Stream Analytics job**.
Specify the following values, and then click **Create**:

	- **Job Name**: Enter a job name.
	- **Region**: Select the region where you want to run the job. Consider placing the job and the event hub in the same region to ensure better performance and to ensure that you won't be paying to transfer data between regions.
	- **Storage Account**: Choose the Azure storage account that you'd like to use to store monitoring data for all Stream Analytics jobs running within this region. You have the option to choose an existing storage account or to create a new one.

	![Creating Stream Analytics job](Images/new-stream-analytics.png?raw=true "Creating Stream Analytics job")

	_Creating Stream Analytics job_

1. The new job will be shown with a status of Created. Notice that the Start button is disabled. You must configure the job **Input**, **Output**, and **Query** before you can start the job.

<a name="Ex2Task2"></a>
#### Task 2 - Specifying job Input ####

In this task, you'll specify a job Input using the Event Hub you previously created.

1. In your Stream Analytics job topology click **Inputs**, and then click **Add**. The blade to create a new Input appears on the right.

1. Type or select the following values for each setting:

	- **Input Alias**: Enter a friendly name for this job input such as CallStream. Note that you'll be using this name in the query later.
	- **Source Type**: Select Data Stream.
	- **Source**: Event Hub.
	- **Service bus namespace**: Use the one you used for the Event Hub you created.
	- **Event Hub name**: Use the one you created in the previous task.
	- **Event Hub policy name**: The receive policy name you set in the previous task when creating the Event Hub.
	- **Event hub policy key**: The key you copied in the previous task when creating the Event Hub.
	- **Event Serializer Format**: JSON.
	- **Encoding**: UTF8.

	![Creating Stream Analytics Input](Images/new-stream-analytics-input.png?raw=true "Creating Stream Analytics Input")

	_Creating Stream Analytics Input_

1. Click **Create**.

<a name="Ex2Task3"></a>
#### Task 3 - Specifying job Query ####

Stream Analytics supports a simple, declarative query model for describing transformations for real-time processing. To learn more about the language, see the [Azure Stream Analytics Query Language Reference](https://msdn.microsoft.com/library/dn834998.aspx).
In this task, you'll create a query that extracts events from your input stream.

1. Click **Query** from the main Stream Analytics job page.

1. Add the following to the code editor:

	````
	SELECT
	    *
	INTO
	    analyticsoutput
	FROM
	    CallStream
	````

1. Click **Save**.

	>**Note:** In this query you're projecting all fields in the payload of the event to the output, you could read some of them of them by using _SELECT [field name]_.

<a name="Ex2Task4"></a>
#### Task 4 - Specifying job Output ####

In this task, you'll create an output that will store the query results in Blob storage.

1. Use an existing storage account or create a new storage account by following these steps: [Create a storage account](https://azure.microsoft.com/documentation/articles/storage-create-storage-account/#create-a-storage-account).

1. Create a Container such as eventhubanalytics and set its access to Blob.

	1. Open the **Azure Storage Explorer** or the tool of your preference and configure a new storage account using the account name and key from the previous step. In _Azure Storage Explorer_, right-click on **Storage Accounts**, select **Attach External Storage...** and enter the account name and key in the dialog, then click **OK**.

	1. Create a new Blob Container with the name "**eventhubanalytics**" and "Container" access level. In _Azure Storage Explorer_ expand your account and right-click on **Blob Containers**, select **Create Blob Container** and enter "eventhubanalytics". Press enter to create the container. Then right-click on the new container and select **Set Public Access Level..** and choose **Public read access for blobs**.

1. Now, in your Stream Analytics job, click **Outputs** from the main page, and then click **Add**. The options blade requires the following information:

	- **Output alias**: Use the name you set in the previous task, e.g. analyticsoutput
	- **Sink**: Blob storage.
	- **Storage account**: Select the name of the storage account.
	- **Storage account key**: Set the account key.
	- **Container**: Select the name of the container.
	- **Path pattern**: Type in a file prefix to use when writing blob output. E.g. analyticsoutput-{date}
	- **Event Serializer Format**: JSON.
	- **Encoding**: UTF8.

1. Click **Create**.

	![Creating Stream Analytics Output](Images/new-stream-analytics-output.png?raw=true "Creating Stream Analytics Output")

	_Creating Stream Analytics Output_

<a name="Ex2Task5"></a>
#### Task 5 - Starting the job for real time processing ####

In this task you'll run the Stream Analytics job and view the output in Visual Studio.

1. From the job **Dashboard**, click the **Start** button.

1. In the **Start job** blade, select Job output start time **Now**, and then click **Start** at the bottom of the blade. The job status will change to Starting; it can take several minutes to start.

1. Open the blob storage using **Azure Storage Explorer**.

1. Navigate to the container you set in the previous task.

	![Browsing container](Images/reviewing-analytics-job.png?raw=true "Browsing container")

	_Browsing container_

1. Open the csv file to see the output.

	![Reviewing Stream Analytics job output](Images/reviewing-analytics-job-output.png?raw=true "Reviewing Analytics job output")

	_Reviewing the Stream Analytics job output_

<a name="Exercise3"></a>
### Exercise 3: Visualizing your data with Power BI ###

In this exercise, you'll use Azure Stream Analytics with Microsoft Power BI. You will learn how to build a live dashboard quickly.

<a name="Ex3Task1"></a>
#### Task 1 - Adding Power BI output to Stream Analytics ####

In this task, you'll add a new output to your Stream Analytics job.

1. From the [Azure classic portal](https://manage.windowsazure.com/), go to Stream Analytics and click the one you created.

1. Click the **STOP** button below. We need to stop it in order to add a new output.

1. Click **OUTPUTS** from the top of the page, and then click **Add Output**.

1. In the **Add an Output** dialog box, select **Power BI** and then click the right button.

1. In the **Add a Microsoft Power BI output**, supply a work or school account for the Stream Analytics job output. If you already have Power BI account, select **Authorize Now**. If not, choose **Sign up now**.

	![Adding Power BI output](Images/adding-powerbi-output.png?raw=true "Adding Power BI output")

	_Adding Power BI output_

1. Next, provide the values for:

	- **Output Alias** – You can put any output alias that is easy for you to refer to. This output alias is particularly helpful if you decide to have multiple outputs for your job. In that case, you have to refer to this output in your query. For example, let’s use the output alias value “OutPowerBI”.
	- **Dataset Name** - Provide a dataset name that you want your Power BI output to have. For example, let’s use “datamodulepbi”.
	- **Table Name** - Provide a table name under the dataset of your Power BI output. Let’s say we call it “datamodulepbi”. Currently, Power BI output from Stream Analytics jobs may only have one table in a dataset.
	- **Workspace** - You can use the default, My Workspace.

1. Click **OK, Test Connection** and now your Power BI connection is completed.

1. Lastly, you should update your **Query** to use this output and start the job.

	````
	WITH AllEvents AS (
	SELECT
	    productId, title, category, type, Count(title) AS [total]
	FROM
	    CallStream
	GROUP BY
	    productId, title, category, type, TumblingWindow(minute, 10)
	)
	SELECT * INTO OutPowerBI FROM AllEvents
	SELECT * INTO analyticsoutput FROM CallStream
	````

	>**Note:** As we are grouping the results, a window type is required. See [GROUP BY](https://msdn.microsoft.com/library/azure/dn835023.aspx). The query uses a 10-minute tumbling window. The INTO clause tells Stream Analytics which of the outputs to write the data from this statement. The WITH statement is to reuse the results for different statements; in this case we could used it for both outputs but we'll keep storing all fields into the blob.

<a name="Ex3Task2"></a>
#### Task 2 - Creating the dashboard in Power BI ####

1. Go to [PowerBI.com](https://powerbi.microsoft.com/) and login with your work or school account. If the Stream Analytics job query outputs results, you'll see your dataset is already created:

1. For creating the dashboard, go to the **Dashboards** option and create a new Dashboard, e.g. My Dashboard.

1. Now click the dataset created by your Stream Analytics job (“datamodulepbi” in our current example). You will be taken to a page to create a chart on top of this dataset.

	![Power BI Workspace](Images/powerbi-workspace.png?raw=true "Power BI Workspace")

	_Power BI Workspace_

1. Select the **Table visualization** icon from the **Visualizations** menu on the right, then check all fields but productId from the **Fields** list.

1. Within the **Filters** section, click **category** Advanced filtering and select the option to show items when the value _is not blank_.

1. Apply the filter and click **Save** on the top right. You can name it "Events report".

1. You will see the new report within the **Reports** section, click on it, select **Pin Live Page** to your existing dashboard.

1. Go to your dashboard, click on the **ellipsis** button at the top-right corner of the tile and click the **pen** button to edit the _Tile details_, select the **Display last refresh time** functionality and apply the changes.

	![Power BI new report](Images/powerbi-dashboard.png?raw=true "Power BI new report")

	_Power BI new report_

1. Go back to the "datamodulepbi" dataset, click the **Funnel** icon from the **Visualization** menu and check _type_ and _total_ from the **Fields** list.

1. Click **Save** on the top right, enter a name like Events Summary and save it.

1. Lets add it to your dashboard by clicking **Pin Live Page** on the top right, select your dashboard and hit **Pin Live**.

	![Power BI Dashboard](Images/powerbi-my-dashboard.png?raw=true "Power BI Dashboard")

	_Power BI Dashboard_

---

<a name="Summary"></a>
## Summary ##

By completing this module, you should have:

- Created an **Event Hub** and integrated it into a Web App
- Walked through **DocumentDB** integration
- Used **Stream Analytics** to process data in near-realtime and spool data to **Blob Storage** and **Power BI**
- Created sample **Power BI** charts & graphs
