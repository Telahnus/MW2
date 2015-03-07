using UnityEngine;
using System.Collections;

public class HexGraph : MonoBehaviour {

	public GameObject hex;
	public int rows;
	public int cols;

	private float hexWidth;
	private float hexHeight;

	private System.Random rand = new System.Random();

	void setSizes()
	{
		hexWidth = hex.renderer.bounds.size.x;
		hexHeight = hex.renderer.bounds.size.z;
	}

	void createGrid()
	{
		for (int y = 0; y<rows; y++) 
		{
			float offset = 0;
			if (y%2!=0) offset = hexHeight/2;
			for (int x=0; x<cols; x++)
			{
				Vector3 pos = new Vector3(y*hexWidth*3/4,0,x*hexHeight+offset);
				GameObject GO = (GameObject)Instantiate (hex, pos, Quaternion.identity);
				GO.transform.parent=this.transform;
				Tile t = GO.GetComponent<Tile>();
				t.coord = new Vector2(x,y);
				int type = rand.Next (1, 4);
				switch(type)
				{
					case 1:
						GO.renderer.material.color = Color.green;
						break;
					case 2:
						GO.renderer.material.color = Color.red;
						break;
					case 3:
						GO.renderer.material.color = Color.blue;
						break;
				}
			}
		}
	}

	void Start()
	{
		setSizes ();
		createGrid ();
	}

}
