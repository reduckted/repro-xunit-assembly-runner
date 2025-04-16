Reproduction for not being able to reliably dispose an `AssemblyRunner` after the execution is complete.

Run using:
```
dotnet run --project ./TestRunner/TestRunner.csproj
```

Expected output is:
```
Discovery complete.
Test complete: Test1
Execution complete.
System.InvalidOperationException: Cannot dispose the assembly runner when it's not idle
   at Xunit.Runners.AssemblyRunner.DisposeAsync() in /_/src/xunit.v3.runner.utility/Runners/AssemblyRunner.cs:line 179
   at TestRunner.Program.Main(String[] args) in repro-xunit-assembly-runner\TestRunner\Program.cs:line 28
Press enter to exit.
```