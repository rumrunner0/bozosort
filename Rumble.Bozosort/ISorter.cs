using System;
using System.Collections.Generic;

namespace Rumble.Bozosort;

/// <summary>
/// Sorter for a collection of items of type "<typeparamref name="TItem"/>".
/// </summary>
/// <typeparam name="TItem">Type of the collection items.</typeparam>
public interface ISorter<TItem> where TItem : IComparable<TItem>
{
	/// <summary>
	/// Event that is raised before the sorting is started.
	/// </summary>
	event EventHandler<SorterEventArgs<TItem>>? Started;

	/// <summary>
	/// Event that is raised after the sorting is completed.
	/// </summary>
	event EventHandler<SorterCompletedEventArgs<TItem>>? Completed;

	/// <summary>
	/// Event that is raised after each iteration of the sorting is completed. <br />
	/// Note, the event is always raised after the <see cref="Started"/> and before <see cref="Completed"/>.
	/// </summary>
	event EventHandler<BozosorterIterationEventArgs<TItem>>? IterationCompleted;

	/// <summary>
	/// Runs the sorting on a given collection.
	/// </summary>
	/// <param name="collection">The collection to sort.</param>
	void Run(in IList<TItem> collection);
}