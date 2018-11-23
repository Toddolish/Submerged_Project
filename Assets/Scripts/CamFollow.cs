using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{

    public GameObject SubTarget;
    public Vector3 OffsetSub;
    Camera cam;
    void Start()
    {
        cam = this.gameObject.GetComponent<Camera>();
        OffsetSub = transform.position - SubTarget.transform.position;
    }

    void Update()
    {
        transform.position = SubTarget.transform.position + new Vector3(-1, -0.69f, -30);
    }
}
