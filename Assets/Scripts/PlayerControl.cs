using System;
using System.Collections;
using Cinemachine.Utility;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //角色的移动状态
    public static bool isMoving = false;
    //角色状态机
    public Animator anim;
    //绘制运动路径
    private Vector3 lastMousePosition;
    private Vector3 dashDirection;
    private Vector3 mousePos;
    private Coroutine drawDashPathCoroutine;
    public LineRenderer lineRenderer;
    public LineRenderer normalLineRenderer;
    private struct NextRay
    {
        public Vector3 hitpoint;
        public Vector3 direction;
        public float distance;
    }
    //让碰撞点往回走一点，确保在碰撞体外
    public float rayFiner = 0.2f;
    //角色运动
    private float remainingDistance;
    private Vector3[] targetPoints;
    private int currentTargetIndex = 1;
    public float normalDashDistance;
    public float normalMaxSpeed; // 默认最大速度
    public float normalAcceleration; // 默认加速度
    public float normalDeceleration; // 默认减速率
    private float dashDistance;
    private float maxSpeed; // 最大速度
    private float acceleration; // 加速度
    private float deceleration; // 减速率
    //角色Buff
    public GameObject buffvfx;
    public float buffDashDistance;
    public float buffMaxSpeed;
    public float buffAcceleration;
    public float buffDeceleration;
    private bool isAddDashing = false;
    public float buffDuration;
    private Coroutine buffCoroutine;
    
    
    void Start()
    {
        //初始化角色运动参数
        dashDistance = normalDashDistance;
        maxSpeed = normalMaxSpeed;
        acceleration = normalAcceleration;
        deceleration = normalDeceleration;
    }
    
    void Update()
    {
        if (!isMoving)
        {
            GetDashDirection();
            if (Input.GetMouseButtonDown(0))
            {
                PlayerMove();
            }
        }
    }

    //获得鼠标当前指向运动方向
    private void GetDashDirection()
    {
        if (lastMousePosition != Input.mousePosition)
        {
            if (drawDashPathCoroutine != null)
            {
                StopCoroutine(drawDashPathCoroutine);
                drawDashPathCoroutine = null;
            }
            lineRenderer.positionCount = 0;
            Vector3 tempDirection = new Vector3();
            // 创建射线鼠标点击方向的射线
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            // Perform the raycast
            if (Physics.Raycast(ray, out hit))
            {
                tempDirection = hit.point - transform.position;
            }
            dashDirection = new Vector3(tempDirection.x, 0, tempDirection.z);
            dashDirection.Normalize();
            lastMousePosition = Input.mousePosition;
            //DrawDashPath();
            drawDashPathCoroutine = StartCoroutine(DrawDashPath_IE());
        }
    }
    
    //生成运动路径
    private void DrawDashPath()
    {
        lineRenderer.positionCount = 0;
        //绘制起始点
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, transform.position);
        float remainingDistance = dashDistance;
        Vector3 nowdirection = dashDirection;
        Vector3 nowPosition = transform.position;
        while (remainingDistance > 0.0f)
        {
            NextRay nextRay = CastRay(nowPosition, nowdirection, remainingDistance);
            // 绘制路径点到LineRenderer
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, nextRay.hitpoint);
            if (nextRay.distance < 0)
            {
                break;
            }
            remainingDistance -= nextRay.distance;
            nowdirection = nextRay.direction;
            nowPosition = nextRay.hitpoint;
        }
    }
    
    //协程生成运动路径
    IEnumerator DrawDashPath_IE()
    {
        //绘制起始点
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, transform.position);
        remainingDistance = dashDistance;
        Vector3 nowdirection = dashDirection;
        Vector3 nowPosition = transform.position;
        while (remainingDistance > 0.0f)
        {
            NextRay nextRay = CastRay(nowPosition, nowdirection, remainingDistance);
            // 绘制路径点到LineRenderer
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, nextRay.hitpoint);
            if (nextRay.distance < 0)
            {
                break;
            }
            remainingDistance -= nextRay.distance;
            nowdirection = nextRay.direction;
            nowPosition = nextRay.hitpoint;
            yield return null;
        }
    }
    
    NextRay CastRay(Vector3 origin, Vector3 direction, float maxDistance)
    {
        NextRay nextRay = new NextRay();
        RaycastHit hit;
        if (Physics.Raycast(origin, direction, out hit, maxDistance, LayerMask.GetMask("Wall")))
        {
            // 计算折射方向
            ///找到正确的法线向量
            //Vector3 normal_collider = hit.collider.gameObject.transform.forward;
            /*
             Vector3 normal = new Vector3(normal_collider.z, 0, -normal_collider.x);
            if (Vector3.Dot(normal, direction) > 0)
            {
                normal *= -1;
            }
            */
            Vector3 normal = Refract_Normal(direction, hit.collider.gameObject.transform.forward, hit.collider.gameObject.transform.position,hit.point,hit.collider.gameObject.transform.localScale.x * 0.5f, hit.collider.gameObject.transform.localScale.z  * 0.5f);
            Vector3 refractedDirection = Refract(direction, normal);
            nextRay.hitpoint = hit.point - rayFiner * direction;
            nextRay.direction = refractedDirection;
            nextRay.distance = Vector3.Distance(origin, nextRay.hitpoint);
            //Debug法线
            //normalLineRenderer.SetPosition(0, nextRay.hitpoint);
            //normalLineRenderer.SetPosition(1, nextRay.hitpoint+normal*5f);
        }
        else
        {
            Vector3 endPoint = origin + direction * maxDistance;
            nextRay.hitpoint = endPoint;
            nextRay.distance = -1f;
        }
        return nextRay;
    }

    //计算选取折射的方向向量
    Vector3 Refract_Normal(Vector3 direction,Vector3 objectForward, Vector3 objectPoint, Vector3 hitpoint, float x, float z)
    {
        Vector3 normal;
        Vector3 point = hitpoint - objectPoint;
        if (point.x - z > 0.01f)
        {
            Debug.Log("Z边");
            normal = new Vector3(objectForward.x, 0, objectForward.z);
            if (Vector3.Dot(normal, direction) > 0)
            {
                normal *= -1;
            }
        }
        else
        {
            Debug.Log("X边");
            normal = new Vector3(-objectForward.z, 0, objectForward.x);
            if (Vector3.Dot(normal, direction) > 0)
            {
                normal *= -1;
            }
        }
        return normal;
    }
    // 折射计算函数
    Vector3 Refract(Vector3 I, Vector3 N)
    {
        return I - 2 * Vector3.Dot(I, N) * N;
    }
    
    //开始运动
    private void PlayerMove()
    {
        //得到当前物体运动的点列表
        targetPoints = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(targetPoints);
        currentTargetIndex = 1;
        StartCoroutine(MoveToNextPoint());
        isMoving = true;
        anim.SetBool("isMoving", true);
        lineRenderer.enabled = false;
    }
    
    //运动至下个点
    IEnumerator MoveToNextPoint()
    {
        while (currentTargetIndex < targetPoints.Length)
        {
            Vector3 targetPosition = targetPoints[currentTargetIndex];
            float speed = 0f;
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                // 计算当前速度
                speed = Mathf.Lerp(speed, maxSpeed, acceleration * Time.deltaTime);
                // 判断是否接近目标点，开始减速
                if (Vector3.Distance(transform.position, targetPosition) < 2.0f)
                {
                    speed = Mathf.Lerp(speed, 0, deceleration * Time.deltaTime);
                }
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                yield return null;
            }
            currentTargetIndex++;
        }
        
        isMoving = false;
        anim.SetBool("isMoving", false);
        lineRenderer.enabled = true;
    }
    
    //碰撞加速点
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DashCube")&&isMoving)
        {
            SFXManager.instance.PlaySFX(SFXManager.instance.buffPickupClip);
            Destroy(other.gameObject);
            if (buffCoroutine!= null)
            {
                StopCoroutine(buffCoroutine);
                buffCoroutine = null;
            }
            buffCoroutine = StartCoroutine(BuffCoroutine());
        }
    }
    //Buff状态撞击敌人
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy")&&isAddDashing)
        {
            other.gameObject.GetComponent<EnemyOne>().GetHurt();
        }
    }
    //Buff状态结束
    private IEnumerator BuffCoroutine()
    {
        // Buff生效期间执行的代码
        dashDistance = buffDashDistance;
        maxSpeed = buffMaxSpeed;
        acceleration = buffAcceleration;
        deceleration = buffDeceleration;
        isAddDashing = true;
        GameObject vfx =  Instantiate(buffvfx, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(buffDuration);
        // Buff结束时的处理
        dashDistance = normalDashDistance;
        maxSpeed = normalMaxSpeed;
        acceleration = normalAcceleration;
        deceleration = normalDeceleration;
        isAddDashing = false;
        Destroy(vfx);
    }
}
