using UnityEngine;
using System.Collections;

public class HexGraph : MonoBehaviour {

	public GameObject hex;
	public int maxPlayers;
	public int rows;
	public int cols;

	public Hashtable map;

	private float hexWidth;
	private float hexHeight;

	//private System.Random rand = new System.Random();

	void setSizes()
	{
		hexWidth = hex.renderer.bounds.size.x;
		hexHeight = hex.renderer.bounds.size.z;
	}

	void createGrid()
	{
		for (int y=0; y<rows; y++) 
		{
			float offset = 0;
			if (y%2!=0) offset = hexHeight/2;
			for (int x=0; x<cols; x++)
			{
				Vector3 pos = new Vector3(y*hexWidth*3/4,0,x*hexHeight+offset);
				GameObject GO = (GameObject)Instantiate (hex, pos, Quaternion.identity);
				GO.transform.parent=this.transform;
				Tile t = GO.GetComponent<Tile>();
				Vector2 key = new Vector2(x,y);
				t.initialize (key,maxPlayers);
				map.Add (key, GO);
			}
		}
	}

	void setNeighbours()
	{
		for (int y=0; y<rows; y++) {
			for (int x=0; x<cols; x++){
				Vector2 key = new Vector2(x,y);
				GameObject GO = (GameObject)map[key];
				Tile t = GO.GetComponent<Tile>();
				for (int i=0; i<6; i++)
				{
					GameObject n = (GameObject)map[t.dir[i]];
					if (n!=null)
					{
						t.neighbours.Add (n);
					}
				}
			}
		}
	}

	void Start()
	{
		map = new Hashtable();
		setSizes ();
		createGrid ();
		//remove tiles here!!
		setNeighbours ();
		GameObject GO = (GameObject)map[new Vector2(2,3)];
		Tile t = GO.GetComponent<Tile>();
		GameObject n = t.neighbours[1];
		Tile m = n.GetComponent<Tile> ();
		print (m.myType);
	}

}
