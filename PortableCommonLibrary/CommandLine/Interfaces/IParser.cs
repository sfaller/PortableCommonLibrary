using System;
using System.Collections.Generic;

namespace PortableCommonLibrary.Interfaces.CommandLine
{
	public interface IParser
	{
		Dictionary<string, List<string>> Commands { get; }
		void Parse();
	}
}

