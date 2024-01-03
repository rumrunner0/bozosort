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

			var indexes = collection.RandomUniqueIndexes(count: 2).Order().ToArray();
			var (i1, i2) = (indexes[0], indexes[1]);
			var itemsChanged = false;

			if (collection[i1].CompareTo(collection[i2]) >= 0)
			{
				(collection[i1], collection[i2]) = (collection[i2], collection[i1]);
				itemsChanged = true;
			}

			IterationCompleted?.Invoke(sender: this, new ()
			{
				Collection = collection,
				Iteration = iteration,
				FirstItem = collection[i2],
				SecondItem = collection[i1],
				ItemsChanged = itemsChanged
			});
		}

		Completed?.Invoke(sender: this, new ()
		{
			Collection = collection,
			Iteration = iteration
		});
	}
}