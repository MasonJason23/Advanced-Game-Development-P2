using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneGeneration : MonoBehaviour
{
    // Reference to plane and self. Player reference must be dragged on.
    public GameObject plane, player, parent;
    
    // Relative to how far the environment from the player is generated
    [SerializeField] private int radius = 3;
    // Relative to how to separate each ground plane when generating new environment
    // 10 (plane offset) : 1 (ground plane size) relation
    [SerializeField] private int planeOffset = 50;

    // Starting position, player gameObject must be at this location in order to generate the environment at the start
    private Vector3 startPos = Vector3.zero;

    private int XPlayerMove => (int)(player.transform.position.x - startPos.x);
    private int ZPlayerMove => (int)(player.transform.position.z - startPos.z);

    private int XPlayerLocation => (int)Mathf.Floor(player.transform.position.x / planeOffset) * planeOffset;
    private int ZPlayerLocation => (int)Mathf.Floor(player.transform.position.z / planeOffset) * planeOffset;

    // Keeps a history of where our last position is so that duplicate plane generations won't be made.
    Hashtable tilePlane = new Hashtable();
    
    // Custom class to store each ground plane and with their relative timestamps upon creation
    private class Tile
    {
        public float cTimestamp;
        public GameObject tileObject;

        public Tile(float cTimestamp, GameObject tileObject)
        {
            this.tileObject = tileObject;
            this.cTimestamp = cTimestamp;
        }
    }

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
    // This is where the generation of ground planes take place
    private void GenerationHelper()
    {
        // Used to keep a history of what tiles have been generated relative to player position and plane generation range
        Hashtable newTiles = new Hashtable();
        
        // Used as a key for each ground plane on the hashtable
        float cTime = Time.realtimeSinceStartup;
        
        for (int x = -radius; x < radius; x++)
        {
            for (int z = -radius; z < radius; z++)
            {
                // Used to check if a ground plane has been generated
                Vector3 pos = new Vector3((x * planeOffset + XPlayerLocation),
                    0,
                    (z * planeOffset + ZPlayerLocation));

                // if there is no ground plane in this position, generate one
                if (!tilePlane.Contains(pos))
                {
                    // Generating new ground plane
                    GameObject tile = Instantiate(plane, pos, Quaternion.identity, parent.transform);
                    
                    // Tile contains the specific ground plane game object and its timestamp.
                    Tile t = new Tile(cTime, tile);
                    
                    // Ground plane added into "history"
                    tilePlane.Add(pos, t);
                }
                // Update existing ground plane timestamp.
                else
                {
                    ((Tile)tilePlane[pos]).cTimestamp = cTime;
                }
            }
        }
        
        // Used to check if the ground plane is near the plane generation range
        // Timestamps is used to keep track if the player has left the area
        // If the timestamp of the ground plane equals the current time, then it is in range of the plane generator
        // Otherwise it will "unload" itself
        foreach (Tile t in tilePlane.Values)
        {
            if (!t.cTimestamp.Equals(cTime))
            {
                Destroy(t.tileObject);
            }
            else
            {
                newTiles.Add(t.tileObject, t);
            }
        }

        // Updates our "history" hashtable of the existing ground planes
        tilePlane = newTiles;
        
        // Updates current starting position since it is used to determine what ground planes should be "unloaded"
        startPos = player.transform.position;
    }

    // Checks to make sure the player has moved
    private bool HasPlayerMoved(int playerX, int playerZ) {
        if (Mathf.Abs(XPlayerMove) >= planeOffset || Mathf.Abs(ZPlayerMove) >= planeOffset){
            return true;
        }
        return false;
    }
}