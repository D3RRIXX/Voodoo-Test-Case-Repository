using UnityEngine;

namespace SpaceRails.Utilities
{
	public static class Vector3Extensions
	{
		public static Vector3 WithNewX(this Vector3 vec, float x) => new(x, vec.y, vec.z);
		public static Vector3 WithNewY(this Vector3 vec, float y) => new(vec.x, y, vec.z);
		public static Vector3 WithNewZ(this Vector3 vec, float z) => new(vec.x, vec.y, z);
	}
}