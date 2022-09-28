using CreativeTrager.RandomSorting.Demo.Runnable;
using CreativeTrager.RandomSorting.Library;


var sorter = RandomSorter<int>.Factory.Create();
sorter.SortingBegin        += (_, sortingArgs) => Console.WriteLine($"Iteration {sortingArgs.IterationNumber}:\t{sortingArgs.Array.ToString(separator: " ")}");
sorter.SortingEnd          += (_, sortingArgs) => Console.WriteLine($"Integer sequence has been sorted in {sortingArgs.IterationNumber} iterations.");
sorter.SortingIterationEnd += (_, sortingArgs) => Console.WriteLine($"Iteration {sortingArgs.IterationNumber}:\t{sortingArgs.Array.ToString(separator: " ")}\t{sortingArgs.LeftElement} <=> {sortingArgs.RightElement}");

while(true) 
{
	var intInput = default(int);
	while(true) 
	{
		var input = Console.ReadLine()!;
		if(input.ToLower().Equals("q") is true) goto EndOfApplication;
		if(int.TryParse(input, out intInput) is true) break;
		Console.Clear();
	}

	sorter.Run(Enumerable.Range(0, intInput).Shuffle());
	Console.ReadLine();
	Console.Clear();
}

EndOfApplication:;