
using System;
using CiqonaCompiling.Parsing;

namespace CiqonaCompiling.Errors
{
	internal enum CompilerError
	{
		PrintExpectation
	}

	internal static class CompilerErrors
	{
		public static void PrintCompilerError(LineColTrace lineColTrace, string message)
		{
			Console.WriteLine($"CIQONA COMPILER ERROR ({lineColTrace}): {message}");
		}

		public static void PrintCompilerError(LineColTrace lineColTrace, CompilerError compilerError)
		{
			Console.Write($"CIQONA COMPILER ERROR ({lineColTrace}): ");

			if (compilerError == CompilerError.PrintExpectation)
			{ Console.WriteLine("The print statement expects one string literal."); }
		}
	}
}
