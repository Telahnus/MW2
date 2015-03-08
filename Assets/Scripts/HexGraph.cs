using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HexGraph : MonoBehaviour {

	public GameObject hex;
	public int maxPlayers;
	public int rows;
	public int cols;

	public Hashtable map;
	public List<Village> villages;

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
				t.initialize (key,maxPlayers+1);
				map.Add (key, GO);
			}
		}
	}

	void setNeighbours()
	{
		foreach (DictionaryEntry DE in map) {
			GameObject GO = (GameObject)DE.Value;
			Tile t = GO.GetComponent<Tile> ();
			for (int i=0; i<6; i++) {
				GameObject n = (GameObject)map [t.dir [i]];
				if (n != null)
					t.neighbours.Add (n);
			}
		}
	}

	void initializeRegions()
	{
		foreach (DictionaryEntry DE in map) 
		{
			GameObject GO = (GameObject)DE.Value;
			Tile t = GO.GetComponent<Tile> ();
			if (!t.isChecked){
				Village v = new Village();
				v.region = new List<GameObject>();
				BFS (GO, t, v);
				villages.Add (v);
			}
		}
	}

	void BFS(GameObject GO, Tile t, Village v)
	{
		if (!t.isChecked) 
		{
			t.isChecked=true;
			v.region.Add (GO);
			foreach (GameObject n in t.neighbours)
			{
				Tile s = n.GetComponent<Tile> ();
				if (!s.isChecked && (t.myColor == s.myColor))
					BFS (n,s,v);
			}
		}
	}

	void removeRegions(){
		foreach (Village v in villages) {
			if (v.region.Count<3)
			{
				foreach (GameObject GO in v.region)
				{
					GO.renderer.material.color = Color.white;
				}
			}
		}
	}

	void Start()
	{
		map = new Hashtable();
		villages = new List<Village>();
		setSizes ();
		createGrid ();
		setNeighbours ();
		initializeRegions ();
		removeRegions ();
		print (villages.Count);
	}

}
