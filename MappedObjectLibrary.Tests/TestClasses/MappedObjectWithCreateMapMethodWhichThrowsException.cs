using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MappedObjectLibrary.Tests.TestClasses
{
	[MappedObject]
	public class MappedObjectWithCreateMapMethodWhichThrowsException
	{
		public static void CreateMap()
		{
			throw new ArgumentException("Argh!");
		}
	}
}
