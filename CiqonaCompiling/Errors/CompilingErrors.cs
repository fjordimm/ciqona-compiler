
using System;

namespace CiqonaCompiling.Errors
{
	internal enum CompilingError
	{
		
	}

	internal static class CompilingErrors
	{
		public static void PrintError(string message)
		{
			Console.WriteLine($"Compiling Error: {message}");
		}
	}
}
