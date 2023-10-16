using UnityEngine;

namespace SpaceRails.Utilities
{
	public static class Vector3Extensions
	{
		public static Vector3 WithNewX(this Vector3 vec, float x)
		{
			vec.x = x;
			return vec;
		}

		public static Vector3 WithNewY(this Vector3 vec, float y)
		{
			vec.y = y;
			return vec;
		}

		public static Vector3 WithNewZ(this Vector3 vec, float z)
		{
			vec.z = z;
			return vec;
		}
	}
}