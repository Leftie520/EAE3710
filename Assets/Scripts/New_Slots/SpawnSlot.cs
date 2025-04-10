using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class SpawnSlot : Slot
{
    public GameObject slotToSpawn;
    public Transform[] spawnPoints;

    protected override void Score(int score)
    {
        base.Score(score);

        if (slotToSpawn != null && spawnPoints.Length > 0)
        {
            int index = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[index];

            GameObject spawned = Instantiate(slotToSpawn, spawnPoint.position, Quaternion.identity);
            spawned.tag = "GeneratedSlot";
        }
    }
}