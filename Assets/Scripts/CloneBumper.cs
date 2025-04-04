using UnityEngine;
using System.Collections.Generic;

public class CloneBumper : Bumper
{
    public GameObject objectToSpawn;
    private int spawnHeight = 21;

    protected override void OnCollisionEnter2D(Collision2D col)
    {
        base.OnCollisionEnter2D(col);
        score += 100;
        Debug.Log(score);
        if (col.gameObject.CompareTag("mainPinball"))
        {
            float randomValue = Random.Range(-4f, 6f); // Both values are inclusive
            Vector2 spawnPosition = new Vector2(randomValue, spawnHeight);
            GameObject ghostBall = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
            GameManager.instance.GhostList.Add(ghostBall);
        }
    }
}
