using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AdvancedCharacterController))]
public class AdvancedCharacterControllerEditor : Editor
{
    private AdvancedCharacterController _controller;

    // Serialized Propeties
    private SerializedProperty _radius;
    private SerializedProperty _height;
    private SerializedProperty _center;
    private SerializedProperty _mass;
    private SerializedProperty _drag;
    private SerializedProperty _angularDrag;
    private SerializedProperty _gravityForce;
    private SerializedProperty _stickToGroundForce;
    private SerializedProperty _sphereCastRadius;
    private SerializedProperty _sphereCastDepth;
    private SerializedProperty _layerMask;
    private SerializedProperty _ignoreColliders;
    private SerializedProperty _slopeLimit;
    private SerializedProperty _autoApplyGravity;

    //Foldouts
    private static bool _colliderValuesFoldout;
    private static bool _rigidbodyValuesFoldout;
    private static bool _groundedPropFoldout;
    private static bool _movementPropFoldout;

    // Other
    private static bool _showGroundedSphereCast = false;

    private void OnEnable()
    {
        _controller = (AdvancedCharacterController) target;

        _radius = serializedObject.FindProperty(nameof(_controller.radius));
        _height = serializedObject.FindProperty(nameof(_controller.height));
        _center = serializedObject.FindProperty(nameof(_controller.center));
        _mass = serializedObject.FindProperty(nameof(_controller.mass));
        _drag = serializedObject.FindProperty(nameof(_controller.drag));
        _angularDrag = serializedObject.FindProperty(nameof(_controller.angularDrag));
        _gravityForce = serializedObject.FindProperty(nameof(_controller.gravityForce));
        _stickToGroundForce = serializedObject.FindProperty(nameof(_controller.stickToGroundForce));
        _sphereCastRadius = serializedObject.FindProperty(nameof(_controller.sphereCastRadius));
        _sphereCastDepth = serializedObject.FindProperty(nameof(_controller.sphereCastDepth));
        _layerMask = serializedObject.FindProperty(nameof(_controller.layerMask));
        _ignoreColliders = serializedObject.FindProperty(nameof(_controller.ignoreColliders));
        _slopeLimit = serializedObject.FindProperty(nameof(_controller.slopeLimit));
        _autoApplyGravity = serializedObject.FindProperty(nameof(_controller.autoApplyGravity));
    }

    private void OnSceneGUI()
    {
        DrawCapsule.DrawWireCapsule(_controller.transform.position + _center.vector3Value,
            _controller.transform.rotation, _radius.floatValue, _height.floatValue,
            Color.blue);

        if (_showGroundedSphereCast)
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
            float radius = EditorGUILayout.FloatField(new GUIContent("Radius"), _radius.floatValue);
            float height = EditorGUILayout.FloatField(new GUIContent("Height"), _height.floatValue);
            var center = EditorGUILayout.Vector3Field(new GUIContent("Center"), _center.vector3Value);
            if (EditorGUI.EndChangeCheck())
            {
                height = Mathf.Clamp(height, 0, Mathf.Infinity);
                if (height <= 0)
                {
                    radius = 0;
                }
                else
                {
                    radius = Mathf.Clamp(radius, 0, height / 2f);
                }

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
            float mass = EditorGUILayout.FloatField(new GUIContent("Mass"), _mass.floatValue);
            float drag = EditorGUILayout.FloatField(new GUIContent("Drag"), _drag.floatValue);
            float angularDrag = EditorGUILayout.FloatField(new GUIContent("Angular Drag"), _angularDrag.floatValue);
            EditorGUILayout.Space();

            EditorGUILayout.HelpBox(
                new GUIContent(
                    "Auto apply gravity will use default gravity, setting this to false will allow you to apply your own gravity or not use it at all."));
            EditorGUILayout.PropertyField(_autoApplyGravity, true);
            GUI.enabled = _autoApplyGravity.boolValue;
            EditorGUILayout.PropertyField(_gravityForce, true);
            EditorGUILayout.PropertyField(_stickToGroundForce, true);
            GUI.enabled = true;


            if (EditorGUI.EndChangeCheck())
            {
                mass = Mathf.Clamp(mass, 0, Mathf.Infinity);
                drag = Mathf.Clamp(drag, 0, Mathf.Infinity);
                angularDrag = Mathf.Clamp(angularDrag, 0, Mathf.Infinity);
                _gravityForce.floatValue = Mathf.Clamp(_gravityForce.floatValue, 0, Mathf.Infinity);
                _stickToGroundForce.floatValue = Mathf.Clamp(_stickToGroundForce.floatValue, 0, Mathf.Infinity);

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
                new GUIContent("These values change how accurately the controller will detect the ground."));

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(_sphereCastRadius, true);
            EditorGUILayout.PropertyField(_sphereCastDepth, true);
            EditorGUILayout.PropertyField(_layerMask, true);
            EditorGUILayout.PropertyField(_ignoreColliders, true);
            _showGroundedSphereCast = EditorGUILayout.Toggle(new GUIContent("[Debug] Show Gizmo"),
                _showGroundedSphereCast);
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