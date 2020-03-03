# CompanyRegistry

This application demonstrate the usage of MongoDB with Microsoft .NET Core C# using the MongoDB .NET Core Driver for maintaining a register of companies. It implements the ApiKey-based Authentication. This means that in order to be able to access the data you need to provide proper API key and password.
The API is then consumed by a simple client web application using the new Micosoft ASP.NET Core technology - Blazor.

### The REST API

The API is designed against the Repository Pattern. For this purpase a generic repository pattern is developed that covers the basic Create, Read, Update, Delete and Search operations.


### The client application

The client is build in Blazor and consists of 2 main pages - List and Details.

The List page displays data of all registered companies. And the Details pages allows the user to Create new company or Update the data of an existing one.

# Prerequisites and Running the Application

**Software installation prerequisites:**
* MongoDB 4.2.3 or greater
* Visual Studio 2019 Community Edition
* .NET Core 3.1 - SDK version 3.1.102
* Postman

**To get the application running, execute the following steps:**
1. Install the MongoDB Server as a Service:
   * Download The MongoDB Community Server version 4.2.3 from [MongoDB download center](https://www.mongodb.com/download-center/community).
   * Configure MongoDB as a Windows Service during the install. The MongoDB service is started upon successful installation.
   * At the end of the MongoDB server installation, when prompted, install the MongoDB officail GUI - MongoDB Compass Community.
   * Open MongoDB Compass and create new database. Name it 'CompanyRegister'.
   * Add new collection to the database and name it 'Company'.
   * Add data by importing the 'companyDB.json' file located in the _DDATA directory of the solution.
2. Download the application source code - The source code for the application can be downloaded from the download source code link at the top of this article.
3. .NET Core 3.1 - When you download and install lates Visual Studio 2019 Community Edition, .NET Core 3.1 should automatically get installed with it. If you already have Visual Studio 2019, you can verify your installation by going to the Tools menu and selecting Get Tools and Features and this will start the Visual Studio Installer. From the installer options, you can verify that .NET Core 3.1 has been installed. If you need to download the .NET Core SDK for Visual Studio 2019 - choose SDK version 3.1.102 for Windows 64 bit operating systems from the [.NET Core SDK download page](https://dotnet.microsoft.com/download/dotnet-core/3.1).
4. Download and install [Postman](https://www.postman.com/downloads/). Create the following rquests:
   * Get list of all companies - GET: https://localhost:44311/api/companies
   * Get compnay by ID - GET: https://localhost:44311/api/company/xxx (replace 'xxx' with appropriate record ID)
   * Update / Create company POST: https://localhost:44311/api/company. For updating an existing record add the following to the 'Body' section ```{ "Id": "5e5ce9e25e3f3857e85144fd", "Name": "qwe", "VAT": "asd1" }```. To insert new record use ```{ "Name": "qwer", "VAT": "qwer1" }``` part.
   * Search by name - GET: https://localhost:44311/api/company. In the 'Params' section add the following ```KEY: searchTerm | VALUE: B```. The ```VALEUE``` contains the search term.
   * For each of the requsts add the following two 'Headers': ```KEY: username | VALUE: CompanyRegistryApi``` and ```KEY: password | VALUE: $p@ssW0rd123!56asdg64s6fga61sdf```
5. Build and Run the API Project:
   * Compile the Web API project ```CompanyRegistry.API``` and set is as start project.
   * Start the application.
6. Build and Run the Client Project:
   * Compile the Blazor client project ```CompanyRegistry.WebUI```.
   * Open the solution properties and in the 'Startup Project' section select 'Multiple startup projects'. Then for the ```CompanyRegistry.API``` and ```CompanyRegistry.WebUI``` projects set the Action to 'Start'.
   * Start the application.
