using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace MappedObjectLibrary
{
	/// <summary>
	/// The base class of types which map themselves using AutoMapper, this is
	/// an alternative to using the MappedObjectInitializer with the MappedObject
	/// attribute.
	/// </summary>
	/// <typeparam name="T">The type which is being mapped.</typeparam>
	public abstract class MappedType<T>
	{
		public MappedType()
		{
			CreateMapIfRequired();
		}

		/// <summary>
		/// An object used to lock the AutoMapper so that multiple threads
		/// can't accidentally attempt to call AutoMapper.CreateMap 
		/// simultaneously.
		/// </summary>
		[IgnoreDataMember]
		[XmlIgnore]
		public static object LockObject = new object();

		/// <summary>
		/// Gets or sets a value indicating whether this type has had CreateMap 
		/// called yet.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is mapped; otherwise, <c>false</c>.
		/// </value>
		[IgnoreDataMember]
		[XmlIgnore]
		public static bool IsMapped { get; set; }

		/// <summary>
		/// When overridden in an inheriting class, creates the map, using 
		/// AutoMapper.CreateMap.
		/// </summary>
		public abstract void CreateMap();

		/// <summary>
		/// Creates the map if required (it hasn't been done yet).
		/// This should be called in the constructor of any class which
		/// is to be mapped.
		/// </summary>
		public void CreateMapIfRequired()
		{
			if (!MappedType<T>.IsMapped)
			{
				lock (MappedType<T>.LockObject)
				{
					CreateMap();

					MappedType<T>.IsMapped = true;
				}
			}
		}
	}
}
