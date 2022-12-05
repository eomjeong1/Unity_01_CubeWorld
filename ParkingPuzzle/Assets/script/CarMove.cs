using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour
{
    bool UpStop;
    bool DownStop;
    RaycastHit hitLayerMask;
    


    private void OnMouseDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.green);


        int layerMask = 1 << LayerMask.NameToLayer("Stage");
        if (Physics.Raycast(ray, out hitLayerMask, Mathf.Infinity, layerMask))
        {
            if (UpStop == true)
            {
                if (this.transform.position.y <= hitLayerMask.point.y)
                {

                    
                    Debug.Log($"transform : {transform.position.y}      hitLayerMask : {hitLayerMask.point.y}    result : {transform.position.y - hitLayerMask.point.y}");
                    float x = this.transform.position.x;
                    float z = this.transform.position.z;

                    this.transform.position = new Vector3(x, transform.position.y, z);
                }
                else
                {
                    float x = this.transform.position.x;
                    float z = this.transform.position.z;
                    this.transform.position = new Vector3(x, hitLayerMask.point.y ,z);
                }

            }
            else if (DownStop == true)
            {
                if (this.transform.position.y >= hitLayerMask.point.y)
                {

                    Debug.Log($"transform : {transform.position.y}      hitLayerMask : {hitLayerMask.point.y}    result : {transform.position.y - hitLayerMask.point.y}");
                    float x = this.transform.position.x;
                    float z = this.transform.position.z;

                    this.transform.position = new Vector3(x, transform.position.y, z);
                }
                else
                {
                    float x = this.transform.position.x;
                    float z = this.transform.position.z;
                    this.transform.position = new Vector3(x, hitLayerMask.point.y, z);
                }


            }
            else
            { //Debug.Log("OnMouseDrag");
                float x = this.transform.position.x;
                float z = this.transform.position.z;
                this.transform.position = new Vector3(x, hitLayerMask.point.y, z);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Move move = collision.gameObject.GetComponent<Move>();
        Wall wall = collision.gameObject.GetComponent<Wall>();
        if (move || wall)
        {
            Debug.Log("�浹");
            // �÷��̾� ��ġ��, �ݸ��� ��ġ�� ���
            // �� ��ġ������ x�� ����ؼ� ���̸� ���� x���� ���� ���� ������
            Debug.Log("game : " + gameObject.transform.position);
            Debug.Log("collision : " + collision.transform.position);
            if (gameObject.transform.position.y <= collision.transform.position.y)
            {
                Debug.Log("���� �浹");
                UpStop = true;
            }
            if (gameObject.transform.position.y >= collision.transform.position.y)
            {
                Debug.Log("�Ʒ��� �浹");
                DownStop = true;
            }

        }

    }
    private void OnCollisionExit(Collision collision)
    {
        Move move = collision.gameObject.GetComponent<Move>();
        Wall wall = collision.gameObject.GetComponent<Wall>();
        if (move || wall)
        {
            if (gameObject.transform.position.y <= collision.transform.position.y)
            {
                Debug.Log("���� �浹 ��");
                UpStop = false;
            }
            if (gameObject.transform.position.y >= collision.transform.position.y)
            {
                Debug.Log("�Ʒ��� �浹 ��");
                DownStop = false;
            }
        }
    }

}
