using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using MappedObjectLibrary.Tests.MappedTypeTests.TestClasses;

namespace MappedObjectLibrary.Tests.MappedTypeTests
{
	[TestFixture]
	public class MappedTypeTest
	{
		[Test]
		public void CreatingMultipleInstancesOfTheMappedTypeResultsInASingleCallToAutoMapperCreateMap()
		{
			MappedTypeExample.IsMapped = false;
			MappedTypeExample.MapCount = 0;

			for(int i = 0; i < 1000; i ++)
			{
				var mte = new MappedTypeExample();
			}

			// Check that the type was mapped once, and only once.
			Assert.That(MappedTypeExample.MapCount, Is.EqualTo(1));
		}

		[Test]
		public void HidingTheBaseConstructorDoesNotResultInAnError()
		{
			MappedTypeExampleWithConstructor.IsMapped = false;
			MappedTypeExampleWithConstructor.MapCount = 0;

			for (int i = 0; i < 1000; i++)
			{
				var mte = new MappedTypeExampleWithConstructor();
			}

			// Check that the type was mapped once, and only once.
			Assert.That(MappedTypeExampleWithConstructor.MapCount, Is.EqualTo(1));
		}

		[Test]
		public void HidingTheBaseConstructorWithAParameterizedConstructorDoesNotResultInMappingIfTheBaseConstructorIsNotInherited()
		{
			MappedTypeExampleWithConstructor.IsMapped = false;
			MappedTypeExampleWithConstructor.MapCount = 0;

			for (int i = 0; i < 1000; i++)
			{
				var mte = new MappedTypeExampleWithConstructor(i.ToString());
			}

			Assert.That(MappedTypeExampleWithConstructor.MapCount, Is.EqualTo(1));
		}

		[Test]
		public void HidingTheBaseConstructorWithAParameterizedConstructorResultsInMappingIfTheBaseConstructorIsInherited()
		{
			MappedTypeExampleWithConstructor.IsMapped = false;
			MappedTypeExampleWithConstructor.MapCount = 0;

			for (int i = 0; i < 1000; i++)
			{
				var mte = new MappedTypeExampleWithConstructor(i.ToString(), "Test");
			}

			// Here, we're expecting the CreateMap method not to be called.  Not ideal.
			Assert.That(MappedTypeExampleWithConstructor.MapCount, Is.EqualTo(1));
		}
	}
}
