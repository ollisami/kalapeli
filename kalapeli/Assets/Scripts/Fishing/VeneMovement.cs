using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VeneMovement : MonoBehaviour {

    public float speed;
    public float rotationSpeed;
    public float maxSPeed = 10;
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
            float turnSpeedIncrease = moveVertical > 0 ? Mathf.Abs(moveHorizontal) * 0.5F : 0;
            float rotationMultiplier = 1.5F;
            Vector3 movement = new Vector3(0, 0, moveVertical + turnSpeedIncrease);
            Vector3 rotation = new Vector3(0,Mathf.Clamp(moveHorizontal * rotationMultiplier * 2, -rotationSpeed * rotationMultiplier, rotationSpeed * rotationMultiplier), 0.0F);
            if ((Mathf.Abs(rotation.y) > 0.2F || Mathf.Abs(movement.z) > 0.2F)) AudioController.instance.PlaySound("moottori");
            rb.AddRelativeTorque(rotation * rotationSpeed, ForceMode.Acceleration);
            if(rb.velocity.magnitude < maxSPeed)
                rb.AddRelativeForce(movement * speed);
        }
	}
}
