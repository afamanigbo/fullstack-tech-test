# Tablet Usage Web Api

## Installation

To install and run the application please follow these steps
* Unzip file named fullstack-tech-test.zip to any location on your computer
* Open the folder TeacherTabletService
* Open the solution file TeacherTabletService.sln. This is created using Visual studio 2019

## Running the application

* Once the visual studio solution is open, ensure the target project is set to TeacherTabletService and press F5
* The default URL points to the developed api at location /api/tabletUsage
* You can also open the command prompt at location "fullstack-tech-test\TeacherTabletService" and type dotnet run
* The api should now run on URL "http://localhost:5000/api/tabletusage"

## Solution Components

The solution components include the following

### Configuration file appsettings.json

* This file contains configuration information for the application. The most important aspect is the setting "DataLocation" which denotes the location of the data file "battery.json"

### Controller TabletUsageController.cs

This is the API invocation endpoint and provides one Http GET method that does the following

* Injects the DataLoader class that is responsible for loading json files into POCO objects
* Injects the standard IConfiguration class for reading configuration files contained in appsettings.json
* Injects the standard IwebHostConfiguration class for determining an applications current hosting environment
* Injects the standard ILogger class for logging exceptions and other information
* Injects the TabletUsageService class responsible for calculating battery usage from the loaded data

#### Method LoadData()

This is responsible for loading json configuration files using the injected DataLoader class

#### Method Get()

This is responsible for 

* Returning Http status code 404 if no data is found or can be loaded
* Sorting loaded data by serial number and then timestamp
* Calculating battery usage by invoking the TabletUsageService GetTabletUsageSummary method

### Class DataLoader.cs

This handles loading of json data and does the following

* Reads the json text file
* Deserializes the content of the json text file into a List<> of the TabletUsageData class

### Class TabletUsageService.cs

This calculates the tablet battery usage within the following method

#### Method GetTabletUsageSummary()

* Loops through the list of records and keep track of the current tablet serial number
* For each new record visited in the loop, so long as the serial numbers remain the same, the percentage total usage and number of minutes total usage
* Once a new tablet serial is encountered, create a new object of type TabletUsageSummary and add the summed values accordingly

This method also handles the following edge cases

* Where only one tablet data is present in the json file
* Where multiple tablet information are present but only for one tablet serial number

## Running unit tests

Tests are developed using NUnit. The following scenarios are tested

* TestDataLoader.cs tests the data loading process ensuring that valid JSON files are correctly serialized
* TestService.cs tests the TabletusageService class to ensure the right tablet summary results are calculated for the JSON data files. These files are called "dataServiceTest1.json" and "dataServiceTest2.json"
* TestHttpResponse ensures the right Http response code is returned based on user input while running the API
* For a successful execution of the TestHttpResponse, the api needs to be running. 
* Run the API by opening the command window at location "fullstack-tech-test\TeacherTabletService" and type in dotnet run

