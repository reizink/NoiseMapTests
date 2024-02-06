using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using static UnityEngine.GraphicsBuffer;

[CustomEditor(typeof(MapGenerator2))]
public class MapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI(); //sometimes needed, sometimes not
        MapGenerator2 map = (MapGenerator2)target;

        if (DrawDefaultInspector())
        {
            if (map.auto)
            {
                map.GenerateMap();
            }
        }

        if (GUILayout.Button("Generate Me"))
        {
            map.GenerateMap();
        }
    }

}
