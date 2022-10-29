using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBody : MonoBehaviour
{
    [Tooltip("ボスの体力")]
    [SerializeField]
    public int HP;

    public bool isHitBody;

    private string BulletId = "Bullet";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isHitBody = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //弾と接触したらダメージ
        if(other.tag == BulletId)
        {
            //HPを減らす
            //HP -= 1;
            isHitBody = true;
        }
    }
}
