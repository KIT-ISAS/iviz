# iviz_model_service

iviz_model_service is a program that starts a ROS service that provides 3D models from a PC to a mobile device using iviz.
This program should be run on the PC that has a ROS installation and has the assets of interest.

## Getting Started

You can either:
* Open the .csproj file in your favorite C# editor, and hit Run.
* Open a console, go to the folder containing this README, and run the following command:
```bash
dotnet run -c Release
```
* Run the dll directly
```bash
dotnet Publish/Iviz.ModelService.dll
```
Make sure that ROS_MASTER_URI and ROS_PACKAGE_PATH are both set.

By default, this will provide access to models that start with the URI schema 'package://'.
If you want to enable URIs that start with 'file://', use the _--enable-file-schema_ argument.
Note that this will give all the ROS network access to every file. 
