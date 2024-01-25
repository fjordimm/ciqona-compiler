
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace CiqonaCompiling.Parsing
{
    internal static class Parser
    {
        public static IEnumerable<Token> Parse(string ciqonaCode)
		{
			if (CiqonaCompiler.EnableCompilerPrintingWalkthrough) Console.WriteLine("===[ Started parsing... ]===");

			Dictionary<string, Tk> keywordDict = new();
			KeywordDict.Initialize(keywordDict);

			Queue<Token> tokenQue = new();

			int c = 0;
			string unitBuilder = "";
			while (c < ciqonaCode.Length)
			{
				bool isEndOfUnit = false;

				if (c == ciqonaCode.Length)
				{
					isEndOfUnit = true;
				}
				else
				{
					char chr = ciqonaCode[c];

					if (IsWhitespace(chr))
					{
						isEndOfUnit = true;
					}
					else
					{
						if (unitBuilder.Length == 0)
						{
							unitBuilder += chr;
						}
						else
						{
							if (unitBuilder[0] == '\'')
							{
								if (chr == '\'' && unitBuilder[^1] != '\\')
								{
									unitBuilder += chr;
									isEndOfUnit = true;
								}
								else
								{
									unitBuilder += chr;
								}
							}
							else if (unitBuilder[0] == '\"')
							{
								if (chr == '\"' && unitBuilder[^1] != '\\')
								{
									unitBuilder += chr;
									isEndOfUnit = true;
								}
								else
								{
									unitBuilder += chr;
								}
							}
							else if (chr == '\'' || chr == '\"')
							{
								isEndOfUnit = true;
								c--;
							}
							else if (IsDelineator(chr))
							{
								isEndOfUnit = true;
								c--;
							}
							else
							{
								unitBuilder += chr;
							}
						}
					}
				}

				// if at the end of the unit, push unitBuilder onto the stack and reset it
				if (isEndOfUnit)
				{
					if (unitBuilder.Length == 0)
					{
						// do nothing
					}
					else if (IsWhitespace(unitBuilder[0]))
					{
						// do nothing, but this shouldn't happen
						Console.WriteLine("This shouldn't print.");
					}
					else if (IsNum(unitBuilder[0]))
					{
						tokenQue.Enqueue(new Token(Tk.NumLiteral, unitBuilder));
					}
					else if (unitBuilder[0] == '\'')
					{
						tokenQue.Enqueue(new Token(Tk.CharLiteral, unitBuilder[1..^1]));
					}
					else if (unitBuilder[0] == '\"')
					{
						tokenQue.Enqueue(new Token(Tk.StringLiteral, unitBuilder[1..^1]));
					}
					else if (IsAbc(unitBuilder[0]))
					{
						if (keywordDict.ContainsKey(unitBuilder))
						{
							tokenQue.Enqueue(new Token(keywordDict[unitBuilder], ""));
						}
						else
						{
							tokenQue.Enqueue(new Token(Tk.Identifier, unitBuilder));
						}
					}
					else
					{
						if (keywordDict.ContainsKey(unitBuilder))
						{
							tokenQue.Enqueue(new Token(keywordDict[unitBuilder], ""));
						}
						else
						{
							tokenQue.Enqueue(new Token(Tk.Bruh, unitBuilder));
						}
					}

					unitBuilder = "";
				}

				c++;
			}

			if (CiqonaCompiler.EnableCompilerPrintingWalkthrough) Console.WriteLine("===[ Finished parsing ]===");
			return tokenQue;
		}

		internal static bool IsAbc(char the)
		{
			return Regex.IsMatch(the.ToString(), @"[a-zA-Z_]");
		}

		internal static bool IsWord(char the)
		{
			return Regex.IsMatch(the.ToString(), @"[a-zA-Z0-9_]");
		}

		internal static bool IsNum(char the)
		{
			return Regex.IsMatch(the.ToString(), @"[0-9]");
		}

		internal static bool IsWhitespace(char the)
		{
			return Regex.IsMatch(the.ToString(), @"\s");
		}

		internal static bool IsDelineator(char the)
		{
			return Regex.IsMatch(the.ToString(), @"[\{\}\[\]\(\)\;]");
		}
	}
}