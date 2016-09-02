using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MonsterCtrl : MonoBehaviour {

    //몬스터의 상태 정보를 담은 Enumerable 변수 선언
    public enum MonsterState { idle, trace, attack, die};
    public MonsterState monsterState = MonsterState.trace;

    //추적 범위
    public float traceDist = 10.0f;
    //공격범위
    public float attackDist = 2.0f;

    //몬스터 사망여부 
    private bool isDie = false;

    private Transform monsterTr;
    private Transform playerTr;
    private NavMeshAgent nvAgent;
    private Animator animator;

    public float currentHP;
    public float startingHP = 100.0f;
    public Slider healthSlider;
    public Text healthText;
    //혈흔 효과 프리팹
    public GameObject bloodEffect;
    //혈흔 데칼 프리팹
    public GameObject bloodDecal;
    //이벤트 발생시 이벤트를 처리할 함수를 연결한다. 
    void OnEnable()
    {
        PlayerCtrl.OnPlayerDie += this.OnPlayerDie;
    }
    //이벤트 처리후 연결을 해제한다.
    void OnDisable()
    {
        PlayerCtrl.OnPlayerDie -= this.OnPlayerDie;
    }

	// Use this for initialization
	void Start () {
        currentHP = startingHP;
        monsterTr = this.gameObject.GetComponent<Transform>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        nvAgent =  this.gameObject.GetComponent<NavMeshAgent>();
        animator = this.gameObject.GetComponent<Animator>();
        nvAgent.destination = playerTr.position;
        healthText.text = currentHP + "/" + startingHP;
        //몬스터의 상태를 변경하는 코루틴 함수 실행
        StartCoroutine(this.checkMonsterState());
        //몬스터의 상태에 따라 행동을 취하는 코루틴 함수 실행
        StartCoroutine(this.doAction());
	}
	IEnumerator checkMonsterState()
    {
        while(!isDie)
        {
            yield return new WaitForSeconds(0.2f);
            float dist = Vector3.Distance(monsterTr.position, playerTr.position);

            if (dist <= traceDist && dist > attackDist)
            {
                monsterState = MonsterState.trace;
            }
            else if (dist <= attackDist)
            {
                monsterState = MonsterState.attack;
            }
            else
            {
                monsterState = MonsterState.idle;
            }
        }
    }

    IEnumerator doAction()
    {
        while(!isDie)
        {

            switch (monsterState)
            {
                case MonsterState.idle:
                    nvAgent.Stop();
                    animator.SetBool("IsTrace", false);
                    break;
                case MonsterState.trace:
                    nvAgent.destination = playerTr.position;
                    nvAgent.Resume();
                    animator.SetBool("IsTrace", true);
                    animator.SetBool("IsAttack", false);
                    break;
                case MonsterState.attack:
                    nvAgent.Stop();
                    animator.SetBool("IsAttack", true);
                    break;
            }
            yield return null;

        }
    }
    // Update is called once per frame

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.tag == "BULLET")
        {
            currentHP -= coll.gameObject.GetComponent<BulletCtrl>().damage;
            healthSlider.value = currentHP;
            healthText.text = currentHP + "/" + startingHP;
            Debug.Log(currentHP);
            if(currentHP <= 0)
            {
                monsterDie();
            }
            CreateBloodEffect(coll.transform.position);
            Destroy(coll.gameObject, 0.0f);
            animator.SetTrigger("IsHit");
        }
    }
    void CreateBloodEffect(Vector3 pos)
    {
        GameObject blood1 =  (GameObject)Instantiate(bloodEffect, pos, Quaternion.identity);
        Destroy(blood1, 2.0f);

        //데칼 생성위치
        Vector3 decalPos = monsterTr.position + (Vector3.up * 0.05f);
        //데칼 회전값 
        Quaternion decalRot = Quaternion.Euler(90, 0, Random.Range(0.0f, 360.0f));

        //데칼 크기
        float scale = Random.Range(1.5f, 3.5f);
        GameObject blood2 = (GameObject)Instantiate(bloodDecal, decalPos, decalRot);
        blood2.transform.localScale = Vector3.one * scale;
        Destroy(blood2, 5.0f);

    }
    public void monsterDie()
    {
        currentHP = 0.0f;
        healthSlider.value = currentHP;
        healthText.text = currentHP + "/" + startingHP;
        StopAllCoroutines();

        isDie = true;
        monsterState = MonsterState.die;
        nvAgent.Stop();

        animator.SetTrigger("IsDie");

        //몬스터의 Collider 비활성화
        gameObject.GetComponentInChildren<CapsuleCollider>().enabled = false;
        foreach(Collider coll in gameObject.GetComponentsInChildren<SphereCollider>())
        {
            coll.enabled = false;
        }
        Destroy(this.gameObject, 5.0f);
    }
    void OnPlayerDie()
    {

        StopAllCoroutines();
        nvAgent.Stop();
        animator.SetTrigger("IsPlayerDie");
    }
	void Update () {
	
	}

}
