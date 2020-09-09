# iviz_roslib

iviz_roslib is a (partial) implementation of the ROS API in pure C#.

It is written using the .NETStandard 2.0 runtime with C#8, but can be used in C#7.3 (Unity) without issues.
I have tested it in Windows, Ubuntu Linux, macOS, iOS, Android, and UWP 32-bit (Hololens 1), so I hope it will work pretty much anywhere.  

## Getting Started

To use this library in your code, you can either:
* Include the iviz_roslib project into your solution, or
* Reference the files __iviz_roslib.dll__, __iviz_msgs.dll__, and __Newtonsoft.Json.dll__ from _iviz/Assets/Application/Dependencies_ in your own project.

If you are in Unity, all you need to do is copy the three files and put them somewhere in your Assets directory. 

## Examples

The interface is inspired by [ROS#](https://github.com/siemens/ros-sharp), so if you come from that background you will find the library more intuitive than if you were using ros-cpp.
Important differences include:
* You communicate with the roscore node (port 11311) directly. A rosbridge node is not needed.
* TCPROS is a peer-to-peer network, which means that when you publish a topic, you need to accept direct connections from subscriber nodes that may be in other computers.
This can cause firewall issues that were not present in rosbridge, because that only requires a single outgoing connection.
* Also for this reason, iviz_roslib needs to know an IP that is accessible from the outside.
Currently, the library tries to guess it, but if you have multiple interfaces we may send the wrong one.
A symptom of this is if you can subscribe to other nodes, but cannot publish. Check roswtf to verify the connectivity.
* You also need a caller id. This can be any name (no spaces), but it needs to be unique in the network.

A publisher example follows:
```c#
// set the master uri: check ROS_MASTER_URI first, else use the given value
Uri masterUri = RosClient.EnvironmentMasterUri ?? new Uri("http://192.168.0.220:11311");
// the name of our node
string callerId = "/iviz_test";

// create the connection
RosClient client = new RosClient(masterUri, callerId);

// note: to set our own IP directly, use
// Uri callerUri = new Uri("http://192.168.0.10:" + RosClient.AnyPort);
// RosClient client = new RosClient(masterUri, callerId, callerUri);

// now we advertuse a topic. note that we retrieve a publisher object.
client.Advertise<PoseStamped>("/test_topic", out RosPublisher publisher);

// create a random message
PoseStamped msg = new PoseStamped
{
    Header = new Header { FrameId = "/map", Stamp = time.Now() },
    Pose = new Pose
    {
        Orientation = new Quaternion(0, 0, 0, 1),
        Position = new Point(0, 0, 2)
    }
};

for (int i = 0; i < 20; i++)
{
    // we use the publisher object to send the message
    publisher.Publish(msg);

    // wait a bit for the next message
    Thread.Sleep(1000);
}

client.Close(); 
```
(unfinished)