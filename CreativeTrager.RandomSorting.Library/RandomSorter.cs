using System.Security.Cryptography;


namespace CreativeTrager.RandomSorting.Library;
public sealed class RandomSorter<TSortable> 
{
	#region Data

	public event EventHandler<RandomSorterEventArgs>? SortingBegin;
	public event EventHandler<RandomSorterEventArgs>? SortingEnd;
	public event EventHandler<RandomSorterIterationEventArgs>? SortingIterationEnd;

	#endregion

	#region Interface

	public void Run(in IEnumerable<TSortable> enumerable) 
	{
		var iterationCount = 0;
		var enumeratedCollection = enumerable.ToArray();

		SortingBegin?.Invoke(sender: this, new () {
			IterationNumber = iterationCount,
			Array = enumeratedCollection
		});

		while(true) 
		{
			iterationCount++;

			var bIndex = default(int);
			var aIndex = RandomNumberGenerator.GetInt32(enumeratedCollection.Length);

			do  bIndex = RandomNumberGenerator.GetInt32(enumeratedCollection.Length);
			while(bIndex == aIndex);

			var indexes = new [] { aIndex, bIndex };
			var leftIndex = indexes.Min();
			var rightIndex = indexes.Max();

			(enumeratedCollection[aIndex], enumeratedCollection[bIndex])
				= (enumeratedCollection[bIndex], enumeratedCollection[aIndex]);

			SortingIterationEnd?.Invoke(sender: this, new () {
				IterationNumber = iterationCount,
				Array = enumeratedCollection,
				LeftElement = enumeratedCollection[leftIndex],
				RightElement = enumeratedCollection[rightIndex]
			});

			if(SortingVerifier.IsSorted(
				enumerable: enumeratedCollection, 
				order: SortingVerifier.SortingOrder.Ascending
			) is true) { break; }
		}

		SortingEnd?.Invoke(sender: this, new () {
			IterationNumber = iterationCount,
			Array = enumeratedCollection
		});
	}

	#endregion

	#region Utils

	private RandomSorter() 
	{
		// empty 
	}

	#endregion

	#region Related

	public static class Factory 
	{
		public static RandomSorter<TSortable> Create() => new ();
	}

	public class RandomSorterEventArgs : EventArgs 
	{
		public int IterationNumber { get; init; }
		public TSortable[] Array { get; init; } = null!;
	}

	public sealed class RandomSorterIterationEventArgs : RandomSorterEventArgs 
	{
		public TSortable LeftElement  { get; init; } = default!;
		public TSortable RightElement { get; init; } = default!;
	}

	#endregion
}