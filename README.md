# ROS iviz suite
Utilities for working with ROS and C#. Contains the following modules:

## [iviz](iviz)
Unity application for ROS data visualization on mobile devices.

## [iviz_bridge](iviz_bridge)
Partial implementation of the [rosbridge protocol](https://github.com/RobotWebTools/rosbridge_suite/blob/groovy-devel/ROSBRIDGE_PROTOCOL.md) in C#. Does not support services (yet).

## [iviz_model_service](iviz_model_service)
Service to upload 3D models from a PC to a mobile device using iviz.

## iviz_msgs
A small set of pre-generated ROS messages as C# files.

## iviz_msgs_gen
Parser that reads .msg files and generates C# files.

## iviz_urdf
Parser for URDF resources.

## iviz_roslib
ROS client library.
