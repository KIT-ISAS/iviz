using System;
using System.Collections.Generic;
using System.IO;
using Iviz.Tools;

namespace Iviz.MsgsGen
{
    static class Program
    {
        static void Main(string[] args)
        {
            try
            {
                DoMain(args);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("EE Fatal error: " + Logger.ExceptionToString(e));
            }
        }

        static void DoMain(string[] args)
        {
            if (args.Length == 0)
            {
                PrintHelpMessage();
                return;
            }

            if (args.Length == 1)
            {
                switch (args[0])
                {
                    case "-h":
                    case "--help":
                        PrintHelpMessage();
                        return;
                    case "-r":
                    case "--regenerate":
                        RegenerateIvizMessages();
                        return;
                }
            }

            if (args.Length < 4 || (args[0] != "-o" && args[0] != "--output"))
            {
                Console.Error.WriteLine("EE Invalid arguments. Use -h to show usage.");
                return;
            }

            string fullOutputPath = Path.GetFullPath(args[1]);

            switch (args[2])
            {
                case "-p" or "--package":
                {
                    string package = args[3];

                    if (args.Length < 5)
                    {
                        Console.Error.WriteLine("EE Invalid arguments. Use -h to show usage.");
                        return;
                    }

                    switch (args[4])
                    {
                        case "-i" or "--input":
                            CreateIndividualMessages(args, fullOutputPath, package);
                            break;
                        case "-if" or "--input-folder":
                            CreateMessagesFromFolder(args, fullOutputPath);
                            break;
                        default:
                            Console.Error.WriteLine($"EE Unknown argument {args[4]}. Use -h to show usage.");
                            break;
                    }

                    break;
                }
                case "-i" or "--input" or "-if" or "--input-folder":
                    Console.Error.WriteLine($"EE Argument {args[2]} is out of order. Use -h to show usage.");
                    break;
                default:
                    Console.Error.WriteLine($"EE Argument {args[2]} is unknown or out of order. Use -h to show usage.");
                    break;
            }
        }


        static void PrintHelpMessage()
        {
            Console.WriteLine("** Welcome to Iviz.MsgsGen, an utility to generate C# code from ROS messages.");
            Console.WriteLine("** Usage:");
            Console.WriteLine("    dotnet Iviz.MsgsGen.dll -h");
            Console.WriteLine("        Shows this text");
            Console.WriteLine("    dotnet Iviz.MsgsGen.dll -o OutputFolder -p Package -i Files... ");
            Console.WriteLine(
                "        Converts the given files (.msg, .srv, .action) into C# messages, and writes the result in OutputFolder");
            Console.WriteLine(
                "    dotnet Iviz.MsgsGen.dll -o OutputFolder -p Package -if InputFolder  [-p Package* -if InputFolder*]...");
            Console.WriteLine(
                "        Searches all files in the input folders (.msg, .srv, .action), converts them into C# messages, and writes the result in OutputFolder");
            Console.WriteLine("    dotnet Iviz.MsgsGen.dll -r");
            Console.WriteLine("        Regenerates the default Iviz message files from the ros_msgs folder");
            Console.WriteLine();
        }

        static void CreateMessagesFromFolder(string[] args, string fullOutputPath)
        {
            if (args.Length < 6)
            {
                Console.Error.WriteLine($"EE Expected folder path after -if or --input-folder.");
                return;
            }

            var p = new PackageInfo();

            for (int i = 3; i < args.Length; i += 4)
            {
                Console.WriteLine();
                Console.WriteLine("** Processing package '" + args[i] + "' at " + args[i + 2]);
                string package = args[i];
                string inputPath = args[i + 2];
                string fullInputPath = Path.GetFullPath(inputPath);
                if (!Directory.Exists(fullInputPath))
                {
                    throw new FileNotFoundException($"Input path '{fullOutputPath}' does not exist.");
                }

                p.AddAllInPackagePath(fullInputPath, package);
            }

            Directory.CreateDirectory(fullOutputPath);

            p.ResolveAll();

            if (p.Messages.Count == 0 && p.Services.Count == 0)
            {
                Console.WriteLine("** No files given. Nothing to do.");
                return;
            }

            if (p.Messages.Count != 0)
            {
                Directory.CreateDirectory($"{fullOutputPath}/msg/");
            }

            if (p.Services.Count != 0)
            {
                Directory.CreateDirectory($"{fullOutputPath}/srv/");
            }

            var usedNames = new HashSet<string>();
            foreach (ClassInfo classInfo in p.Messages.Values)
            {
                string text = classInfo.ToCsString();
                string fileName = usedNames.Contains(classInfo.Name)
                    ? classInfo.CsPackage + classInfo.Name
                    : classInfo.Name;
                usedNames.Add(classInfo.Name);
                File.WriteAllText($"{fullOutputPath}/msg/{fileName}.cs", text);
            }

            foreach (ServiceInfo classInfo in p.Services.Values)
            {
                string text = classInfo.ToCsString();
                File.WriteAllText($"{fullOutputPath}/srv/{classInfo.Name}.cs", text);
            }

            Console.WriteLine("** Done!");
        }

