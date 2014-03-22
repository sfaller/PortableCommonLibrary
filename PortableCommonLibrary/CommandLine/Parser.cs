using System.Collections.Generic;

using PortableCommonLibrary.Interfaces.CommandLine;
using PortableCommonLibrary.Interfaces.String;

namespace PortableCommonLibrary.CommandLine
{
	public struct Token
	{
		public string Value;
		public bool IsCommand;

		public Token(string value, string commandPrefix)
		{
			Value = value.Trim (commandPrefix.ToCharArray ());
			IsCommand = value.Contains (commandPrefix);
		}
	}

	public class Parser : IParser
	{
		private readonly ITokenizer _tokenizer;
		private readonly string _commandPrefix;
		private int _currentPosition;
		private Dictionary<string, List<string>> _commands;

		private Token CurrentToken
		{
			get 
			{
				return new Token (_tokenizer.Tokens [_currentPosition], _commandPrefix);
			}
		}

		public Dictionary<string, List<string>> Commands
		{
			get { return _commands; }
		}

		public Parser(ITokenizer tokenizer, string commandPrefix)
		{
			_tokenizer = tokenizer;
			_commandPrefix = commandPrefix;
		}

		public void Parse()
		{
			_commands = null;

			for (_currentPosition = 0; _currentPosition < _tokenizer.Tokens.Length; _currentPosition++) 
			{
				if (CurrentToken.IsCommand) 
				{
					_commands = _commands ?? new Dictionary<string, List<string>> ();
					_commands.Add (CurrentToken.Value, CollectArguments ());
				}
			}
		}

		private List<string> CollectArguments ()
		{
			List<string> arguments = null;

			while (++_currentPosition < _tokenizer.Tokens.Length) 
			{
				if (CurrentToken.IsCommand) 
				{
					_currentPosition--;
					break;
				}

				arguments = arguments ?? new List<string> ();
				arguments.Add (CurrentToken.Value);
			}

			return arguments;
		}
	}
}