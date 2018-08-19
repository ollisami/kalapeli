using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VeneController : MonoBehaviour {

    public bool isAnchored = false;
    public GameObject joystick;
    public GameObject setAnchorButton;
    public GameObject releaseAnchorButton;
    public GameObject spinningWheel;

    public GameObject KalaPanel;
    public Text kalaNameText;
    public Text kalaWeightText;
    public Image kalaImage;
    public Text pointsText;
    public Text bonusText;
    public GameController gameController;

    public KalaController kalaController;
    public Sprite[] kalaSprites;

    public GameObject throwGuide;

    private float cooldown = 1.0F;


    // Use this for initialization
    void Start () {
        StartMode();
    }

    private void Update()
    {
        if (cooldown > 0) cooldown -= Time.deltaTime;
    }

    public void StartMode()
    {
        DisableAll();
        isAnchored = true;
        releaseAnchorButton.SetActive(true);
        throwGuide.SetActive(true);
    }

    public bool CanThrow()
    {
        return isAnchored && !KalaPanel.activeInHierarchy && !spinningWheel.activeInHierarchy;
    }

    public void SetSpinningWheel(bool showWheel)
    {
        if(showWheel)
        {
            DisableAll();
            spinningWheel.SetActive(showWheel);
        } else
        {
            spinningWheel.SetActive(false);
            releaseAnchorButton.SetActive(true);
        }
    }

    public void SetAnchored (bool anchored)
    {
        isAnchored = anchored;
        releaseAnchorButton.SetActive(isAnchored);
        throwGuide.SetActive(isAnchored);

        setAnchorButton.SetActive(!isAnchored);
        joystick.SetActive(!isAnchored);
        if(anchored) AudioController.instance.PlaySound("ankkuri");
    }

    public void HideKalaScreen()
    {
        if (cooldown > 0) return;
        DisableAll();
        isAnchored = true;
        releaseAnchorButton.SetActive(true);
        setAnchorButton.SetActive(false);
        joystick.SetActive(false);
    }

    private string SizeString(float size)
    {
        string sizeString = "";
        if (size < 5) sizeString =  "Pikku";
        if (size > 10) sizeString =  "Iso";
        if (size > 20) sizeString = "Jätti";
        if (size > 50) sizeString = "Hirmu";
        if (size > 100) sizeString = "Uskomaton";
        return sizeString;
    }

    public void ShowKalaScreen(Kala kala)
    {
        DisableAll();
        KalaPanel.SetActive(true);
        string sizeString = kala.laji.Equals("Seaweed") ? "" : SizeString(kala.weight);
        if (sizeString.Length != 0 && !sizeString.Equals("Pikku") )
        {
            AudioController.instance.StopPlaying();
            AudioController.instance.PlaySound("kalaNostettu");
        }

        kalaNameText.text = (sizeString.Length == 0 ? "" : sizeString + " ") + kala.laji;
        kalaWeightText.text = kala.weight.ToString("F2") + " kg";
        kalaImage.sprite = kala.gameObject.GetComponent<SpriteRenderer>().sprite;

        int points = Mathf.FloorToInt(kala.weight * 100);
        int bonusPoints = 0;

        pointsText.text = "Pisteet\n" + points;
        if (kalaController.CheckTargetFish(kala))
        {
            bonusPoints = points * 3;
            bonusText.text = "Bonus\n" + bonusPoints;
        }
        else
            bonusText.text = "";
        gameController.AddScore(points + bonusPoints);
        cooldown = 1.0F;
    }

    private int GetSpriteID(string kalaName)
    {
        if (kalaName.Equals("Hauki")) return 0;
        if (kalaName.Equals("Särki")) return 1;
        if (kalaName.Equals("Taimen")) return 2;
        if (kalaName.Equals("Peto")) return 3;
        if (kalaName.Equals("Harjus")) return 4;
        if (kalaName.Equals("Made")) return 5;
        if (kalaName.Equals("Seaweed")) return 6;
        return 0;
    }

    public Sprite GetKalaImage(string name){
        return kalaSprites[GetSpriteID(name)];
    }

    private void DisableAll()
    {
        joystick.SetActive(false);
        setAnchorButton.SetActive(false);
        releaseAnchorButton.SetActive(false);
        releaseAnchorButton.SetActive(false);
        spinningWheel.SetActive(false);
        KalaPanel.SetActive(false);
        throwGuide.SetActive(false);
    }

}