        static void CreateIndividualMessages(string[] args, string fullOutputPath, string package)
        {
            var messageFullPaths = new List<string>();
            var serviceFullPaths = new List<string>();
            var actionFullPaths = new List<string>();

            foreach (string path in args.Skip(5))
            {
                string absolutePath = Path.GetFullPath(path);
                if (!File.Exists(absolutePath))
                {
                    if (Directory.Exists(absolutePath))
                    {
                        Console.WriteLine($"EE File path '{absolutePath}' refers to a directory. " +
                                          $"Did you mean to use '-if' instead of '-i'?");
                        return;
                    }

                    Console.WriteLine($"EE File path '{absolutePath}' does not exist.");
                    return;
                }

                Console.WriteLine(absolutePath);
                string extension = Path.GetExtension(absolutePath);
                switch (extension)
                {
                    case ".msg":
                        messageFullPaths.Add(absolutePath);
                        break;
                    case ".srv":
                        serviceFullPaths.Add(absolutePath);
                        break;
                    case ".action":
                        actionFullPaths.Add(absolutePath);
                        break;
                    default:
                        Console.WriteLine($"EE File path '{absolutePath}' has an unknown extension.");
                        return;
                }
            }

            if (messageFullPaths.Count == 0 && serviceFullPaths.Count == 0 && actionFullPaths.Count == 0)
            {
                Console.WriteLine("** No files given. Nothing to do.");
                return;
            }

            Directory.CreateDirectory(fullOutputPath);
            
            if (messageFullPaths.Count != 0)
            {
                Directory.CreateDirectory($"{fullOutputPath}/msg/");
            }

            if (serviceFullPaths.Count != 0)
            {
                Directory.CreateDirectory($"{fullOutputPath}/srv/");
            }

            foreach (string fullPath in messageFullPaths)
            {
                ClassInfo classInfo = new ClassInfo(package, fullPath);
                string classCode = classInfo.ToCsString();
                string destPath = $"{fullOutputPath}/msg/{classInfo.Name}.cs";
                File.WriteAllText(destPath, classCode);
            }

            foreach (string fullPath in serviceFullPaths)
            {
                ServiceInfo serviceInfo = new ServiceInfo(package, fullPath);
                string classCode = serviceInfo.ToCsString();
                string destPath = $"{fullOutputPath}/srv/{serviceInfo.Name}.cs";
                File.WriteAllText(destPath, classCode);
            }

            foreach (string fullPath in actionFullPaths)
            {
                var actionClasses = ActionGenerator.GenerateFor(package, fullPath);
                if (actionClasses == null)
                {
                    continue;
                }

                foreach (ClassInfo classInfo in actionClasses)
                {
                    string classCode = classInfo.ToCsString();
                    string destPath = $"{fullOutputPath}/msg/{classInfo.Name}.cs";
                    File.WriteAllText(destPath, classCode);
                }
            }

            Console.WriteLine("** Done!");
        }

