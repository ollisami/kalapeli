using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VeneController : MonoBehaviour {

    public bool isAnchored = false;
    public GameObject joystick;
    public GameObject setAnchorButton;
    public GameObject releaseAnchorButton;

    // Use this for initialization
    void Start () {
        SetAnchored(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetAnchored (bool anchored)
    {
        isAnchored = anchored;

        releaseAnchorButton.SetActive(isAnchored);
        setAnchorButton.SetActive(!isAnchored);
        joystick.SetActive(!isAnchored);
    }
}
