using UnityEngine;
using System.Collections;

public class BulletCtrl : MonoBehaviour {
    public int damage = 20;
    public float speed = 1000.0f;

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
        Destroy(this.gameObject, 5.0f);
    }
	
	// Update is called once per frame
	void Update () {
       
    }
}
