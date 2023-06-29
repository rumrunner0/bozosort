namespace Rumble.Bozosort;

/// <summary>
/// Data of the <see cref="Bozosorter{TSortable}"/>.<see cref="Bozosorter{TSortable}.IterationCompleted"/> event.
/// </summary>
/// <typeparam name="TSortable">Type of the sequence elements</typeparam>
public sealed class BozosorterIterationEventArgs<TSortable> : SorterEventArgs<TSortable>
{
	/// <summary>
	/// First randomly picked element of the sequence.
	/// </summary>
	public required TSortable FirstElement { get; init; }

	/// <summary>
	/// Second randomly picked element of the sequence.
	/// </summary>
	public required TSortable SecondElement { get; init; }

	/// <summary>
	/// Number of the sorting iteration.
	/// </summary>
	public required int IterationNumber { get; init; }
}