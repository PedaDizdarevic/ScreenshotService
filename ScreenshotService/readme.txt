#########################################################################
#	Title: ScreenshotService                                            #
#                                                                       #
#	Author: Peda Dizdarevic							                    #	
#########################################################################

Table of contents
_________________________________________________________________________

1. Installation and environment
2. Database configurations and automatic deployment
3. Application interface
_________________________________________________________________________

1. Installation and environment

The application is written in C# and utalizes the ASP .Net Core framework. An SDK supporting
.Net Core 3.0 is required to compile the source code. The .bat file BuildAndRun.bat in the
ScreenshotService project folder can be used to build and start the application.

The Google Chrome web browser is required to be installed on the machine for the application 
to function properly as it is used in a headless mode with Selenium for taking screenshots.

2. Database configuration and automatic deployment

The application relies upon a SQL Server database for persisting data. A connection 
string for this database needs to be provided in the appsettings.json file. The tables 
in the database will then automatically be created upon application startup using the  
DbUp open source script runner. The database itself is not created by the script but must 
already exist on the server and be named ScreenshotService. 

For review, testing and development purposes, if it is not convenient to create a database 
to connect to, the application can also be configured to utalize an in-memory database. This 
is also done in the appesettings.json file with the parameter "UsingInMemoryDatabase". Note that
data is not persisted beyond the applications run time in this case, this option only exists 
for facilitating quick tests of functionality other then persistance.

3. Application interface

The application exposes a REST API with endpoints for taking new screenshots and querrying 
previously taken ones. The application also provides a Swagger page as a convenient way of 
exploring and accessing these endpoints in a GUI. This page can be found at /swagger/index.html. 