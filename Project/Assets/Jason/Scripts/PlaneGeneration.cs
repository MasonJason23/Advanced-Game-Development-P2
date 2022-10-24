using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneGeneration : MonoBehaviour
{
    // Reference to plane and self. Player reference must be dragged on.
    public GameObject plane, player, parent;
    
    // Relative to how far the environment from the player is generated
    private int radius = 3;
    // Relative to how to separate each ground plane when generating new environment
    // 10 (plane offset) : 1 (ground plane size) relation
    private int planeOffset = 50;

    // Starting position, player gameObject must be at this location in order to generate the environment at the start
    private Vector3 startPos = Vector3.zero;

    private int XPlayerMove => (int)(player.transform.position.x - startPos.x);
    private int ZPlayerMove => (int)(player.transform.position.z - startPos.z);

    private int XPlayerLocation => (int)Mathf.Floor(player.transform.position.x / planeOffset) * planeOffset;
    private int ZPlayerLocation => (int)Mathf.Floor(player.transform.position.z / planeOffset) * planeOffset;

    // Keeps a history of where our last position is so that duplicate plane generations won't be made.
    Hashtable tilePlane = new Hashtable();

    void Update()
    {
        GenerateWorld();
    }

    // World generation from starting point and when player moves
    private void GenerateWorld () {
        if(startPos == Vector3.zero){
            GenerationHelper();
        }
        if (HasPlayerMoved(XPlayerMove, ZPlayerMove))
        {
            GenerationHelper();
        }
    }

    // Helper function to reduce code duplication
    private void GenerationHelper()
    {
        for (int x = -radius+1; x < radius; x++)
        {
            for (int z = -radius+1; z < radius; z++)
            {
                Vector3 pos = new Vector3((x * planeOffset + XPlayerLocation),
                    0,
                    (z * planeOffset + ZPlayerLocation));

                if (!tilePlane.Contains(pos))
                {
                    GameObject tile = Instantiate(plane, pos, Quaternion.identity, parent.transform);
                    tilePlane.Add(pos, tile);
                }
            }
        }
    }

    // Checks to make sure the player has moved
    private bool HasPlayerMoved(int playerX, int playerZ) {
        if (Mathf.Abs(XPlayerMove) >= planeOffset || Mathf.Abs(ZPlayerMove) >= planeOffset){
            return true;
        }
        return false;
    }
}