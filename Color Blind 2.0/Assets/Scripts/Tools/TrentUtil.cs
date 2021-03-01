using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trent
{
	public class TrentUtil : MonoBehaviour
	{
		/// <summary>
		///  This returns the angle between 3 vectors
		/// </summary>
		public static float GetAngle(Vector2 A, Vector2 B, Vector2 C)
		{
			return Vector2.SignedAngle(A - B, C - B);
		}
	}
}