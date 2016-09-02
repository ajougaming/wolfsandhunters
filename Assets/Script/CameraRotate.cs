using UnityEngine;
using System.Collections;

public class CameraRotate : MonoBehaviour {

    public Transform target;
    private Transform tr;
	// Use this for initialization
	void Start () {
	    tr = GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
        tr.LookAt(target);
	}
}
