using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBird : Bird
{
    public List<Pig> blocks = new List<Pig>();

    //进入触发检测
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Enemy")
        {
            blocks.Add(collision.gameObject.gameObject.GetComponent<Pig>());
        }

    }

    //离开触发区域
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            blocks.Remove(collision.gameObject.gameObject.GetComponent<Pig>());
        }
    }
    public override void ShowSkill()
    {
        base.ShowSkill();
        if(blocks.Count > 0 &&blocks!=null) {
            for(int i = 0; i < blocks.Count; i++)
            {
                blocks[i].Dead();
            }
        }
        OnClear();
    }

    void OnClear()
    {
        rd.velocity = Vector3.zero;
        Instantiate(boom, transform.position, Quaternion.identity);
        render.enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        myTrail.ClearTrails();
    }
    protected override void Next()
    {
        GameManager._instance.birds.Remove(this);
        Destroy(gameObject);
        GameManager._instance.NextBird();
    }
}
