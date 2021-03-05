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

		// ** NOTE
		// To add a color to the game just add another value here
		// then add another case to GetColor below
		public enum gameColor
		{
			red,
			green,
			blue
		}

		/// <summary>
		/// This script gets the color to shade something
		/// from a gameColor
		/// </summary>
		public static Color GetColor(gameColor color)
		{
			switch (color)
			{
				case gameColor.red:
					return new Color(1, 0, 0, 1);
				case gameColor.green:
					return new Color(0, 1, 0, 1);
				case gameColor.blue:
					return new Color(0, 0, 1, 1);
				default:
					return new Color(1, 0, 0, 1);
			}
		}
	}
}