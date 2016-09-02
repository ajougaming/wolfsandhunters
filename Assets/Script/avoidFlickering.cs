using UnityEngine;
using System.Collections;

public class avoidFlickering : MonoBehaviour {

    // Use this for initialization
    private Transform tr;
	void Start () {
        transform.position = transform.position + Vector3.up * 0.1f;
   	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
