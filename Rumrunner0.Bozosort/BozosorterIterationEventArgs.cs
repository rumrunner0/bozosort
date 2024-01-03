namespace Rumrunner0.Bozosort;

/// <summary>
/// Data of the <see cref="Bozosorter{TSortable}" />.<see cref="Bozosorter{TSortable}.IterationCompleted" /> event.
/// </summary>
/// <typeparam name="TItem">Type of the collection items.</typeparam>
public sealed class BozosorterIterationEventArgs<TItem> : SorterEventArgs<TItem>
{
	/// <summary>
	/// Flag that indicates whether the items are changed.
	/// </summary>
	public required bool ItemsChanged { get; init; }

	/// <summary>
	/// First randomly picked item of the collection.
	/// </summary>
	public TItem? FirstItem { get; init; }

	/// <summary>
	/// Second randomly picked item of the collection.
	/// </summary>
	public TItem? SecondItem { get; init; }
}