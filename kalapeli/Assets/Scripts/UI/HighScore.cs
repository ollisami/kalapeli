


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour {

    public int highScoresCount = 5;
    public GameObject hsObjectPrefab;
    public GameObject hsObjectPrefabPlayer;
    public GameController gameController;

    public ScrollRect scrollRect;
    public bool scroll = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(scroll && scrollRect != null)
        {
            scrollRect.verticalNormalizedPosition += Time.deltaTime;
            if (scrollRect.verticalNormalizedPosition >= 1) scroll = false;
            Debug.Log(scrollRect.verticalNormalizedPosition);
        }
	}

    public void ShowHighScores()
    {
        string userName = PlayerPrefs.GetString("username", "noName");
        int userScore = gameController.score;

        List<HSObject> hSObjects = new List<HSObject>();
        HSObject cur = new HSObject();
        cur.name = userName;
        cur.score = userScore;
        hSObjects.Add(cur);

        for (int i = 0; i < highScoresCount; i++)
        {
            HSObject hSObject = new HSObject();
            hSObject.score = PlayerPrefs.GetInt("hsScores_" + i, 0);
            hSObject.name = PlayerPrefs.GetString("hsNames_" + i, "player");
            hSObjects.Add(hSObject);
        }

        hSObjects.Sort(new StructComparer());
        hSObjects.Reverse();
        hSObjects.RemoveAt(hSObjects.Count - 1);
        bool userShown = false;

        for (int i = 0; i < hSObjects.Count; i++)
        {
            HSObject obj = hSObjects[i];
            GameObject go = null;
            if (!userShown && obj.score == userScore && obj.name.Equals(userName))
            {
                 go = Instantiate(hsObjectPrefabPlayer, this.transform);
                userShown = true;
            } else
            {
                 go = Instantiate(hsObjectPrefab, this.transform);
            }

            go.GetComponent<HSUIObject>().SetValues(obj.name, obj.score, i + 1);

            PlayerPrefs.SetString("hsNames_" + i, obj.name);
            PlayerPrefs.SetInt("hsScores_" + i, obj.score);
        }
        if (scrollRect != null)
        {
            scroll = true;
        }

    }
}

public struct HSObject{
    public int score;
    public string name;
}


public class StructComparer : IComparer<HSObject>
{
    public int Compare(object x, object y)
    {
        if (!(x is HSObject) || !(y is HSObject)) return 0;
        HSObject a = (HSObject)x;
        HSObject b = (HSObject)y;
        return a.score.CompareTo(b.score);
    }

    public int Compare(HSObject x, HSObject y)
    {
        return x.score.CompareTo(y.score);
    }
}
