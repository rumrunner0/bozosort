using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using CreativeTrager.RandomSorting.Entities;


// ReSharper disable UnassignedGetOnlyAutoProperty
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable JoinDeclarationAndInitializer
// ReSharper disable MemberCanBeMadeStatic.Global


namespace CreativeTrager.RandomSorting;
public sealed class RandomSorter<TElement> 
{
	public class RandomSorterEventArgs : EventArgs 
	{
		public int IterationNumber { get; init; }
		public TElement[] Array { get; init; } = null!;
	}
	public sealed class RandomSorterIterationEventArgs : RandomSorterEventArgs 
	{
		public TElement LeftElement  { get; init; } = default!;
		public TElement RightElement { get; init; } = default!;
	}

	public event EventHandler<RandomSorterEventArgs>? SortingBegin;
	public event EventHandler<RandomSorterEventArgs>? SortingEnd;
	public event EventHandler<RandomSorterIterationEventArgs>? SortingIterationEnd;

	public void Run(in IEnumerable<TElement> enumerable) 
	{
		var array = enumerable.ToArray();
		var iterationCount = 0;

		SortingBegin?.Invoke(sender: this, new () {
			IterationNumber = iterationCount, Array = array
		});

		while(true) 
		{
			iterationCount++;

			var bIndex = default(int);
			var aIndex = RandomNumberGenerator.GetInt32(array.Length);

			do  bIndex = RandomNumberGenerator.GetInt32(array.Length);
			while(bIndex == aIndex);

			var indexes = new [] { aIndex, bIndex };
			var leftIndex = indexes.Min();
			var rightIndex = indexes.Max();
			(array[aIndex], array[bIndex]) = (array[bIndex], array[aIndex]);

			SortingIterationEnd?.Invoke(sender: this, new () {
				IterationNumber = iterationCount, Array = array,
				LeftElement = array[leftIndex], RightElement = array[rightIndex]
			});

			if( SortingVerifier.IsSorted(array,
				SortingVerifier.SortingOrder.Ascending)
			) { break; }
		}

		SortingEnd?.Invoke(sender: this, new () {
			IterationNumber = iterationCount, Array = array
		});
	}
}