using UnityEditor;
using UnityEngine;

namespace LD47.Editor
{
    [CustomEditor(typeof(LevelGenerator))]
    public class LevelGeneratorEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            LevelGenerator levelGenerator = (LevelGenerator) target;
            if (GUILayout.Button("Generate Map"))
            {
                levelGenerator.Generate();
            }

        }
    }
}