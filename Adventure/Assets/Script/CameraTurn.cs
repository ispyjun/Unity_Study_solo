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
    public Transform playerAxis; //�÷��̾��� �߽���
    public Transform player; //�÷��̾�       

    void Awake()
    {

        playerR = GetComponent<Player>();
        //meshs = GetComponentsInChildren<MeshRenderer>();

        //PlayerPrefs.SetInt("MaxScore", 0);
    }

    void CamMove()
    {
        //���콺 �����̳� ������ Ŭ���϶� ī�޶� ȸ����Ű��
        if (Input.GetMouseButton(0) | Input.GetMouseButton(1))
        {
            mouseX += Input.GetAxis("Mouse X");
            mouseY += Input.GetAxis("Mouse Y") * -1;

            //ī�޶� ���� ������ ����
            if (mouseY > 5)
                mouseY = 5;
            if (mouseY < 0)
                mouseY = 0;

            centralAxis.rotation = Quaternion.Euler(new Vector3(centralAxis.rotation.x + mouseY,
                    centralAxis.rotation.y + mouseX, 0) * camSpeed);

            //���콺 ��Ŭ���϶��� �÷��̾��� �߽��൵ ���� ȸ����Ű��
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

            //�÷��̾� �̵�,ȸ�� ��Ʈ
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");
            movement = new Vector3(moveX, 0, moveY);

            if (movement != Vector3.zero)
            {
                playerAxis.Translate(movement * Time.deltaTime * 7f);
                player.localRotation = Quaternion.Slerp(player.transform.localRotation,
                    Quaternion.LookRotation(movement), 7 * Time.deltaTime);

                //�÷��̾� �̵� �ִϸ��̼�
                player.GetComponent<Animator>().SetBool("isRun", true);
                player.GetComponent<Animator>().SetBool("isWalk", wDown);
            }
        //�÷��̾� ��� �ִϸ��̼�
        if (movement == Vector3.zero)
            player.GetComponent<Animator>().SetBool("isRun", false);
    }
    private void LateUpdate()
    {
        // ī�޶� �߽����� �÷��̾� ����ٴϱ�
        centralAxis.position = new Vector3(player.transform.position.x, player.transform.position.y + 4f, player.transform.position.z);
    }
}
