using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Rumble.Essentials;

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
		var stopwatch = new Stopwatch();

		Started?.Invoke(sender: this, new ()
		{
			Sequence = array
		});

		while(true)
		{
			stopwatch.Start();

			if(array.IsOrdered())
			{
				stopwatch.Stop();
				break;
			}

			iterationNumber++;

			var (aIndex, bIndex, _) = array.RandomUniqueIndexes(count: 2).Order().ToArray();
			(array[aIndex], array[bIndex]) = (array[bIndex], array[aIndex]);

			stopwatch.Stop();

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
			IterationNumber = iterationNumber,
			ElapsedTime = stopwatch.Elapsed
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