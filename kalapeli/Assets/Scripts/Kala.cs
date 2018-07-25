using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kala : MonoBehaviour {

    public string name;
    public float maxWeight;
    public float minWeight;

    private Rigidbody rb;
    private float weight;
    public float swimSpeed;
    private float scale;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        weight = Random.Range(minWeight, maxWeight);
        swimSpeed = Random.Range(0.01F, 0.1F);
        scale = (weight / (maxWeight - minWeight)) * 9 + 1;
        transform.localScale = new Vector3(scale, scale, scale);
	}
	
	// Update is called once per frame
	void Update () {
        if(transform.position.y < -1)
        {
            if (Random.value > 0.8F)
                rb.AddForce(new Vector3(Random.value, Random.value, Random.value) * swimSpeed * scale, ForceMode.Impulse);
        } else
        {
            rb.AddRelativeForce(Vector3.down * swimSpeed * scale);
        }
	}
}
