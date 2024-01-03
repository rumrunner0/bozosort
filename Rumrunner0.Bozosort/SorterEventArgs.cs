using System;
using System.Collections.Generic;

namespace Rumrunner0.Bozosort;

/// <summary>
/// Data of events raised by <see cref="ISorter{TSortable}" />.
/// </summary>
/// <typeparam name="TItem">Type of the collection items.</typeparam>
public class SorterEventArgs<TItem> : EventArgs
{
	/// <summary>
	/// Collection to sort.
	/// </summary>
	public required IList<TItem> Collection { get; init; }

	/// <summary>
	/// Number of the sorting iteration.
	/// </summary>
	public required int Iteration { get; init; }
}