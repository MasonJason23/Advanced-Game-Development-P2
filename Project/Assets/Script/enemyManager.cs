using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class enemyManager : MonoBehaviour
{
 
    public int numObjects = 10;
    public Transform player;
    public GameObject enemyPrefab;

    private void Start()
    {
        Vector3 center = player.position;
        CreateEnemiesAroundPoint(numObjects, center, 2f);
    }

    private void Update()
    {
        throw new NotImplementedException();
    }
    
    public void CreateEnemiesAroundPoint (int num, Vector3 point, float radius) {
        for (int i = 0; i < num; i++){
         
            /* Distance around the circle */  
            var radians = 2 * MathF.PI / num * i;
         
            /* Get the vector direction */ 
            var vertical = MathF.Sin(radians);
            var horizontal = MathF.Cos(radians); 
         
            var spawnDir = new Vector3 (horizontal, 0, vertical);
         
            /* Get the spawn position */ 
            var spawnPos = point + spawnDir * radius; // Radius is just the distance away from the point
         
            /* Now spawn */
            var enemy = Instantiate (enemyPrefab, spawnPos, Quaternion.identity) as GameObject;
         
            /* Rotate the enemy to face towards player */
            enemy.transform.LookAt(point);
         
            /* Adjust height */
            enemy.transform.Translate (new Vector3 (0, 0, enemy.transform.localScale.z / 2));
        }
    }    
}
