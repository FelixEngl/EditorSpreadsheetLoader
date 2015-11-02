using UnityEngine;
using System;
using System.ComponentModel;

namespace EditorSpreadsheetLoader
{
	public static class StaticFunctions
	{

		/// <summary>
		/// Get the subarray of an array
		/// </summary>
		/// <typeparam name="T">input data</typeparam>
		/// <param name="data"></param>
		/// <param name="index">Start at</param>
		/// <param name="length">With the length</param>
		/// <returns>The subarray</returns>
		public static T[] SubArray<T>(this T[] data, int index, int length)
		{
			T[] result = new T[length];
			Array.Copy(data, index, result, 0, length);
			return result;
		}


		//List of supported Types by the function CastStringArrayToType
		private static readonly Type[] SupportedTypes =
		{
			typeof (int?),// typeof (short?), typeof (long?), typeof (double?),
			typeof (float?)
		};

		//Check if the function is Supported
		private static readonly Func<Type, Type> SupportFunc = (type) =>
		{

			for (int i = 0; i < SupportedTypes.Length; i++)
			{
				if (SupportedTypes[i] == type)
					return SupportedTypes[i];
			}
			return null;

		};

		/// <summary>
		/// Casts a stringarray to a specific type
		/// </summary>
		/// <typeparam name="U">Type returned</typeparam>
		/// <param name="args">string given</param>
		/// <param name="result">array returned</param>
		/// <returns>true when successfull</returns>
		public static bool CastStringArrayToType<U>(this string[] args, int[] places, out U[] result)
		{
			if (!default(U).Equals(null))
				Debug.LogError("No nullable type given");

			Type type = SupportFunc(typeof(U));

			TypeConverter converter = TypeDescriptor.GetConverter(type);

			result = new U[args.Length-1];

			if (type != null)
			{
				for (int i = 0; i < places.Length; i++)
				{
					result[places[i]] = (U) converter.ConvertFromString(args[places[i]]);
				}
				return true;
			}
			Debug.LogError("Unsupportet Type");
			return false;
		}

	}
}