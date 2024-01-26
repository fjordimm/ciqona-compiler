
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using CiqonaCompiling.Errors;

namespace CiqonaCompiling.Parsing
{
    internal static class Parser
    {
		/*
        public static Token[] Parse(string ciqonaCode)
		{
			if (CiqonaCompiler.EnableCompilerPrintingWalkthrough) Console.WriteLine("===[ Started parsing... ]===");

			Dictionary<string, Tk> keywordDict = new();
			KeywordDict.Initialize(keywordDict);

			Queue<Token> tokenQue = new();

			int c = 0;
			int lineNum = 0;
			int colNum = 0;
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
							tokenQue.Enqueue(new Token(Tk.BadToken, unitBuilder));
						}
					}

					unitBuilder = "";
				}

				c++;
			}

			if (CiqonaCompiler.EnableCompilerPrintingWalkthrough) Console.WriteLine("===[ Finished parsing ]===");
			return tokenQue.ToArray();
		}
		*/

		public static List<Token> Parse(string ciqonaFilePath)
		{
			if (CiqonaCompiler.EnableCompilerPrintingWalkthrough) Console.WriteLine("===[ Started parsing... ]===");

			List<Token> tokenList = new();

			try
			{
				using (StreamReader sr = new(ciqonaFilePath))
				{
					int lineNum = 0;
					string line;
					while ((line = sr.ReadLine()) != null)
					{
						lineNum++;
						ParseLine(tokenList, lineNum, line);
					}
				}
			}
			catch
			{
				CompilingErrors.PrintError($"Error reading file '{ciqonaFilePath}'.");
			}

			if (CiqonaCompiler.EnableCompilerPrintingWalkthrough) Console.WriteLine("===[ Finished parsing ]===");
			
			// debug print
			// Console.WriteLine("tokens:");
			// Console.WriteLine("[");
			// foreach (Token t in tokenList)
			// {
			// 	Console.WriteLine($"  {t}");
			// }
			// Console.WriteLine("]");
			
			return tokenList;
		}

		private static void ParseLine(List<Token> tokenList, int lineNum, string line)
		{
			int colNum = 0;
			StringBuilder unitBuilder = new();
			while (colNum <= line.Length)
			{
				bool isEndOfUnit = false;

				if (colNum == line.Length)
				{ isEndOfUnit = true; }
				else
				{
					char chr = line[colNum];

					if (IsWhitespace(chr))
					{ isEndOfUnit = true; }
					else
					{
						if (unitBuilder.Length == 0)
						{
							unitBuilder.Append(chr);
						}
						else
						{
							if (unitBuilder[0] == '\'')
							{
								unitBuilder.Append(chr);
								if (chr == '\'' && unitBuilder[^1] != '\\')
								{ isEndOfUnit = true; }
							}
							else if (unitBuilder[0] == '\"')
							{
								unitBuilder.Append(chr);
								if (chr == '\"' && unitBuilder[^1] != '\\')
								{ isEndOfUnit = true; }
							}
							else if (chr == '\'' || chr == '\"')
							{
								isEndOfUnit = true;
								colNum--;
							}
							else if (IsDelineator(chr))
							{
								isEndOfUnit = true;
								colNum--;
							}
							else
							{
								unitBuilder.Append(chr);
							}
						}
					}
				}

				// If at the end of the unit, push unitBuilder onto the tokenList and reset it
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
						tokenList.Add(new Token(Tk.NumLiteral, unitBuilder.ToString(), new LineColTrace(lineNum, colNum)));
					}
					else if (unitBuilder[0] == '\'')
					{
						tokenList.Add(new Token(Tk.CharLiteral, unitBuilder.ToString()[1..^1], new LineColTrace(lineNum, colNum)));
					}
					else if (unitBuilder[0] == '\"')
					{
						tokenList.Add(new Token(Tk.StringLiteral, unitBuilder.ToString()[1..^1], new LineColTrace(lineNum, colNum)));
					}
					else if (IsAbc(unitBuilder[0]))
					{
						if (ParseDict.Dict.ContainsKey(unitBuilder.ToString()))
						{ tokenList.Add(new Token(ParseDict.Dict[unitBuilder.ToString()], null, new LineColTrace(lineNum, colNum))); }
						else
						{ tokenList.Add(new Token(Tk.Identifier, unitBuilder.ToString(), new LineColTrace(lineNum, colNum))); }
					}
					else
					{
						if (ParseDict.Dict.ContainsKey(unitBuilder.ToString()))
						{ tokenList.Add(new Token(ParseDict.Dict[unitBuilder.ToString()], null, new LineColTrace(lineNum, colNum))); }
						else
						{ tokenList.Add(new Token(Tk.BadToken, unitBuilder.ToString(), new LineColTrace(lineNum, colNum))); }
					}

					unitBuilder = new();
				}

				colNum++;
			}
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