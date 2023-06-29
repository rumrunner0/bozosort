using System;
using System.IO;

namespace Rumble.Bozosort.Demo.Runnable;

internal static class Input
{
	internal static TParsable Line<TParsable>(TextReader source) where TParsable : IParsable<TParsable>
	{
        ArgumentNullException.ThrowIfNull(source);

        var input = default(string);
		while(string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
		{
			input = (source.ReadLine() ?? string.Empty).Trim();
		}

		if(TParsable.TryParse(input, provider: null, out var parsedInput) is false)
		{
			throw new IOException
			(
				$""
			);
		}

		return parsedInput;
	}
}