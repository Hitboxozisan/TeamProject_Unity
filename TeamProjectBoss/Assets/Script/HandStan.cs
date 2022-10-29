using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandStan : MonoBehaviour
{
    [Tooltip("ボスのスタン体力")]
    [SerializeField]
    private int hp = 50;

    private int damage = 5;

    public bool isHitStan;

    //オブジェクトTag
    private string bulletId = "Bullet";
    
    //アニメーションID
    private string animStanId = "Stan";

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        //アニメーター取得
        TryGetComponent(out animator);
    }

    // Update is called once per frame
    void Update()
    {
        isHitStan = false;
        if(hp == 0)
        {
            //スタンさせる
            animator.SetBool(animStanId, true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //弾が当たったら
        if(other.tag == bulletId)
        {
            //ゲージを減らす
            //hp -= damage;
            isHitStan = true;       //当たった状態にする
        }
    }
}
