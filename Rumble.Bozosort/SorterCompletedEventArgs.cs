using System;

namespace Rumble.Bozosort;

/// <summary>
/// Data of the <see cref="ISorter{TSortable}" />.<see cref="ISorter{TSortable}.Completed" /> event.
/// </summary>
/// <typeparam name="TItem">Type of the sequence items.</typeparam>
public sealed class SorterCompletedEventArgs<TItem> : SorterEventArgs<TItem>
{
	/// <summary>
	/// Number of the sorting iteration.
	/// </summary>
	public required int Iteration { get; init; }

	/// <summary>
	/// Time elapsed since the sorting has started.
	/// </summary>
	public required TimeSpan ElapsedTime { get; init; }
}