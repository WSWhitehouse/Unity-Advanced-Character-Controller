using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ComponentExtensions
{
    /// <summary>
    /// Adds a component to this components gameobject
    /// </summary>
    /// <param name="component"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T AddComponent<T>(this Component component) where T : Component
    {
        return component.gameObject.AddComponent<T>();
    }

    /// <summary>
    /// Gets a component and sends an error if one isn't attached
    /// </summary>
    /// <param name="component"></param>
    /// <typeparam name="T">Component Type</typeparam>
    /// <returns>Component</returns>
    /// <exception cref="Exception"></exception>
    public static T GetComponentRequired<T>(this Component component) where T : Component
    {
        T comp = component.gameObject.GetComponent<T>();
        if (comp == null)
        {
            throw new Exception(typeof(T) + " is not attached to " + component.gameObject.name);
        }

        return comp;
    }

    /// <summary>
    /// Gets a component or adds one if it isn't attached
    /// </summary>
    /// <param name="component"></param>
    /// <typeparam name="T">Component Type</typeparam>
    /// <returns>Component</returns>
    public static T GetComponentOrAdd<T>(this Component component) where T : Component
    {
        T comp = component.gameObject.GetComponent<T>();
        if (comp == null)
        {
            comp = component.gameObject.AddComponent<T>();
        }

        return comp;
    }
}