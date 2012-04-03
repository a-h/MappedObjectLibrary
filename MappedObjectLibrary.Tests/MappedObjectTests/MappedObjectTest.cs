using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using MappedObjectLibrary.Tests.MappedObjectTests.TestClasses;

namespace MappedObjectLibrary.Tests.MappedObjectTests
{
	[TestFixture]
	public class MappedObjectTest
	{
		[Test]
		[ExpectedException(typeof(MappingInitializationException))]
		public void AnExceptionIsThrownIfATypeHasACreateMapMethodButNoAttributeInStrictMode()
		{
			var typesToIgnore = new List<Type>()
					{ 
						typeof(MappedObjectWithCreateMapMethod),
						typeof(MappedObjectWithCreateMapMethodWhichThrowsException),
						typeof(MappedObjectWithoutCreateMapMethod)
					};

			MappedObjectInitializer.Initialize(typeof(MappedObjectTest).Assembly, typesToIgnore, true);
		}

		[Test]
		public void AnExceptionIsNotThrownIfATypeHasACreateMapMethodButNoAttributeInStrictMode()
		{
			var typesToIgnore = new List<Type>()
					{ 
						typeof(MappedObjectWithCreateMapMethod),
						typeof(MappedObjectWithCreateMapMethodWhichThrowsException),
						typeof(MappedObjectWithoutCreateMapMethod)
					};

			List<Type> success = MappedObjectInitializer.Initialize(typeof(MappedObjectTest).Assembly, typesToIgnore, false);

			Assert.That(success.Count, Is.EqualTo(0));
		}

		[Test]
		public void TheCreateMapMethodIsCalledOnTypesWithACreateMapMethodAndAttribute()
		{
			int initialCount = MappedObjectWithCreateMapMethod.CreateMapCount;

			var typesToIgnore = new List<Type>()
					{ 
						typeof(MappedObjectWithCreateMapMethodWhichThrowsException),
						typeof(MappedObjectWithoutCreateMapMethod),
						typeof(ObjectWithCreateMapMethodButNoAttribute)
					};

			List<Type> success = MappedObjectInitializer.Initialize(typeof(MappedObjectTest).Assembly, typesToIgnore, true);

			Assert.That(success.Count, Is.EqualTo(1));
			Assert.That(success.First(), Is.EqualTo(typeof(MappedObjectWithCreateMapMethod)));
			Assert.That(MappedObjectWithCreateMapMethod.CreateMapCount, Is.GreaterThan(initialCount));
		}

		[Test]
		[ExpectedException(typeof(MappingInitializationException))]
		public void AnExceptionIsThrownIfATypeHasTheAttributeButNotCreateMapMethod()
		{
			int initialCount = MappedObjectWithCreateMapMethod.CreateMapCount;

			var typesToIgnore = new List<Type>()
					{ 
						typeof(MappedObjectWithCreateMapMethodWhichThrowsException),
						typeof(MappedObjectWithCreateMapMethod),
						typeof(ObjectWithCreateMapMethodButNoAttribute)
					};

			MappedObjectInitializer.Initialize(typeof(MappedObjectTest).Assembly, typesToIgnore, true);
		}

		[Test]
		[ExpectedException(typeof(MappingInitializationException))]
		public void AnExceptionIsThrownIfAMappingInitializerThrowsAnException()
		{
			int initialCount = MappedObjectWithCreateMapMethod.CreateMapCount;

			var typesToIgnore = new List<Type>()
					{ 
						typeof(MappedObjectInitializer),
						typeof(MappedObjectWithCreateMapMethod),
						typeof(ObjectWithCreateMapMethodButNoAttribute)
					};

			MappedObjectInitializer.Initialize(typeof(MappedObjectTest).Assembly, typesToIgnore, true);
		}
	}
}
