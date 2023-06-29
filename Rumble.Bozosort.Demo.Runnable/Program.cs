using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Rumble.Bozosort;
using Rumble.Bozosort.Demo.Runnable;
using Rumble.Essentials;
using Serilog;

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;

var settings = Essential.OfType<Settings>();
Log.Logger = Essential.OfType<ILogger>();

var logger = Log.Logger.ForContext<Program>();
logger.Information("Application has been started");

var sorter = Bozosorter<int>.Factory.New();
sorter.Started += (_, sortingArgs) =>
{
	logger.Information
	(
		"{AlgorithmName} has been started to sort array: [{Array}]",
		nameof(Bozosorter<int>), sortingArgs.Sequence.Joined()
	);
};

sorter.IterationCompleted += (_, sortingArgs) =>
{
	logger.Information
	(
		"Iteration {IterationNumber}. Changes: {FirstElement} <=> {SecondElement}. Array: [{Array}].",
		sortingArgs.IterationNumber, sortingArgs.FirstElement,
		sortingArgs.SecondElement, sortingArgs.Sequence.Joined()
	);
};

sorter.Completed += (_, sortingArgs) =>
{
	logger.Information
	(
		"{AlgorithmName} has been completed. Sequence has been sorted in {IterationNumber} iterations",
		nameof(Bozosorter<int>), sortingArgs.IterationNumber
	);
};

var sequence = Enumerable.Range(0, Input.Line<int>(Console.In)).ToArray();
RandomNumberGenerator.Shuffle(sequence.AsSpan());
sorter.Run(sequence);

logger.Information("Application has been shut down");
logger.Information("");
Log.CloseAndFlush();