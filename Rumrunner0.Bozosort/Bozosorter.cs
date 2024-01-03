using System;
using System.Collections.Generic;
using System.Linq;

namespace Rumrunner0.Bozosort;

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
	public event EventHandler<SorterEventArgs<TItem>>? Completed;

	///
	/// <inheritdoc />
	///
	public event EventHandler<BozosorterIterationEventArgs<TItem>>? IterationCompleted;

	///
	/// <inheritdoc />
	///
	public void Run(in IList<TItem> collection)
	{
		var iteration = 0;
		Started?.Invoke(sender: this, new ()
		{
			Collection = collection,
			Iteration = iteration
		});

		while(collection.IsOrdered() is false)
		{
			iteration++;
			var indexes = collection.RandomUniqueIndexes(count: 2).ToArray();
			(collection[indexes[0]], collection[indexes[1]]) = (collection[indexes[1]], collection[indexes[0]]);

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
			Iteration = iteration
		});
	}
}