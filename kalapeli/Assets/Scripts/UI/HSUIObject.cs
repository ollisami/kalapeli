using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HSUIObject : MonoBehaviour {
    public Text nameText;
    public Text scoreText;
    public Text rankText;

    public void SetValues(string name, int score, int rank) {
        nameText.text = name;
        scoreText.text = "" + score;
        rankText.text = rank + ".";
    }
}
