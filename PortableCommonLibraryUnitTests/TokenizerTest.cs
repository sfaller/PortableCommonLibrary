using NUnit.Framework;
using System;
using PortableCommonLibrary.String;

namespace PortableCommonLibraryUnitTests
{
	[TestFixture]
	public class Test
	{
		[Test]
		public void TestGetTokenReturnsListOfStrings ()
		{
			var sut = new Tokenizer (string.Empty);

			Assert.AreEqual(typeof(string[]), sut.Tokens.GetType());
		}

		[Test]
		public void TestGetTokenReturnsListOfStringsTwoElements ()
		{
			var sut = new Tokenizer ("--c 100");
			var actual = sut.Tokens;

			Assert.AreEqual(typeof(string[]), actual.GetType());
			Assert.AreEqual(2, actual.Length);
			Assert.AreEqual("--c", actual[0]);
			Assert.AreEqual("100", actual[1]);
		}
	}
}

