using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VeneMovement : MonoBehaviour {

    public float speed;
    private Rigidbody rb;
    public VirtualJoystick joystick;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void FixedUpdate() {
		if(rb != null)
        {
            float moveHorizontal = joystick.Horizontal();
            float moveVertical = joystick.Vertical();
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

            rb.AddRelativeForce(movement * speed);
        }
	}
}
