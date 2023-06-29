using System;
using System.Collections.Generic;
using System.Linq;

namespace Rumble.Bozosort;

///
/// <inheritdoc />
///
/// Uses "bozosort" as sorting algorithm.
public sealed class Bozosorter<TSortable> : ISorter<TSortable> where TSortable : IComparable<TSortable>
{
	///
	/// <inheritdoc />
	///
	public event EventHandler<SorterEventArgs<TSortable>>? Started;

	///
	/// <inheritdoc />
	///
	public event EventHandler<SorterCompletedEventArgs<TSortable>>? Completed;

	///
	/// <inheritdoc />
	///
	public event EventHandler<BozosorterIterationEventArgs<TSortable>>? IterationCompleted;

	/// <summary>
	/// Constructor of the instance.
	/// </summary>
	private Bozosorter()
	{
		// Empty
	}

	///
	/// <inheritdoc />
	///
	public void Run(in IEnumerable<TSortable> sequence)
	{
		var iterationNumber = 0;
		var array = sequence.ToArray();

		Started?.Invoke(sender: this, new ()
		{
			Sequence = array
		});

		while(true)
		{
			if(array.IsOrdered())
			{
				break;
			}

			iterationNumber++;

			var (aIndex, bIndex, _) = array.RandomUniqueIndexes(count: 2).Order().ToArray();
			(array[aIndex], array[bIndex]) = (array[bIndex], array[aIndex]);

			IterationCompleted?.Invoke(sender: this, new ()
			{
				IterationNumber = iterationNumber,
				Sequence = array,
				FirstElement  = array[bIndex],
				SecondElement = array[aIndex]
			});
		}

		Completed?.Invoke(sender: this, new ()
		{
			Sequence = array,
			IterationNumber = iterationNumber
		});
	}

	/// <summary>
	/// Factory for the <see cref="Bozosorter{TSortable}"/>.
	/// </summary>
	public static class Factory
	{
		/// <summary>
		/// Creates default instance of the <see cref="Bozosorter{TSortable}"/>.
		/// </summary>
		/// <returns>Default instance of the <see cref="Bozosorter{TSortable}"/></returns>
		public static ISorter<TSortable> New() => new Bozosorter<TSortable>();
	}
}