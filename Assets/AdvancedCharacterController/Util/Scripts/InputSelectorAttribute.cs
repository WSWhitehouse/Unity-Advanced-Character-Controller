using System;
using UnityEngine;

// https://answers.unity.com/questions/1378822/list-of-tags-in-the-inspector.html

[AttributeUsage(AttributeTargets.Field)]
public class InputSelectorAttribute : PropertyAttribute
{
}