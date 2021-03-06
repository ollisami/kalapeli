﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;

public class OnlineHighScore : MonoBehaviour
{
    public GameObject hsObjectPrefab;
    public GameObject hsObjectPrefabPlayer;
    public GameObject loadingText;
    public GameController gameController;
    public GameObject tryAgainButton;

    private bool postSucc = false;
    private string URL = "https://this-is-secret.com";
    public TextAsset urlAsset;

    public ScrollRect scrollRect;
    public bool scroll = false;

    /* TODO: Trigger this when game starts
    private void Awake()
    {
        if (urlAsset != null)
            URL = urlAsset.text;
        WakeUpServer();
    }
    */

    public void Update()
    {
        if (scroll && scrollRect != null)
        {
            scrollRect.verticalNormalizedPosition += Time.deltaTime;
            if (scrollRect.verticalNormalizedPosition >= 1) scroll = false;
            Debug.Log(scrollRect.verticalNormalizedPosition);
        }
    }

    public void ShowHighScores()
    {
        if(urlAsset != null)
            URL = urlAsset.text;
        
        loadingText.SetActive(true);
        tryAgainButton.SetActive(false);
        if (!postSucc)
            DoPost();
        else
            DoFetch();
    }

    private void DoPost()
    {
        string userName = PlayerPrefs.GetString("username", "player");
        int userScore = gameController.score;
        string json = "{'name':'" + userName + "', 'score':'" + userScore + "'}";
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Content-Type", "application/json");
        json = json.Replace("'", "\"");
        byte[] postData = System.Text.Encoding.UTF8.GetBytes(json);
        //Now we call a new WWW request
        WWW www = new WWW(URL, postData, headers);
        //And we start a new co routine in Unity and wait for the response.
        StartCoroutine(WaitForPost(www));
    }

    private void DoFetch()
    {
        WWW www = new WWW(URL);
        StartCoroutine(WaitForRequest(www));
    }

    private void WakeUpServer()
    {
        WWW www = new WWW(URL);
        StartCoroutine(WaitForWakeup(www));
    }

    IEnumerator WaitForWakeup(WWW www)
    {
        yield return www;
        if (www.error == null)
        {
            //Print server response
            Debug.Log("Server alive!" + www.text);
        }
        else
        {
            //Something goes wrong, print the error response
            Debug.Log(www.error);
        }

    }


    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;

        if (www.error == null)
        {
            Debug.Log("WWW Ok!: " + www.text);
            JSONNode test = JSONNode.Parse(www.text);
            JSONArray count = test.AsArray;

            List<HSObject> userList = new List<HSObject>();
            for (int i = 0; i < count.Count; i++)
            {
                HSObject hsObj = new HSObject();
                hsObj.name = test[i]["name"].Value;
                hsObj.score = int.Parse(test[i]["score"].Value);
                userList.Add(hsObj);
            }
            CreateHighscoreList(userList);
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
            tryAgainButton.SetActive(true);
        }
    }

    IEnumerator WaitForPost(WWW www)
    {
        yield return www;
        if (www.error == null)
        {
            //Print server response
            Debug.Log(www.text);
            postSucc = true;
            DoFetch();
        }
        else
        {
            //Something goes wrong, print the error response
            Debug.Log(www.error);
            tryAgainButton.SetActive(true);
        }

    }

    private void CreateHighscoreList(List<HSObject> hSObjects)
    {

        string userName = PlayerPrefs.GetString("username", "noName");
        int userScore = gameController.score;

        hSObjects.Sort(new StructComparer());
        hSObjects.Reverse();
        bool userShown = false;

        for (int i = 0; i < hSObjects.Count; i++)
        {
            HSObject obj = hSObjects[i];
            GameObject go = null;
            if (!userShown && obj.score == userScore && obj.name.Equals(userName))
            {
                go = Instantiate(hsObjectPrefabPlayer, this.transform);
                userShown = true;
            }
            else
            {
                go = Instantiate(hsObjectPrefab, this.transform);
            }

            go.GetComponent<HSUIObject>().SetValues(obj.name, obj.score, i + 1);
        }
        loadingText.SetActive(false);
        tryAgainButton.SetActive(false);

        if (scrollRect != null)
        {
            scroll = true;
        }
    }
}
