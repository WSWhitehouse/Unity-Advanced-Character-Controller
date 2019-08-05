// -------------------------------------------- //
// Author : William Whitehouse / DoctorWolfy121 //
// GitHub : github.com/DoctorWolfy121           //
// Date   : 01/07/2019                          //
// -------------------------------------------- //

using UnityEngine;

public static class VectorExtensions
{
    #region Vector2

    /// <summary>
    /// Resets this vector2 to 0
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static Vector2 Reset(this Vector2 v)
    {
        return Vector2.zero;
    }

    /// <summary>
    /// Adds the X value to this vector2
    /// </summary>
    /// <param name="v"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector2 ModifyX(this Vector2 v, float x)
    {
        return new Vector2(v.x + x, v.y);
    }

    /// <summary>
    /// Adds the Y value to this vector2
    /// </summary>
    /// <param name="v"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static Vector2 ModifyY(this Vector2 v, float y)
    {
        return new Vector2(v.x, v.y + y);
    }

    /// <summary>
    /// Set the X value of this vector2
    /// </summary>
    /// <param name="v"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector2 SetX(this Vector2 v, float x)
    {
        return new Vector2(x, v.y);
    }

    /// <summary>
    ///  Set the Y value of this vector2
    /// </summary>
    /// <param name="v"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static Vector2 SetY(this Vector2 v, float y)
    {
        return new Vector2(v.x, y);
    }

