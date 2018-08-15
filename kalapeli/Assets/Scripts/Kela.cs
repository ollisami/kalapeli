using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kela : MonoBehaviour
{
    public float spinningPower = 10.0F;
    private Viehe viehe;

    void Update()
    {
        if (viehe == null) viehe = FindObjectOfType<Viehe>();

        if (Input.GetMouseButton(0))
        {
            AudioController.instance.PlaySound("kelaus");
            viehe.MoveTowardsShip(spinningPower);
        }
    }
}