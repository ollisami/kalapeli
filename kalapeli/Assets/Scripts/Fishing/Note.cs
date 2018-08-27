using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour {

    private bool isHit = false;
    private RectTransform trans;
    private Image img;
    public Kela kela;

	// Use this for initialization
	void Start () {
        trans = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
		if(trans.position.y < 0)
        {
            if(kela != null)
                kela.DestroyNote(this.gameObject, isHit);
        }
	}

    public void NoteHit(Color c)
    {
        if (isHit) return;
        if (img == null) img = this.gameObject.GetComponent<Image>();
        img.color = c;
        isHit = true;
    }

    public void SetSprite (Sprite sprite)
    {
        if (img == null) img = this.gameObject.GetComponent<Image>();
        img.sprite = sprite;
    }
}
