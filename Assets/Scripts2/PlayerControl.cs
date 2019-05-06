using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerControl : MonoBehaviour {

    //public static PlayerControl Instance;

    //linerenderer
    public LineRenderer circleLineRenderer;
    public LineRenderer trackLineRender;

    private Animator ani;

    static float Pi = 3.1415926f;
    public float angle = 0f;

    // position y
    float player_y;
    //distance from camera
    float max_distance = -12.0f;
    //player's rigidbody
    private Rigidbody2D rBody;
    //与移动板的相对位置
    private Vector2 distance;
    private GameObject moveFloor;

    //conditions, 
    public static bool canJump;
    public static bool inAir ;
    public static bool canRush ;
    //private bool follow = false;//判断在移动板上该离开时的条件
    public static bool isTouching = false;
    public static bool isRushing = false;

    //Hp
    int Hp = 1;

    //vector2 关于触屏
    Vector2 startPos;
    Vector2 direction;

    //bool click_flag = false;

    //vars to record time
    //蓄力最大时间，与点击时间界限
    float pressTimeMax = 1.0f;
    float clickTimeMin = 0.2f;
    //点击屏幕的时间与当前时间
    float time_start = 0.0f;
    float time_current = 0.0f; 

    //rush时addforce 使用的乘法因子
    float speed = 1300f;
    //jump时的乘法因子
    float jumpRatio = 7.0f;

    //分数
    public static int score = 0;

    // Use this for initialization
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }

     //Update is called once per frame
     void Update()
    {
        player_y = transform.position.y;
        float camera_y = CameraControl.Camera_Y;
        //out of screen
        if(player_y - camera_y < max_distance)
        {
            Death();
        }
        if (Hp <= 0)
        {
            Death();
        }
        /*if (follow&&moveFloor)
        {
            Vector2 otherPos = moveFloor.transform.position;
            this.transform.position = otherPos - distance;
        }*/
    }
   /* void Update()
    {
        //空格跳跃
        if (Input.GetKeyDown(KeyCode.Space) && !click_flag)
        {
            if (!click_flag)
            {
                time_start = Time.time;
                click_flag = true;
                plane_rigid.AddForce(new Vector2(speed * (float)(System.Math.Cos(angle / 360 * 2 * Pi)),
                    speed * (float)(System.Math.Sin(angle / 360 * 2 * Pi))));
                //plane_rigid.AddForce(new Vector2(0 ,speed ));
            }
        }
        time_current = Time.time;
        if (time_current - time_start > time_loop)
        {
            click_flag = false;
        }

        //鼠标按下拖动
        if (Input.GetMouseButtonDown(0) && !isReady)
        {
            if (!isReady)
            {
                pos_1 = Input.mousePosition;
                isReady = true;
                ready_time = Time.time;
            }

        }
        end_time = Time.time;
        if (isReady && end_time - ready_time > time_frame)
        {
            pos_2 = Input.mousePosition;
            x = pos_2.x - pos_1.x;
            y = pos_2.y - pos_1.y;
            plane_rigid.AddForce(new Vector2(speed * x / (float)(System.Math.Sqrt(x * x + y * y)),
                speed * y / (float)(System.Math.Sqrt(x * x + y * y))));
            isReady = false;
        }
        
        //飞行 无重力
        if (Input.GetKey(KeyCode.W))
        {
            plane_rigid.gravityScale = 0;
            plane_rigid.gameObject.transform.Translate(0, 0.1f, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            plane_rigid.gameObject.transform.Translate(-0.1f, 0, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            plane_rigid.gameObject.transform.Translate(0, -0.1f, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            plane_rigid.gameObject.transform.Translate(0.1f, 0, 0);
        }
        
    }*/
    //fixed time to update, to add force
    void FixedUpdate()
    {
        //Debug.Log(gameObject.name);
        //开始按下时，记录按下时间
        if (!isTouching && TouchBegin()){
            isTouching = true;
            time_start = Time.time;//what's the uint?

            startPos = Input.mousePosition;

            //mobile
            if(Input.touchCount > 0)
            {
            Touch touch = Input.GetTouch(0);
            startPos = touch.position;
                Debug.Log("touchCount > 0");
            }

            //animation
            if(canJump)
            ani.SetBool("Accumulate", true);
            
        }

        time_current = Time.time;

        //画蓄力圈
        if(canJump && isTouching)
        {
            //DrawCircle(time_current - time_start);
            DrawTrack(time_current - time_start);
        }

        //如果按很久，且超时
        if(isTouching && TouchIng())
        {
            //按得时间超过最大限制
            if(time_current - time_start >= pressTimeMax && canJump)
            {
                //to do something about animation
                ani.SetBool("Accumulate", false);

                isTouching = false;
                Jump(time_current - time_start);
                //擦除线
                circleLineRenderer.enabled = false;
            }
        }
        //之前按下，没超时，
        if(isTouching&& TouchEnd())
        {
            isTouching = false;
            if (canJump)
            {
                //to do something about animation
                ani.SetBool("Accumulate", false);

                Jump(time_current - time_start);
                //擦除线
                circleLineRenderer.enabled = false;


            }
            else if (canRush)
            {
                //pc
                direction = (Vector2)Input.mousePosition - startPos;

                //mobile
                if(Input.touchCount > 0)
                {
                Touch touch = Input.GetTouch(0);
                direction = touch.position - startPos;
                }
                //to do something about animation
                ani.SetBool("Rush", true);
                Rush(direction);
                
            }

            


        }
    }

    private void DrawTrack(float slot)
    {

        trackLineRender.enabled = true;
            float timeRatio = 1;

            //if (slot < clickTimeMin) timeRatio = 1;
            //else
            {
                float percent = slot / pressTimeMax;
                timeRatio = percent * 2;
            }
            //此处velocity的计算式应和jump函数中的一致
            Vector2 velocity = (new Vector2(timeRatio * jumpRatio * (float)(System.Math.Cos(angle / 360 * 2 * Pi)),
                        timeRatio * jumpRatio * (float)(System.Math.Sin(angle / 360 * 2 * Pi))));
        //对tmp取余来控制画轨迹的频率
        int tmp = (int)(slot / pressTimeMax * 100);
        

        if(trackLineRender != null && tmp % 5 ==0)
        {
            int linesNum = 15;
            Vector2[] segments = new Vector2[linesNum];
            segments[0] = gameObject.transform.position;

            for(int i = 1;i < linesNum;i++)
            {
                //数字为可调系数
                float time = Time.fixedDeltaTime * i * 10;
                segments[i] = segments[0] + velocity * time + 0.5f *Physics2D.gravity* rBody.gravityScale * Mathf.Pow(time, 2);
            }

            //draw
            trackLineRender.positionCount = linesNum;
            for(int i = 0;i < linesNum;i++)
            {
                trackLineRender.SetPosition(i, segments[i]);
            }
        }
    }

    private void DrawCircle(float time)
    {
        circleLineRenderer.enabled = true;
        if(circleLineRenderer != null)
        {
            
            int maxLines = 20;//圆的最多段数
            int lines =  (int)(time / pressTimeMax * maxLines);

            //draw
            circleLineRenderer.positionCount = lines + 1;
            Vector2 center = gameObject.transform.position;
            float radium = 2.0f;
            for(int  i = 0;i < lines + 1;i++)
            {
                //逆时针的角度
                double angle = -0.5 * Math.PI - i / 20.0 * 2 * Math.PI;
                circleLineRenderer.SetPosition(i, new Vector2((float)(center.x + radium * Math.Cos(angle)),(float)(center.y + radium*Math.Sin(angle))));
                //Debug.Log((float)(center.x + radium * Math.Cos(angle)) + "  "+ (float)(center.y + radium * Math.Sin(angle)));
                
            }
            //消失
            //lineRenderer.enabled = false;
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        
        //stop rush 
        ani.SetBool("Rush", false);

        //slow camera move碰到任何东西？
        CameraControl.SpeedRecover();

        //碰到wall

        //碰到enermy
        if(other.gameObject.tag == "Enermy")
        {
            //do something negative
            Hp--;
            Debug.Log(Hp);

        }else if(other.gameObject.tag == "UnderEnermy")
        {
            Death();
        }
        //恢复角度
        transform.rotation = Quaternion.Euler(0, 0, 0);

        isRushing = false;

    }

    //手机和触摸屏通用end，begin,continue
    public static bool TouchBegin()
    {
        if (Input.GetMouseButtonDown(0))
        {
            return true;
        }
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            return true;
        }
        return false;
    }

    public static bool TouchEnd()
    {
        if (Input.GetMouseButtonUp(0))
        {
            return true;
        }
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            return true;
        }
        return false;

    }

    public static bool TouchIng()
    {
        if (Input.GetMouseButton(0))
        {
            return true;
        }
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            return true;
        }
        return false;
    }

    void Jump(float slot)//force is related to press time
    {
        
        float timeRatio = 1;

       // if (slot < clickTimeMin) timeRatio = 1;
       // else
        {
            float percent = slot / pressTimeMax;
            timeRatio = percent * 2;
        }
        transform.position = new Vector2(transform.position.x, transform.position.y + 0.2f);
        rBody.velocity = new Vector2(0.0f, 0.0f);

        //give velocity
        rBody.velocity = (new Vector2(timeRatio * jumpRatio* (float)(System.Math.Cos(angle / 360 * 2 * Pi)),
                    timeRatio * jumpRatio * (float)(System.Math.Sin(angle / 360 * 2 * Pi))));
        //rBody.AddForce(new Vector2(0, speed*1.5f));
        // rBody.AddForce(new Vector2(ratio*speed * (float)(System.Math.Cos(angle / 360 * 2 * Pi)),
        //ratio*speed * (float)(System.Math.Sin(angle / 360 * 2 * Pi))));

        //跳跃音效
        AudioManager2.Instance.PlaySound("jump");

        canJump = false;
        inAir = true;
        canRush = true;
    }

    void Rush(Vector2 direction)
    {
        float x = direction.x;
        float y = direction.y;
        if (y < 0)
        {
            //change camera velocity
        CameraControl.SpeedUp();
        //擦掉参考线
        trackLineRender.enabled = false;

            //去掉初速度
            rBody.velocity = new Vector2(0.0f,0.0f);
            //addforce
            rBody.AddForce(new Vector2(speed * x / (float)(System.Math.Sqrt(x * x + y * y)),
                speed * y / (float)(System.Math.Sqrt(x * x + y * y))));

            float shoot_angle = Vector2.Angle(new Vector2(0, -1), direction) * x/Mathf.Abs(x);
            rBody.gameObject.transform.Rotate(new Vector3(0, 0, shoot_angle)); //旋转 反向延长线
            canRush = false;
            isRushing = true;
            //音效
            AudioManager2.Instance.PlaySound("drop");
        }

        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "floor" | other.gameObject.tag == "MovingFloor")
        {
            //slow camera move
            CameraControl.speed = 0.01f;
            //记录起跳角度
            float z = other.gameObject.transform.rotation.z;
             if (z < 0)
                 angle = z * 100 + 75;
             else if (z > 0)
                 angle = z * 100 + 105;
             else
                 angle = 90.0f;
            //angle = z + 90.0f;

            canJump = true;
            canRush = false;
            if(other.gameObject.tag == "MovingFloor")
            {
                //记录相对位置
                //follow = true;
                moveFloor = other.gameObject;
                Vector2 playerPos = transform.position;
                Vector2 otherPos = other.transform.position;
                distance = otherPos - playerPos;
                //StartCoroutine(Follow(moveFloor));
            }
        }
        else if (other.gameObject.tag == "Enermy")
        {
            //do something negative
            Hp--;
            Debug.Log(Hp);

        }

        //恢复角度
        transform.rotation = Quaternion.Euler(0, 0, 0);

        isRushing = false;

        if (other.gameObject.tag == "coin")
            score += 10;
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "MovingFloor" && distance != null)
        {
            {
                ani.SetBool("Rush", false);
                canJump = true;
                canRush = false;

            }
           //跟随移动
            {
                Vector2 otherPos = other.transform.position;
                this.transform.position = otherPos - distance;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "floor" | other.gameObject.tag == "MovingFloor")
        {
            canJump = false;
            canRush = true;
        }
    }

    IEnumerator WaitToDeath(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject, 0.5f);

        //return start scene
        //just test
        SceneManager.LoadScene("level2");
    }

    //角色死亡
    void Death()
    {
        //镜头停止移动
        CameraControl.StopMoveView();
        //animation & sound
        //音效
        AudioManager2.Instance.PlaySound("die");
        //死亡状态机
        ani.SetBool("die_flag", true);

        StartCoroutine(WaitToDeath(0.5f));

        score = 0;




       /* Destroy(gameObject, 0.5f);

        //return start scene
        //just test
        SceneManager.LoadScene("Start");*/
    }
}
