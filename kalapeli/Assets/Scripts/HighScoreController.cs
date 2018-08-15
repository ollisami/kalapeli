using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreController : MonoBehaviour {

    public HighScore local;
    public OnlineHighScore online;

    private void OnEnable()
    {

        if (local != null) local.ShowHighScores();
        if (online != null) online.ShowHighScores();
    }
}
