// using UnityEditor;
// using UnityEngine;

// [CustomEditor(typeof(CompositeBehaviour))]
// public class CompositeBehaviourPanel : Editor
// {
//   public override void OnInspectorGUI()
//   {
//     CompositeBehaviour cb = (CompositeBehaviour)this.target;

//     Rect r = EditorGUILayout.BeginHorizontal();
//     r.height = EditorGUIUtility.singleLineHeight;

//     if (cb.behaviours == null || cb.behaviours.Length == 0)
//     {
//       EditorGUI.HelpBox(r, "No behaviours added.", MessageType.Warning);
      
//       EditorGUILayout.EndHorizontal();
//       r = EditorGUILayout.BeginHorizontal();
//       r.height = EditorGUIUtility.singleLineHeight;
//     }
//     else{

//     }
//   }
// }
