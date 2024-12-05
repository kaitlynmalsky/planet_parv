using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocks : MonoBehaviour
{
    public Terrain marsTerrain;
    void Start()
    {
        TerrainData terrainData = marsTerrain.terrainData;

        // Loop through the tree instances and instantiate them as GameObjects
        foreach (var rock in terrainData.treeInstances)
        {
            GameObject rockPrefab = terrainData.treePrototypes[rock.prototypeIndex].prefab;
            Vector3 position = Vector3.Scale(rock.position, terrainData.size) + marsTerrain.transform.position;
            Instantiate(rockPrefab, position, Quaternion.identity, marsTerrain.transform);
        }
    }
}
