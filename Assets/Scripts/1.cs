using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main != null)
        {
            transform.LookAt(Camera.main.transform);

            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + 180, 0);

        }
        else
        {
            Debug.LogError("δ�ҵ������������ȷ����������һ�����Ϊ 'MainCamera' ���������");

        }
    }
}