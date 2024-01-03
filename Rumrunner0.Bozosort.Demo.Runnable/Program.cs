using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Rumrunner0.Bozosort.Demo.Runnable;
using Rumrunner0.Bozosort;
using Rumrunner0.UuMatter.Console;
using Serilog;

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;

Log.Logger = UuMatter.OfType<ILogger>();
Log.Logger.Information("Application has been started");

var stopwatch = new Stopwatch();
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



//
// Callback methods.
//

void OnSortingStarted(object? _, SorterEventArgs<int> args)
{
	Log.Logger.Information
	(
		"{SorterName} has started sorting the sequence {Collection}",
		nameof(Bozosorter<int>), args.Collection
	);

	stopwatch.Start();
}

void OnSortingIterationCompleted(object? _, BozosorterIterationEventArgs<int> args)
{
	stopwatch.Stop();

	Console.WriteLine($"Collection {args.Collection}");
	Log.Logger.Information
	(
		"Iteration {Iteration}. Change {FirstItem} <=> {SecondItem}. Collection {Collection}",
		args.Iteration, args.FirstItem, args.SecondItem, args.Collection
	);

	stopwatch.Start();
}


void OnSortingCompleted(object? _, SorterEventArgs<int> args)
{
	stopwatch.Stop();

	Log.Logger.Information
	(
		"{SorterName} has completed sorting in {TotalSeconds}s and {Iteration} iterations",
		nameof(Bozosorter<int>), stopwatch.Elapsed.TotalSeconds, args.Iteration
	);
}