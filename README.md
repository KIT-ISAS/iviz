# ROS iviz suite
Utilities for working with ROS and C#. 

If you are interested in the visualization app, you can start [here](iviz).

Contains the following modules:

## [iviz](iviz)
Unity application for ROS data visualization on mobile devices.

## [iviz_roslib](iviz_roslib)
ROS client library in C#.

## [iviz_model_service](iviz_model_service)
Service to upload 3D models from a PC to a mobile device using iviz.

## iviz_msgs
A small set of pre-generated ROS messages as C# files.

## [iviz_msgs_gen](iviz_msgs_gen)
Parser that reads .msg files and generates C# files.

## iviz_msgs_gen_lib
Library to read .msg files dynamically. Used by **iviz_msgs_gen**. 

## iviz_urdf
Parser for URDF and SDF resources.

## iviz_utils
Dummy project that references all the others.
If you make a change in a library and want to use it in **iviz**, build this
and copy the DLLs from the _iviz_utils/Publish_ folder into _iviz/Assets/Dependencies_.

(Note: Do not copy _Newtonsoft.Json.dll_, it will conflict with a Unity dependency. This is fixed in 2020.1+).

## iviz_utils_tests
Unit tests for the different modules.
