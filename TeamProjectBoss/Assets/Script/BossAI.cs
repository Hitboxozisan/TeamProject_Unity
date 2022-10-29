using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossAI : MonoBehaviour
{
    //攻撃するまでの時間
    private float _attackTime = 5f;

    //通常のアニメーション速度
    private float _normalAnimSpeed = 1f;
    //遅くしたときのアニメーション速度
    private float _slowAnimSpeed = 0.1f;
    //時間を遅くする時間
    private float _slowTime = 5f;
    //経過時間
    private float _elapsedTime = 0f;
    private float _slowElapsedTime = 0f;
    //時間を遅くしているか
    private bool _isSlowDown = false;

    //プレイヤーに攻撃が当たったか
    public bool isHitPlayer = false;

    //アニメーター
    private Animator _animator;
    //カーソル
    private Image _rightLockOnMarker;
    private Image _leftLockOnMarker;
    //Imageオブジェクトの名前
    private string _RLockOnMarkerId = "RightHandLockOnMarker";
    private string _LLockOnMarkerId = "LeftHandLockOnMarker";

    //プレイヤーが狙うべき対象
    private GameObject _rightHand;
    private GameObject _leftHand;
    //プレイヤーが狙うべきオブジェクトの名前
    private string _rightHandId = "R_Hand_Hit";
    private string _leftHandId = "L_Hand_Hit";

    //アニメーションパラメータId
    private string _animAttack1Id = "Attack1";
    private string _animAttack2Id = "Attack2";
    private string _animAttack3Id = "Attack3";
    private string _animStanId = "Stan";

    //次の攻撃モーション
    private string _nextAttack;
    
    
    // Start is called before the first frame update
    void Start()
    {
        //アニメーター取得
        TryGetComponent(out _animator);
        _rightLockOnMarker = GameObject.Find(_RLockOnMarkerId).GetComponent<Image>();
        _leftLockOnMarker = GameObject.Find(_LLockOnMarkerId).GetComponent<Image>();

        _rightLockOnMarker.enabled = false;              //画像を非表示にする
        _leftLockOnMarker.enabled = false;               //画像を非表示にする

        //マーカーサイズを設定
        _rightLockOnMarker.transform.localScale = new Vector3(20f, 20f, 0.0f);
        _leftLockOnMarker.transform.localScale = new Vector3(20f, 20f, 0.0f);

        //ゲームオブジェクト取得
        _rightHand = GameObject.Find(_rightHandId);
        _leftHand = GameObject.Find(_leftHandId);
    }

    // Update is called once per frame
    void Update()
    {
        isHitPlayer = false;                //当たっていない状態にする

        //スローモーション時
        if (_isSlowDown)
        {
            _slowElapsedTime += Time.deltaTime;
            LockOn();               //ロックオンマーカーの表示および位置調整

            //スタンしたら
            if (_animator.GetBool(_animStanId))
            {
                SetNormalTime();        //時間を元に戻す
            }
            //制限時間を超えたら攻撃プレイヤーにダメージ
            if(_slowTime <= _slowElapsedTime)
            {
                HitBossAttack();
            }
        }
        else
        {
            _elapsedTime += Time.deltaTime;
        }
        //攻撃モーションに入る
        if (_attackTime <= _elapsedTime)
        {
            SetNextAttack();
            _animator.SetBool(_nextAttack, true);
            SlowDown();
        }
    }

    //次の攻撃を決定する
    public void SetNextAttack()
    {
        //前回と違う攻撃をさせる
        //完全ランダムも考えたが偏りがでるため却下
        if(_nextAttack == _animAttack1Id)
        {
            _nextAttack = _animAttack2Id;
        }
        else if(_nextAttack == _animAttack2Id)
        {
            _nextAttack = _animAttack3Id;
        }
        else
        {
            _nextAttack = _animAttack1Id;
        }
    }


    //時間を遅くする処理
    public void SlowDown()
    {
        _elapsedTime = 0f;
        _slowElapsedTime = 0f;
        _animator.speed = _slowAnimSpeed;
        //ロックオンマーカーを表示
        _rightLockOnMarker.enabled = true;
        _leftLockOnMarker.enabled = true;

        _isSlowDown = true;
    }

    //時間をもとに戻す処理
    public void SetNormalTime()
    {
        _elapsedTime = 0f;
        _slowElapsedTime = 0f;
        _animator.speed = _normalAnimSpeed;
       
        //ロックオンマーカーを非表示に
        _rightLockOnMarker.enabled = false;     
        _leftLockOnMarker.enabled = false;

        //マーカーサイズを設定
        _rightLockOnMarker.transform.localScale = new Vector3(20f, 20f, 0.0f);
        _leftLockOnMarker.transform.localScale = new Vector3(20f, 20f, 0.0f);

        //アニメーターのフラグをfalse化
        //_animator.SetBool(_animAttackId, false);
        //_animator.SetBool(_animStanId, false);

        _isSlowDown = false;
    }

    //ロックオンマーカーを一定値まで縮小
    public bool StartLockOn()
    {
        //ロックオンマーカーを置くべき場所を取得
        Vector2 rightHandPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, _rightHand.transform.position);
        _rightLockOnMarker.transform.position = new Vector3(rightHandPosition.x, rightHandPosition.y, 0f);
        Vector2 leftHandPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, _leftHand.transform.position);
        _leftLockOnMarker.transform.position = new Vector3(leftHandPosition.x, leftHandPosition.y, 0f);

        //一定サイズまで縮小する
        if (_rightLockOnMarker.transform.localScale != new Vector3(2f, 2f, 0f) &&
            _leftLockOnMarker.transform.localScale != new Vector3(2f, 2f, 0f))
        {
            _rightLockOnMarker.transform.localScale -= new Vector3(0.5f, 0.5f, 0.0f);
            _leftLockOnMarker.transform.localScale -= new Vector3(0.5f, 0.5f, 0.0f);
           
        }
        else
        {
            return true;
        }

        return false;
    }

    //攻撃前にロックオンマーカーを表示する処理
    public void LockOn()
    {
        float sin1 = Mathf.Sin(Time.time);
        float sin2 = Mathf.Sin(Time.time);
        float scaling1 = 1.5f;                   //拡大補正値
        float scaling2 = 1.5f;                   //拡大補正値

        //ロックオンマーカーを置くべき場所を取得
        Vector2 rightHandPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, _rightHand.transform.position);
        _rightLockOnMarker.transform.position = new Vector3(rightHandPosition.x, rightHandPosition.y, 0f);
        Vector2 leftHandPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, _leftHand.transform.position);
        _leftLockOnMarker.transform.position = new Vector3(leftHandPosition.x, leftHandPosition.y, 0f);
       
        //拡大縮小を繰り返す
        _rightLockOnMarker.transform.localScale = new Vector3(scaling2 * sin1, scaling2 * sin2, 1.0f);
        _rightLockOnMarker.transform.Rotate(0f, 0f, 120f * Time.deltaTime);
        _leftLockOnMarker.transform.Rotate(0f, 0f, 120f * Time.deltaTime);
        _leftLockOnMarker.transform.localScale = new Vector3(scaling1 * sin1, scaling1 * sin2, 1.0f);
    }

    public bool IsSlowDown()
    {
        return _isSlowDown;
    }

    //ボスの攻撃がプレイヤーに当たった
    public bool HitBossAttack()
    {
        //プレイヤーに攻撃が当たった
        isHitPlayer = true;
        //時間を元に戻す
        SetNormalTime();
        return isHitPlayer;
    }

    //アニメーションイベント：Idle
    //Idleアニメーションに戻ったら各パラメータをfalseにする
    public void Idle()
    {
        _animator.SetBool(_animAttack1Id, false);
        _animator.SetBool(_animAttack2Id, false);
        _animator.SetBool(_animStanId, false);
    }
}
