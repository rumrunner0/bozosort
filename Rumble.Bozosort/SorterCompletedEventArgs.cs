namespace Rumble.Bozosort;

/// <summary>
/// Data of the <see cref="ISorter{TSortable}"/>.<see cref="ISorter{TSortable}.Completed"/> event.
/// </summary>
/// <typeparam name="TSortable">Type of the sequence elements</typeparam>
public class SorterCompletedEventArgs<TSortable> : SorterEventArgs<TSortable>
{
	/// <summary>
	/// Number of the sorting iteration.
	/// </summary>
	public required int IterationNumber { get; init; }
}