using UnityEngine;
using System.Collections;

//System.Serializable로 직렬화를 해야지 Inspector 창에서 Anim class가 보이고, Inspector 창에서 각 AnimationClip를 실제 Animation과 연결할수있다.
[System.Serializable]
public class Anim
{
    public AnimationClip idle;
    public AnimationClip runForward;
    public AnimationClip runBackward;
    public AnimationClip runRight;
    public AnimationClip runLeft;

}
public class PlayerCtrl : MonoBehaviour {

	private float h = 0.0f;
	private float v = 0.0f;
	private Transform tr;
    private Rigidbody rigidbody;
	private Vector3 moveDir;
	public float moveSpeed = 10.0f;
    public float moveForce = 365f;
	public float rotSpeed = 100.0f;

    public Anim anim; //인스펙터뷰에 표시될 Animation 클래스 변수
    public Animation _animation; //Animation 클래스에 저장된 각 Animation 클립에 접근하기 위한 변수

    public float playerHP = 100.0f;
    //델리게이트 및 이벤트 선언
    public delegate void PlayerDieHandler();
    public static event PlayerDieHandler OnPlayerDie;

    public bool isDamage = false;
    public bool isDie = false;
	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> ();
        rigidbody = GetComponent<Rigidbody>();
        //Player 하위 계층의 PlayerModel에 있는 Animation컴포넌트를 가져와야 하므로, GetComponentInChildren으로 하위계층에서 Animation을 가져온다.
        _animation = GetComponentInChildren<Animation>();

        //Animation 컴포넌트의 애니메이션 클립을 지정하고 실행
        _animation.clip = anim.idle;
        _animation.Play();
    }
	
	// Update is called once per frame
	void Update () {
		h = Input.GetAxis("Horizontal");
		v = Input.GetAxis ("Vertical");
		moveDir = (Vector3.forward * v) + (Vector3.right * h);
        tr.Translate (moveDir.normalized * Time.deltaTime * moveSpeed, Space.Self);
        //float y = rigidbody.velocity.y;
        //Debug.Log(rigidbody.velocity.y);
        //Vector3 temp = transform.forward * moveSpeed * v + transform.right * h * moveSpeed +  new Vector3(0, y, 0);
        //rigidbody.velocity = temp;
        
        
        tr.Rotate (Vector3.up * Time.deltaTime * rotSpeed * Input.GetAxis ("Mouse X"), Space.Self);
        if(v >= 0.1f)
        {
            _animation.CrossFade(anim.runForward.name, 0.3f);
        }
        else if(v <= -0.1f)
        {
            _animation.CrossFade(anim.runBackward.name, 0.3f);
        }
        else if (h >= 0.1f)
        {
            _animation.CrossFade(anim.runRight.name, 0.3f);
        }
        else if (h <= -0.1f)
        {
            _animation.CrossFade(anim.runLeft.name, 0.3f);
        }
        else
        {
            _animation.CrossFade(anim.idle.name, 0.3f);
        }

    }

    //몬스터에게 맞을때 이벤트 처리
    void OnTriggerEnter(Collider coll)
    {
        isDamage = true;
        if (coll.gameObject.tag == "PUNCH")
        {
            playerHP -= coll.gameObject.GetComponent<MonsterAttack>().damage;
  
            Debug.Log(playerHP);
            if (playerHP <= 0)
            {
                PlayerDie();
            }
        }

    }

    void PlayerDie()
    {
        isDie = true;
        OnPlayerDie();
    }


    private void OnCollisionEnter(Collision Col)
    {
        if(Col.gameObject.tag == "MONSTER")
        {
           // rigidbody.velocity = Vector3.zero;
        }
        
    }

}
