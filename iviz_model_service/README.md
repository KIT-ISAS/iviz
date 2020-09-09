# iviz_model_service

iviz_model_service is a program that starts a ROS service to upload 3D models from a PC to a mobile device using iviz.
This program should be run on the same PC that has the assets of interest.

## Getting Started

You can either:
* Open the .csproj file in your favorite C# editor, or
* Open a console, go to this folder, and run the following command:
```bash
dotnet run -c Release
```
Make sure that ROS_MASTER_URI and ROS_PACKAGE_PATH are both set.
