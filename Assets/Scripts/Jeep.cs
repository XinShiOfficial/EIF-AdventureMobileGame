using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jeep : MonoBehaviour {

    //相机
    GameObject camera;

    static float Pi = 3.1415926f;
    float angle = 0f;

    private Rigidbody2D plane_rigid;

    bool click_flag = false;

    float time_start = 0.0f;
    float time_current = 0.0f;
    float time_loop = 1.0f;
    float time_frame = 0.3f;

    float ready_time = 0f;
    float end_time = 0f;

    float jump_speed = 400f;
    float jump_speed_init = 0f;
    float jump_speed_max = 680f;
    float shoot_speed = 1300f;
    Vector3 f_dir;
    float shoot_angle;

    Vector3 pos_1;
    Vector3 pos_2;
    bool isReady = false;

    float x = 0f;
    float y = 0f;

    Collider2D last_floor_collider;
    GameObject last_floor;

    //主角状态
    public enum State { ON_FLOOR, ON_JUMP, ON_SHOOT };
    public enum F_State { LEVEL1, LEVEL2, LEVEL3 };
    public static int ball_state;
    public static int f_level;

    //动画状态机
    Animator animator;

    //初始位置
    Vector3 init_position;

    //分数
    public static int score = 0;
    //声明
    int life = 1;

    //轨迹render
    public LineRenderer trackLineRender;

    //施加力后的 速度
    Vector2 force_speed;

    // Use this for initialization
    void Start () {
        plane_rigid = GetComponent<Rigidbody2D>();
        last_floor = GameObject.Find("last_floor");
        //last_floor_collider = last_floor.GetComponent<Collider2D>();
        //初始状态
        ball_state = (int)(State.ON_FLOOR);
        init_position = plane_rigid.gameObject.transform.position;
        jump_speed = jump_speed_init;
        animator = GetComponent<Animator>();
        //相机
        camera = GameObject.Find("Main Camera");
    }
	
	// Update is called once per frame
	void FixedUpdate() {
        changeLevel(); //在板子上 动画状态
        jumpForState(); //施加力
        changeCameraSpeed(); //镜头上升速度
        isDead(); //判断死亡
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //angle = other.gameObject.transform.rotation.z * 100 + 90;

        if (other.gameObject.name == "coin")
        {
            Destroy(other.gameObject);
        }
        //Debug.Log(other.gameObject.name+"------开始碰撞----------");

        if(ball_state == (int)(State.ON_SHOOT))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (other.gameObject.tag == "MovingFloor")
        {
            //静止物体
            plane_rigid.velocity = Vector3.zero;
            plane_rigid.angularVelocity = 0f;
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        angle = other.gameObject.transform.rotation.z * 100 + 90;
        //Debug.Log(other.gameObject.name + "------正在碰撞-------------");
        ball_state = (int)(State.ON_FLOOR);
        transform.rotation = Quaternion.Euler(0, 0, 0); //恢复玩家角度

        if(other.gameObject.tag == "MovingFloor" && transform.position.y - other.gameObject.transform.position.y > 0.7f)
        {
            float sp = Horizonal_Floor.horizontal_speed;  //跟随板子
            transform.Translate(new Vector3(sp, 0, 0));
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        //Debug.Log(other.gameObject.name + "------结束碰撞-------------");
        ball_state = (int)(State.ON_JUMP);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "coin")
        {
            Destroy(other.gameObject);
            score++;
            //音效
            AudioManager.Instance.PlaySound("coin");
        }
        if(other.gameObject.tag == "fire")
        {
            life--;
        }
    }

    void jumpForState()
    {
        switch (ball_state)
        {
            case (int)(State.ON_FLOOR):
                if (Input.GetMouseButtonDown(0) || click_flag)
                {
                    //静止物体
                    plane_rigid.velocity = Vector3.zero;
                    plane_rigid.angularVelocity = 0f;
                    click_flag = true;
                }
                if (click_flag && jump_speed < jump_speed_max)
                    jump_speed += 15f;

                //力
                Vector2 force = new Vector2(jump_speed * (float)(System.Math.Cos(angle / 360 * 2 * Pi)),
                   jump_speed * (float)(System.Math.Sin(angle / 360 * 2 * Pi)));
                //获取物体施加力后的速度
                force_speed = force * Time.fixedDeltaTime / plane_rigid.mass;
                //Debug.Log(force_speed);

                //绘制轨迹
                DrawTrack(force_speed);

                if (Input.GetMouseButtonUp(0))
                {
                    //再添加力
                    plane_rigid.AddForce(force);

                    //跳跃音效
                    //AudioManager.Instance.PlaySound("jump");

                    click_flag = false;
                    jump_speed = jump_speed_init;
                }
                break;
            case (int)(State.ON_JUMP):
                if (Input.GetMouseButtonDown(0) && !isReady)
                {
                    if (!isReady)
                    {
                        pos_1 = Input.mousePosition;
                    }
                }
                end_time = Time.time;
                if (Input.GetMouseButtonUp(0))
                {
                    pos_2 = Input.mousePosition;
                    x = pos_2.x - pos_1.x;
                    y = pos_2.y - pos_1.y;
                    if (y < 0)
                    {
                        plane_rigid.AddForce(new Vector2(shoot_speed * x / (float)(System.Math.Sqrt(x * x + y * y)),
                        shoot_speed * y / (float)(System.Math.Sqrt(x * x + y * y))));
                        //Debug.Log(Vector3.Angle(new Vector3(0, 0, 0), new Vector3(-1, 1, 0)));
                        shoot_angle = Vector3.Angle(pos_1, pos_2) * x/Mathf.Abs(x); //判断冲刺角度正负
                        plane_rigid.gameObject.transform.Rotate(new Vector3(0, 0, shoot_angle)); //旋转 反向延长线
                    }
                    isReady = false;
                    //加速镜头
                    cameraSpeedUp();
                    //切换状态
                    ball_state = (int)(State.ON_SHOOT);
                    //音效
                    AudioManager.Instance.PlaySound("drop");
                }
                break;
            case (int)(State.ON_SHOOT): break;
            default: break;

        }
    }

    void changeLevel()
    {
        if (jump_speed < 200)
            f_level = (int)(F_State.LEVEL1);
        else if (jump_speed >= 200 && jump_speed < 450)
            f_level = (int)(F_State.LEVEL2);
        else if (jump_speed >= 450 && jump_speed <= 500)
            f_level = (int)(F_State.LEVEL3);
        else;

        switch (f_level)
        {
            case (int)(F_State.LEVEL1):
                animator.SetFloat("f_level", 0f);
                break;
            case (int)(F_State.LEVEL2):
                animator.SetFloat("f_level", 0.5f);
                break;
            case (int)(F_State.LEVEL3):
                animator.SetFloat("f_level", 1f);
                break;
            default:
                break;
        }

        switch(ball_state)
        {
            case (int)(State.ON_FLOOR):
                animator.SetBool("on_floor_flag", true);
                break;
            case (int)(State.ON_JUMP):
            case (int)(State.ON_SHOOT):
                animator.SetBool("on_floor_flag", false);
                break;
            default:
                break;
        }
    }

    IEnumerator WaitToDeath(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject, 0.5f);

        //重载场景
        SceneManager.LoadScene("level1");
    }

    void isDead()
    {
        if (plane_rigid.gameObject.transform.position.y - camera.transform.position.y < -9.6f || life <= 0)
        {
            score = 0;
            //音效
            AudioManager.Instance.PlaySound("die");
            //死亡状态机
            animator.SetBool("die_flag", true);
            //0.5S后重载场景
            StartCoroutine(WaitToDeath(0.5f));
        }
    }

    void cameraSpeedUp()
    {
        Camera_Move.speed = Camera_Move.shoot_speed;
    }

    void cameraSpeedRec()
    {
        Camera_Move.speed = Camera_Move.normal_speed;
    }

    void changeCameraSpeed()
    {
        if (camera.transform.position.y - plane_rigid.gameObject.transform.position.y > 4.5f)
            cameraSpeedRec();
    }

    public void onClick()
    {
        Debug.Log("Pause");
    }

    private void DrawTrack(Vector2 velocity)
    {

        trackLineRender.enabled = true;
        float timeRatio = 1;


        if (trackLineRender != null )
        {
            int linesNum = 15;
            Vector2[] segments = new Vector2[linesNum];
            segments[0] = gameObject.transform.position;

            for (int i = 1; i < linesNum; i++)
            {
                //数字为可调系数
                float time = Time.fixedDeltaTime * i * 10;
                segments[i] = segments[0] + velocity * time + 0.5f * Physics2D.gravity * plane_rigid.gravityScale * Mathf.Pow(time, 2);
            }

            //draw
            trackLineRender.positionCount = linesNum;
            for (int i = 0; i < linesNum; i++)
            {
                trackLineRender.SetPosition(i, segments[i]);
            }
        }
    }

}
