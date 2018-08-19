using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Text scoreText;
    public int score;

    public Light scenelight;
    public Color endColor;
    private Color startColor;

    public float sessionLenght = 300;
    private float sessionTime = 0;

    public GameObject highScores;
    public bool gameHasEnded = false;
    private bool showingHighscores = false;

	// Use this for initialization
	void Start () {
        score = 0;
        startColor = scenelight.color;
        sessionTime = 0;
	}
	
	// Update is called once per frame
	void Update () {

        if(!gameHasEnded && sessionTime < sessionLenght) {
            sessionTime += Time.deltaTime;
            float percent = sessionTime / sessionLenght;
            scenelight.color = Color.Lerp(startColor, endColor, percent);
        } else {
            gameHasEnded = true;
        }
		
	}

    public void ShowHighscores()
    {
        if (showingHighscores) return;
        AudioController.instance.StopPlaying();
        AudioController.instance.PlaySound("ilta");
        highScores.SetActive(true);
        showingHighscores = true;
    }

    public void AddScore (int addition) {
        score += addition;
        scoreText.text = "Score: " + score;
    }

    public void ReloadScene() {
        Application.LoadLevel(1);
    }
}
