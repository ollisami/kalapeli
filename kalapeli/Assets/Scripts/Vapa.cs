using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vapa : MonoBehaviour {

    public GameObject viehePrefab;
    public VeneController veneController;
    public Transform vieheSpawnPoint;
    public GameController gameController;

    private LineRenderer lineRenderer;
    private GameObject viehe;

    Vector3 touchStartPoint;
    Vector3 touchCurrentPoint;

    private bool readyToThrow = true;

    // Use this for initialization
    void Start () {
        lineRenderer = GetComponent<LineRenderer>();
        InstantiateViehe();
    }
	
	// Update is called once per frame
	void Update () {
        if (veneController.CanThrow())
        {
            if (Input.GetMouseButtonDown(0))
                MousePressedDown();

            if (Input.GetMouseButton(0))
                MouseDrag();

            if (Input.GetMouseButtonUp(0))
                MouseUp();            
        }
        if (viehe == null)
        {
            InstantiateViehe();
        }
        else
        {
            lineRenderer.SetPosition(0, vieheSpawnPoint.position);
            lineRenderer.SetPosition(1, viehe.transform.localPosition);
        }
    }

    void InstantiateViehe()
    {
        if (!veneController.KalaPanel.activeInHierarchy && gameController.gameHasEnded)
        {
            gameController.ShowHighscores();
            return;
        }

        if (viehe != null) Destroy(viehe);
        viehe = Instantiate(viehePrefab, transform.position + vieheSpawnPoint.position, Quaternion.identity);
        viehe.GetComponent<Rigidbody>().isKinematic = true;
        viehe.GetComponent<Viehe>().vapa = this;
    }

    public void ShootViehe(float speed)
    {
        if (viehe != null && speed > 0.1F)
        {
            Rigidbody vieheRb = viehe.GetComponent<Rigidbody>();
            vieheRb.isKinematic = false;
            Vector3 upforce = this.transform.up * (1.5F * speed);
            upforce = new Vector3(Mathf.Min(upforce.x, 1.5F), Mathf.Min(upforce.y, 1.5F), Mathf.Min(upforce.z, 1.5F));
            Vector3 fwdforce = this.transform.forward * (5.0F * speed);
            fwdforce = new Vector3(Mathf.Min(fwdforce.x, 3), Mathf.Min(fwdforce.y, 3), Mathf.Min(fwdforce.z, 3));
            vieheRb.AddForce(upforce, ForceMode.Impulse);
            vieheRb.AddForce(fwdforce, ForceMode.Impulse);
            ShowSpinningWheel(true);
        }
    }

    void MousePressedDown()
    {
        touchStartPoint = Input.mousePosition;
    }

    void MouseDrag()
    {
        touchCurrentPoint = Input.mousePosition;
    }
    void MouseUp()
    {
        if (!readyToThrow)
        {
            readyToThrow = true;
            return;
        }
        ShootViehe((touchCurrentPoint.y - touchStartPoint.y) / Screen.height);
        touchStartPoint = Vector3.zero;
        touchCurrentPoint = Vector3.zero;
    }

    public void ShowSpinningWheel(bool show)
    {
        veneController.SetSpinningWheel(show);
        lineRenderer.enabled = show;
        if (!show)
        {
            Destroy(viehe);
        } else
        {
            readyToThrow = false;
        }
    }

}
