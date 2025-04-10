using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class WormholeSlot : Slot
{
    public static List<WormholeSlot> allWormholes = new List<WormholeSlot>();

    public float launchForce = 10f;
    public float teleportCooldown = 1f;

    private static Dictionary<int, float> teleportTimestamps = new Dictionary<int, float>();

    void OnEnable()
    {
        if (!allWormholes.Contains(this))
            allWormholes.Add(this);
    }

    void OnDisable()
    {
        if (allWormholes.Contains(this))
            allWormholes.Remove(this);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Rigidbody2D rb = col.GetComponent<Rigidbody2D>();
        if (rb == null) return;

        int id = col.gameObject.GetInstanceID();
        float lastTime = teleportTimestamps.ContainsKey(id) ? teleportTimestamps[id] : -999f;

        if (Time.time - lastTime < teleportCooldown) return;

        WormholeSlot destination = GetRandomDestination();
        if (destination != null && destination != this)
        {
            teleportTimestamps[id] = Time.time;

            rb.transform.position = destination.transform.position + Vector3.up * 0.5f;
            //rb.linearVelocity = Vector2.up * launchForce;
            Vector2 originalVel = rb.linearVelocity;
if (originalVel == Vector2.zero)
    originalVel = Vector2.up; // fallback in case ball is still

float angleOffset = Random.Range(-45f, 45f);
Vector2 newDir = Quaternion.Euler(0, 0, angleOffset) * originalVel.normalized;

rb.linearVelocity = newDir * launchForce;
        }

        Score(score);
    }

    WormholeSlot GetRandomDestination()
    {
        if (allWormholes.Count <= 1) return null;

        WormholeSlot target = this;
        while (target == this)
        {
            target = allWormholes[Random.Range(0, allWormholes.Count)];
        }

        return target;
    }

    protected override void Score(int score)
    {
        GameManager.Instance.addScore(score);
    }
}