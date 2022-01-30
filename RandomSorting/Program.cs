using System;
using System.Linq;
using System.Text;
using RandomSorting;
using RandomSorting.Utils.Extensions;


var sorter = new RandomSorter<int>();

sorter.SortingBegin += (_, sortingArgs) => Console.WriteLine(
	new StringBuilder()
		.Append($"Iteration {sortingArgs.IterationNumber}:\t")
		.Append($"{sortingArgs.Array.ValuesToString(separator: " ")}")
);

sorter.SortingEnd += (_, sortingArgs) => Console.WriteLine(
	new StringBuilder()
		.Append($"Integer sequence has been sorted in ")
		.Append($"{sortingArgs.IterationNumber} iterations.")
);

sorter.SortingIterationEnd += (_, sortingArgs) => Console.WriteLine(
	new StringBuilder()
		.Append($"Iteration {sortingArgs.IterationNumber}:\t")
		.Append($"{sortingArgs.Array.ValuesToString(separator: " ")}\t")
		.Append($"{sortingArgs.LeftElement} <=> {sortingArgs.RightElement}")
);

while(true) 
{
	var intInput = default(int);
	while(true) 
	{
		var input = Console.ReadLine()!;
		if(input.ToLower().Equals("q")) goto Exit;
		if(int.TryParse(input, out intInput) is true) break;
		Console.Clear();
	}

	sorter.Run(enumerable: Enumerable.Range(0, intInput).Shuffle());
	Console.ReadLine();
	Console.Clear();
}

Exit:;