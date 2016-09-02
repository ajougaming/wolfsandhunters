using UnityEngine;
using System.Collections;

public class BarrelCtrl : MonoBehaviour {
    //폭발 효과 파티클 연결변수
    public GameObject expEffect;

    //무작위로 선택할 텍스쳐 배열
    public Texture[] textures;
    public float expRadius = 10.0f;
    private Transform tr;
    private int hitCount = 0;

    //델리게이트 및 이벤트 선언
    // public delegate void MonsterDieHandler();
    // public static event MonsterDieHandler OnMonsterDie;
    // Use this for initialization
    void Start() {
        tr = GetComponent<Transform>();

        int idx = Random.Range(0, textures.Length);
        GetComponentInChildren<MeshRenderer>().material.mainTexture = textures[idx];
    }
    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.tag == "BULLET")
        {
            Destroy(coll.gameObject);

            if (++hitCount >= 3)
            {
                ExpBarrel();
            }
        }
    }

    void ExpBarrel()
    {
        GameObject Explosion =  (GameObject)Instantiate(expEffect, tr.position, Quaternion.identity);

        Collider[] colls = Physics.OverlapSphere(tr.position, expRadius);

        foreach (Collider coll in colls)
        {
            Rigidbody rbody = coll.GetComponent<Rigidbody>();
            if (rbody != null)
            {
                rbody.mass = 1.0f;
                rbody.AddExplosionForce(1000.0f, tr.position, expRadius, 300.0f);

            }
            if (coll.tag == "MONSTER")
            {
                MonsterCtrl monsterCtrl = coll.GetComponent<MonsterCtrl>();
                monsterCtrl.monsterDie();
            }
        }
        Destroy(this.gameObject, 5.0f);
        Destroy(Explosion, Explosion.GetComponent<ParticleSystem>().duration + 0.1f);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
