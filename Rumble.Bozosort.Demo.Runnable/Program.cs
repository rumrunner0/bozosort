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

Log.Logger = Essential.OfType<ILogger>();
var logger = Log.Logger.ForContext<Program>();
logger.Information("Application has been started");

var sorter = Bozosorter<int>.Factory.Default();
sorter.Started += (_, args) =>
{
	logger.Information
	(
		"{AlgorithmName} has started sorting the sequence: {Sequence}",
		nameof(Bozosorter<int>), args.Sequence
	);
};

sorter.IterationCompleted += (_, args) =>
{
	logger.Information
	(
		"Iteration {IterationNumber}. " +
		"Changes: {FirstElement} <=> {SecondElement}. " +
		"Sequence: {Sequence}",

		args.IterationNumber,
		args.FirstElement, args.SecondElement,
		args.Sequence
	);
};

sorter.Completed += (_, args) =>
{
	logger.Information
	(
		"{AlgorithmName} has completed sorting the sequence. " +
		"The sequence has been sorted in {TotalSeconds} seconds and {IterationNumber} iterations",
		nameof(Bozosorter<int>), args.ElapsedTime.TotalSeconds, args.IterationNumber
	);
};

logger.Information("Asked user to enter the upper bound of the sequence");
Console.Write($"Please, enter the upper bound of the sequence: ");

var sequenceUpperBound = Input.Line<int>(reader: Console.In);
var sequence = Enumerable.Range(0, sequenceUpperBound).ToArray();
RandomNumberGenerator.Shuffle(sequence.AsSpan());

sorter.Run(sequence);

logger.Information("Application has been shut down");
logger.Information("");
Log.CloseAndFlush();