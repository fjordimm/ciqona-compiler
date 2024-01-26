
using System;
using System.Collections.Generic;
using System.IO;
using CiqonaCompiling.Errors;
using CiqonaCompiling.Parsing;

namespace CiqonaCompiling.Translation
{
	internal static class Translator
	{
		/*
		public static void Translate(StreamWriter sw, List<Token> tokens)
		{
			if (CiqonaCompiler.EnableCompilerPrintingWalkthrough) Console.WriteLine("===[ Started abstractifying... ]===");

			CodeBlock mainBlock = new();

			IEnumerator<Token> tokensEnumer = tokens.GetEnumerator();
			while(tokensEnumer.MoveNext())
			{
				Token tok = tokensEnumer.Current;

				// if (tok.tk == Tk.)

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
					{ SyntaxErrors.PrintError(tokensEnumer.Current.lineColTrace, SyntaxError.PrintExpectation); }

					if (restOfLine[0].tk != Tk.StringLiteral)
					{ SyntaxErrors.PrintError(tokensEnumer.Current.lineColTrace, SyntaxError.PrintExpectation); }
					
					mainBlock.AddCodeStatement(new CodeStatementPrint(new ExpressionStringLiteral(restOfLine[0].contents)));
				}
			}

			AbstractedProgram abstractedProgram = new(mainBlock);
			if (CiqonaCompiler.EnableCompilerPrintingWalkthrough) Console.WriteLine("===[ Finished abstractifying ]===");
			
			if (CiqonaCompiler.EnableCompilerPrintingWalkthrough) Console.WriteLine("===[ Translating... ]===");
			return abstractedProgram.InC();
		}
		*/
		
		public static void Translate(StreamWriter sw, List<Token> tokenList)
		{
			if (CiqonaCompiler.EnableCompilerPrintingWalkthrough) Console.WriteLine("===[ Started translating... ]===");

			if (CiqonaCompiler.EnableCompilerPrintingWalkthrough) Console.WriteLine("===[ Started abstractifying... ]===");
			
			CodeBlock mainBlock = new();

			int cur = 0;
			while (cur < tokenList.Count)
			{
				Token tok = tokenList[cur];

				if (tok.tk == Tk.K_print)
				{
					int curB = cur;
					while (tokenList[curB].tk != Tk.Semicolon)
					{
						if (curB >= tokenList.Count - 1)
						{ SyntaxErrors.PrintError(tok.lineColTrace, SyntaxError.StatementLacksSemicolon); }
						else
						{ curB++; }
					}

					if (curB - cur != 2)
					{ SyntaxErrors.PrintError(tok.lineColTrace, SyntaxError.ExpectationForPrint); break; }

					if (tokenList[cur + 1].tk != Tk.StringLiteral)
					{ SyntaxErrors.PrintError(tok.lineColTrace, SyntaxError.ExpectationForPrint); break; }

					mainBlock.AddCodeStatement(new CodeStatementPrint(new ExpressionStringLiteral(tokenList[cur + 1].contents)));
				}

				cur++;
			}

			AbstractedProgram abstractedProgram = new(mainBlock);

			if (CiqonaCompiler.EnableCompilerPrintingWalkthrough) Console.WriteLine("===[ Finished abstractifying ]===");

			abstractedProgram.WriteToC(sw);

			if (CiqonaCompiler.EnableCompilerPrintingWalkthrough) Console.WriteLine("===[ Finished translating ]===");
		}

		private const string _failedInC = "";
	}
}