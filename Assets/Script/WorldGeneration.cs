﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    //public fields
    public int xLenght;
    public int zLenght;
    public int seed;
    public float terHeight;
    public float terDetail;
    public GameObject[] blocks;
    //private fields
    private new Transform transform;        //finds the parent by tag
    private Assets.Script.Tree trees;        //tree class

    // Start is called before the first frame update
    void Start()
    {
        //seed = Random.Range(100000, 999999);
        transform = GameObject.FindGameObjectWithTag("Enviroment").transform;
        trees = new Assets.Script.Tree(blocks);
        GenerateWorld();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateWorld()
    {
        for (int z = 0; z < zLenght; z++)
        {
            for (int x = 0; x < xLenght; x++)
            {
                int dirtLayers = Random.Range(3, 5);
                int yLimit = (int)(Mathf.PerlinNoise(((x / 2) + seed) / terDetail, (z / 2 + seed) / terDetail) * terHeight);
                for (int y = 0; y < yLimit; y++)
                {
                    GameObject block;
                    if (y == yLimit - 1)    //last layer
                    {
                        if (CanPlaceTree())
                        {
                            //place the tree
                            PlaceTree(new Vector3(x, y + 1, z));
                        }
                        block = Instantiate(blocks[0], new Vector3(x, y, z), Quaternion.identity);
                    }
                    else if (yLimit - y < dirtLayers)
                    {
                        //create dirtBlock and place it, then set its parent
                        block = Instantiate(blocks[1], new Vector3(x, y, z), Quaternion.identity);
                    }
                    else
                    {
                        //create stoneBlock and place it, then set its parent
                        block = Instantiate(blocks[2], new Vector3(x, y, z), Quaternion.identity);
                    }
                    block.transform.SetParent(transform);
                }
            }
        }
    }
    #region Tree Placement Script
    /// <summary>
    /// determine if a tree can be placed
    /// </summary>
    /// <returns>a boolean value which determine if the tree can be placed</returns>
    private bool CanPlaceTree()
    {
        int chance = Random.Range(1, 1000);      //tree spawn = 0,5%
        if (chance < 5)
        {
            return true;
        }
        else { return false; }
    }

    /// <summary>
    /// Place the tree in a determined position rapresented by the Vector3
    /// </summary>
    /// <param name="vector">Where the tree will be placed</param>
    private void PlaceTree(Vector3 vector)     //y = yLimit
    {
        int treeHeigh = 6; //Random.Range(4, 7);                     TESTING
        for (int i = 0; i < treeHeigh; i++)
        {
            //instantiate the block and set its parent
            GameObject block = Instantiate(blocks[3], new Vector3(vector.x, vector.y + i, vector.z), Quaternion.identity);
            block.transform.SetParent(transform);
        }
        PlaceTreeLeaves(treeHeigh, new Vector3(vector.x, vector.y + treeHeigh, vector.z));
    }
    /// <summary>
    /// Place leaves to the tree depending on its height
    /// </summary>
    /// <param name="height">height of the tree in blocks</param>
    /// <param name="pos">position of the top log of wood of the tree</param>
    private void PlaceTreeLeaves(int height, Vector3 pos)
    {
        if (height == 6)
        {
            for (int y = (int)(pos.y + 1); y > (int)(pos.y - 1); y--)
            {
                if (y == pos.y + 1)
                {
                    GameObject leavesBlock = Instantiate(blocks[4], new Vector3(pos.x, y, pos.z), Quaternion.identity);
                    leavesBlock.transform.SetParent(transform);
                }
                else if (y == pos.y)
                {
                    trees.CreateLeavesLayer2()
                    /*GameObject leavesBlock1 = Instantiate(blocks[4], new Vector3(pos.x + 1, y, pos.z), Quaternion.identity);
                    leavesBlock1.transform.SetParent(transform);
                    GameObject leavesBlock2 = Instantiate(blocks[4], new Vector3(pos.x - 1, y, pos.z), Quaternion.identity);
                    leavesBlock2.transform.SetParent(transform);
                    GameObject leavesBlock3 = Instantiate(blocks[4], new Vector3(pos.x, y, pos.z + 1), Quaternion.identity);
                    leavesBlock3.transform.SetParent(transform);
                    GameObject leavesBlock4 = Instantiate(blocks[4], new Vector3(pos.x, y, pos.z - 1), Quaternion.identity);
                    leavesBlock4.transform.SetParent(transform);*/
                }
                else if(y == pos.y - 1)
                {
                    
                }
            }
        }
    }
    #endregion 
}
