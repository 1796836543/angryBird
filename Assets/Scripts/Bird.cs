using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private bool isClick = false;
    public float maxDis;
    [HideInInspector]
    public SpringJoint2D sp;
    protected Rigidbody2D rd;

    public LineRenderer right;
    public Transform rightPos;
    public LineRenderer left;
    public Transform leftPos;
    public GameObject boom;

    protected TestMyTrail myTrail;

    private bool canMove = true;
    public float smooth = 3;

    public AudioClip select;
    public AudioClip fly;

    private bool isFly = false;

    public Sprite hurt;
    protected SpriteRenderer render;
    private void Awake()
    {
        sp = GetComponent<SpringJoint2D>();
        rd = GetComponent<Rigidbody2D>();
        myTrail = GetComponent<TestMyTrail>();
        render = GetComponent<SpriteRenderer>();
    }
    private void OnMouseDown()//鼠标按下
    {
        if (canMove)
        {
            AudioPlay(select);
            isClick = true;
            rd.isKinematic = true;
        }
        
       
    }
    private void OnMouseUp()//鼠标抬起
    {
        if (canMove) 
        { 
        isClick = false;
        rd.isKinematic = false;
        Invoke("Fly", 0.15f);
        //禁用划线组件（弹弓的绳）
        right.enabled = false;
        left.enabled = false;
        canMove = false;
        }
    }
    private void Update()
    {
        if (isClick)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position += new Vector3(0, 0, 10);
       
        if (Vector3.Distance(transform.position,rightPos.position)>maxDis)
            { 
            Vector3 pos = (transform.position-rightPos.position).normalized;
            pos *= maxDis;
            transform.position = pos+rightPos.position;
            }
            Line();
        }


        //相机跟随
        Vector3 cam_pos = Camera.main.transform.position;
        float posX = transform.position.x;

        Camera.main.transform.position = Vector3.Lerp(cam_pos,new Vector3(Mathf.Clamp(posX,0,15),cam_pos.y,cam_pos.z),smooth*Time.deltaTime);
        
        if(isFly)
        {
            if(Input.GetMouseButtonDown(0))
            {
                ShowSkill();
            }
        }
    }
    void Fly()
    {
        isFly = true;
        AudioPlay(fly);
        myTrail.StartTrails();
        sp.enabled = false;
        Invoke("Next", 5);
    }
    void Line()
    {
        right.enabled = true;
        left.enabled = true;
        right.SetPosition(0,rightPos.position);
        right.SetPosition(1,transform.position);

        left.SetPosition(0,leftPos.position);
        left.SetPosition(1,transform.position);
    }
    protected virtual void Next()
    {
        GameManager._instance.birds.Remove(this);
        Destroy(gameObject);
        Instantiate(boom, transform.position, Quaternion.identity);
        GameManager._instance.NextBird();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        isFly = false;
        myTrail.ClearTrails();

    }

    public void AudioPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }

    public virtual void ShowSkill()//炫技操作
    {
        isFly = false;
    }

    public void Hurt()
    {

        render.sprite = hurt;
    }
}
