using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TavoiteScreen : MonoBehaviour {

    public GameObject targetPanel;
    public Image targetImg;
    public Image targetButtonImage;
    public Text targetName;
    public VeneController veneController;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetVisible()
    {
        targetPanel.SetActive(true);
    }

    public void ToggleVisibility() {
        targetPanel.SetActive(!targetPanel.activeInHierarchy);
    }

    public void SetTavoite(Kala kala) {
        Sprite kalaSprite = kala.gameObject.GetComponent<SpriteRenderer>().sprite;
        targetImg.sprite = kalaSprite;
        targetButtonImage.sprite = kalaSprite;
        targetName.text = kala.laji;
    }
}
