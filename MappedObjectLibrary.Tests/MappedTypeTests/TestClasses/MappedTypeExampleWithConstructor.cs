using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MappedObjectLibrary.Tests.MappedTypeTests.TestClasses
{
	public class MappedTypeExampleWithConstructor : MappedType<MappedTypeExampleWithConstructor>
	{
		public MappedTypeExampleWithConstructor()
		{
			// Here, I've created a constructor.  But what about the call to 
			// CreateMapIfRequired() in the base constructor?

			// Actually, it still works fine.
			DoSomething("Hello!");
		}

		public MappedTypeExampleWithConstructor(string input)
		{
			// Here, I've created a constructor with a parameter.  But what about the call to 
			// CreateMapIfRequired() in the base parameterless constructor?

			// Here, CreateMap will still be called in the base constructor.
			DoSomething(input);
		}

		public MappedTypeExampleWithConstructor(string inputA, string inputB)
			: base()
		{
			// Here, I've created a constructor with more parameters, but I've inherited
			// from the base constructor.

			// CreateMap will again, also be called.
			DoSomething(inputA);
		}

		private void DoSomething(string text)
		{
			// You can write this out as an example, but it slows down
			// the tests.
			// Console.WriteLine(text);
		}

		public static int MapCount = 0;

		public override void CreateMap()
		{
			MapCount++;
		}
	}
}
