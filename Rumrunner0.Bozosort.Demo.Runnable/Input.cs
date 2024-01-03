using System;
using System.IO;

namespace Rumrunner0.Bozosort.Demo.Runnable;

/// <summary>
/// Dummy input handling utility.
/// </summary>
internal static class Input
{
	/// <summary>
	/// Reads a line of input from a specified <see cref="TextReader" />
	/// and tries to parse it into a specified "<typeparamref name="T" />" type.
	/// </summary>
	/// <remarks>
	/// The method continuously reads lines using the <see cref="TextReader" />
	/// until it parses a string into an instance of the "<typeparamref name="T" />" type.
	/// The method doesn't manage the lifecycle of the <see cref="TextReader" />.
	/// </remarks>
	/// <param name="reader">The reader used to read the data.</param>
	/// <typeparam name="T">The type of the data.</typeparam>
	/// <returns>Data of "<typeparamref name="T"/>" type.</returns>
	internal static T Line<T>(TextReader reader) where T : IParsable<T>
	{
		if (reader is null)
		{
			throw new ArgumentNullException(nameof(reader));
		}

		if (reader.Peek() is -1)
		{
			throw new ArgumentException($"{nameof(TextReader)} isn't open or isn't ready to read.", nameof(reader));
		}

		while (true)
		{
			if (T.TryParse(reader.ReadLine()!.Trim(), provider: null, out var instance))
			{
				return instance;
			}
		}
	}
}