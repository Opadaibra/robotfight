using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetScore : MonoBehaviour
{
    public TextMeshProUGUI newScore;
    public TextMeshProUGUI newHighScore;
    //PlayerManger PM;

    private void Update()
    {

        newScore.text = PlayerRifleManger.instance.CurrentScore.ToString() ;
        newHighScore.text = PlayerRifleManger.instance.HighScorenum.ToString();
    }
}
