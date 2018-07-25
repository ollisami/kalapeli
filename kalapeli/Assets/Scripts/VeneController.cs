using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VeneController : MonoBehaviour {

    public bool isAnchored = false;
    public GameObject joystick;
    public GameObject setAnchorButton;
    public GameObject releaseAnchorButton;
    public GameObject spinningWheel;

    // Use this for initialization
    void Start () {
        StartMode();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartMode()
    {
        joystick.SetActive(true);
        setAnchorButton.SetActive(true);
        releaseAnchorButton.SetActive(false);
        spinningWheel.SetActive(false);
    }

    public void SetSpinningWheel(bool showWheel)
    {
        releaseAnchorButton.SetActive(!showWheel);
        spinningWheel.SetActive(showWheel);
    }

    public void SetAnchored (bool anchored)
    {
        isAnchored = anchored;

        releaseAnchorButton.SetActive(isAnchored);
        setAnchorButton.SetActive(!isAnchored);
        joystick.SetActive(!isAnchored);
    }
}
