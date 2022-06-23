# Welcome to iviz!

**iviz** is a mobile 3D visualization app for ROS based on the Unity Engine.
You can use it to display 3D information about topics, navigate your virtual environment, or watch your robot in Augmented Reality.

iviz has been designed primarily for use in **mobile devices** (iOS/Android smartphones and tablets), but can also be used in a normal PC if you don't mind the big buttons.

If you want a technical overview or want to cite this app, you can read the [publication here](
https://doi.org/10.1016/j.simpa.2021.100057) (open access!).

![image](../wiki_files/iviz_front_2.png)

## 1. Installation

To run iviz, you need the following:
* For mobile: iOS (> 11.0) or Android (> 7.0)
* For the standalone package: Windows, Linux, or macOS
* For running from the editor: Unity 2021.3 (make sure to select the scene at 'Scenes/UI AR')

The Unity project has no external dependencies (all required libraries are included), so installing it is just a matter of cloning the repository and launching Unity.

## 2. Getting Started

Here are some instructions on how to get started:

### Connect to the ROS master

* On the panel at the top-left, right underneath the "- iviz -" label, tap on the address in bold with the arrow at the end.

* You should now see the **Connection Dialog**.
    - In **Master URI** write the URL of the master, i.e., where roscore is running. This is the content usually stored in the environment variable ROS_MASTER_URI.
        * The arrow icon will show you previously used masters.
    - Optional: In **My Caller URI** write the URL you want for your device. The URL should have the form http://_hostname_:port/
        * The hostname is the address of your device. It would usually be stored in ROS_HOSTNAME or ROS_IP.
        * The port (7613) can be set to anything, just make sure it's not being used by another application.
        * The hostname and port should be accessible to the ROS nodes that you want to contact. Do not use something like http://localhost:7613, as this will cause other computers to try to connect to themselves instead of you.
        * The arrow icon will show you all the addresses of your device.
    - Optional: In **My ID** write your ROS id. This is the name of your ROS node. It can be anything, but make sure it is unique in your network.
* Now tap on the **Connect** button. The application will try to connect to the ROS master, and keep retrying if it does not work. 
Tap **Stop** to cancel the operation.
* Once you have connected, the top-left panel should become green. You can now start using the app!

![image](../wiki_files/connection-dialog.png)

### Navigation

To move around:
* On a mobile device: Tap with one finger and drag to rotate the camera. Tap with two fingers and move to translate the camera. Pinch to zoom in and out.
* On a PC: Hold down the right mouse button and move the mouse to rotate the camera. While holding the right button down, press W-A-S-D or Q-E to translate the camera. (This is the same behaviour as in Unity)

The axis frame at the top-right tell you the current orientation of the camera (red is +X, green is +Y, purple is +Z) in relation to the fixed frame.
If you get lost, you can click on the TF button on the left, and then click on the frame panel on the top right (the green one).
It will take you back to the map frame, positioned on the origin - or whatever frame you chose as the fixed frame.
Many modules also have frame panels in green, you can click them to go to where the module is centered.

If the frame referenced by the frame panel is moving, you can click on the panel and _drag_ it to the left (into the screen).
This will **Lock the Camera** onto it.
You will now follow the frame whenever it moves.
Click on the lock on the bottom of the screen to restore the camera.

### Adding Modules

In iviz, modules are entities that display visual data, usually from ROS streams. 
They are represented by the rectangular buttons on the left side, right below the two rows of dialog buttons.
When you start iviz, two modules are started by default: TF (in charge of the transform frames) and Grid (the floor plane).
All modules except TF can also be removed.

In order to add a module for a ROS topic, click on the **+ Topic** dialog.
You will be shown a list of available topics you can add.
* **Show Unsupported**: Select this to show all the topics, even the ones that cannot be visualized by iviz.
* **Sort by Type**: Sorts the topics by type. Otherwise they are sorted by topic name.

![image](../wiki_files/add-topic.png)

<!--
These include:
* **Augmented Reality**: The AR manager (more in Section 11).
* **Robot**: Displays a robot (more in Section 10).
* **DepthCloud**: Transforms a depth image (optionally with a color image) into a point cloud.
* **Joystick**: Displays two on-screen joysticks that publish twist messages.
* **Grid**: Creates a new grid.
-->

Helpful Notes:
* If you just want to see the a plain list of every topic and service, use the **System** dialog.
* You can find additional debugging messages (from iviz and other nodes) in the Console **Log** dialog.
Select [Me] in the **From** dropdown to filter out log messages from other nodes.
* If you're only interested in listening to the messages in a topic, but not visualizing them, you can use the **Echo** dialog.

Other modules that are not related to topics can be found by clicking on the **+ Module** button.

In order to remove a module, select the module and then on the trash button on the top right.
Alternatively, you can click on the module button and drag it to the left, away from the screen.

### Connections

When you click on a module that listens to a ROS topic, you will see light-blue panels on the top right showing the ROS topic and its statistics.
These are called the **Listener Widgets**.
An example can be found by clicking on the TF module.
The cyan panels on the top right are the two listener widgets, one that listens to /tf and one that listens to /tf_static.
In each, right below the topic, you will see statistics in the form of
* Num Publishers | Messages per Second | KBytes per Second

These wil give you an idea of how much information is being transmitted in that topic.

![image](../wiki_files/tf-listeners.png)

A **Listener Widget** can be paused by clicking on it.
You can unpause it by clicking it again.
Sometimes, iviz will not be able to connect to a publisher, showing '0 pub' even if a publisher is available.
In this case, a quick solution is to 'reset' the listener by pausing and unpausing it.
If this doesn't work, check the **Network** dialog for more information, or the Console **Log** dialog for to see if the other node has published an error message.

In some data panels, on the bottom in darker blue, are **Publisher Widgets** showing similar information for outgoing data.

### Connection Troubleshooting

If you are having trouble with connections, you can try the following:

* The **Network** dialog contains information about which nodes iviz is connected to. 
If the app is trying to connect to a node and failing, this dialog can give an idea of why it's happening.

![image](../wiki_files/network-dialog.png)
* In ROS networks **DNS resolving** is very important, as we need to know which IP correspond to which hostname.
As some networks are improvised, it is often required to put these entries in /etc/hosts, which may not exist in a mobile device.
To address this, you can use the **Aliases** tab in the **System** dialog.
For example, the previous picture has a node called 'telepresence-virtualbox', whose IP we add as follows.

![image](../wiki_files/system-aliases.png)
* If you are able to listen to other nodes publishing messages, but all of your iviz publishers show zero subscribers, then there may be a firewall problem preventing incoming connections.
Or maybe the hostname you gave in **My Caller Uri** is not reachable.


### Settings

Finally, one option you should check out is the **Settings** dialog (the button with the gear icon).
It presents multiple options that control the quality and CPU usage of the application.
You can set the maximum FPS at 60 if you want a fluid display, or you can lower the graphics quality to preserve battery. 
The CPU usage can also be reduced by lowering the frequency at which network data is being processed.
The Settings configuration is saved automatically, and will be reused the next time iviz is started.


## 3. Panels and Dialogs

In this section we will present a description of the app's UI layout, with all of its panels and buttons.

On the left side, below the Connect panel, you will find a panel with two rows of buttons.
This is the **Dialog Panel**.
Clicking on the buttons will open the corresponding dialog in the center of the screen.

Below it is the **Module Panel**.
It shows the active dialogs as large rectangular buttons.
For example, the TF module is always present.
Clicking a module will open the **Data Panel** on the right side, showing information for that module.
Clicking the module button again will close it.
You can drag a button to the right (into the screen) to make it invisible in the world.
You can also drag it to the left (away from the screen) to destroy it - but be careful with it!

Deep below the **Module Panel** is the **Camera Panel**.
It tells you which view you are in (Virtual View / AR View / whatever).
Below it is the current camera position in relation to the fixed frame.
And finally, the panel also gives you the **p**itch and the **y**aw of the rotation.

The last line contains basic statistics about the program.
On PC, it will list the used memory, the current bandwidth, the battery (if available) and the frame rate.
On mobile, it will show the clock instead of the used memory.

You can hide the GUI by pressing the **Hide GUI** button with the arrows: in PCs it is located at the center bottom, while on mobile it is located on the left side, next to the __Module Panel__.  
Once the GUI is hidden, the button becomes semitransparent, and you can click it back to reopen the GUI.

### Detachable Dialogs

When opened, all dialogs will appear at the center of the screen.
However, most dialogs can also be **detached** by dragging the title to any position.
When detached, the dialog will turn white, and a small circle will appear at the bottom right.
Now you can move the dialog around by dragging its title, and resize it with the white circle at the bottom.
In order to return the dialog to its previous mode, you can simply close it.

![image](../wiki_files/draggable-dialogs.png)


## 4. Working with Transform Frames

The TF module automatically subscribes itself to /tf and /tf_static when the client is connected, and will by default display every transformation frame.
iviz does not enforce a unique root frame, and all frames without parents are assumed to be on the 'origin'.
On panels that deal with transform frames, the frame panel (green) shows the latest frame referenced by a message header.
You can click on it to move the camera to that frame.
For the TF module, it will point to the fixed frame. 

![image](../wiki_files/tf-dialog.png)

Of interest when working with frames are two modules:

### TF module
First, the **TF** module is in charge of displaying transform frames. It contains the following options:

On top:
* **Visible**: Click on the eye on the top right to show or hide the frames.
* **Reset**: Click on the circle on the top right to remove all frames. 
* **Close**: Closes the data panel.

Below the listeners:
* **Prefer UDP**: Whether to use UDP when connecting to /tf. Only use this if you don't mind losing some packets.
* **Keep All Frames, Even if Unused**: If deactivated, it will remove all frames that do not have a module attached to them.
This is similar to a permanent **Reset**, that is, iviz will not add new frames unless a module starts using it.
* **Show Frame Names**: Displays a text next to the frame with their names.
* **Connect Children to Parents**: Displays lines that connect a child frame to their parent.
* **Frame Size**: The size of the frame in meters.  
* **Make Z Axis Point Down**: Rotates the world 180 degrees so that +Z points down. Can be useful in certain scenarios.
* **Frames Interactable**: Makes the frames clickable so you can identify them quickly.
But if you have a lot of clickable objects in the background, the frames might get in the way.

### TF dialog

Second, the **Transform Frames Dialog** can be accessed by clicking on the **Frames** button on the left panel. 
It shows a list of all frames in the scene.
When you click on a frame name, it will show you information about it:
* The frame name (in bold).
* The parent frame.
* The position.
* The orientation in roll-pitch-yaw.

If you double-click the frame name, the camera will move to center on it.

The following drop-downs control how the information of the selected frame is shown:
* **Show as Tree/Show as Root**: Shows how the frames from the scene are displayed in the list.
* **Pose to Root/Relative to Parent/Relative to Fixed**: Controls how the position and rotation are displayed.

Below that:
* **Make Fixed**: This will put the frame at the origin of the scene.
  If the frame is transformed, iviz will apply the inverse of the transform to the world instead.
  This is useful in AR mode when you want to 'teleport' the scene to a different place, or just to keep the camera on top of a moving robot.
  Furthermore, events such as clicks on the grid will be published based on this frame.
* **Trail: On/Off**: Enables or disables a 5 second trail that follows the movement of the frame.
* **Lock Pivot: On/Off**: Enables or disables the lock pivot navigation.
When active, the only movement allowed is rotation around the frame.
You can zoom in and out by moving forward or backwards.
The camera will follow the frame as it moves.
Once active, an unlock button will appear on the lower part of the screen which can be clicked to deactivate it.

**Limitations**: Because iviz uses the Unity transform system as the backend for TF, you will have trouble working with coordinates that are extremely large in value (>10000).
This is because 32-bit floats have low resolution in those ranges (ROS TF works with doubles), usually causing 3D models to be rendered incorrectly.
For this reason, TF messages with large values will be ignored.
This can make it difficult to work with scenarios that depend on UTM coordinates.
A workaround for this is to use a child of the UTM frame as the fixed frame.

### Custom Frames

The remaining two options control how iviz publishes its own frames.
It consists of two options:

* **Publishing: On/Off**: Tells iviz to take control of a frame, and start publishing it.
* **Publish Custom**: Creates a new TF frame.

When you take control of a frame or create a new one, a new data panel will appear to the right, **Published Frame**.
This will let you set the local position and orientation of the frame, together with its parent.
iviz will keep publishing the pose of the frame in /tf, unless you click **Publishing: Off** to release ownership.
If you do this, you might want to refresh the frame list (circle button on top) in order to remove the outdated frame.

![image](../wiki_files/tf-own-dialog.png)
  
## 5. Log Dialog

The **Console Log Dialog** is a detachable dialog in charge of presenting messages of iviz and other nodes sent to /rosout.
On top there are three buttons:
* **Refresh**: Clears all messages.
* **Pause/Continue**: Pauses or unpauses the collection of log messages.
* **Close**: Closes the dialog.

It also has two widgets:
* **From**:  This limits whose messages will be printed. You can choose between _[All]_, _[None]_, _[Me]_, or a specific node.
* **Log Level**: This sets the lowest message level to be displayed (such as Debug, Info, Warning, etc.). 

![image](../wiki_files/console-log.png)

## 6. Echo Dialog

The **Echo Dialog** can be used to display messages from any topic.
If iviz does not know the message, it will try to reconstruct the definition based on information exchanged in the handshake.
Arrays and long strings will be shortened to keep the GUI from being overflowed.

On top there are three buttons:
* **Refresh**: Clears all messages.
* **Pause/Continue**: Pauses or unpauses the collection of log messages.
* **Close**: Closes the dialog.

It also has one widgets:
* **Topic**: Use this to select the topic, or (None) to cancel.

![image](../wiki_files/echo-dialog.png)

Note that closing the window will leave the subscriber open (and will continue to consume bandwidth).
If this is not desired, simply select (None) as the topic.

## 7. Image Streams

The **Image** module is in charge of displaying image streams, such as 'sensor_msgs/Image'.
It has the following widgets:
* **Description**: This label provides general information about the image.
* **Preview**: Shows a preview of the last image received. Clicking on the preview opens a detachable dialog that provides a better view of the image.

Inside of **Colormap** there are multiple options that only become active when dealing with grayscale images: 
* **Colormap**: A colormap that applies colors to the image to better distinguish between the grayscale values.
* **Colormap Override Min/Max**: By default, the colormap range is resized automatically to the minimum and maximum values in the image.
Select this to use fixed values, selected in the following fields.
* **Colormap Min**: The minimum value of the colormap range.  Use the arrows to multiply by a power of 10, and the [+] sign to make it negative or positive.   
* **Colormap Max**: The maximum value of the colormap range.  Use the arrows to multiply by a power of 10, and the [+] sign to make it negative or positive.
* **Flip Min/Max**: Flips the color scale used by the colormap.

You can also add a billboard rectangle in-world that shows the image next to its transform frame.
The options for this are in **Billboard**.

* **Show as Billboard**: Enables or disables the billboard.
* **Billboard Size**: Applies an additional scale on the billboard. Default is 1.
* **Use Intrinsic for Billboard Scale**: If an intrinsic matrix is being published, it will be used to scale the billboard depending on the Z value of the offset.   
* **Billboard Points to You**: If active, the billboard will act like a sprite and point to the camera at all times.
* **Billboard Offset**: The distance between the frame and the billboard. 

![image](../wiki_files/image-dialog.png)

Some sensors use depth images, which can be transformed into point clouds.
In iviz this can be achieved using the **DepthCloud** module, which can be found by clicking the **+ Module** button on the **Dialog Panel**.

It has the following widgets:
* **Depth Topic**: The topic containing the depth image.
* **Color Topic**: An optional topic containing a color image that corresponds to the depths.
* **Depth Preview**: Shows a preview of the last depth image received. Clicking on the preview opens a detachable dialog that provides a better view of the image.
* **Color Preview**: Shows a preview of the last color image received. Clicking on the preview also opens a detachable dialog.

Similar to the **Image** module, a colormap is applied to the depth image if no color image is set.
The parameters can be changed using the options inside **Colormap**:
* **Colormap**: The name of the colormap.
* **Colormap Override Min/Max**: By default, the colormap range is resized automatically to the minimum and maximum values in the depth image.
  Select this to use fixed values, selected in the following fields.
* **Colormap Min**: The minimum value of the colormap range.  Use the arrows to multiply by a power of 10, and the [+] sign to make it negative or positive.
* **Colormap Max**: The maximum value of the colormap range.  Use the arrows to multiply by a power of 10, and the [+] sign to make it negative or positive.
* **Flip Min/Max**: Flips the color scale used by the colormap.

![image](../wiki_files/image-pointcloud.png)

## 8. Model Loader Service and External Models

When working with robots and markers, you will often need to work with 3D assets stored in external files, for example in the machine with the ROS master.
This is problematic, as iviz will usually run on an mobile device without a ROS installation.
Instead of copying the files manually, you can simply start the Model Loader service node on the machine with the ROS assets, and
iviz will detect the node automatically and it whenever an asset file is requested.

The easiest way to start the model loader service is to start the _Iviz.ModelService_ program corresponding to your OS in the _iviz_model_service/Binaries_ folder.
It will look something like this:

![image](../wiki_files/model-service-console-2.png)

Make sure ROS_MASTER_URI is set and your ROS_PACKAGE_PATH variable points to your packages.
Also don't forget to start the model loader on the machine that has the assets. This may not be the device that's running iviz!

## 9. Working with Robots

You can add a robot by opening the **+ Module** dialog, and then selecting the **Robot** option.
The new robot module will be empty. 
There are two ways to setup a robot:
* **Load From Source Parameter:** This downloads a URDF from the parameter server. 
  You can write any parameter name you like, or select a parameter from the suggestions (i.e., all entries that include the text 'robot_description').
* **Load From Saved:** This loads a robot from the iviz defaults.

If you need external assets such as 3D models, make sure the model loader service is running! 

Other options include:
* **Hide**: Use the eye button on the top-middle to hide the robot or make it visible again. 
* **Trash**: The trash button on the top-right deletes the robot.
* **Frame**: The widget in green shows the name of the base link frame. Click on it to focus the camera there. 
Drag it to the left to follow the robot.
* **Attach to TF Frames:** By default the robot only listens to the frame that corresponds to the base link.
Enable this to read the TF frames for all links. If you see the robot crumple onto the floor, then the TF frames have not been set.
* **Save this Robot Locally:** Saves a copy of the robot on the device. The robot can then be loaded from the **Load From Saved** menu.

Hidden within the **Visuals** collapsible are more options:
* **AR Occlusion Only Mode:** This tells the shader to only write the depth values of the robot, but not the color.
  In essence, this 'punches' a hole in the scene, showing the background where the robot should be.
  This is useful in Augmented Reality when you have a digital twin of the robot at the exact same position as the real robot.
  Then, by enabling this, you can hide virtual objects that are behind the real robot, while leaving objects in front of the robot intact.
* **Tint:** This changes the color of the robot. Click on the colored rectangle to change to HSV.
* **Alpha:** Sets the transparency of the robot.
* **Metallic:** Sets the metallic property of the robot shader. This controls how reflective the robot is.
* **Smoothness:** Sets the smoothness property of the robot shader. This indicates how 'polished' the robot's surface is.

Finally, hidden in **Prefixes and Suffixes** are two fields.
These control how links are connected to TF frames.
If the fields are not empty, then each link read from the robot specification will be adjusted with the given prefix and suffix, and the result will be the TF frame that controls it. 
Thus, if you have multiple robots of the same type, you can add a prefix and publish a different set of TF frames to target each robot separately.

![image](../wiki_files/robot_new.png)

### Troubleshooting
* You can see the frames moving, but the robot is empty.
  * The model loader service is not active. Check the Console **Log**, and select [Me] in the **From** dropdown. You will probably see multiple errors like 'Resource not in cache and model loader failed'.
Start the model loader and try again.
* The robot is there but it doesn't move, even though the frames do.
  * Enable 'Attach to TF Frames'.
* There is an error message saying 'provider not found'.
  * You need an external asset, but there is no Model Loader service active in the system.
* There is an error message saying 'Failed to find resource path XXX' or 'Failed to find package XXX'
  * The model loader service is running, but could not find the requested file. 
    Make sure that the model service node is running in the same PC as the assets, and that the root folder of the ROS package you need is in ROS_PACKAGE_PATH.
* There is an error message saying 'Failed to retrieve visual' or 'Failed to retrieve collider'.
  * This happens if retrieving a file fails multiple times. The original error should appear a little higher. If it says 'no provider', start the model loader and try again.

## 10. Loading and Saving

You can save the current loaded modules and their configurations using the **Save** button.
When you open it, you can get a list of previously saved files.
By clicking on one of the buttons, you can overwrite. Or you can click on the cross to delete it.
Similarly, you can load a previously saved file with the **Load** button.
Your current modules will be deleted and overwritten with the ones from the saved file.

![image](../wiki_files/saving-dialog.png)

An interesting save file is **_default**. It will be loaded every time you start iviz.
You can use it if you're working on something and want a given topic module to be started every time, or if you change something in the camera or the background.

## 11. Working with Augmented Reality
To enable Augmented Reality (AR), go to the **Dialog Panel** and click the **AR View** button.
Alternatively, you can click the **+ Module** button and then choose _Augmented Reality_.
You will need an AR-capable device (tablet or smartphone) for this.

Once the AR module is active, the usual **Virtual View** visualization is replaced with the **AR View**.
Now, you need to setup the AR mode.
* First, you need to let your device find a plane on which the AR view will be displayed. 
  This is achieved by moving the device laterally (_not_ by rotating it).
  Only by translating your device can the AR system find the 3D location of its point features and estimate a plane from them.
  (Newer devices with depth cameras will find a plane almost instantly.) 
* Once a plane is found, the transparent frame becomes opaque, and the _Start_ button becomes available.
* When you find an appropriate origin, click on _Start_.
  Your scene should now become visible.
* **Note:** If the image appears too grainy, or iviz takes too long to find a plane, you can try moving to a more illuminated place, or adding more light to your scene.

Once you start the AR visualization, pay attention to the FPS counter (left panel, at the bottom).
Depending on the device, it should be either 30 or 60.
If your FPS is lower than expected, you should try lowering the graphics quality (**Settings** dialog, gears button on the left panel).
It is not a good idea to have the AR tracking system compete with the visualization for CPU.

You should now see something like this (minus the robot):

![image](../wiki_files/ar-bagger.jpeg)

On the left part there is a toolbar with multiple buttons:
* **Toggle**: This will switch between the **AR View** and the **Virtual View**.
* **Mover**: This presents a menu that allows you to move the origin. Red is X, Green is Y, Blue is Z, and White is Yaw Rotation.
  It has two modalities:
    * Global: Here, X, Y, Z, and Yaw are in the coordinate system of the origin. X is right, Y is front, and Z is up.
    * Screen: Here the directions are in the coordinate system of the screen, and change depending on how your screen is oriented.
      X is to the right of the device, Y is forwards into the screen, and Z points upwards.
      Yaw rotates the world around ~1 meter in front of the device.
* **Meshing**: If your device can reconstruct the environment, this activates or deactivates it. 
* **TF**: This provides easy access to some TF options.
* **Reposition**: This tells iviz to restart the AR setup mode. Use this if you moved some distance from the origin and want to reposition it to be wherever you are now. 
* **Reset All**: This restarts the AR system. Use this if something goes wrong.

There is also a long vertical grey bar. Press this in order to hide the toolbar.
The button with the arrow will show the main GUI again.

This image shows the **Virtual View** of the same scene.
On the right, you will see the contents of the **Augmented Reality** module, with the following options:
* **Trash**: This disables the AR system.
* **Frame Widget**: Shows the fixed TF frame used for the AR origin.
* **Status Label**: Shows diagnostic data about the AR system, including the tracking state and the number of detected planes.
* **World Scale**: This allows you to make the virtual world smaller or bigger. Useful when visualizing large robots such as cars on a small table.
* **Enable Auto Focus**: If the device keeps refocusing all the time making the scene blurry, you can try disabling this.
* **Publish AR camera images**: The AR module advertises topics which publish the color and depth images from the device if a node is suscribed.
Use these if you want to capture data from your device into another application.
* **Occlusion Quality**: This enables the Depth API present in newer devices that can hide a virtual object if it is located behind a real-world object.
* **AR Markers**: (iPad only - to be written)


## 12. Credits

The code of iviz is open-source and released under the MIT license. 
This work is part of the ROBDEKON project (https://robdekon.de) and is financed by the German Federal Ministry of Education and Research (BMBF).

![image](../wiki_files/robdekon_logo_web.svg)

![image](../wiki_files/BMBF_gefoerdert_2017_de_web.svg)
