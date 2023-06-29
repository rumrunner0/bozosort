using System;
using System.Collections.Generic;

namespace Rumble.Bozosort;

/// <summary>
/// Sorter for a sequence of elements of type "<typeparamref name="TSortable"/>".
/// </summary>
/// <typeparam name="TSortable">Type of the sequence elements</typeparam>
public interface ISorter<TSortable> where TSortable : IComparable<TSortable>
{
	/// <summary>
	/// Event that is raised before the sorting is started.
	/// </summary>
	event EventHandler<SorterEventArgs<TSortable>>? Started;

	/// <summary>
	/// Event that is raised after the sorting is completed.
	/// </summary>
	event EventHandler<SorterCompletedEventArgs<TSortable>>? Completed;

	/// <summary>
	/// Event that is raised after each iteration of the sorting is completed. <br />
	/// Note, the event is always raised after the <see cref="Started"/> and before <see cref="Completed"/>.
	/// </summary>
	event EventHandler<BozosorterIterationEventArgs<TSortable>>? IterationCompleted;

	/// <summary>
	/// Runs the sorting on a given sequence.
	/// </summary>
	/// <param name="sequence">The sequence to sort</param>
	void Run(in IEnumerable<TSortable> sequence);
}