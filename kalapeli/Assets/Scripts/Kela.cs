using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kela : MonoBehaviour
{
    public float rotationSpeed = 10.0F;
    public float lerpSpeed = 2.5F;
    public float spinningPower = 10.0F;

    private Vector3 speed;
    private Vector3 avgSpeed = Vector3.zero;
    private bool dragging = false;
    private Vector3 targetSpeedX;

    private Viehe viehe;

    void OnMouseDown()
    {
        dragging = true;
    }

    void Update()
    {
        if (viehe == null) viehe = FindObjectOfType<Viehe>();

        if (Input.GetMouseButton(0) && dragging)
        {
            speed = new Vector3(Mathf.Abs(Input.GetAxis("Mouse X")), Mathf.Abs(Input.GetAxis("Mouse Y")), 0);
            avgSpeed = Vector3.Lerp(avgSpeed, speed, Time.deltaTime * 5);
        }
        else
        {
            if (dragging)
            {
                speed = avgSpeed;
                dragging = false;
            }
            var i = Time.deltaTime * lerpSpeed;
            speed = Vector3.Lerp(speed, Vector3.zero, i);
        }
        Vector3 eulers = -Camera.main.transform.forward * (speed.y + speed.x) * rotationSpeed;
        this.transform.parent.transform.Rotate(eulers, Space.World);
        Vector3 temp = this.transform.parent.transform.localEulerAngles;
        temp.x = 0;
        temp.y = 0;
        this.transform.parent.transform.localEulerAngles = temp;
        float absSpeed = Mathf.Abs(speed.y + speed.x);
        viehe.MoveTowardsShip(absSpeed * spinningPower);
    }
}