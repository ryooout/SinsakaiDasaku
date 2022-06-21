using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove2 : MonoBehaviour
{
    Vector3 objPos;
    public float range;
    void Start()
    {
        objPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position =new Vector3(objPos.x, objPos.y, Mathf.Sin(Time.time) * range + objPos.z);
    }
}
