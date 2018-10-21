using System;
using NUnit.Framework;

namespace DBTest.test
{
	[TestFixture]
	public class SampleTest
	{
		[SetUp]
		public void setUp()
		{
			Console.WriteLine("setUp of SampleTest");
		}
		
		[TearDown]
		public void tearDown()
		{
			Console.WriteLine("tearDown of SampleTest");			
		}
		
		[Test]
		public void testSuccess()
		{
			Console.WriteLine("testSuccess of SampleTest");
			Assert.IsTrue(true, "This exemplary test is expected to pass");
		}
		
		[Test]
		public void testFailure()
		{
			Console.WriteLine("testFailure of SampleTest");
			Assert.IsTrue(false, "This exemplary test is expected to fail");
		}
	}
}
