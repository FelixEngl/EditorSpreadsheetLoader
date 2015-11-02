using UnityEngine;
using System.Text;
using EditorSpreadsheetLoader;
using UnityEditor;

[CustomEditor(typeof(AttackScript))]
public class EditorAttackScript : EditorSpreadsheetLoader<AttackScript> {

	override public void Awake()
	{
		Url = "https://docs.google.com/spreadsheets/u/0/d/1PgOAo3Vd-cWHIei2N8Ox68ZSaypWMuQJY_wADSICpY8/export?format=tsv&id=1PgOAo3Vd-cWHIei2N8Ox68ZSaypWMuQJY_wADSICpY8&gid=0";
		base.Awake();
	}

	override public void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		if (GUILayout.Button("Clear Example"))
		{
			targ.actualLife = 0f;
			targ.maximumLife = 0f;
			targ.minimumLife = 0f;
			targ.oldLife = 0f;
			targ.stamnia = 0;
			targ.stamniaOld = 0;
		}
		EditorGUILayout.Space();
		DrawDefaultInspector();
	}


	public override bool InterpreterInt(string description, params int?[] args)
	{
		switch (description)
		{
			case "Stamnia":
				setStamnia(args);
				break;
		}
		return true;
	}

	private void setStamnia(int?[] args)
	{
		for (int i = 0; i < args.Length; i++)
		{
			if(args[i] == null)
				continue;
			switch (i)
			{
				case 0:
					targ.stamnia = args[i].Value;
					break;
				case 3:
					targ.stamniaOld = args[i].Value;
					break;
			}
		}
	}

	public override bool InterpreterFloat(string description, params float?[] args)
	{
		switch (description)
		{
			case "Life":
				setLife(args);
				break;
		}
		return true;
	}

	private void setLife(float?[] args)
	{
		for (int i = 0; i < args.Length; i++)
		{
			if (args[i] == null)
				continue;
			switch (i)
			{
				case 0:
					targ.actualLife = args[i].Value;
					break;
				case 1:
					targ.minimumLife = args[i].Value;
					break;
				case 2:
					targ.maximumLife = args[i].Value;
					break;
				case 3:
					targ.oldLife = args[i].Value;
					break;
			}
		}
	}
}
