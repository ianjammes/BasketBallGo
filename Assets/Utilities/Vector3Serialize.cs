using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Vector3Serialize
{

	private float x;
	private float y;
	private float z;

	public Vector3Serialize() { }
	public Vector3Serialize(Vector3 vec3)
	{
		this.x = vec3.x;
		this.y = vec3.y;
		this.z = vec3.z;
	}

	public static implicit operator Vector3Serialize(Vector3 vec3)
	{
		return new Vector3Serialize(vec3);
	}
	public static explicit operator Vector3(Vector3Serialize wb_vec3)
	{
		return new Vector3(wb_vec3.x, wb_vec3.y, wb_vec3.z);
	}
}
