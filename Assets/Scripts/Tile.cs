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
	public Vector2[] dir;
	public GameObject tree;
	public GameObject meadow;
	public int myColor;
	public LandType myType;
	public bool isChecked;
	//private System.Random rand = new System.Random();

	public void initialize(Vector2 xy, int maxPlayers)
	{
		neighbours = new List<GameObject>();
		coord = xy;
		setDir ();
		setInitialType ();
		setInitialColor (maxPlayers);
		isChecked = false;
	}

	//set directions (aka coordinates) of neighbours
	public void setDir()
	{
		//tile coords = x,y
		dir = new Vector2[6];
		if (coord.y % 2 == 0) {
			dir [0] = new Vector2 (coord.x + 1, coord.y);
			dir [1] = new Vector2 (coord.x, coord.y - 1);
			dir [2] = new Vector2 (coord.x - 1, coord.y - 1);
			dir [3] = new Vector2 (coord.x - 1, coord.y);
			dir [4] = new Vector2 (coord.x - 1, coord.y + 1);
			dir [5] = new Vector2 (coord.x, coord.y + 1);
		} else {
			dir [0] = new Vector2 (coord.x + 1, coord.y);
			dir [1] = new Vector2 (coord.x + 1, coord.y - 1);
			dir [2] = new Vector2 (coord.x, coord.y - 1);
			dir [3] = new Vector2 (coord.x - 1, coord.y);
			dir [4] = new Vector2 (coord.x, coord.y + 1);
			dir [5] = new Vector2 (coord.x + 1, coord.y + 1);
		}
	}

	//randomly assign landtype and add decoration
	public void setInitialType()
	{	
		int type = Random.Range (1, 11);
		GameObject GO;
		Vector3 scale;
		if (type < 3) // 20% trees
		{
			GO = (GameObject)Instantiate (tree, transform.position, Quaternion.identity); //instantiate decoration
			scale = GO.transform.localScale; //save scaling
			GO.transform.parent = this.transform; //set parent tile
			GO.transform.Translate (0f, 0.1f, 0f); //move the decoration up a bit
			GO.transform.localScale = scale; //rescale the decoration
			GO.transform.eulerAngles = new Vector3(0,Random.Range (0,360),0); //give it a random rotation
			myType = LandType.Trees; //set the landtype attribute
		} 
		else if (type == 3) // 10% meadows
		{
			GO = (GameObject)Instantiate (meadow, transform.position, Quaternion.identity);
			scale = GO.transform.localScale;
			GO.transform.parent = this.transform;
			GO.transform.Translate (0f, 0.1f, 0f);
			GO.transform.localScale = scale;
			GO.transform.eulerAngles = new Vector3(0,Random.Range (0,360),0);
			myType = LandType.Meadow;
		}
		else
		{	
			myType = LandType.Grass;
		}
	}

	//randomly assign a color based on max number of players
	public void setInitialColor(int maxPlayers)
	{
		int color = Random.Range (1, maxPlayers+1);
		myColor = color; //set tile's color attribute
		switch (color) {
		case 1:
			renderer.material.color = Color.blue;
			break;
		case 2:
			renderer.material.color = Color.red;
			break;
		case 3:
			renderer.material.color = Color.green;
			break;
		case 4:
			renderer.material.color = Color.yellow;
			break;
		case 5:
			renderer.material.color = Color.cyan;
			break;
		case 6:
			renderer.material.color = Color.magenta;
			break;
		}
	}
}
