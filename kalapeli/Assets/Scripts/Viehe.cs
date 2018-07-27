using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viehe : MonoBehaviour {

    public Vapa vapa;
    private Rigidbody rb;
    private bool hasTouchedWater = false;
    public Kala kala;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();	
	}
	
	// Update is called once per frame
	void Update () {
        if (vapa != null)
        {
            if (rb.isKinematic)
            {
                transform.position = vapa.vieheSpawnPoint.position;
            } else
            {
                if(hasTouchedWater && Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(vapa.transform.position.x, vapa.transform.position.z)) < 4)
                {
                    if(kala != null)
                    {
                        Debug.Log("Hyvä homma hermanni! " + kala.laji + " " + kala.weight);
                        Destroy(kala.gameObject);
                    }
                    vapa.ShowSpinningWheel(false);
                }
            }
        }
	}
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Vesi"))
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.drag = 20;
            hasTouchedWater = true;
            vapa.ShowSpinningWheel(true);
        }
    }

    public void MoveTowardsShip(float speed)
    {
        rb.AddForce((vapa.vieheSpawnPoint.position - transform.position) * speed);
    }

}
