using System;
using System.IO;

namespace Rumble.Bozosort.Demo.Runnable;

/// <summary>
/// Basic and strait input handling utility.
/// </summary>
internal static class Input
{
	/// <summary>
	/// Reads a line using the <see cref="TextReader"/> and parses that data to "<typeparamref name="TParsable"/>".
	/// </summary>
	/// <param name="reader">Reader used to read the data</param>
	/// <typeparam name="TParsable">Type of the data</typeparam>
	/// <returns>Data of "<typeparamref name="TParsable"/>" type</returns>
	/// <exception cref="ApplicationException">Thrown if data can't be read or parsed to "<typeparamref name="TParsable"/>"</exception>
	internal static TParsable Line<TParsable>(TextReader reader) where TParsable : IParsable<TParsable>
	{
		ArgumentNullException.ThrowIfNull(reader);
		TParsable? parsedInput;

		try
		{
			var input = default(string);
			while
			(
				string.IsNullOrWhiteSpace(input) ||
				TParsable.TryParse(input, provider: null, out parsedInput) is false
			)
			{
				input = (reader.ReadLine() ?? string.Empty).Trim();
			}
		}
		catch(Exception e)
		{
			throw new ApplicationException
			(
				$"An error occured while reading and parsing input data. Details: {e.Message}",
				innerException: e
			);
		}

		return parsedInput;
	}
}