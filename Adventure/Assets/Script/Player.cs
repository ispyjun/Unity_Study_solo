using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed;
    public GameObject spawnPoint;
    public GameManager manager;

    float hAxis;
    float vAxis;

    bool wDown;
    bool jDown;
    bool isJump;
    public bool isRespawn;

    Vector3 moveVec;
    //Vector3 dodgeVec;

    Rigidbody rigid;
    public Animator anim;
    MeshRenderer[] meshs;

    void Awake()
    {

        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        //manager = GetComponent<GameManager>();
        //meshs = GetComponentsInChildren<MeshRenderer>();

        //PlayerPrefs.SetInt("MaxScore", 0);
    }

    void Update()
    {
        GetInput();
        //Move();
        Turn();
        Jump();
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        //wDown = Input.GetButton("Walk");
        jDown = Input.GetButtonDown("Jump");
    }

    void Turn()
    {
        // 키보드에 의한 회전
        transform.LookAt(transform.position + moveVec);

        // 마우스에 의한 회전
        /*if (fDown)
        {
            Ray ray = followCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit, 100))
            {
                Vector3 nextVec = rayHit.point - transform.position;
                nextVec.y = 0;
                transform.LookAt(transform.position + nextVec);
            }
        }*/
    }

    /*void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        //if (isDodge)
        //moveVec = dodgeVec;

        //if (isSwap || isReload || !isFireReady)
        //moveVec = Vector3.zero;

        //if (!isBorder)
        transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime;

        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWalk", wDown);
    }*/
    void Jump() // 점프
    {
        if (jDown && !isJump)
        {
            rigid.AddForce(Vector3.up * 10, ForceMode.Impulse);
            isJump = true;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "Floor" )
        {
            if(isRespawn == true)
            {
                anim.SetTrigger("DoRespawn");
                isRespawn = false;
            }
            //anim.SetBool("isJump", false);
            isJump = false;
        }

        
    }

    void OnTriggerEnter(Collider other)
    {
        /*if (other.gameObject.tag == "Water")
        {
            anim.SetTrigger("DoDie");
            Invoke("ReScene", 2f);
            //player.GetComponent<Animator>().SetTrigger("DoRespawn");
        }*/
        if (other.tag == "finish")
        {
            /*if (itemCount == manager.totalItemCount)
            {
                SceneManager.LoadScene(manager.stage + 1);
            }
            else
            {*/
            manager.stage++;
            SceneManager.LoadScene(manager.stage);
            //}
        }
    }
    /*void ReScene()
    {
        SceneManager.LoadScene(manager.stage);
        isRespawn = true;

    }*/

}
