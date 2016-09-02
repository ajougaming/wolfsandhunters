using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class FireCtrl : MonoBehaviour {

    //총알 프리팹 변수
    public GameObject bullet;
    //총알 발사좌표
    public Transform firePos;
    public float distance = 1000.0f;
    private Vector3 center;
    private Ray ray;
    private RaycastHit hit;

    public MeshRenderer muzzleFlash;

    //총알발사사운드
    public AudioClip fireSfx;
    //Audio Source 컴포넌트를 저장할 변수
    public AudioSource source = null;

    public float fireDelay = 0.2f;
    private float currentTime = 0.0f;

	// Use this for initialization
	void Start () {
        muzzleFlash.enabled = false;
        source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        currentTime += Time.deltaTime;
        if (Time.timeScale != 0)
        {
            if (Input.GetMouseButton(0) && currentTime >= fireDelay)
            {

                Fire();
                currentTime = 0.0f;
            }
        }
	}

    void Fire()
    {
        CreateBullet();
        StartCoroutine(this.ShowMuzzleFlash());
        source.PlayOneShot(fireSfx, 0.9f);
    }

    IEnumerator ShowMuzzleFlash()
    {
        float scale = Random.Range(1.0f, 2.0f);
        muzzleFlash.transform.localScale = Vector3.one * scale;

        Quaternion rot = Quaternion.Euler(0, 0, Random.Range(0.0f, 360.0f));
        muzzleFlash.transform.localRotation = rot; 

        muzzleFlash.enabled = true;
        yield return new WaitForSeconds(Random.Range(0.05f, 0.03f));
        muzzleFlash.enabled = false;
    }
    void CreateBullet()
    { 

        ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        ray.origin = ray.GetPoint(5.0f);
        Debug.DrawRay(ray.origin, ray.direction, Color.red, 1f);
        Vector3 crossbarDest;
     
        if(Physics.Raycast(ray.origin, ray.direction, out hit))
        {
            crossbarDest = hit.point;
        }
        else
        {
            crossbarDest = ray.GetPoint(distance);
        }
        firePos.LookAt(crossbarDest);
        Instantiate(bullet, firePos.position, firePos.rotation);
    }
}
