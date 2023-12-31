using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Rumble.Bozosort;
using Rumble.Bozosort.Demo.Runnable;
using Rumrunner0.UuMatter.Console;
using Serilog;

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;

Log.Logger = UuMatter.OfType<ILogger>();
Log.Logger.Information("Application has been started");

var sorter = new Bozosorter<int>();
sorter.Started += OnSortingStarted;
sorter.Completed += OnSortingCompleted;
sorter.IterationCompleted += OnSortingIterationCompleted;

Console.Write("Enter the upper bound of the collection to sort: ");

var sequenceUpperBound = Input.Line<int>(reader: Console.In);
var sequence = Enumerable.Range(0, sequenceUpperBound).ToArray();
RandomNumberGenerator.Shuffle(sequence.AsSpan());

sorter.Run(sequence);

Log.Logger.Information("Application has been shut down");
Log.Logger.Information("");
Log.CloseAndFlush();

return;

void OnSortingStarted(object? _, SorterEventArgs<int> args)
{
	Log.Logger.Information
	(
		"{SorterName} has started sorting the sequence {Collection}",
		nameof(Bozosorter<int>), args.Collection
	);
}

void OnSortingCompleted(object? _, SorterCompletedEventArgs<int> args)
{
	Log.Logger.Information
	(
		"{SorterName} has completed sorting in {TotalSeconds}s and {Iteration} iterations",
		nameof(Bozosorter<int>), args.ElapsedTime.TotalSeconds, args.Iteration
	);
}

void OnSortingIterationCompleted(object? _, BozosorterIterationEventArgs<int> args)
{
	Log.Logger.Information
	(
		"Iteration {Iteration}. Change {FirstItem} <=> {SecondItem}. Collection {Collection}",
		args.Iteration, args.FirstItem, args.SecondItem, args.Collection
	);
}