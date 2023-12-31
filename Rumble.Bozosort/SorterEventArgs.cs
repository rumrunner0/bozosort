using System;
using System.Collections.Generic;

namespace Rumble.Bozosort;

/// <summary>
/// Data of events raised by <see cref="ISorter{TSortable}" />.
/// </summary>
/// <typeparam name="TItem">Type of the sequence items.</typeparam>
public class SorterEventArgs<TItem> : EventArgs
{
	/// <summary>
	/// Collection to sort.
	/// </summary>
	public required IList<TItem> Collection { get; init; }
}