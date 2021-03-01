using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager singleton;
	void Awake(){
		singleton = this;
	}

	// All game manager related things go in here
	// E.g. Player stats
	// Which are all read by other scripts

}
