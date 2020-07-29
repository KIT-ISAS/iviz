# Welcome to iviz!

iviz is a mobile 3D visualization app for ROS based on the Unity Engine.
You can use it to display 3D information about topics, navigate your virtual environment, or watch your robot in Augmented Reality.
iviz has been designed primarily for use in mobile devices (iOS, Android smartphones and tablets), but can also be used in a normal PC if you don't mind the big buttons.


## 1. Installation

To run iviz, you need the following
* Unity 2019.4f LTS editor on either Windows, Linux, or macOS
* An iOS or Android device (optional)

The iviz project has no external dependencies (all required libraries are included), so installing it is just a matter of cloning the repository and buiding it for your plaform.

## 2. Getting Started

* On the panel at the top-left, right underneath the "- iviz -" label, tap on the URL with the arrow at the end.
* You should now see the Connection Dialog.
  - Type in the URL of the master, i.e., where roscore is running. This is the content of the environment variable _ROS_MASTER_URI_.
  - Type in the URL of your device, or leave the default. The hostname should be accessible to other devices, and can be obtained from _ROS_HOSTNAME_ or _ROS_IP_.
  The port (7613) can be set to anything, just make sure it's not being used by another application.
  - Type in your id, or leave the default. This is the name of your ROS node. It can be anything, but make sure it is unique in your network.
* Tap on the _Connect_ button. The application will now try to connect to the ROS master, and keep retrying if it does not work. Tap _Stop_ to cancel the operation.

The app may refuse to connect if it detects that your URL is not accessible from the outside (in doubt, use the IP address of your device as the hostname). 

## 3. The Dialogs

### Getting Started
