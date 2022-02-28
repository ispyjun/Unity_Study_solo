using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed;
    public GameObject spawnPoint;
    public GameManager manager;
    GameObject nearObject;
    public bool[] hasWeapons;

    float hAxis;
    float vAxis;

    bool wDown;
    bool jDown;
    bool iDown;
    bool isJump;
    public bool haveKey;
    public bool isRespawn;
    public bool isFinish;
    public bool doorOpen;

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
        Interation();
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        //wDown = Input.GetButton("Walk");
        jDown = Input.GetButtonDown("Jump");
        iDown = Input.GetButtonDown("Interation");
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
        if (other.gameObject.tag == "Water")
        {
            anim.SetTrigger("DoDie");
            Invoke("ReScene", 2f);
            //player.GetComponent<Animator>().SetTrigger("DoRespawn");
        }
        if (other.gameObject.tag == "Finish")
        {
            /*if (itemCount == manager.totalItemCount)
            {
                SceneManager.LoadScene(manager.stage + 1);
            }
            else
            {*/
            //manager.stage++;
            //isFinish = true;
            SceneManager.LoadScene(manager.stage+1);
            //}
        }
        
    }
    void ReScene()
    {
        SceneManager.LoadScene(manager.stage);
        isRespawn = true;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Item")
            nearObject = other.gameObject;
        if (other.tag == "Door")
            nearObject = other.gameObject;
    }

    void Interation()
    {
        /*if (nearObject.tag == "Weapon")
        {
            Item item = nearObject.GetComponent<Item>();
            int weaponIndex = item.value;
            hasWeapons[weaponIndex] = true;

            Destroy(nearObject);
        }
        else if (nearObject.tag == "Shop")
        {
            Shop shop = nearObject.GetComponent<Shop>();
            shop.Enter(this);
            isShop = true;
        }*/
        if (iDown && nearObject != null)
        {
            if (nearObject.CompareTag("Item"))
            {
                
                Item item = nearObject.GetComponent<Item>();
                int weaponIndex = item.value;
                hasWeapons[weaponIndex] = true;

                Destroy(nearObject);
                haveKey = true;
            }
            if (nearObject.CompareTag("Door") && haveKey == true)
            {
                //Destroy(nearObject);
                //anim.SetTrigger("DoOpen");
                //doorOpen = true;
                haveKey = false;

            }
        }
    }

    

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Item")
            nearObject = null;
        /*else if (other.tag == "Shop")
        {
            Shop shop = nearObject.GetComponent<Shop>();
            shop.Exit();
            isShop = false;
            nearObject = null;
        }*/

    }

}
