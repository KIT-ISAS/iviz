using System;
using System.Threading;
using Iviz.Msgs.ActionlibTutorials;
using Iviz.Roslib;
using Iviz.Roslib.Actionlib;

namespace iviz_test
{
    public static class ActionClientTests
    {
        static Uri RosMasterUri => RosClient.EnvironmentMasterUri ?? throw new NullReferenceException("Please set the ROS_MASTER_URI variable!");
        
        public static void FibonacciActionTest(bool withCancel)
        {
            Console.WriteLine("** Starting Fibonacci action test!");
            using RosClient client = new RosClient(RosMasterUri, callerUri: RosClient.TryGetCallerUri(7632));
            using var actionClient = default(FibonacciAction).Create(client, "fibonacci");

            Console.WriteLine("** Waiting for server...");
            actionClient.WaitForServer(150000);

            Console.WriteLine("** Setting goal...");
            actionClient.SetGoal(new FibonacciGoal
            {
                Order = 15
            });

            Console.WriteLine("** Waiting for results...");
            
            int n = 0;

            using var maxTimeout = new CancellationTokenSource(10000);
            foreach (var (feedback, result) in actionClient.ReadAll(maxTimeout.Token))
            {
                if (feedback != null)
                {
                    Console.WriteLine($"Feedback: {feedback}");

                    n++;
                    if (n == 5 && withCancel)
                    {
                        // suddenly a cancel!
                        actionClient.Cancel();
                    }
                } else if (result != null)
                {
                    Console.WriteLine($"Result: {result}");
                }
            }

            Console.WriteLine("** Done!");
        }        
    }
}