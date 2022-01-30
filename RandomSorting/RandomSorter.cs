using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using RandomSorting.Utils.Entities;


// ReSharper disable UnassignedGetOnlyAutoProperty
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable JoinDeclarationAndInitializer
// ReSharper disable MemberCanBeMadeStatic.Global


namespace RandomSorting;
internal sealed class RandomSorter<TElement> 
{
	internal class RandomSorterEventArgs : EventArgs 
	{
		internal int IterationNumber { get; init; }
		internal TElement[] Array { get; init; } = null!;
	}
	internal sealed class RandomSorterIterationEventArgs : RandomSorterEventArgs 
	{
		internal TElement LeftElement  { get; init; } = default!;
		internal TElement RightElement { get; init; } = default!;
	}

	internal event EventHandler<RandomSorterEventArgs>? SortingBegin;
	internal event EventHandler<RandomSorterEventArgs>? SortingEnd;
	internal event EventHandler<RandomSorterIterationEventArgs>? SortingIterationEnd;

	internal void Run(in IEnumerable<TElement> enumerable) 
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