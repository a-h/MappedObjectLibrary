using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MappedObjectLibrary.Tests.MappedTypeTests.TestClasses
{
	public class MappedTypeExample : MappedType<MappedTypeExample>
	{
		public static int MapCount = 0;

		public override void CreateMap()
		{
			MapCount++;
		}
	}
}
