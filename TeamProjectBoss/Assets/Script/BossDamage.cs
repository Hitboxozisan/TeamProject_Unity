using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamage : MonoBehaviour
{
    //パラメータ
    [Tooltip("ボスの体力")]
    [SerializeField]
    private int _hp;

    [Tooltip("スタンするまでの体力")]
    [SerializeField]
    private int _stanHp;

    [Tooltip("ボスに与えるダメージ値")]
    [SerializeField]
    private int _damage;

    [Tooltip("スタンダメージ値")]
    [SerializeField]
    private int _stanDamage;

    private bool _isDeath = false;       //死んだかどうか
    private bool _isStan = false;        //スタンしたかどうか

    //アニメーションパラメーター
    private string _animIdDeath = "Death";
    private string _animIdStan = "Stan";

    private Animator _animator;
    //private bool isDamage;  //ダメージを受けたか
    //private bool isStan;    //スタンしたか

    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out _animator);                          //アニメーター取得
    }

    // Update is called once per frame
    void Update()
    {
        //体力が尽きたら
        if (_hp <= 0)
        {
            _animator.SetBool(_animIdDeath, true);
            _isDeath = true;
        }

        //スタンゲージが尽きたらスタンする
        if (_stanHp <= 0)
        {
            _animator.SetBool(_animIdStan, true);
            _isStan = true;
        }
    }
}
