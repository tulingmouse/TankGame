using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObj : MonoBehaviour
{
    public float rotateSpeed=5;

    private void Update()
    {
        this.transform.Rotate(Vector3.up, rotateSpeed*Time.deltaTime);
    }

}
