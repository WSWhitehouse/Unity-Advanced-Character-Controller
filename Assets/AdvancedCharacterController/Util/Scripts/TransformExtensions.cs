// -------------------------------------------- //
// Author : William Whitehouse / DoctorWolfy121 //
// GitHub : github.com/DoctorWolfy121           //
// Date   : 01/07/2019                          //
// -------------------------------------------- //

using UnityEngine;

public static class TransformExtensions
{
    /// <summary>
    /// Resets the position of this transform
    /// </summary>
    /// <param name="transform"></param>
    /// <returns></returns>
    public static Transform ResetPos(this Transform transform)
    {
        transform.position = Vector3.zero;
        return transform;
    }

    /// <summary>
    /// Resets the rotation of this transform
    /// </summary>
    /// <param name="transform"></param>
    /// <returns></returns>
    public static Transform ResetRot(this Transform transform)
    {
        transform.rotation = Quaternion.identity;
        return transform;
    }

    /// <summary>
    /// Resets the local scale of this transform
    /// </summary>
    /// <param name="transform"></param>
    /// <returns></returns>
    public static Transform ResetScale(this Transform transform)
    {
        transform.localScale = Vector3.one;
        return transform;
    }

    /// <summary>
    /// Resets the position, rotation and scale of this transform
    /// </summary>
    /// <param name="transform"></param>
    /// <returns></returns>
    public static Transform Reset(this Transform transform)
    {
        transform.ResetPos();
        transform.ResetRot();
        transform.ResetScale();
        return transform;
    }

    /// <summary>
    /// Resets the position and rotation of this transform
    /// </summary>
    /// <param name="transform"></param>
    /// <returns></returns>
    public static Transform ResetPosRot(this Transform transform)
    {
        transform.ResetPos();
        transform.ResetRot();
        return transform;
    }
}