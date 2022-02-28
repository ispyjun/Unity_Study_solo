using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
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
        transform.position = playerTransform.position + offset;
    }*/
    Vector3 _startPosition;
    Quaternion _startRotate;
    // Use this for initialization
    void Start()
    {
        _startPosition = this.transform.position;
        _startRotate = this.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        float speed = 30.0f;
        float offset = Time.deltaTime * speed;

        if (Input.GetKey(KeyCode.Escape) == true)
        {
            this.transform.position = _startPosition;
            this.transform.rotation = _startRotate;
            return;
        }

        if (Input.GetKey(KeyCode.UpArrow) == true)
        {
            if (Input.GetKey(KeyCode.LeftShift) == true)
            {
                transform.Rotate(Vector3.left * offset);
            }
            else if (Input.GetKey(KeyCode.LeftControl) == true)
            {
                this.transform.position += (this.transform.up * offset); // transform.up: The green axis of the transform in world space.
            }
            else
            {
                this.transform.position += (this.transform.forward * offset); // transform.forward: The blue axis of the transform in world space.
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow) == true)
        {
            if (Input.GetKey(KeyCode.LeftShift) == true)
            {
                transform.Rotate(Vector3.right * offset);
            }
            else if (Input.GetKey(KeyCode.LeftControl) == true)
            {
                this.transform.position += (this.transform.up * -offset); // transform.up: The green axis of the transform in world space.
            }
            else
            {
                this.transform.position += (-this.transform.forward * offset); // transform.forward: The blue axis of the transform in world space.
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow) == true)
        {
            if (Input.GetKey(KeyCode.LeftShift) == true)
            {
                transform.Rotate(Vector3.down * offset);
            }
            else
            {
                this.transform.position += (this.transform.right * -offset); // this.transform.right: The red axis of the transform in world space.
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow) == true)
        {
            if (Input.GetKey(KeyCode.LeftShift) == true)
            {
                transform.Rotate(Vector3.up * offset);
            }
            else
            {
                this.transform.position += (this.transform.right * offset); // this.transform.right: The red axis of the transform in world space.
            }
        }
    }
}
