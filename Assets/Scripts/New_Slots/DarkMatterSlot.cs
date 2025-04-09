using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class DarkMatterSlot : Slot
{
    public int positiveScore = 1000;
    public int negativeScore = -500;

    protected override void Score(int score)
    {
        int roll = Random.Range(0, 2); // 0 or 1
        int finalScore = (roll == 0) ? positiveScore : negativeScore;
        GameManager.Instance.addScore(finalScore);
    }
}
