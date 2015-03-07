using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum LandType
{
	Grass,
	Trees,
	Meadow
}

public class Tile : MonoBehaviour {

	public Vector2 coord;
	public List<GameObject> neighbours;
	//private LandType myType;
	//private System.Random rand = new System.Random();

	void Start()
	{
		neighbours = new List<GameObject>();
	}


}