        static void RegenerateIvizMessages()
        {
            string rosBasePath;
            string ivizMsgPaths;

            Console.WriteLine("** Starting iviz_msgs_gen...");

            {
                string debugRosBasePath = Path.GetFullPath("../../../../ros_msgs/ros1");
                string debugIvizMsgPaths = Path.GetFullPath("../../../../iviz_msgs/ros1");

                string releaseRosBasePath = Path.GetFullPath("../ros_msgs/ros1");
                string releaseIvizMsgPaths = Path.GetFullPath("../iviz_msgs/ros1");

                if (Directory.Exists(debugRosBasePath) && Directory.Exists(debugIvizMsgPaths))
                {
                    // running from an IDE
                    rosBasePath = debugRosBasePath;
                    ivizMsgPaths = debugIvizMsgPaths;
                }
                else if (Directory.Exists(releaseRosBasePath) && Directory.Exists(releaseIvizMsgPaths))
                {
                    // running from the Publish folder
                    rosBasePath = releaseRosBasePath;
                    ivizMsgPaths = releaseIvizMsgPaths;
                }
                else
                {
                    Console.WriteLine("EE Failed to find the iviz_msgs and ros_msgs folders. " +
                                      "They should be in the ../ folder from where you run this.");
                    return;
                }

                PackageInfo p = new PackageInfo();

                string[] packages = Directory.GetDirectories(rosBasePath);
                foreach (string packageDir in packages)
                {
                    string package = Path.GetFileName(packageDir);
                    p.AddAllInPackagePath(packageDir, package);
                }

                p.ResolveAll();

                foreach (var classInfo in p.Messages.Values)
                {
                    string dstPackageDir = $"{ivizMsgPaths}/{classInfo.RosPackage}/msg/";
                    Directory.CreateDirectory(dstPackageDir);
                    string text = classInfo.ToCsString();
                    File.WriteAllText($"{dstPackageDir}{classInfo.Name}.cs", text);
                }

                foreach (var classInfo in p.Services.Values)
                {
                    string packageDir = $"{ivizMsgPaths}/{classInfo.RosPackage}/srv/";
                    Directory.CreateDirectory(packageDir);
                    string text = classInfo.ToCsString();
                    File.WriteAllText($"{packageDir}{classInfo.Name}.cs", text);
                }
            }

            ///////////////////////////

            {
                string debugRosBasePath = Path.GetFullPath("../../../../ros_msgs/ros2");
                string debugIvizMsgPaths = Path.GetFullPath("../../../../iviz_msgs/ros2");

                string releaseRosBasePath = Path.GetFullPath("../ros_msgs/ros2");
                string releaseIvizMsgPaths = Path.GetFullPath("../iviz_msgs/ros2");

                if (Directory.Exists(debugRosBasePath) && Directory.Exists(debugIvizMsgPaths))
                {
                    // running from an IDE
                    rosBasePath = debugRosBasePath;
                    ivizMsgPaths = debugIvizMsgPaths;
                }
                else if (Directory.Exists(releaseRosBasePath) && Directory.Exists(releaseIvizMsgPaths))
                {
                    // running from the Publish folder
                    rosBasePath = releaseRosBasePath;
                    ivizMsgPaths = releaseIvizMsgPaths;
                }
                else
                {
                    Console.WriteLine("EE Failed to find the iviz_msgs and ros_msgs folders. " +
                                      "They should be in the ../ folder from where you run this.");
                    return;
                }

                PackageInfo p = new PackageInfo();

                string[] packages = Directory.GetDirectories(rosBasePath);
                foreach (string packageDir in packages)
                {
                    string package = Path.GetFileName(packageDir);
                    p.AddAllInPackagePath(packageDir, package);
                }

                p.ResolveAll();

                foreach (var classInfo in p.Messages.Values)
                {
                    string dstPackageDir = $"{ivizMsgPaths}/{classInfo.RosPackage}/msg/";
                    Directory.CreateDirectory(dstPackageDir);
                    string text = classInfo.ToCsString();
                    File.WriteAllText($"{dstPackageDir}{classInfo.Name}.cs", text);
                }

                /*
                foreach (ServiceInfo classInfo in p.Services.Values)
                {
                    string packageDir = $"{ivizMsgPaths}/{classInfo.RosPackage}/srv/";
                    Directory.CreateDirectory(packageDir);
                    string text = classInfo.ToCsString();
                    File.WriteAllText($"{packageDir}{classInfo.Name}.cs", text);
                }
                */
            }

            Console.WriteLine("** Done!");
        }
    }
}