using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTurn : MonoBehaviour
{
    /*public Transform centralAxis;
    public Transform player;
    public float camSpeed;
    float mouseX;
    float mouseY;
    /*Transform playerTransform;
    Vector3 offset;

    // Start is called before the first frame update
    void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - playerTransform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        centralAxis.position = new Vector3(player.transform.position.x, player.transform.position.y + 4f, player.transform.position.z);
    }

    void CamMove()
    {
        if (Input.GetMouseButton(0))
        {
            mouseX += Input.GetAxis("Mouse X");
            mouseY += Input.GetAxis("Mouse Y") * -1;

            centralAxis.rotation = Quaternion.Euler(
                new Vector3(centralAxis.rotation.x + mouseY, centralAxis.rotation.y + mouseX, 0) * camSpeed);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CamMove();
    }*/
    public Transform centralAxis;
    public Transform cam;
    public float camSpeed;
    float mouseX;
    float mouseY;
    float wheel = -4;
    bool wDown;

    Vector3 movement;
    Player playerR;
    public Transform playerAxis; //플레이어의 중심축
    public Transform player; //플레이어       

    void Awake()
    {

        playerR = GetComponent<Player>();
        //meshs = GetComponentsInChildren<MeshRenderer>();

        //PlayerPrefs.SetInt("MaxScore", 0);
    }

    void CamMove()
    {
        //마우스 왼쪽이나 오른쪽 클릭일때 카메라 회전시키기
        if (Input.GetMouseButton(0) | Input.GetMouseButton(1))
        {
            mouseX += Input.GetAxis("Mouse X");
            mouseY += Input.GetAxis("Mouse Y") * -1;

            //카메라 상하 움직임 제한
            if (mouseY > 5)
                mouseY = 5;
            if (mouseY < 0)
                mouseY = 0;

            centralAxis.rotation = Quaternion.Euler(new Vector3(centralAxis.rotation.x + mouseY,
                    centralAxis.rotation.y + mouseX, 0) * camSpeed);

            //마우스 우클릭일때는 플레이어의 중심축도 같이 회전시키기
            if (Input.GetMouseButton(0) | Input.GetMouseButton(1))
            {
                playerAxis.rotation = Quaternion.Euler(new Vector3(
              0, playerAxis.rotation.y + mouseX, 0) * camSpeed);
            }
        }
    }
    void Zoom()
    {
        wheel += Input.GetAxis("Mouse ScrollWheel");
        if (wheel >= -1)
            wheel = -1;
        if (wheel <= -5)
            wheel = -5;
        cam.localPosition = new Vector3(0, 0, wheel);
    }
    void Update()
    {
        CamMove();
        Zoom();
        wDown = Input.GetButton("Walk");

            //플레이어 이동,회전 파트
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");
            movement = new Vector3(moveX, 0, moveY);

            if (movement != Vector3.zero)
            {
                playerAxis.Translate(movement * Time.deltaTime * 7f);
                player.localRotation = Quaternion.Slerp(player.transform.localRotation,
                    Quaternion.LookRotation(movement), 7 * Time.deltaTime);

                //플레이어 이동 애니메이션
                player.GetComponent<Animator>().SetBool("isRun", true);
                player.GetComponent<Animator>().SetBool("isWalk", wDown);
            }
        //플레이어 대기 애니메이션
        if (movement == Vector3.zero)
            player.GetComponent<Animator>().SetBool("isRun", false);
    }
    private void LateUpdate()
    {
        // 카메라 중심축이 플레이어 따라다니기
        centralAxis.position = new Vector3(player.transform.position.x, player.transform.position.y + 4f, player.transform.position.z);
    }
}
