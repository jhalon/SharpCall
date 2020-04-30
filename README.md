# SharpCall

Simple proof of concept code that allows you to execute direct system calls in C# by utilizing unmanaged code to bypass EDR and API Hooking.


This Proof of Concept is directly related to the following blog posts:

* [Red Team Tactics: Utilizing Syscalls in C# - Prerequisite Knowledge](https://jhalon.github.io/utilizing-syscalls-in-csharp-1/)
* [Red Team Tactics: Utilizing Syscalls in C# - Writing The Code](https://jhalon.github.io/utilizing-syscalls-in-csharp-2/)

### File Structure:

* __Native.cs__: Contains all the Native Window API function calls, as well as the necessary structures, and flag enumerators.
* __Syscalls.cs__: Contains the delegate definition and delegate implementation used to execute our syscall assembly from unmanaged memory.
* __Program.cs__: Main program application that utilizes our implemented syscall delegate to execute the syscall.
