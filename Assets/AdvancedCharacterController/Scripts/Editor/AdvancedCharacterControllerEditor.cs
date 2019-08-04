using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AdvancedCharacterController))]
public class AdvancedCharacterControllerEditor : Editor
{
    private AdvancedCharacterController _controller;
    
    private void OnEnable()
    {
        _controller = (AdvancedCharacterController) target;
    }

    private void OnSceneGUI()
    {
        DrawCapsule.DrawWireCapsule(_controller.transform.position, _controller.transform.rotation, 0.5f,2, Color.blue);
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
    }
}
