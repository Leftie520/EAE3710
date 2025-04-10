using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class MoblizeBumper : Bumper
{
   protected override void OnCollisionEnter2D(Collision2D col)
    {
        base.OnCollisionEnter2D(col);
        score += 100;
        Debug.Log(score);
        GameManager.instance.ball.bonus += 100;
    }
}
