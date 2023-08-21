# bozosort
Implementation of the bozosort sorting algorithm.

This repository contains the `Rumble.Bozosort` class library and `Rumble.Bozosort.Demo.Runnable` console application. All the content in the repository is an original work created as a personal project, and serves as a .NET C# adaptation of the infamous bozosort algorithm.

[![License](https://img.shields.io/github/license/rumrunner0/bozosort?label=license)](https://github.com/rumrunner0/bozosort/blob/main/LICENSE)
[![Nuget](https://img.shields.io/nuget/v/Rumble.Bozosort?label=nuget)](https://www.nuget.org/packages/Rumble.Bozosort)

## Description
The `Rumble.Bozosort` is a .NET C# implementation of Bozosort — a highly inefficient sorting algorithm that operates by repeatedly selecting two elements of the list at random and swapping them if they are in the wrong order. This process continues until the entire list is sorted. Due to its random nature, Bozosort has an unbounded worst-case time complexity, and is typically used as a humorous example of how not to design an algorithm.

The `Rumble.Bozosort.Demo.Runnable` is a console application demonstrating the usage of the `Rumble.Bozosort` library.

## Usage
> **WARNING: The `Rumble.Bozosort` library is NOT intended for use in any real-world, production-level software. It is highly inefficient and is provided here for educational and entertainment purposes only.**

### Rumble.Bozosort Library
```csharp
using System;
using Rumble.Bozosort;

var sequence = new int[] { 3, 2, 4, 1, 5 };
var sorter = Bozosorter<int>.Factory.New();

sorter.Started += (sender, args) => Console.WriteLine($"Sorting has been started");
sorter.Completed += (sender, args) => Console.WriteLine($"Sorting has been completed in {args.ElapsedTime.TotalSeconds} seconds");
sorter.IterationCompleted += (sender, args) => Console.WriteLine
(
    $"Iteration: {args.IterationNumber}. " +
    $"Changes: {args.FirstElement} <=> {args.SecondElement}. " +
    $"Sequence: {string.Join(separator: " ", args.Sequence)}."
);

sorter.Run(sequence);
```

### Rumble.Bozosort.Demo.Runnable Console Application
After building the solution, run the `Rumble.Bozosort.Demo.Runnable` console application. It demonstrates a simple usage of the `Rumble.Bozosort` library.

## History
The original creator of the bozosort algorithm isn't definitively known, as it's more of a concept used to illustrate an inefficient sorting method rather than a formally recognized algorithm.

## Contributing
If you have any suggestions, ideas, or feedback to enhance the project, please feel free to create an issue. Your collaboration is welcomed to make this project a bit better.

## Note
The code is 100% original.
