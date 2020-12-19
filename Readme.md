# Fast Object Filter
![Build and Test](https://github.com/DarkRiftNetworking/fast-object-filter/workflows/Build%20and%20Test/badge.svg) [![NuGet](https://buildstats.info/nuget/FastObjectFilter)](https://www.nuget.org/packages/FastObjectFilter/)

This library is designed for a future version of [DarkRift Networking](https://github.com/DarkRiftNetworking/DarkRift) to provide dynamic filtering in streams of objects.

## Background
### The Idea
Some DarkRift plugins will need to filter certain messages depending on rules that could be defined by a user at runtime or in configuration. Given the number of possibilities of these filters it is not easy to write custom logic to filter messages without limiting what combinations are possible.

Therefore this library provides compilation of custom filtering functions that can handle the speeds at which DarkRift needs without sacrificing the possibilities of those filters.

The filtering language is designed to look similar to C# or that use in Wireshark.

### The Implementation
Fast Object Filter works by using [Expressions](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/expressions) to compile the filter string into a `Func<bool, T>`.

## Usage
FastObjectFilter is available as a [NuGet package!](https://www.nuget.org/packages/FastObjectFilter/)

A simple usage would look like:
```csharp
var filter = new FastObjectFilterCompiler().Compile<DataObject>("Forename == \"Steve\"");

var data = new DataObject(Forename = "Steve", Surname = "Irwin", DOB = new DateTime(1962, 9, 22), LikesCats = true);

if (filter(data))
    Console.WriteLine("Match");
else
    Console.WriteLine("No match");
```

This would print out:
```
Match
```

Of course, this example is trivial, the real power of Fast Object Filter is found when a single filter string needs to be applied to thousands of different data objects.
