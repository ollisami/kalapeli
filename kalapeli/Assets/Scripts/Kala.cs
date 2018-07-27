using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kala : MonoBehaviour
{

    public string laji;
    public float maxWeight;
    public float minWeight;

    private Rigidbody rb;
    public float weight;
    public float swimSpeed;
    private float scale;
    public float maxSpeed;
    public float smooth = 1f;
    private Vector3 targetAngles;
    private float timer = 0;

    private bool catched = false;
    private Transform target;
    private float followSpeed;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        weight = Random.Range(minWeight, maxWeight);
        swimSpeed = Random.Range(100.0F, 250.0F);
        scale = (weight / (maxWeight - minWeight)) * 9 + 1;
        transform.localScale = new Vector3(scale, scale, scale);
        targetAngles = transform.eulerAngles;
    }

    private void FixedUpdate()
    {
        if (catched)
        {
            if (target == null)
            {
                catched = false;
                return;
            }
            FollowTargetWithRotation(target, 0.1F, followSpeed);
        }
        else
        {
            if (rb.velocity.magnitude < maxSpeed)
            {
                rb.AddForce(-transform.right * Time.deltaTime * swimSpeed, ForceMode.Acceleration);
            }

            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, targetAngles, smooth * Time.deltaTime);
        }
        if (timer > 0) timer -= Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (timer <= 0 && collision.gameObject.tag.Equals("Pohja"))
        {
            timer = 5;
            float turnAngle = 180.0F;
            if (Random.value > 0.5F) turnAngle *= -1;
            targetAngles = transform.eulerAngles + turnAngle * Vector3.up;
        }

        if (!catched && collision.gameObject.tag.Equals("Viehe"))
        {
            Debug.Log("kala kiinni");
            target = collision.gameObject.transform;
            catched = true;
            followSpeed = Random.Range(1, 10);
            target.gameObject.GetComponent<Viehe>().kala = this;
        }
    }

    void FollowTargetWithRotation(Transform target, float distanceToStop, float speed)
    {
        float dist = Vector3.Distance(transform.position, target.position);
        if (dist > distanceToStop)
        {
            if(dist > 2)
            {
                Debug.Log("Karkas saatana");
                catched = false;
                target.gameObject.GetComponent<Viehe>().kala = null;
            }
            transform.LookAt(target);
            rb.AddRelativeForce(Vector3.forward * speed, ForceMode.Force);
        }
    }
}
