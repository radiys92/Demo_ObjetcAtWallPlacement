using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ObjectControllerTester))]
[CanEditMultipleObjects]
public class ObjectControllerTesterEditor : Editor
{
	public override void OnInspectorGUI ()
	{
		if (!Application.isPlaying) {
			EditorGUILayout.LabelField ("Can run only in play mode");
			return;
		}
		
		var t = target as ObjectControllerTester;
		var con = t.GetComponent<IObjectController> ();
		con.IsSelected = EditorGUILayout.Toggle ("Is selected", con.IsSelected);
		con.IsMoving = EditorGUILayout.Toggle ("Is moving", con.IsMoving);
		con.IsEditing = EditorGUILayout.Toggle ("Is editing", con.IsEditing);
	}
}
