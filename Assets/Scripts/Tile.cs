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
	public GameObject tree;
	public GameObject meadow;
	//private LandType myType;
	//private System.Random rand = new System.Random();

	public void setInitialType()
	{	
		int type = Random.Range (1, 4);
		GameObject GO;
		Vector3 scale;

		switch(type)
		{
			case 1:
				//renderer.material.color = Color.green;
				break;
			case 2:
				//renderer.material.color = Color.red;
				GO = (GameObject)Instantiate (tree, transform.position, Quaternion.identity);
				scale = GO.transform.localScale;
				GO.transform.parent=this.transform;
				GO.transform.Translate(0f,0.1f,0f);
				GO.transform.localScale = scale;
				break;
			case 3:
				//renderer.material.color = Color.blue;
				GO = (GameObject)Instantiate (meadow, transform.position, Quaternion.identity);
				scale = GO.transform.localScale;
				GO.transform.parent=this.transform;
				GO.transform.Translate(0f,0.1f,0f);
				GO.transform.localScale = scale;
				break;
		}
	}

	void Start()
	{
		neighbours = new List<GameObject>();
	}


}
