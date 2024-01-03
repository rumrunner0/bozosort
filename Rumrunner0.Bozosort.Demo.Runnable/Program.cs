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

Console.Write("Enter the upper bound of the collection to sort >");
var collectionUpperBound = Input.Line<int>(reader: Console.In);
var collection = Enumerable.Range(0, collectionUpperBound).ToArray();
RandomNumberGenerator.Shuffle(collection.AsSpan());

var stopwatch = new Stopwatch();
var sorter = new Bozosorter<int>();

sorter.Started += OnSortingStarted;
sorter.Completed += OnSortingCompleted;
sorter.IterationCompleted += OnSortingIterationCompleted;

sorter.Run(collection);

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
		"{SorterName} has started sorting the collection {Collection}",
		nameof(Bozosorter<int>), args.Collection
	);

	stopwatch.Start();
}

void OnSortingIterationCompleted(object? _, BozosorterIterationEventArgs<int> args)
{
	stopwatch.Stop();

	var maxSingleOffsetLength = collectionUpperBound.ToString().Length;
	var firstItemOffset = string.Empty.PadRight(maxSingleOffsetLength - args.FirstItem.ToString().Length);
	var secondItemOffset = string.Empty.PadRight(maxSingleOffsetLength - args.SecondItem.ToString().Length);
	var sectionOffset = string.Empty.PadRight(maxSingleOffsetLength);

	if (args.ItemsChanged)
	{
		Log.Logger.Information
		(
			"Collection {Collection}. Change {FirstItemOffset}{FirstItem} <=> {SecondItemOffset}{SecondItem}.{SectionOffset}Iteration {Iteration}",
			args.Collection, firstItemOffset, args.FirstItem, secondItemOffset, args.SecondItem, sectionOffset, args.Iteration
		);
	}
	else
	{
		Log.Logger.Information
		(
			"Collection {Collection}. Change skipped.{SectionOffset}Iteration {Iteration}",
			args.Collection, sectionOffset + sectionOffset, args.Iteration
		);
	}

	stopwatch.Start();
}


void OnSortingCompleted(object? _, SorterEventArgs<int> args)
{
	stopwatch.Stop();

	Log.Logger.Information
	(
		"{SorterName} has completed sorting in {TotalSeconds}s and {Iteration}i",
		nameof(Bozosorter<int>), stopwatch.Elapsed.TotalSeconds, args.Iteration
	);
}