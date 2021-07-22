using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
//using UnityEditor.SceneManagement;

public class MapGeneration : MonoBehaviour
{

    [Range(0, 100)]
    public int initChance;

    [Range(0, 8)]
    public int birthLimit;

    [Range(0, 8)]
    public int deathLimit;

    [Range(0, 10)]
    public int numR;
    private int count = 0;

    private int[,] terrainMap;
    public Vector3Int tmapSize;

    public Tilemap topMap;
    public Tilemap botMap;
    public Tile topTile;
    public Tile botTile;

    int width;
    int height;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            DoSim(numR);
        }

        if(Input.GetMouseButtonDown(1))
        {
            ClearMap(true);
        }

        if(Input.GetKeyDown("s"))
        {
            //EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene(), EditorSceneManager.GetActiveScene().path, true);
            
        }

    }

    public void DoSim(int numR)
    {
        ClearMap(false);
        width = tmapSize.x;
        height = tmapSize.y;
        if(terrainMap==null)
        {
            terrainMap = new int[width, height];
            InitPos();
        }

        for(int x=0;x<numR;x++)
        {
            terrainMap = GenTilePos(terrainMap);
        }


        for(int x=0; x<width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if(terrainMap[x,y]==1)
                {
                    topMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), topTile);
                    botMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), botTile);

                }
            }

        }
    }


    public int[,] GenTilePos(int[,] oldMap)
    {
        int[,] newMap = new int[width, height];
        int neighb;
        BoundsInt myB = new BoundsInt(-1, -1, 0, 3, 3, 1);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                neighb = 0;
                foreach(var b in myB.allPositionsWithin)
                {
                    if (b.x == 0 && b.y == 0) continue;
                    if(x+b.x>=0 && x+b.x<width && y+b.y>=0 && y+b.y<height)
                    {
                        neighb += oldMap[x + b.x, y + b.y];
                    }
                    else
                    {
                        neighb++;
                    }
                    if (oldMap[x, y] == 1)
                    {
                        if(neighb>deathLimit)
                        {
                            newMap[x, y] = 0;
                        }
                        else
                        {
                            newMap[x, y] = 1;
                        }

                    }
                    if (oldMap[x, y] == 0)
                    {
                        if (neighb < deathLimit)
                        {
                            newMap[x, y] = 1;
                        }
                        else
                        {
                            newMap[x, y] = 0;
                        }

                    }



                }
            }
        }
        return newMap;
        
    }
    public void ClearMap(bool complete)
    {
        topMap.ClearAllTiles();
        botMap.ClearAllTiles();

        if(complete)
        {
            terrainMap = null;
        }
    }

    public void InitPos()
    {
        for(int x=0;x<width;x++)
        {
            for(int y=0;y<height;y++)
            {
                terrainMap[x, y] = Random.Range(1, 101) < initChance ? 1 : 0;
            }
        }
    }
}
