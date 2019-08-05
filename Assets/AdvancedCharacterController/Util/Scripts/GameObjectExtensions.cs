// -------------------------------------------- //
// Author : William Whitehouse / DoctorWolfy121 //
// GitHub : github.com/DoctorWolfy121           //
// Date   : 01/07/2019                          //
// -------------------------------------------- //

using System;
using UnityEngine;

public static class GameObjectExtensions
{
    /// <summary>
    /// Gets a component and sends an error if one isn't attached
    /// </summary>
    /// <param name="gameObject"></param>
    /// <typeparam name="T">Component Type</typeparam>
    /// <returns>Component</returns>
    /// <exception cref="Exception"></exception>
    public static T GetComponentRequired<T>(this GameObject gameObject) where T : Component
    {
        T component = gameObject.GetComponent<T>();
        if (component == null)
        {
            throw new Exception(typeof(T) + " is not attached to " + gameObject.name);
        }

        return component;
    }

    /// <summary>
    /// Gets a component or adds one if it isn't attached
    /// </summary>
    /// <param name="gameObject"></param>
    /// <typeparam name="T">Component Type</typeparam>
    /// <returns>Component</returns>
    public static T GetComponentOrAdd<T>(this GameObject gameObject) where T : Component
    {
        T component = gameObject.GetComponent<T>();
        if (component == null)
        {
            component = gameObject.AddComponent<T>();
        }

        return component;
    }
    
    /// <summary>
    /// Sets the layers of this GameObject and all of its children, and all of its children recursively
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="layer"></param>
    /// <exception cref="Exception"></exception>
    public static void SetLayerRecursively(this GameObject gameObject, int layer)
    {
        const int layerAmount = 31;
        if (layer > layerAmount)
            throw new Exception("You are attempting to set a GameObject to layer " + layer +
                                ". This is not a valid layer!");

        gameObject.layer = layer;

        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.SetLayerRecursively(layer);
        }
    }
}