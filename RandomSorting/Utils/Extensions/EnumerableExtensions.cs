using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;


namespace RandomSorting.Utils.Extensions;
internal static class EnumerableExtensions 
{
	internal static string ValuesToString<TValue>
		(this IEnumerable<TValue> list, string separator) 
	{
		return string.Join(separator, list);
	}

	internal static IEnumerable<TElement> Shuffle<TElement>
		(this IEnumerable<TElement> enumerable) 
	{
		var array = enumerable.ToArray();
		var rightIndex = array.Length-1;
		while(rightIndex > 1) 
		{
			var leftIndex = RandomNumberGenerator.GetInt32(toExclusive: rightIndex);
			(array[leftIndex], array[rightIndex]) = (array[rightIndex], array[leftIndex]);
			rightIndex--;
		}
		return array;
	}
}