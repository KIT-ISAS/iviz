using System;
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
            using var actionClient = ActionClient.Create(client, "fibonacci", default(FibonacciAction));

            Console.WriteLine("** Waiting for server...");
            if (!actionClient.WaitForServer(150000))
            {
                Console.WriteLine("EE Gave up waiting for the server");
                return;
            }

            Console.WriteLine("** Setting goal...");
            actionClient.SetGoal(new FibonacciGoal
            {
                Order = 15
            });

            Console.WriteLine("** Waiting for results...");
            
            int n = 0;
            void FeedbackCallback(FibonacciFeedback f)
            {
                Console.WriteLine($"Feedback: {f.ToJsonString()}");

                n++;
                if (n == 5 && withCancel)
                {
                    // suddenly a cancel!
                    actionClient.Cancel();
                }
            }

            static void ResultCallback(FibonacciResult f)
            {
                Console.WriteLine($"Result: {f.ToJsonString()}");
            }

            // callbacks are called in the same thread
            Progress<FibonacciFeedback> fp = new Progress<FibonacciFeedback>(FeedbackCallback);
            Progress<FibonacciResult> rp = new Progress<FibonacciResult>(ResultCallback);

            actionClient.WaitForResult(fp, rp);

            Console.WriteLine("** Done!");
        }        
    }
}