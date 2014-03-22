using NUnit.Framework;
using NUnit.Mocks;
using System.Collections.Generic;

using PortableCommonLibrary.CommandLine;
using PortableCommonLibrary.Interfaces.String;

namespace PortableCommonLibraryUnitTests
{
	[TestFixture]
	public class ParserTest
	{

		[Test]
		public void TestParseNoArgumentsReturnNull ()
		{
			var tokenizerMock = new DynamicMock (typeof(ITokenizer));
			tokenizerMock.SetReturnValue ("get_Tokens", new string[0]);
			var sut = new Parser (tokenizerMock.MockInstance as ITokenizer, string.Empty);
			sut.Parse ();

			Assert.IsNull (sut.Commands);
		}

		[Test]
		public void TestParseReturnDictionary ()
		{
			var tokenizerMock = new DynamicMock (typeof(ITokenizer));
			tokenizerMock.SetReturnValue ("get_Tokens", new [] { "--c", "100" });
			var sut = new Parser (tokenizerMock.MockInstance as ITokenizer, string.Empty);
			sut.Parse ();

			Assert.AreEqual (typeof(Dictionary<string, List<string>>), sut.Commands.GetType());
		}

		[Test]
		public void TestParseReturnDictionaryCommandPlusArgument ()
		{
			var tokenizerMock = new DynamicMock (typeof(ITokenizer));
			tokenizerMock.SetReturnValue ("get_Tokens", new [] { "--c", "100" });
			var sut = new Parser (tokenizerMock.MockInstance as ITokenizer, "--");
			sut.Parse ();

			Assert.AreEqual (typeof(Dictionary<string, List<string>>), sut.Commands.GetType());
			Assert.AreEqual (1, sut.Commands.Count);
			Assert.IsTrue (sut.Commands.ContainsKey("c"));
			Assert.AreEqual (new List<string> { "100"}, sut.Commands["c"]);
		}

		[Test]
		public void TestParseReturnDictionaryCommandPlusMultipleArguments ()
		{
			var tokenizerMock = new DynamicMock (typeof(ITokenizer));
			tokenizerMock.SetReturnValue ("get_Tokens", new [] { "--c", "100", "200", "300" });
			var sut = new Parser (tokenizerMock.MockInstance as ITokenizer, "--");
			sut.Parse ();

			Assert.AreEqual (typeof(Dictionary<string, List<string>>), sut.Commands.GetType());
			Assert.AreEqual (1, sut.Commands.Count);
			Assert.IsTrue (sut.Commands.ContainsKey("c"));
			Assert.AreEqual (new List<string> { "100", "200", "300" }, sut.Commands["c"]);
		}

		[Test]
		public void TestParseReturnDictionaryMultipleCommandsPlusMultipleArguments ()
		{
			var tokenizerMock = new DynamicMock (typeof(ITokenizer));
			tokenizerMock.SetReturnValue ("get_Tokens", new [] { "--c", "100", "200", "300", "--d", "400", "500", "600" });
			var sut = new Parser (tokenizerMock.MockInstance as ITokenizer, "--");
			sut.Parse ();

			Assert.AreEqual (typeof(Dictionary<string, List<string>>), sut.Commands.GetType());
			Assert.AreEqual (2, sut.Commands.Count);
			Assert.IsTrue (sut.Commands.ContainsKey("c"));
			Assert.AreEqual (new List<string> { "100", "200", "300" }, sut.Commands["c"]);
			Assert.IsTrue (sut.Commands.ContainsKey("d"));
			Assert.AreEqual (new List<string> { "400", "500", "600" }, sut.Commands["d"]);
		}

		[Test]
		public void TestParseReturnDictionaryMultipleCommandsOnlySecondMultipleArguments ()
		{
			var tokenizerMock = new DynamicMock (typeof(ITokenizer));
			tokenizerMock.SetReturnValue ("get_Tokens", new [] { "--c", "--d", "400", "500", "600" });
			var sut = new Parser (tokenizerMock.MockInstance as ITokenizer, "--");
			sut.Parse ();

			Assert.AreEqual (typeof(Dictionary<string, List<string>>), sut.Commands.GetType());
			Assert.AreEqual (2, sut.Commands.Count);
			Assert.IsTrue (sut.Commands.ContainsKey("c"));
			Assert.IsNull (sut.Commands["c"]);
			Assert.IsTrue (sut.Commands.ContainsKey("d"));
			Assert.AreEqual (new List<string> { "400", "500", "600" }, sut.Commands["d"]);
		}
	}

}

