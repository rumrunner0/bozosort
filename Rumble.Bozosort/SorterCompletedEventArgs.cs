using System;

namespace Rumble.Bozosort;

/// <summary>
/// Data of the <see cref="ISorter{TSortable}"/>.<see cref="ISorter{TSortable}.Completed"/> event.
/// </summary>
/// <typeparam name="TSortable">Type of the sequence elements</typeparam>
public sealed class SorterCompletedEventArgs<TSortable> : SorterEventArgs<TSortable>
{
	/// <summary>
	/// Number of the sorting iteration.
	/// </summary>
	public required int IterationNumber { get; init; }

	/// <summary>
	/// Time elapsed since the sorting start.
	/// </summary>
	public required TimeSpan ElapsedTime { get; init; }
}