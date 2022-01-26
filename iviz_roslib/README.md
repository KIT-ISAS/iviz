# iviz_roslib

iviz_roslib is a (partial) implementation of the ROS API in pure C#.

It is written using the .NETStandard 2.0 runtime with C#8.
I have tested it in Windows, Ubuntu Linux, macOS, iOS, Android, and UWP (Hololens 1 and 2), so I hope it will work pretty much anywhere.  

## Getting Started

There is a project called __iviz_utils__ in the root of the repository that references all the other projects.
You can either:
* include the project into your VS, VSCode, or Rider solution, or
* reference all the DLLs in either __iviz_utils/Publish__ (NET Standard 2.1) or __iviz_utils/Publish5__ (NET 5) in your project.

If you are in Unity, all you need to do is copy all the DLLs and put them somewhere in your Assets directory.
If you are recompiling the library, or adapting the code to your own libraries, keep in mind that Unity only supports .NETStandard 2.0 (2.1  in 2021.2, but not Core).

*Note:* The file Newtonsoft.Json.dll is a bit problematic, because some Unity packages already provide it.
If you get an error of "duplicate references" you can remove the iviz version.
The Unity version is also safe to use.  

## Message Generation

TBW

## Basic Examples

The interface is inspired by [ROS#](https://github.com/siemens/ros-sharp), so if you come from that background you will find the library more intuitive than if you were using roscpp.

### Connection

Unlike Rosbridge, you do not need a special websocket app to talk to ROS.
Still, there are some things you need to keep in mind.
* Instead of URIs like _ws://localhost:9090_, you now connect directly to _http://localhost:11311_.
* ROS is a peer-to-peer network, so if you're working with other computers, you will need an address that can be reached from the outside.
This is because this address will be sent to the other computers when they want to establish a connection with you.
If this address is not reachable to other nodes, you will only be able to subscribe and listen to other nodes, but they will not be able to subscribe to your topics.
* You also need a caller id, such as _/my_node_. This name must be unique in the ROS network.

A connection example follows:
```c#
// set the master uri: check ROS_MASTER_URI first, else use the given value
Uri masterUri = RosClient.EnvironmentMasterUri ?? new Uri("http://192.168.0.1:11311");

// set our own uri, the address should match your ip 
Uri callerUri = new Uri("http://192.168.0.2:7614");

// alternative that tries to guess your address and uses the port 7614
// Uri callerUri = RosClient.TryGetCallerUriFor(masterUri, 7614) 

// set the name of our node
string callerId = "/iviz_test";

// create the connection
RosClient client = new RosClient(masterUri, callerId, callerUri);
```

The function _TryGetCallerUriFor_ can also be used without a port.
In this case, a random free port will be used.
When choosing whether to use your own port or a random port, keep in mind the following guidelines:
* A random port is useful for finished apps. It ensures that you will never get an error like "address is already in use".
* Specific ports are better for new apps being debugged.
The problem is that if your app crashes or gets terminated before it unregisters itself gracefully, the advertisements and subscriptions will remain in the system.
If your program keeps getting restarted with a new port every time, it will keep adding new entries in the list of nodes, which makes debugging difficult.
  By reusing the old port, other apps can also tell that you're the same node that's just restarting.

### Publishers
Publishers are used to send messages to topics.
A publisher example follows:
```c#
// we advertise a topic, and retrieve a publisher object.
string id = client.Advertise("/test_topic", out RosPublisher<PoseStamped> publisher);

for (int i = 0; i < 100; i++)
{
    // create a random message
    PoseStamped msg = new PoseStamped
    {
        Header = new Header { FrameId = "/map", Stamp = time.Now() },
        Pose = new Pose
        {
            Orientation = new Quaternion(0, 0, 0, 1),
            Position = new Point(0, 0, i * 0.1)
        }
    };

    // we use the publisher object to send the message
    publisher.Publish(msg);

    // wait a bit for the next message
    Thread.Sleep(1000);
}

// closing the client automatically unadvertises all topics 
client.Close(); 
```

The identifier _id_ acts as a sort of reference counter, and is needed if you want to unadvertise the topic when you don't need it anymore. 
```c#
publisher.Unadvertise(id);
``` 
You can advertise a topic multiple times, for example if your application has multiple modules that use the same client.
The client will only advertise it once in ROS, and you will receive a different id each time.
If all the ids are unadvertised, the publisher will be disposed and the topic will be unregistered from the ROS system.   

Other things of interest:
* You can use the _NumSubscribersChanged_ event to see if somebody has subscribed or unsubscribed from your topic. 
 This is useful if your topic requires heavy use of resources, such as a video stream from a camera, so that you can activate the processing only if a subscriber appears.
```c#
publisher.NumSubscribersChanged += pub =>
{
    Console.WriteLine("Publisher for topic " + pub.Topic + " now has " + pub.NumSubscribers + " subscribers!");
};
``` 
* If you enable _Latching_, the publisher will automatically send the last message you published to any new node that subscribes.
This is useful if you have a message that doesn't change but which every subscriber needs to know, and you don't want to have to republish it periodically. 
* Most client functions have an _async_ equivalent. For example, you can do: 
```c#
var (id, publisher) = await client.AdvertiseAsync<PoseStamped>("/test_topic");
// ...
await publisher.UnadvertiseAsync(id);
```

 
### Subscribers

Subscribers are used to receive messages from topics.
A subscriber example that prints all messages from "/tf" looks as follows:

```c#
// we subscribe to a topic with the given callback
client.Subscribe<TFMessage>("/tf", msg =>
{
    // transform the message to JSON
    string msgAsJson = JsonConvert.SerializeObject(msg);
    // print the json representation
    Console.WriteLine(msgAsJson);
});

// wait 10 seconds
Thread.Sleep(10000);

// closing the client automatically unsubscribes to all topics 
client.Close();
``` 

Unsubscribing is realized in a similar way as before, by passing the id that we obtained from the _Subscribe_ method. 
The following example also shows us how to obtain a _Subscriber_ object:

```c#
string id = client.Subscribe<TFMessage>("/tf", msg =>
{
    // transform the message to JSON
    string msgAsJson = JsonConvert.SerializeObject(msg);
    // print the json representation
    Console.WriteLine(msgAsJson);
}, out var subscriber);
//...
subscriber.Unsubscribe(id);
```

Note that you can subscribe to a topic before a publisher for it exists.
You will start receiving message once the publisher appears.
You can similarly check the number of publishers with the _NumPublishers_ property in the subscriber object, or by using the event _NumPublishersChanged_.

Finally, as with the publishers, there are _async_ versions of the client functions: 

```c#
var (id, subscriber)  = await client.SubscribeAsync<TFMessage>("/tf", msg =>
{
    // transform the message to JSON
    string msgAsJson = JsonConvert.SerializeObject(msg);
    // print the json representation
    Console.WriteLine(msgAsJson);
});
//...
await subscriber.UnsubscribeAsync(id);
```

### Services

Calling a service consists of creating a service variable, setting up the request, and then reading the response.
For example, let us assume we want to implement a service _/add_two_ints_ of type _rosbridge_library/AddTwoInts_.

On the server side, we implement it as follows:
```c#
client.AdvertiseService<AddTwoInts>("/add_two_ints", srv =>
{
    Console.WriteLine("Received service call!");
    srv.Response.Sum = callAddTwoInts.Request.A + callAddTwoInts.Request.B;
});
```

On the client side, we do as follows:
```c#
AddTwoInts srvAddTwoInts = new AddTwoInts();
srvAddTwoInts.Request.A = 2;
srvAddTwoInts.Request.B = 3;

client.CallService("/add_two_ints", srvAddTwoInts);

long result = srvAddTwoInts.Response.Sum;
Console.WriteLine("The result is " + result);
```
As with the publishers and subscribers, there exist _async_ versions of the functions.
On the server side, we implement it as follows:
```c#
await client.AdvertiseServiceAsync<AddTwoInts>("/add_two_ints", async srv =>
{
    Console.WriteLine("Received service call!");
    srv.Response.Sum = callAddTwoInts.Request.A + callAddTwoInts.Request.B;
    await Task.CompletedTask;
});
```
Then, on the client side:
```c#
await client.CallServiceAsync("/add_two_ints", srvAddTwoInts);
```

### Parameters

To write a parameter to the parameter server, you need a client that is already connected.
Then you do for example:
```c#
client.Parameters.SetParameter("/my_param", "abcd");
```
This will write the string "abcd" as the parameter "/my_param".
The second argument is of type _Arg_, which accepts expressions of type string, integer, float, and so on. 
This means that you can also do
```c#
client.Parameters.SetParameter("/my_other_param", 10);
```
To retrieve parameters, you call:
```c#
client.Parameters.GetParameter("/my_param", out object o);
if (o is string str)
{
    Console.WriteLine("The value of /my_param is " + str);
}
else
{
    Console.WriteLine("Expected string value in /my_param, but got " + o);
}
```

## More Advanced Stuff

### Channels
TBW

### Action Client
TBW

### Dynamic Messages
TBW
