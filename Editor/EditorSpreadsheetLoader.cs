using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using FreeEditorCoroutines;

/*
 * Contact: felix.engl@hotmail.com 
 * EditorSpreadsheetLoader 0.5 - Start coroutines in the Editor
 * written by Felix Engl
 * */
namespace EditorSpreadsheetLoader
{
	/// <summary>
	/// A abstract editorclass, that allows to load values from a spreadsheet.
	/// </summary>
	/// <typeparam name="T">The Run-time type this an editor for</typeparam>
	public abstract class EditorSpreadsheetLoader<T> : Editor where T : UnityEngine.Object
	{

		/// <summary>
		/// The target of the Editorscript
		/// </summary>
		public T targ;


		//url to the spreadsheet
		private string url = "";

		//true when the coroutine is running
		private bool run = false;

		public string Url
		{
			set { url = value; }
		}

		/// <summary>
		/// The base Awake, call in public override void Awake() of child
		/// </summary>
		public virtual void Awake()
		{
			targ = (T)target;
		}

		/// <summary>
		/// The Inspector of the Parent Class, call in override public void OnInspectorGUI() of the cild
		/// </summary>
		override public void OnInspectorGUI()
		{
			url = EditorGUILayout.TextField("URL: ", url);

			if (GUILayout.Button("Load values"))
			{
				if (!run)
				{
					run = true;
					EditorCoroutine.StartCoroutine(LoadFunction());
				}
			}

			if (GUILayout.Button("Stop run"))
			{
				run = false;
			}
			EditorGUILayout.Space();
		}

		//get a spreadsheet and iterate over it
		private IEnumerator LoadFunction()
		{

			WWW sheet = new WWW(url);
			yield return sheet;

			string datasheet = sheet.text;
			string[] rows = datasheet.Split("\n"[0]);

			int rowEnd = FindEnd(rows);

			for (int j = 0; j < rows.Length; j++)
			{
				if (rows[j].StartsWith("-ignore-"))
					continue;
				if (j == rowEnd)
					break;
				if (rows[j].StartsWith("-use-"))
				InterpreterChooser(rows[j].Split("\t"[0]));
				yield return null;
			}
			run = false;
		}

		//Finds the end of a string array
		private int FindEnd(string[] starr)
		{
			for (int i = 0; i < starr.Length; i++)
			{
				if (starr[i].Contains("-end-"))
				{
					return i;
				}
			}
			return -1;
		}

		//Choose the Right interpreter Function
		private void InterpreterChooser(params string[] args)
		{
			for (int i = 0; i < args.Length; i++)
			{
				args[i] = args[i].Trim();
			}

			int[] places = GetValuePlaces(args);

			if (places.Length <= 0)
			{
				Debug.LogError("Something is wrong with the values");
			}

			switch (args[2].ToLower())
			{
				case "int":
					int?[] intValues;
					if (args.SubArray(3, args.Length-3).CastStringArrayToType(places, out intValues))
					{
						if (!InterpreterInt(args[1], intValues))
						{
							Debug.LogError("Unsupported Field: " + args[1]);
						}
					}
					break;
				case "float":
					float?[] floatValues;
					if (args.SubArray(3, args.Length-3).CastStringArrayToType(places, out floatValues))
					{
						if (!InterpreterFloat(args[1], floatValues))
						{
							Debug.LogError("Unsupported Field: " + args[1]);
						}
					}
					break;

				/**
				 * 
				 * Add more interpreter functions here
				 * 
				 */
			}

		}

		//count the number of contained values
		private int[] GetValuePlaces(string[] arr)
		{
			List<int> valPlaces = new List<int>();
			for (int i = 3; i < arr.Length; i++)
			{
				if (!arr[i].Equals("-rowend-"))
				{
					if (arr[i].Equals("-nv-")) continue;
					valPlaces.Add(i-3);
				}
				else
				{
					break;
				}
			}
			return valPlaces.ToArray();
		}




		/// <summary>
		/// Integer interpreter function for a script inheriting from 
		/// </summary>
		/// <param name="description">Name of the field</param>
		/// <param name="args">Given Parameters (vary from 0 to 3), args[0] = default, args[1] = minimum, args[2] = maximum</param>
		/// <returns>true when a field for the description exists</returns>
		public abstract bool InterpreterInt(string description, params int?[] args);

		/// <summary>
		/// Float interpreter function for a script inheriting from 
		/// </summary>
		/// <param name="description">Name of the field</param>
		/// <param name="args">Given Parameters (vary from 0 to 3), args[0] = defaultValue, args[1] = minimumvalue, args[2] = maximumvalue</param>
		/// <returns>true when a field for the description exists</returns>
		public abstract bool InterpreterFloat(string description, params float?[] args);

	}

}


