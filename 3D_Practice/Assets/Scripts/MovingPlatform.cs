using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform start;
    public Transform end;

    public bool isMoving = false;
    public float moveSpeed = 50f;

    private GameObject platform;
    private Transform currentTarget;

    void Start()
    {
        platform = GameObject.Find("Platform");
        currentTarget = end.transform;
    }
    void Update()
    {
        if (isMoving)
        {
            platform.transform.position = Vector3.MoveTowards(platform.transform.position,
                currentTarget.position, Time.deltaTime * moveSpeed);
            if (Vector3.Distance(platform.transform.position, currentTarget.position) < 0.001f)
            {
                if (currentTarget == start) { currentTarget = end; }
                else { currentTarget = start; }
            }
        }
    }
    void OnDrawGizmos()
    {
        // Start ��ġ�� ���� ��ü Gizmo �׸���
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(start.position, 0.1f);  // ũ��� ���ϴ� ��� ���� ����
                                                  // End ��ġ�� �Ķ� ��ü Gizmo �׸���
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(end.position, 0.1f);

        // Start�� End ��ġ�� �ؽ�Ʈ ǥ��
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.white;
        UnityEditor.Handles.Label(start.position, "Start", style);
        UnityEditor.Handles.Label(end.position, "End", style);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(start.position, end.position);
    }
}
