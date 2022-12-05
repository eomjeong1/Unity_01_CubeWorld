using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Move : MonoBehaviour
{
    bool LeftStop;
    bool RightStop;
    RaycastHit hitLayerMask;
    


    private void OnMouseDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.green);


        int layerMask = 1 << LayerMask.NameToLayer("Stage");
        if (Physics.Raycast(ray, out hitLayerMask, Mathf.Infinity, layerMask))
        {
            if (LeftStop == true)
            {
                if (this.transform.position.x >= hitLayerMask.point.x)
                {
                    Vector2 frontVec = new Vector2(hitLayerMask.point.x + hitLayerMask.point.x * 0.4f, hitLayerMask.point.y);
                    if (hitLayerMask.collider != null)
                    {
                        Debug.Log("��ֹ��� �ֽ��ϴ�.");
                        
                    }
                    else
                    {
                        Debug.Log($"transform : {transform.position.x}      hitLayerMask : {hitLayerMask.point.x}    result : {transform.position.x - hitLayerMask.point.x}");
                        float y = this.transform.position.y;
                        float z = this.transform.position.z;

                        this.transform.position = new Vector3(transform.position.x, y, z);
                    }
                }
                else
                {
                    float y = this.transform.position.y;
                    float z = this.transform.position.z;
                    this.transform.position = new Vector3(hitLayerMask.point.x, y, z);
                }
                
            }
            else if (RightStop == true)
            {
                if (this.transform.position.x <= hitLayerMask.point.x)
                {

                    
                    Debug.Log($"transform : {transform.position.x}      hitLayerMask : {hitLayerMask.point.x}    result : {transform.position.x - hitLayerMask.point.x}");
                    float y = this.transform.position.y;
                    float z = this.transform.position.z;

                    this.transform.position = new Vector3(transform.position.x, y, z);
                }
                else
                {
                    float y = this.transform.position.y;
                    float z = this.transform.position.z;
                    this.transform.position = new Vector3(hitLayerMask.point.x, y, z);
                }
                

            }
            else
            { //Debug.Log("OnMouseDrag");
                float y = this.transform.position.y;
                float z = this.transform.position.z;
                this.transform.position = new Vector3(hitLayerMask.point.x, y, z);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        CarMove carMove = collision.gameObject.GetComponent<CarMove>();
        Wall wall = collision.gameObject.GetComponent<Wall>();
        if (carMove || wall)
        {
            Debug.Log("�浹");
            // �÷��̾� ��ġ��, �ݸ��� ��ġ�� ���
            // �� ��ġ������ x�� ����ؼ� ���̸� ���� x���� ���� ���� ������
            Debug.Log("game : " + gameObject.transform.position);
            Debug.Log("collision : " + collision.transform.position);
            if (gameObject.transform.position.x >= collision.transform.position.x)
            {
                Debug.Log("���� �浹");
                LeftStop = true;
            }
            if (gameObject.transform.position.x <= collision.transform.position.x)
            {
                Debug.Log("������ �浹");
                RightStop = true;
            }

        }

    }
    private void OnCollisionExit(Collision collision)
    {
        CarMove carMove = collision.gameObject.GetComponent<CarMove>();
        Wall wall = collision.gameObject.GetComponent<Wall>();
        if (carMove || wall)
        {
            if (gameObject.transform.position.x > collision.transform.position.x)
            {
                Debug.Log("���� �浹 ��");
                LeftStop = false;
            }
            if (gameObject.transform.position.x < collision.transform.position.x)
            {
                Debug.Log("������ �浹 ��");
                RightStop = false;
            }
        }
    }

}
