
using PortableCommonLibrary.Interfaces.String;

namespace PortableCommonLibrary.String
{
	public class Tokenizer : ITokenizer
	{
		private readonly char _seperator;
		private readonly string _commandLineArguments;
		private string[] _tokens;

		public string[] Tokens
		{
			get
			{ 
				return _tokens ?? (_tokens = _commandLineArguments.Split (_seperator));
			}
		}

		public Tokenizer (string args, char seperator = ' ')
		{
			_commandLineArguments = args;
			_seperator = seperator;
		}
	}
}

