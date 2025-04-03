using UnityEngine;

public class KeplerBumper : Bumper
{
    private int hp;

    private void Start()
    {
        hp = 5;
    }

    protected override void OnCollisionEnter2D(Collision2D col)
    {
        base.OnCollisionEnter2D(col);
        score += 200;
        hp--;
        Debug.Log(score);
        if (hp == 0) {
            Score(2000);

            // 'killing' the object without destroying it so it can be replaced next level
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<Renderer>().enabled = false;


        }

    }
}
