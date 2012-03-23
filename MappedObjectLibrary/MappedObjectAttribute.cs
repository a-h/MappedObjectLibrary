using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MappedObjectLibrary
{
	/// <summary>
	/// An attribute that should be applied to classes if the MappedObjectInitializer
	/// should call the public static CreateMap method once (and only once) on application startup 
	/// (e.g. in Application_Start in Global.asax or in the static void Main() method of 
	/// a Console Application).
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
	public sealed class MappedObjectAttribute : Attribute
	{
	}
}
