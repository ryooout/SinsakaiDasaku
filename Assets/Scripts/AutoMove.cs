using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
    Vector3 objPos;
    public float range;
    // Start is called before the first frame update
    void Start()
    {
        objPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(Mathf.Sin(Time.time) * range + objPos.x, objPos.y, objPos.z);
    }
}
