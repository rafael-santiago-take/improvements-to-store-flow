// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using ImprovementStoreFlows;

Console.WriteLine("Press enter to start");
Console.ReadLine();

Console.WriteLine("Processing...");

var summary = BenchmarkRunner.Run<TestDatabase>();

Console.WriteLine("Finished");
Console.ReadLine();
