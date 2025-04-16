using System.Runtime.CompilerServices;
using Xunit.Runners;

namespace TestRunner;

internal class Program {

    static async Task Main(string[] args) {
        try {
            using (ManualResetEventSlim finished = new()) {
                await using (AssemblyRunner runner = AssemblyRunner.WithoutAppDomain(GetTestContainerAssemblyName())) {
                    runner.OnDiscoveryComplete = (_) => {
                        Console.WriteLine("Discovery complete.");
                    };

                    runner.OnTestFinished = (x) => {
                        Console.WriteLine("Test complete: " + x.MethodName);
                    };

                    runner.OnExecutionComplete = (_) => {
                        Console.WriteLine("Execution complete.");
                        finished.Set();
                    };

                    runner.Start();
                    finished.Wait();
                }
            }

        } catch (Exception ex) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.ToString());
            Console.ResetColor();
        }

        Console.WriteLine("Press enter to exit.");
        Console.ReadLine();
    }


    private static string GetTestContainerAssemblyName([CallerFilePath] string thisFileName = "") {
        return Path.GetFullPath(Path.Combine(thisFileName, "../../TestContainer/bin/Debug/net9.0/TestContainer.dll"));
    }

}
