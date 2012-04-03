using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MappedObjectLibrary.Tests.MappedObjectTests.TestClasses
{
	[MappedObject]
	public class MappedObjectWithCreateMapMethod
	{
		public static int CreateMapCount = 0;

		public static void CreateMap()
		{
			CreateMapCount++;
		}
	}
}
