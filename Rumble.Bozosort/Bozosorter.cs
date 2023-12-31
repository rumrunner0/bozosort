using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Rumble.Bozosort;

///
/// <inheritdoc />
///
public sealed class Bozosorter<TItem> : ISorter<TItem> where TItem : IComparable<TItem>
{
	///
	/// <inheritdoc />
	///
	public event EventHandler<SorterEventArgs<TItem>>? Started;

	///
	/// <inheritdoc />
	///
	public event EventHandler<SorterCompletedEventArgs<TItem>>? Completed;

	///
	/// <inheritdoc />
	///
	public event EventHandler<BozosorterIterationEventArgs<TItem>>? IterationCompleted;

	/// <summary>
	/// Constructor of the instance.
	/// </summary>
	public Bozosorter() { /* Empty. */ }

	///
	/// <inheritdoc />
	///
	public void Run(in IList<TItem> collection)
	{
		var iteration = 0;
		var stopwatch = new Stopwatch();

		Started?.Invoke(sender: this, new ()
		{
			Collection = collection
		});

		while(collection.IsOrdered() is false)
		{
			iteration++;
			stopwatch.Start();

			var indexes = collection.RandomUniqueIndexes(count: 2).ToArray();
			(collection[indexes[0]], collection[indexes[1]]) = (collection[indexes[1]], collection[indexes[0]]);

			stopwatch.Stop();
			IterationCompleted?.Invoke(sender: this, new ()
			{
				Collection = collection,
				Iteration = iteration,
				FirstItem  = collection[indexes[1]],
				SecondItem = collection[indexes[0]]
			});
		}

		Completed?.Invoke(sender: this, new ()
		{
			Collection = collection,
			Iteration = iteration,
			ElapsedTime = stopwatch.Elapsed
		});
	}
}