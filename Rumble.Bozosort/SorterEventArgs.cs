using System;
using System.Collections.Generic;

namespace Rumble.Bozosort;

/// <summary>
/// Data of events raised by <see cref="ISorter{TSortable}"/>.
/// </summary>
/// <typeparam name="TSortable">Type of the sequence elements</typeparam>
public class SorterEventArgs<TSortable> : EventArgs
{
	/// <summary>
	/// Sequence to sort.
	/// </summary>
	public required IEnumerable<TSortable> Sequence { get; init; }
}