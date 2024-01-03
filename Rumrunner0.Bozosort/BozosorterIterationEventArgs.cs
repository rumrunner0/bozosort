namespace Rumrunner0.Bozosort;

/// <summary>
/// Data of the <see cref="Bozosorter{TSortable}" />.<see cref="Bozosorter{TSortable}.IterationCompleted" /> event.
/// </summary>
/// <typeparam name="TItem">Type of the sequence items.</typeparam>
public sealed class BozosorterIterationEventArgs<TItem> : SorterEventArgs<TItem>
{
	/// <summary>
	/// First randomly picked item of the sequence.
	/// </summary>
	public required TItem FirstItem { get; init; }

	/// <summary>
	/// Second randomly picked item of the sequence.
	/// </summary>
	public required TItem SecondItem { get; init; }
}