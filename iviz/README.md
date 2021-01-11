# Welcome to iviz!

**iviz** is a mobile 3D visualization app for ROS based on the Unity Engine.
You can use it to display 3D information about topics, navigate your virtual environment, or watch your robot in Augmented Reality.

iviz has been designed primarily for use in **mobile devices** (iOS/Android smartphones and tablets), but can also be used in a normal PC if you don't mind the big buttons.

![image](../wiki_files/iviz_front_2.png)

## 1. Installation

To run iviz, you need the following:
* Unity 2019.4 LTS editor on either Windows, Linux, or macOS
* An iOS or Android device (optional)

The iviz project has no external dependencies (all required libraries are included), so installing it is just a matter of cloning the repository, launching Unity, and selecting the scene at 'Scenes/UI AR'.

## 2. Getting Started

Here are some instructions on how to get started:

* Start Unity 2019.4, and open the project on the iviz folder. Make sure that the scene **Scenes/UI AR** is active. Press Play.
* On the panel at the top-left, right underneath the "- iviz -" label, tap on the address with the arrow at the end.

* You should now see the **Connection Dialog**.
  - Type in the URL of the master, i.e., where roscore is running. This is the content usually stored in the environment variable _ROS_MASTER_URI_.
    * The arrow icon will show you previously used masters.
  - Type in the URL of your device, or leave the default. The URL should have the form http://_hostname_:port/
    * The hostname is the content usually stored in _ROS_HOSTNAME_ or _ROS_IP_. 
    * The port (7613) can be set to anything, just make sure it's not being used by another application.
    * The hostname and port should be accessible to the ROS nodes that you want to contact. This is important for devices that are in multiple networks.
    * Note that the hostname and port will be sent 'as is' to other ROS nodes. For example, if you use something like http://localhost:7613, other computers will try to connect to themselves instead of you.
    * The arrow icon will show a list of example URLs based on the addresses of your device. 
  - Finally, type in your id, or leave the default. This is the name of your ROS node. It can be anything, but make sure it is unique in your network.
* Once the data is correct, tap on the _Connect_ button. The application will now try to connect to the ROS master, and keep retrying if it does not work. Tap _Stop_ to cancel the operation. 
* Once you have connected, the top-left panel should become green. You can now add modules such as topic listeners, robots, watch the TF frames, and so on.

![image](../wiki_files/connection-dialog.png)d

## 3. Navigation

To move around:
* On a PC: Hold down the right mouse button and move the mouse to rotate the camera. While holding the right button down, press W-A-S-D to translate the camera. (This is the same behaviour as in Unity)
* On a mobile device: Tap with one finger and drag to rotate the camera. Tap with two fingers and move to translate the camera. Pinch to zoom in and out.

## 4. Adding Topics and Modules

In order to add a module that listens to a topic, click on the *+ Topic* button.
You will be shown a list of available topics you can add.
* *Show Unsupported*: Select this to show all the topics, even the ones that cannot be displayed.    

iviz also supports some modules that are not related to topics, which can be found by clicking on the *+ Module* button.
These include:
* *Augmented Reality*: The AR manager.
* *Robot*: Displays a robot.
* *DepthCloud*: Transforms a depth image (optionally with a color image) into a point cloud.
* *Joystick*: Displays two on-screen joysticks that publish twist messages.
* *Grid*: Creates a new grid.

If you're only interested in listening to a topic, but not displaying it, you can press the *Echo* button.
In the Echo dialog, select the topic you want, and iviz will display the contents of the messages in JSON format.
* *Topic*: The topic to be listened to, or (None) to deactivate. 

Note that closing the window will leave the subscriber open (and will continue to consume bandwidth).
If this is not desired, simply select (None) as the topic.

## 5. Connections

At each module that involves a ROS connection, you will see light-blue panels showing the ROS topic and statistics.
Cyan panels appear on top and represent listeners. Outside of the topic, you will see statistics in the form of
* Num Publishers | Messages per Second | KBytes per Second | Num Messages Dropped

Note that the number of messages dropped are not because of the connection, but usually because only one message can be displayed at each frame.
Other modules, such as TF or Markers, can process multiple messages per frame, but the maximal amount is still capped for performance reasons.

A listener widget can be paused by clicking on it.
You can unpause it by clicking it again.
Sometimes, iviz will not be able to connect to a publisher, showing '0' even if a publisher is available.
In this case, the listener can be reset by pausing and unpausing it.
To reset all connections, click on the *Reset* button on the left panel. 

On the bottom, in darker blue, are publisher widgets, showing similar information.

To get a summary of all connections the app is managing, together with the name of the nodes it is connected to (and those it failed to connect with), you can click on the *Network* button.
Incoming connections are shown in blue, outgoing connections in red.   

## 6. Working with Transform Frames

The TF module automatically subscribes itself to /tf and /tf_static when the client is connected, and will by default display every transformation frame.
iviz does not enforce a unique fixed frame, and all frames without parents are assumed to be on the origin.
iviz will also assume that a frame named 'map' exists on the origin.
Furthermore, on panels that deal with transform frames, there is a green widget showing the latest frame referenced by a message header.
You can click on it to move the camera to that frame.

Of interest when working with frames are two modules:
First, the *TF* module is in charge of displaying transform frames. It contains the following widgets:
* *Visible*: Click on the eye on the top right to show or hide the frames.
* *Keep All Frames, Even if Unused*: If deactivated, it will remove all frames that do not have a display attached to them. 
This is useful as a soft reset, or just to keep unrelated frames from cluttering the screen.
* *Show Frame Names*: Displays a text next to the frame with their names.
* *Connect Children to Parents*: Displays lines that connect a child frame to their parent.
* *Frame Size*: The size of the frame in meters.  

If there are old static frames or unused frames that you want to get rid of, click on the *Reset All* button on the left.

Second, the *Frame Dialog* can be accessed by clicking on the *Frame* button on the left panel. 
It shows a list of all frames in the scene, together with their absolute position (relative to the root).
When you click on a frame name, the following options become available at the bottom:
* *Go To*: Moves the camera to the frame.
* *Trail: On/Off*: Enables or disables a 5 second trail that follows the movement of the frame.
* *Lock Pivot: On/Off*: Enables or disables the lock pivot navigation.
When active, the only movement allowed is rotation around the frame.
You can zoom in and out by moving forward or backwards.
The camera will follow the frame as it moves.
Once active, an unlock button will appear on the lower part of the screen which can be clicked to deactivate it.
* *Make Fixed*: This will put the frame at the origin of the scene.
If the frame is transformed, iviz will apply the inverse of the transform to the world instead.
This is useful in AR mode when you want to 'teleport' the scene to a different place, or just to keep the camera on top of a moving robot.
Furthermore, events such as clicks on the grid will be published based on this frame.
The fixed frame will be shown in green.
  

 
![image](../wiki_files/tf-dialog.png)


## 7. Working with Robots
![image](../wiki_files/iviz_screen.png)
TBW...

## 8. Working with Augmented Reality
TBW...

## 9. Credits

![image](../wiki_files/robdekon_logo_web.svg)

![image](../wiki_files/BMBF_gefoerdert_2017_de_web.svg)
