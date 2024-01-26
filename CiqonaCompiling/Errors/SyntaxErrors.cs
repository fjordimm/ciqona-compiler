
using System;
using CiqonaCompiling.Parsing;

namespace CiqonaCompiling.Errors
{
	internal enum SyntaxError
	{
		StatementLacksSemicolon,

		ExpectationForPrint
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

			if (err == SyntaxError.StatementLacksSemicolon)
			{ Console.WriteLine("End of statement is not marked by a semicolon."); }
			if (err == SyntaxError.ExpectationForPrint)
			{ Console.WriteLine("The print statement expects one string literal."); }
		}
	}
}
