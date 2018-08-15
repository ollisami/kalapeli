using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowGuide : MonoBehaviour {

    public float startTimer = 1.0F;
    private float timer;
    public Image guideImg;
    private float minY;
    public float maxY;
    private RectTransform trans;
    public float speed;

	// Use this for initialization
	void Start () {
        timer = startTimer;
        trans = GetComponent<RectTransform>();
        minY = trans.localPosition.y;
	}
	
	// Update is called once per frame
	void Update () {
		if(timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0) guideImg.enabled = true;
            return;
        }

        Vector3 pos = trans.localPosition;
        pos.y += speed * Time.deltaTime;
        if (pos.y >= maxY) pos.y = minY;
        trans.localPosition = pos;
        
	}

    private void OnEnable()
    {
        guideImg.enabled = false;
        timer = startTimer;
    }
}
