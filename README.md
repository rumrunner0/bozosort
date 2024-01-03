# bozosort
Implementation of the Bozosort sorting algorithm.

This repository contains the `Rumrunner0.Bozosort` class library and `Rumrunner0.Bozosort.Demo.Runnable` console application. All the content in the repository is an original work created as a personal project, and serves as a .NET C# adaptation of the infamous bozosort algorithm.

[![License](https://img.shields.io/github/license/rumrunner0/bozosort?label=license)](https://github.com/rumrunner0/bozosort/blob/main/LICENSE)
[![Nuget](https://img.shields.io/nuget/v/Rumrunner0.Bozosort?logo=nuget&label=nuget)](https://www.nuget.org/packages/Rumrunner0.Bozosort)

## Description
The `Rumrunner0.Bozosort` is a .NET C# implementation of Bozosort — a highly inefficient sorting algorithm that operates by repeatedly selecting two elements of the list at random and swapping them if they are in the wrong order. This process continues until the entire list is sorted. Due to its random nature, Bozosort has an unbounded worst-case time complexity, and is typically used as a humorous example of how not to design an algorithm.

The `Rumrunner0.Bozosort.Demo.Runnable` is a console application demonstrating the usage of the `Rumrunner0.Bozosort` library.

## Usage
> **WARNING: The `Rumrunner0.Bozosort` library is NOT intended for use in any real-world, production-level software. It is highly inefficient and is provided here for educational and entertainment purposes only.**

### Rumrunner0.Bozosort Library
```csharp
using System;
using Rumrunner0.Bozosort;

// A Bozosorter instance is created to perform the sorting.
var sorter = new Bozosorter<int>();
var collection = (int[])[ 3, 2, 4, 1, 5 ];

// Events can be utilized to gain insight into the stage of the sorting process.
sorter.Started += (_, args) => Console.WriteLine("Sorting has been started.");
sorter.Completed += (_, args) => Console.WriteLine("Sorting has been completed.");

// Event arguments can be utilized to enrich debugging with extra details.
sorter.IterationCompleted += (_, args) => Console.WriteLine
(
    $"Iteration {args.Iteration}. " +
    $"Change {args.FirstItem} <=> {args.SecondItem}. " +
    $"Collection {string.Join(separator: " ", args.Collection)}."
);

// Method Run() is called to start the sorting process.
sorter.Run(collection);
```

### Rumrunner0.Bozosort.Demo.Runnable Console Application
After building the solution, run the `Rumrunner0.Bozosort.Demo.Runnable` console application. It demonstrates a more detailed usage of the `Rumrunner0.Bozosort` library.

## History
The original creator of the bozosort algorithm isn't definitively known, as it's more of a concept used to illustrate an inefficient sorting method rather than a formally recognized algorithm.

## Contributing
If you have any suggestions, ideas, or feedback to enhance the project, please feel free to create an issue. Your collaboration is welcomed to make this project a bit better.