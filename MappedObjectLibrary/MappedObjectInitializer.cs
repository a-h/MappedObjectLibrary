using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace MappedObjectLibrary
{
	/// <summary>
	/// Provides helper methods for initializing types.
	/// </summary>
	public static class MappedObjectInitializer
	{
		/// <summary>
		/// A method which calls the "public static void CreateMap()" method on classes in the given assembly which have the
		/// MappedObjectAttribute applied to them.
		/// This method should only be used on Application_Start (in Global.asax) for
		/// Web Applications or in the static void Main() method of a Console / Windows Application.
		/// </summary>
		/// <param name="assembly">The assembly to search for types which have the MappedObjectAttribute applied to them.</param>
		/// <param name="throwOnWarnings">if set to <c>true</c> throw exceptions if a type is found which has a public
		/// static void CreateMap() method, but doesn't have the MappedObjectAttribute applied to it.</param>
		/// <returns>A list of types which have had their CreateMap method called.</returns>
		public static List<Type> Initialize(Assembly assembly, bool throwOnWarnings)
		{
			return Initialize(assembly, new List<Type>(), throwOnWarnings);
		}

		/// <summary>
		/// A method which calls the "public static void CreateMap()" method on classes in the given assembly which have the
		/// MappedObjectAttribute applied to them.
		/// This method should only be used on Application_Start (in Global.asax) for
		/// Web Applications or in the static void Main() method of a Console / Windows Application.
		/// </summary>
		/// <param name="assembly">The assembly to search for types which have the MappedObjectAttribute applied to them.</param>
		/// <param name="typesToIgnore">The types to skip initialization for.</param>
		/// <param name="throwOnWarnings">if set to <c>true</c> throw exceptions if a type is found which has a public
		/// static void CreateMap() method, but doesn't have the MappedObjectAttribute applied to it.</param>
		/// <returns>A list of types which have had their CreateMap method called.</returns>
		public static List<Type> Initialize(Assembly assembly, List<Type> typesToIgnore, bool throwOnWarnings)
		{
			var warnings = new List<Type>();
			var noCreateMapMethodErrors = new List<Type>();
			var successes = new List<Type>();
			var errors = new List<Tuple<Type, Exception>>();

			// List all of the classes.
			foreach (Type type in assembly.GetTypes().Where(t => !typesToIgnore.Contains(t) && t.IsClass))
			{
				// Check to see if the type has got the MappedObject attriute.
				bool hasAttribute = (type.GetCustomAttributes(typeof(MappedObjectAttribute), false).Length > 0);

				MethodInfo createMapMethod = type.GetMethod("CreateMap", BindingFlags.Public | BindingFlags.Static);
				bool hasStaticCreateMapMethod = (createMapMethod != null);

				if (!hasAttribute && hasStaticCreateMapMethod)
				{
					// Warn that the class has the method, but not the attribute.
					warnings.Add(type);
				}

				if (hasAttribute && !hasStaticCreateMapMethod)
				{
					// Log exception.
					noCreateMapMethodErrors.Add(type);
				}

				if (hasAttribute && hasStaticCreateMapMethod)
				{
					// Call CreateMap.
					try
					{
						createMapMethod.Invoke(null, null);
						successes.Add(type);
					}
					catch (Exception ex)
					{
						errors.Add(new Tuple<Type, Exception>(type, ex));
					}
				}
			}

			var sb = new StringBuilder();
			foreach (Type t in warnings)
			{
				sb.AppendLine(string.Format("(Warning) {0} has the MappedObjectAttribute but does not have a public static CreateMap() method.", t.FullName));
			}

			foreach (var t in noCreateMapMethodErrors)
			{
				sb.AppendLine(string.Format("(Exception) {0} has a CreateMap method, but not the MappedObjectAttribute.", t.FullName));
			}

			foreach (var t in errors)
			{
				sb.AppendLine(string.Format("(Exception) {0} failed with exception - {1}", t.Item1.FullName, t.Item2.Message));
			}

			if ((throwOnWarnings && warnings.Count > 0) || (errors.Count > 0 || noCreateMapMethodErrors.Count > 0))
			{
				throw new MappingInitializationException(sb.ToString());
			}

			return successes;
		}
	}
}
