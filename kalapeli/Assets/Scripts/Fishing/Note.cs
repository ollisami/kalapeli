using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour {

    private bool isHit = false;
    private RectTransform trans;

	// Use this for initialization
	void Start () {
        trans = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
		if(trans.position.y < 0)
        {
            if(!isHit)
                FindObjectOfType<Kela>().TargetMissed();

            this.gameObject.SetActive(false);
        }
	}

    public void NoteHit(Color c)
    {
        if (isHit) return;
        this.gameObject.GetComponent<Image>().color = c;
        isHit = true;
    }
}
