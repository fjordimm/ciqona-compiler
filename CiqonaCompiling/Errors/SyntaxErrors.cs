
using System;
using CiqonaCompiling.Parsing;

namespace CiqonaCompiling.Errors
{
	internal enum SyntaxError
	{
		PrintExpectation
	}

	internal static class SyntaxErrors
	{
		public static void PrintError(LineColTrace lineColTrace, string message)
		{
			Console.WriteLine($"Ciqona Syntax Error ({lineColTrace}): {message}");
		}

		public static void PrintError(LineColTrace lineColTrace, SyntaxError err)
		{
			Console.Write($"Ciqona Syntax Error ({lineColTrace}): ");

			if (err == SyntaxError.PrintExpectation)
			{ Console.WriteLine("The print statement expects one string literal."); }
		}
	}
}
