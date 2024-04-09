using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CoinGenerator))]
public class GenerateCoins : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CoinGenerator script = (CoinGenerator)target;

        if (GUILayout.Button("Generate Coins"))
        {
            script.SpawnCoins();
        }
    }
}
