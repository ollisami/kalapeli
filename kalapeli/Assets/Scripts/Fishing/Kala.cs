﻿using System.Collections;
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
    public int weightIterations = 3;
    private Vector3 targetAngles;
    private float timer = 0;

    private bool catched = false;
    private Transform target;
    private float followSpeed;

    private SpriteRenderer rend;
    public AnimationCurve cumulativeProbability;
    private bool update;

    // Use this for initialization
    void Start()
    {
        rb   = GetComponent<Rigidbody>();
        rend = GetComponent<SpriteRenderer>();
        targetAngles = transform.eulerAngles;
        update = Random.value > 0.5F;
    }

    public void InitializeKala(float fishScale) {
        maxWeight *= fishScale;
        weight = CalculateWeight();
        swimSpeed = Random.Range(100.0F, 250.0F);
        scale = (weight / (maxWeight - minWeight)) * 9 + 2;
        transform.localScale = new Vector3(scale, scale, scale);
    }

    private float CalculateWeight() 
    {
        float size = 0;
        for (int i = 0; i < weightIterations; i++)
        {
            size += maxWeight * cumulativeProbability.Evaluate(Random.value); 
        }
        return size / weightIterations;
    }
    private void FixedUpdate()
    {
        if (timer > 0) timer -= Time.deltaTime;
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
            if(!update)
            {
                update = !update;
                return;
            }
            if (rb.velocity.magnitude < maxSpeed)
            {
                rb.AddForce(-transform.right * Time.deltaTime * swimSpeed, ForceMode.Acceleration);
            }
        }

        if(Random.value < 0.001F) {
            targetAngles.z -= Random.value - 0.5F;
        }
        Color c = rend.color;
        float y = transform.position.y;
        c.a = Mathf.Clamp01((Mathf.Clamp(y, -10, -1) - (-10)) / (-1 - (-10)) - 0.5F);
        rend.color = c;
    }

    private void HandleBottomCollision() {
        targetAngles = transform.eulerAngles;
        int layerMask = 1 << 8;
        RaycastHit hit;
        float turnAngle = 180.0F;

        if (!Physics.Raycast(transform.position, -transform.right, out hit, 20.0F, layerMask))
        {
            turnAngle = 10F;
        }
        if (!Physics.Raycast(transform.position, transform.forward, out hit, 20.0F, layerMask))
        {
            turnAngle = 35F;
        }
        if (!Physics.Raycast(transform.position, -transform.forward, out hit, 20.0F, layerMask))
        {
            turnAngle = -35F;
        }
        if (!Physics.Raycast(transform.position, -transform.right, out hit, 20.0F, layerMask))
        {
            turnAngle = 30.0F;
        }
        else
        {
            targetAngles = Random.value * transform.up;
        }

        timer = 1;
        Vector3 temp = turnAngle * Vector3.up;
        transform.eulerAngles += temp;
        rb.AddExplosionForce(300, transform.position, 1);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (timer <= 0 && collision.gameObject.tag.Equals("Pohja"))
        {
            HandleBottomCollision();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (timer <= 0 && collision.gameObject.tag.Equals("Pohja"))
        {
            HandleBottomCollision();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {

        if (!catched && collision.gameObject.tag.Equals("VieheFish"))
        {
            if (collision.gameObject.transform.parent.gameObject.GetComponent<Viehe>().kala == null)
            {
                Debug.Log("kala kiinni");
                Handheld.Vibrate();
                if(weight > 50)
                    AudioController.instance.PlaySound("isoKala");
                else
                    AudioController.instance.PlaySound("kalaTuli");
                target = collision.gameObject.transform.parent;
                catched = true;
                followSpeed = Random.Range(1, 10);
                target.gameObject.GetComponent<Viehe>().kala = this;
            }
        }
    }

    void FollowTargetWithRotation(Transform target, float distanceToStop, float speed)
    {
        AudioController.instance.PlaySound("kelaRykasy");
        float dist = Vector3.Distance(transform.position, target.position);
        if (dist > distanceToStop)
        {
            if(dist > 4)
            {
                Debug.Log("Karkas saatana");
                AudioController.instance.StopPlaying();
                AudioController.instance.PlaySound("karkas");
                catched = false;
                target.gameObject.GetComponent<Viehe>().kala = null;
            }
            transform.LookAt(target);
            rb.AddRelativeForce(Vector3.forward * speed, ForceMode.Force);
        }
    }
}