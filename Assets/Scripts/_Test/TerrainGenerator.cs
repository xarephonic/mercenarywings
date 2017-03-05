using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainGenerator : MonoBehaviour {

	public GameObject terrainPrefab;
	public GameObject[,] terrs;

	void TerrGen (GameObject g, int x,int y)
	{
		TerrainData tData = new TerrainData();
		tData.heightmapResolution = 513;
		tData.SetDetailResolution(1024,8);
		tData.size = new Vector3(500,600,500);

		Terrain terrain = g.GetComponent<Terrain> ();
		terrain.terrainData = tData;
		float[,] heights = new float[terrain.terrainData.heightmapWidth, terrain.terrainData.heightmapHeight];
		for (int i = 0; i < terrain.terrainData.heightmapWidth; i++) {
			for (int k = 0; k < terrain.terrainData.heightmapHeight; k++) {
				heights [i, k] = Mathf.PerlinNoise (x,y) ;
			}
		}
		terrain.terrainData.SetHeights (0, 0, heights);
	}

	// Use this for initialization
	void Start () {
		terrs = new GameObject[3,3];
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++) {
				GameObject terr = Terrain.CreateTerrainGameObject(null);
				terr.transform.position = new Vector3(-500*i,0,500*j);
				terrs[i,j] = terr;
				TerrGen(terr,i*-500/1500,j*500/1500);
			}
		}


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
