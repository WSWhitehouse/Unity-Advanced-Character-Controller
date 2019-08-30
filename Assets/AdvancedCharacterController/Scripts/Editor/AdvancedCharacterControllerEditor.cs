using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AdvancedCharacterController))]
public class AdvancedCharacterControllerEditor : Editor
{
    private AdvancedCharacterController _controller;

    // Serialized Propeties
    private SerializedProperty _sphereCastRadius;
    private SerializedProperty _sphereCastDepth;
    private SerializedProperty _layerMask;
    private SerializedProperty _ignoreColliders;
    private SerializedProperty _slopeLimit;

    //Foldouts
    private static bool _colliderValuesFoldout;
    private static bool _rigidbodyValuesFoldout;
    private static bool _groundedPropFoldout;
    private static bool _movementPropFoldout;

    private void OnEnable()
    {
        _controller = (AdvancedCharacterController) target;

        _sphereCastRadius = serializedObject.FindProperty(nameof(_controller.sphereCastRadius));
        _sphereCastDepth = serializedObject.FindProperty(nameof(_controller.sphereCastDepth));
        _layerMask = serializedObject.FindProperty(nameof(_controller.layerMask));
        _ignoreColliders = serializedObject.FindProperty(nameof(_controller.ignoreColliders));
        _slopeLimit = serializedObject.FindProperty(nameof(_controller.slopeLimit));
    }

    private void OnSceneGUI()
    {
        DrawCapsule.DrawWireCapsule(_controller.transform.position + _controller.Center,
            _controller.transform.rotation, _controller.Radius, _controller.Height,
            Color.blue);

        if (_groundedPropFoldout)
        {
            DrawCapsule.DrawWireCapsule(_controller.SphereCastPos, _controller.transform.rotation,
                _sphereCastRadius.floatValue,
                _sphereCastRadius.floatValue * 2, Color.red);
        }
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.Space();
        serializedObject.Update();

        // Collider Values
        _colliderValuesFoldout =
            EditorGUILayout.BeginFoldoutHeaderGroup(_colliderValuesFoldout, new GUIContent("Collider Values"));

        if (_colliderValuesFoldout)
        {
            EditorGUI.indentLevel++;

            EditorGUILayout.HelpBox(new GUIContent("These values change the size of the player collider."));

            EditorGUI.BeginChangeCheck();
            float radius = EditorGUILayout.FloatField(new GUIContent("Radius"), _controller.Radius);
            float height = EditorGUILayout.FloatField(new GUIContent("Height"), _controller.Height);
            var center = EditorGUILayout.Vector3Field(new GUIContent("Center"), _controller.Center);
            if (EditorGUI.EndChangeCheck())
            {
                _controller.SetColliderHeight(height);
                _controller.SetColliderRadius(radius);
                _controller.SetColliderCenter(center);
            }

            EditorGUI.indentLevel--;
        }

        EditorGUILayout.EndFoldoutHeaderGroup();

        // Rigidbody values
        _rigidbodyValuesFoldout =
            EditorGUILayout.BeginFoldoutHeaderGroup(_rigidbodyValuesFoldout, new GUIContent("Rigidbody Values"));

        if (_rigidbodyValuesFoldout)
        {
            EditorGUI.indentLevel++;

            EditorGUILayout.HelpBox(
                new GUIContent("These values change how the controller will react to the physics and gravity."));

            EditorGUI.BeginChangeCheck();
            float mass = EditorGUILayout.FloatField(new GUIContent("Mass"), _controller.Mass);
            float drag = EditorGUILayout.FloatField(new GUIContent("Drag"), _controller.Drag);
            float angularDrag = EditorGUILayout.FloatField(new GUIContent("Angular Drag"), _controller.AngularDrag);
            EditorGUILayout.Space();

            EditorGUILayout.HelpBox(
                new GUIContent(
                    "Auto apply gravity will use default gravity, setting this to false will allow you to apply your own gravity or not use it at all."));
            _controller.autoApplyGravity =
                EditorGUILayout.Toggle(new GUIContent("Auto Apply Gravity"), _controller.autoApplyGravity);
            GUI.enabled = _controller.autoApplyGravity;
            EditorGUI.indentLevel++;
            float gravityForce = EditorGUILayout.FloatField(new GUIContent("Gravity Force"), _controller.gravityForce);
            float stickToGroundForce = EditorGUILayout.FloatField(new GUIContent("Stick To Ground Force"),
                _controller.stickToGroundForce);
            EditorGUI.indentLevel--;
            GUI.enabled = true;


            if (EditorGUI.EndChangeCheck())
            {
                _controller.gravityForce = Mathf.Clamp(gravityForce, 0, Mathf.Infinity);
                _controller.stickToGroundForce = Mathf.Clamp(stickToGroundForce, 0, Mathf.Infinity);

                _controller.SetRigidbodyMass(mass);
                _controller.SetRigidbodyDrag(drag);
                _controller.SetRigidbodyAngularDrag(angularDrag);
            }

            EditorGUI.indentLevel--;
        }

        EditorGUILayout.EndFoldoutHeaderGroup();

        // Grounded Properties
        _groundedPropFoldout =
            EditorGUILayout.BeginFoldoutHeaderGroup(_groundedPropFoldout,
                new GUIContent("Ground Check Properties"));

        if (_groundedPropFoldout)
        {
            EditorGUI.indentLevel++;

            EditorGUILayout.HelpBox(
                new GUIContent(
                    "These values change how accurately the controller will detect the ground. Grounded sphere-cast Gizmo will show when this Foldout is open!"));

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(_sphereCastRadius, true);
            EditorGUILayout.PropertyField(_sphereCastDepth, true);
            EditorGUILayout.PropertyField(_layerMask, true);
            EditorGUILayout.PropertyField(_ignoreColliders, true);
            if (EditorGUI.EndChangeCheck())
            {
                _sphereCastRadius.floatValue = Mathf.Clamp(_sphereCastRadius.floatValue, 0, Mathf.Infinity);
                _sphereCastDepth.floatValue = Mathf.Clamp(_sphereCastDepth.floatValue, 0, Mathf.Infinity);
            }

            EditorGUI.indentLevel--;
        }

        EditorGUILayout.EndFoldoutHeaderGroup();

        // Movement Properties
        _movementPropFoldout =
            EditorGUILayout.BeginFoldoutHeaderGroup(_movementPropFoldout, new GUIContent("Movement Properties"));

        if (_movementPropFoldout)
        {
            EditorGUI.indentLevel++;

            EditorGUILayout.HelpBox(
                new GUIContent(
                    "These values change how the character controller will move and react with the environment."));

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(_slopeLimit, true);
            if (EditorGUI.EndChangeCheck())
            { }

            EditorGUI.indentLevel--;
        }

        EditorGUILayout.EndFoldoutHeaderGroup();

        serializedObject.ApplyModifiedProperties();
    }
}