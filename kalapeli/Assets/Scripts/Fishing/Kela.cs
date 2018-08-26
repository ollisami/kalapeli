using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Kela : MonoBehaviour
{
    public float spinningPower = 10.0F;
    private Viehe viehe;
    public GameObject kelausPrefab;
    public Sprite kelaSingle;
    public Sprite kelaTop;
    public Sprite kelaBottom;
    private bool hasFish = false;
    private List<byte> notes = new List<byte>();
    public GameObject notesMaskObj;
    private RectTransform maskTrans;
    private Vector3 maskTransStartPosition;
    public float noteSpeed = 100.0F;
    public Color noteHitColor;
    private Kala targetFish;
    public int misses;
    public int allowedMisses;

    private GraphicRaycaster m_Raycaster;
    private PointerEventData m_PointerEventData;
    private EventSystem m_EventSystem;
    private List<RaycastResult> results = new List<RaycastResult>();

    private void Awake()
    {
        m_Raycaster = FindObjectOfType<GraphicRaycaster>();
        m_EventSystem = FindObjectOfType<EventSystem>();

        maskTrans = notesMaskObj.GetComponent<RectTransform>();
        maskTransStartPosition = maskTrans.localPosition;
        notesMaskObj.SetActive(false);
        //SetNotes();
    }

    void Update()
    {
        if (viehe == null) viehe = FindObjectOfType<Viehe>();

        if (Input.GetMouseButton(0))
        {
            AudioController.instance.PlaySound("kelaus");
            viehe.MoveTowardsShip(spinningPower);

            if(hasFish)
            {
                results.Clear();
                m_PointerEventData = new PointerEventData(m_EventSystem);
                m_PointerEventData.position = this.transform.position;//Input.mousePosition;         
                m_Raycaster.Raycast(m_PointerEventData, results);
                bool didHit = false;
                foreach (RaycastResult result in results)
                {
                    if(result.gameObject.tag.Equals("KelausPiece"))
                    {
                        didHit = true;
                        result.gameObject.GetComponent<Note>().NoteHit(noteHitColor);
                        //Debug.Log("Hit " + result.gameObject.name);
                    }    
                }
                if(!didHit)
                {
                    misses++;
                }
            }
        }
        
        if(hasFish)
        {
            if(misses > allowedMisses)
            {
                notesMaskObj.SetActive(false);
                targetFish.Released();
                hasFish = false;
                return;
            }
            Vector3 pos = maskTrans.localPosition;
            pos.y -= Time.deltaTime * noteSpeed;
            maskTrans.localPosition = pos;
        }   
    }

    public void FishCaught(Kala kala)
    {
        notesMaskObj.SetActive(true);
        targetFish = kala;
        misses = 0;
        allowedMisses = Mathf.Max(200 - (int)kala.weight, 0);
        SetNotes();
        hasFish = true;
    }

    private void SetNotes()
    {
        foreach(Transform child in maskTrans)
        {
            Destroy(child.gameObject);
        }
        maskTrans.localPosition = maskTransStartPosition;

        while(notes.Count < 1000)
        {
            if(Random.value < 0.8F)
            {
                notes.Add(1);
                continue;
            }
            notes.Add(0);
        }

        for (int i = 0; i < notes.Count; i++)
        {
            if (notes[i] == 1) {
                GameObject go = Instantiate(kelausPrefab, maskTrans);
                RectTransform trans = go.GetComponent<RectTransform>();
                trans.localPosition = new Vector3(0, 300 + i * trans.sizeDelta.y, 0);
                Image img = go.GetComponent<Image>();
                if(i == 0 || notes[i-1] == 0)
                {
                    if (i == notes.Count - 1 || notes[i + 1] == 0)
                    {
                        img.sprite = kelaSingle;
                    }
                    else
                    {
                        img.sprite = kelaBottom;
                    }
                }
                else if (i == notes.Count - 1 || notes[i + 1] == 0)
                {
                    img.sprite = kelaTop;
                }
            }
        }
    }

    public void TargetMissed()
    {
        if (!hasFish) return;
        misses += 10;
    }
}