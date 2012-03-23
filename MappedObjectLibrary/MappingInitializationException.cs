using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MappedObjectLibrary
{
	/// <summary>
	/// An exception thrown when Automapping initialization fails.
	/// </summary>
	public class MappingInitializationException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MappingInitializationException"/> class.
		/// </summary>
		/// <param name="message">The error message.</param>
		public MappingInitializationException(string message) : base(message)
		{
		}
	}
}
