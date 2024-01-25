
using System;
using System.Collections.Generic;
using CiqonaCompiling.Errors;
using CiqonaCompiling.Parsing;

namespace CiqonaCompiling.Translation
{
	internal static class Translator
	{
		public static string Translate(IEnumerable<Token> tokens)
		{
			if (CiqonaCompiler.EnableCompilerPrintingWalkthrough) Console.WriteLine("===[ Started abstractifying... ]===");

			CodeBlock mainBlock = new();

			IEnumerator<Token> tokensEnumer = tokens.GetEnumerator();
			while(tokensEnumer.MoveNext())
			{
				Token tok = tokensEnumer.Current;

				// if (tok.tk == Tk.)

				///*
				if (tok.tk == Tk.K_print)
				{
					// Make a list of all tokens up until the next semicolon
					List<Token> restOfLine = new();
					do
					{
						if (!tokensEnumer.MoveNext()) break;
						if (tokensEnumer.Current.tk == Tk.Semicolon) break;

						restOfLine.Add(tokensEnumer.Current);
					}
					while (true);

					if (restOfLine.Count != 1)
					{ CompilerErrors.PrintCompilerError(tokensEnumer.Current.lineColTrace, CompilerError.PrintExpectation); return _failedInC; }

					if (restOfLine[0].tk != Tk.StringLiteral)
					{ CompilerErrors.PrintCompilerError(tokensEnumer.Current.lineColTrace, CompilerError.PrintExpectation); return _failedInC; }
					
					mainBlock.AddCodeStatement(new CodeStatementPrint(new ExpressionStringLiteral(restOfLine[0].contents)));
				}
				//*/
			}

			AbstractedProgram abstractedProgram = new(mainBlock);
			if (CiqonaCompiler.EnableCompilerPrintingWalkthrough) Console.WriteLine("===[ Finished abstractifying ]===");
			
			if (CiqonaCompiler.EnableCompilerPrintingWalkthrough) Console.WriteLine("===[ Translating... ]===");
			return abstractedProgram.InC();
		}

		private const string _failedInC = "";
	}
}