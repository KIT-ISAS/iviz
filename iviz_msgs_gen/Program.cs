using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Iviz.MsgsGen
{
    static class Program
    {
        static void Main(string[] args)
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
                Console.WriteLine("EE Invalid arguments. Use -h to show usage.");
                return;
            }

            string fullOutputPath = Path.GetFullPath(args[1]);

            switch (args[2])
            {
                case "-p":
                case "--package":
                {
                    string package = args[3];

                    if (args.Length < 5)
                    {
                        Console.WriteLine("EE Invalid arguments. Use -h to show usage.");
                        return;
                    }

                    switch (args[4])
                    {
                        case "-i":
                        case "--input":
                            CreateIndividualMessages(args, fullOutputPath, package);
                            break;
                        case "-if":
                        case "--input-folder":
                            CreateMessagesFromFolder(args, fullOutputPath, package);
                            break;
                        default:
                            Console.WriteLine($"EE Unknown argument {args[4]}. Use -h to show usage.");
                            break;
                    }

                    break;
                }
                default:
                    Console.WriteLine($"EE Unknown argument {args[2]}. Use -h to show usage.");
                    break;
            }
        }

        static void CreateMessagesFromFolder(string[] args, string fullOutputPath, string package)
        {
            if (args.Length < 6)
            {
                Console.WriteLine($"EE Expected folder path after -if or --input-folder.");
                return;
            }

            string inputPath = args[5];
            string fullInputPath = Path.GetFullPath(inputPath);
            if (!Directory.Exists(fullInputPath))
            {
                Console.WriteLine($"EE Input path '{fullOutputPath}' does not exist.");
                return;
            }

            Directory.CreateDirectory(fullOutputPath);

            PackageInfo p = new PackageInfo();
            p.AddAllInPackagePath(fullInputPath, package);

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

            foreach (ClassInfo classInfo in p.Messages.Values)
            {
                string text = classInfo.ToCsString();
                File.WriteAllText($"{fullOutputPath}/msg/{classInfo.Name}.cs", text);
            }

            foreach (ServiceInfo classInfo in p.Services.Values)
            {
                string text = classInfo.ToCsString();
                File.WriteAllText($"{fullOutputPath}/srv/{classInfo.Name}.cs", text);
            }

            Console.WriteLine("** Done!");
        }

        static void PrintHelpMessage()
        {
            Console.WriteLine("** Welcome to Iviz.MsgsGen, an utility to generate C# code from ROS messages.");
            Console.WriteLine("Usage:");
            Console.WriteLine("    dotnet Iviz.MsgsGen.dll -h");
            Console.WriteLine("        Shows this text");
            Console.WriteLine("    dotnet Iviz.MsgsGen.dll -p Package -o OutputFolder -i Files... ");
            Console.WriteLine("        Converts the given files (.msg, .srv, .action) into C# messages, and writes the result in OutputFolder");
            Console.WriteLine("    dotnet Iviz.MsgsGen.dll -p Package -o OutputFolder -if InputFolder");
            Console.WriteLine("        Searches all files in the input folder (.msg, .srv, .action), convers them into C# messages, and writes the result in OutputFolder");
            Console.WriteLine("    dotnet Iviz.MsgsGen.dll -r");
            Console.WriteLine("        Regenerates the default Iviz message files from the ros_msgs folder");
            Console.WriteLine();
        }

        static void CreateIndividualMessages(string[] args, string fullOutputPath, string package)
        {
            if (!Directory.Exists(fullOutputPath))
            {
                Console.WriteLine($"EE Output path '{fullOutputPath}' does not exist.");
                return;
            }

            List<string> messageFullPaths = new List<string>();
            List<string> serviceFullPaths = new List<string>();
            List<string> actionFullPaths = new List<string>();

            foreach (string path in args.Skip(5))
            {
                string absolutePath = Path.GetFullPath(path);
                if (!File.Exists(absolutePath))
                {
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

            string debugRosBasePath = Path.GetFullPath("../../../../ros_msgs");
            string debugIvizMsgPaths = Path.GetFullPath("../../../../iviz_msgs");

            string releaseRosBasePath = Path.GetFullPath("../ros_msgs");
            string releaseIvizMsgPaths = Path.GetFullPath("../iviz_msgs");

            Console.WriteLine("** Starting iviz_msgs_gen...");

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
                Console.WriteLine(
                    "EE Failed to find the iviz_msgs and ros_msgs folders. They should be in the ../ folder from where you run this.");
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

            foreach (ClassInfo classInfo in p.Messages.Values)
            {
                string dstPackageDir = $"{ivizMsgPaths}/{classInfo.RosPackage}/msg/";
                Directory.CreateDirectory(dstPackageDir);
                string text = classInfo.ToCsString();
                File.WriteAllText($"{dstPackageDir}{classInfo.Name}.cs", text);
            }

            foreach (ServiceInfo classInfo in p.Services.Values)
            {
                string packageDir = $"{ivizMsgPaths}/{classInfo.RosPackage}/srv/";
                Directory.CreateDirectory(packageDir);
                string text = classInfo.ToCsString();
                File.WriteAllText($"{packageDir}{classInfo.Name}.cs", text);
            }

            Console.WriteLine("** Done!");
        }
    }
}