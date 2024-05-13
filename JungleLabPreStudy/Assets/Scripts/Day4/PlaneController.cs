using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public float planeSpeed = 20;
    public float rotateSpeed = 20;
    public GameObject propeller;
    public float propellerSpeed = 20;

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Vertical");
        float z = Input.GetAxis("Horizontal");

        //����,����
        transform.Translate(Vector3.forward * Time.deltaTime * planeSpeed*x);
        //����ȸ��
        transform.Rotate(Vector3.up*Time.deltaTime * rotateSpeed*z);
        //�����緯 ȸ��
        propeller.transform.Rotate(Vector3.forward * propellerSpeed*x);
        
        //����� ȸ��
       if (Input.GetKey(KeyCode.Space))
        {
            transform.Rotate(Vector3.left * Time.deltaTime * rotateSpeed);
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            transform.Rotate(-Vector3.left * Time.deltaTime * rotateSpeed);
        }
        //����� ����
        planeSpeed = Input.GetKey(KeyCode.LeftShift) ? 30 : 20;
    }
}
