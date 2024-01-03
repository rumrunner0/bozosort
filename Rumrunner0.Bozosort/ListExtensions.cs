using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Rumrunner0.Bozosort;

/// <summary>
/// Extensions for the type <see cref="IList{T}"/>.
/// </summary>
internal static class ListExtensions
{
	/// <summary>
	/// Generates a number of random unique indexes within a collection.
	/// </summary>
	/// <param name="source">The collection from which indexes are generated.</param>
	/// <param name="count">The number of random unique indexes to generate.</param>
	/// <typeparam name="TItem">The type of items in the collection.</typeparam>
	/// <returns>Random unique indexes within the collection.</returns>
	/// <exception cref="ArgumentNullException">Thrown if "<paramref name="source"/>" is <c>null</c>.</exception>
	/// <exception cref="ArgumentOutOfRangeException">Thrown if "<paramref name="count"/>" is out of "<paramref name="source"/>" range.</exception>
	internal static IEnumerable<int> RandomUniqueIndexes<TItem>(this IList<TItem> source, int count)
	{
		if (source is null)
		{
			throw new ArgumentNullException(nameof(source));
		}

		if (count < 0 || count > source.Count)
		{
			throw new ArgumentOutOfRangeException($"Requested count ({count}) is out of \"{nameof(source)}\" range.");
		}

		var indexes = new HashSet<int>();
		while (indexes.Count < count)
		{
			if (RandomNumberGenerator.GetInt32(source.Count) is var index && indexes.Add(index))
			{
				yield return index;
			}
		}
	}

	/// <summary>
	/// Checks whether a list is sorted in ascending order.
	/// </summary>
	/// <param name="source">The list of items to check.</param>
	/// <typeparam name="TItem">The type of list items.</typeparam>
	/// <returns><c>true</c>, if list is sorted in ascending order, <c>false</c>, otherwise.</returns>
	/// <exception cref="ArgumentNullException">Thrown if "<paramref name="source"/>" is <c>null</c>.</exception>
	public static bool IsOrdered<TItem>(this IList<TItem> source) where TItem : IComparable<TItem>
	{
		if (source is null)
		{
			throw new ArgumentNullException(nameof(source));
		}

		for (var i = 0; i < source.Count - 1; i++)
		{
			if (source[i].CompareTo(source[i + 1]) > 0)
			{
				return false;
			}
		}

		return true;
	}
}