    /// <summary>
    /// Converts this vector2 to vector2Int
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static Vector2Int ToInt(this Vector2 v)
    {
        return new Vector2Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y));
    }

    /// <summary>
    /// Convert this vector2 to vector3
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static Vector3 ToVector3(this Vector2 v)
    {
        return new Vector3(v.x, v.y, 0);
    }

    /// <summary>
    /// Convert this vector2 to vector3
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static Vector4 ToVector4(this Vector2 v)
    {
        return new Vector4(v.x, v.y, 0, 0);
    }

    #endregion

    #region Vector3

    /// <summary>
    /// Resets this vector3 to 0
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static Vector3 Reset(this Vector3 v)
    {
        return Vector3.zero;
    }

    /// <summary>
    /// Adds the X value to this vector3
    /// </summary>
    /// <param name="v"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector3 ModifyX(this Vector3 v, float x)
    {
        return new Vector3(v.x + x, v.y, v.z);
    }

    /// <summary>
    /// Adds the Y value to this vector3
    /// </summary>
    /// <param name="v"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector3 ModifyY(this Vector3 v, float y)
    {
        return new Vector3(v.x, v.y + y, v.z);
    }

    /// <summary>
    /// Adds the Z value to this vector3
    /// </summary>
    /// <param name="v"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector3 ModifyZ(this Vector3 v, float z)
    {
        return new Vector3(v.x, v.y, v.z + z);
    }

    /// <summary>
    /// Set the X value of this vector2
    /// </summary>
    /// <param name="v"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector3 SetX(this Vector3 v, float x)
    {
        return new Vector3(x, v.y, v.z);
    }

    /// <summary>
    ///  Set the Y value of this vector2
    /// </summary>
    /// <param name="v"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static Vector3 SetY(this Vector3 v, float y)
    {
        return new Vector3(v.x, y, v.z);
    }

    /// <summary>
    ///  Set the Y value of this vector2
    /// </summary>
    /// <param name="v"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public static Vector3 SetZ(this Vector3 v, float z)
    {
        return new Vector3(v.x, v.y, z);
    }

    /// <summary>
    /// Converts this vector3 to vector3Int
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static Vector3Int ToVector3Int(this Vector3 v)
    {
        return new Vector3Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y), Mathf.RoundToInt(v.z));
    }

    /// <summary>
    /// Convert this vector3 to vector2
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static Vector2 ToVector2(this Vector3 v)
    {
        return new Vector2(v.x, v.y);
    }

    /// <summary>
    /// Convert this vector3 to vector4
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static Vector4 ToVector4(this Vector3 v)
    {
        return new Vector4(v.x, v.y, v.z, 0);
    }
    
    /// <summary>
	/// Gets the magnitude on an axis given a <see cref="Vector3"/>.
	/// </summary>
	/// <param name="vector">The vector.</param>
	/// <param name="axis">The axis on which to calculate the magnitude.</param>
	/// <returns>The magnitude.</returns>
	public static float GetMagnitudeOnAxis(this Vector3 vector, Vector3 axis)
	{
		var vectorMagnitude = vector.magnitude;
		if (vectorMagnitude <= 0)
		{
			return 0;
		}
		var dot = Vector3.Dot(axis, vector / vectorMagnitude);
		var val = dot * vectorMagnitude;
		return val;
	}
	
	/// <summary>
	/// Get the square magnitude from vectorA to vectorB.
	/// </summary>
	/// <returns>The sqr magnitude.</returns>
	/// <param name="vectorA">First vector.</param>
	/// <param name="vectorB">Second vector.</param>
	public static float SqrMagnitudeFrom(this Vector3 vectorA, Vector3 vectorB)
	{
		var diff = vectorA - vectorB;
		return diff.sqrMagnitude;
	}

    #endregion

    #region Vector4

    /// <summary>
    /// Resets this vector to 0
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static Vector4 Reset(this Vector4 v)
    {
        return Vector4.zero;
    }

    /// <summary>
    /// Adds the X value to this vector4
    /// </summary>
    /// <param name="v"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector4 ModifyX(this Vector4 v, float x)
    {
        return new Vector4(v.x + x, v.y, v.z, v.w);
    }

    /// <summary>
    /// Adds the Y value to this vector4
    /// </summary>
    /// <param name="v"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static Vector4 ModifyY(this Vector4 v, float y)
    {
        return new Vector4(v.x, v.y + y, v.z, v.w);
    }

    /// <summary>
    /// Adds the Z value to this vector4
    /// </summary>
    /// <param name="v"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public static Vector4 ModifyZ(this Vector4 v, float z)
    {
        return new Vector4(v.x, v.y, v.z + z, v.w);
    }

    /// <summary>
    /// Adds the W value to this vector4
    /// </summary>
    /// <param name="v"></param>
    /// <param name="w"></param>
    /// <returns></returns>
    public static Vector4 ModifyW(this Vector4 v, float w)
    {
        return new Vector4(v.x, v.y, v.z, v.w + w);
    }

    /// <summary>
    /// Set the X value of this vector4
    /// </summary>
    /// <param name="v"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector4 SetX(this Vector4 v, float x)
    {
        return new Vector4(x, v.y, v.z, v.w);
    }

    /// <summary>
    ///  Set the Y value of this vector4
    /// </summary>
    /// <param name="v"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static Vector4 SetY(this Vector4 v, float y)
    {
        return new Vector4(v.x, y, v.z, v.w);
    }

    /// <summary>
    ///  Set the Y value of this vector4
    /// </summary>
    /// <param name="v"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public static Vector4 SetZ(this Vector4 v, float z)
    {
        return new Vector4(v.x, v.y, z, v.w);
    }

    /// <summary>
    ///  Set the W value of this vector4
    /// </summary>
    /// <param name="v"></param>
    /// <param name="w"></param>
    /// <returns></returns>
    public static Vector4 SetW(this Vector4 v, float w)
    {
        return new Vector4(v.x, v.y, v.z, w);
    }

    /// <summary>
    /// Convert this vector4 to vector2
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static Vector2 ToVector2(this Vector4 v)
    {
        return new Vector2(v.x, v.y);
    }

    /// <summary>
    /// Convert this vector4 to vector3
    /// </summary> 
    /// <param name="v"></param>
    /// <returns></returns>
    public static Vector3 ToVector3(this Vector4 v)
    {
        return new Vector3(v.x, v.y, v.z);
    }

    #endregion
}
