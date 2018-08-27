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
    private HashSet<GameObject> notes = new HashSet<GameObject>();
    public GameObject notesMaskObj;
    private RectTransform maskTrans;
    private Vector3 maskTransStartPosition;
    public float noteSpeed = 100.0F;
    public Color noteHitColor;
    private Kala targetFish;
    public int misses;
    public int allowedMisses;

    private int index = 0;
    private byte curr = 0;
    private byte prev = 0;
    private byte next = 0;

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
    }

    void FixedUpdate()
    {
        if (viehe == null) viehe = FindObjectOfType<Viehe>();

        if (Input.GetMouseButton(0))
        {
            AudioController.instance.PlaySound("kelaus");
            viehe.MoveTowardsShip(spinningPower);

            if(hasFish)
            {
                results.Clear();
                m_PointerEventData = new PointerEventData(m_EventSystem)
                {
                    position = this.transform.position
                };
                m_Raycaster.Raycast(m_PointerEventData, results);
                bool didHit = false;
                foreach (RaycastResult result in results)
                {
                    if(result.gameObject.tag.Equals("KelausPiece"))
                    {
                        didHit = true;
                        result.gameObject.GetComponent<Note>().NoteHit(noteHitColor);
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
            SetNotes();
            if (misses > allowedMisses)
            {
                HideNotes();
                targetFish.Released();
                return;
            }
            Vector3 pos = maskTrans.localPosition;
            pos.y -= Time.deltaTime * noteSpeed;
            maskTrans.localPosition = pos;
        }   
    }

    public void FishCaught(Kala kala)
    {
        targetFish = kala;
        misses = 0;
        allowedMisses = Mathf.Max(200 - (int)kala.weight, 0);

        maskTrans.localPosition = maskTransStartPosition;
        index = 0;
        curr = 0;
        prev = 0;
        next = 0;

        hasFish = true;
    }

    public void HideNotes()
    {
        foreach (GameObject go in notes)
        {
            Destroy(go);
        }
        notes.Clear();
        hasFish = false;
    }

    private void SetNotes()
    {
        if (notes.Count < 50)
        {
            index++;
            curr = next;
            next = Random.value < 0.7F ? (byte)1 : (byte)0;

            if (curr == 1)
            {
                GameObject go = Instantiate(kelausPrefab, maskTrans);
                notes.Add(go);
                RectTransform trans = go.GetComponent<RectTransform>();
                trans.localPosition = new Vector3(0, 300 + index * trans.sizeDelta.y, 0);
                Note note = go.GetComponent<Note>();
                note.kela = this;
                if (prev == 0)
                {
                    if (next == 0)
                    {
                        note.SetSprite(kelaSingle);
                    }
                    else
                    {
                        note.SetSprite(kelaBottom);
                    }
                }
                else if (next == 0)
                {
                    note.SetSprite(kelaTop);
                }
            }
            prev = curr;
        }
    }

    public void DestroyNote(GameObject go, bool isHit)
    {
        if (!hasFish) return;
        if (!isHit) misses += 10;

        if (notes.Contains(go))
        {
            notes.Remove(go);
        }
        Destroy(go);
    }
}