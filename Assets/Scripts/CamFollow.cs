using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour {

    public GameObject SubTarget;
    public GameObject DiverTarget;
    public Vector3 OffsetSub;
    public Vector3 offsetDiver;
    public bool SwitchTarget;
    Camera cam;
	void Start () {
        cam = this.gameObject.GetComponent<Camera>();
        offsetDiver = transform.position - DiverTarget.transform.position;
        OffsetSub = transform.position - SubTarget.transform.position;
    }
	
	void Update () {
        
        if(SwitchTarget == true)
        {
            cam.orthographicSize = 12;
            transform.position = SubTarget.transform.position + new Vector3(-1, -0.69f, -40);
        }

        if(SwitchTarget == false)
        {
            cam.orthographicSize = 7;
            transform.position = DiverTarget.transform.position + offsetDiver;
        }

	}
}
