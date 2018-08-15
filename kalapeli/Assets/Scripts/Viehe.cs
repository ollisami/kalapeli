using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viehe : MonoBehaviour {

    public Vapa vapa;
    private Rigidbody rb;
    public bool hasTouchedWater = false;
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
                if(hasTouchedWater && Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(vapa.vieheSpawnPoint.position.x, vapa.vieheSpawnPoint.position.z)) < 2)
                {
                    if(kala != null)
                    {
                        GameObject.FindObjectOfType<VeneController>().ShowKalaScreen(kala);
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
            AudioController.instance.PlaySound("plop");
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.drag = 15;
            hasTouchedWater = true;
            vapa.ShowSpinningWheel(true);
        }
    }

    public void MoveTowardsShip(float speed)
    {
        rb.AddForce((vapa.vieheSpawnPoint.position - transform.position) * speed);
    }

}
