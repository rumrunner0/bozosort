namespace Rumble.Bozosort;
internal static class SortingVerifier 
{
	internal enum SortingOrder 
	{
		Ascending,
		Descending
	}

	internal static bool IsSorted<TElement>(in IEnumerable<TElement> enumerable, SortingOrder order) 
	{
		var compare = (Func<TElement?, TElement?, bool>) (order switch {
			SortingOrder.Ascending  => bool (curr, next) => ((IComparable<TElement>)curr!).CompareTo(next) > 0,
			SortingOrder.Descending => bool (curr, next) => ((IComparable<TElement>)curr!).CompareTo(next) < 0,
			_ => throw new ArgumentOutOfRangeException(paramName: nameof(order), actualValue: order,
				message: $"Invalid sorting order value has been specified."
			)
		});

		var array = enumerable.ToArray();
		for(var i = 0; i < array.Length-1; i++)
			if(compare(array[i], array[i+1])) return false;

		return true;
	}
}