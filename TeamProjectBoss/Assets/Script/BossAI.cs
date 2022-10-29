using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossAI : MonoBehaviour
{
    //�U������܂ł̎���
    private float _attackTime = 5f;

    //�ʏ�̃A�j���[�V�������x
    private float _normalAnimSpeed = 1f;
    //�x�������Ƃ��̃A�j���[�V�������x
    private float _slowAnimSpeed = 0.1f;
    //���Ԃ�x�����鎞��
    private float _slowTime = 5f;
    //�o�ߎ���
    private float _elapsedTime = 0f;
    private float _slowElapsedTime = 0f;
    //���Ԃ�x�����Ă��邩
    private bool _isSlowDown = false;

    //�v���C���[�ɍU��������������
    public bool isHitPlayer = false;

    //�A�j���[�^�[
    private Animator _animator;
    //�J�[�\��
    private Image _rightLockOnMarker;
    private Image _leftLockOnMarker;
    //Image�I�u�W�F�N�g�̖��O
    private string _RLockOnMarkerId = "RightHandLockOnMarker";
    private string _LLockOnMarkerId = "LeftHandLockOnMarker";

    //�v���C���[���_���ׂ��Ώ�
    private GameObject _rightHand;
    private GameObject _leftHand;
    //�v���C���[���_���ׂ��I�u�W�F�N�g�̖��O
    private string _rightHandId = "R_Hand_Hit";
    private string _leftHandId = "L_Hand_Hit";

    //�A�j���[�V�����p�����[�^Id
    private string _animAttack1Id = "Attack1";
    private string _animAttack2Id = "Attack2";
    private string _animAttack3Id = "Attack3";
    private string _animStanId = "Stan";

    //���̍U�����[�V����
    private string _nextAttack;
    
    
    // Start is called before the first frame update
    void Start()
    {
        //�A�j���[�^�[�擾
        TryGetComponent(out _animator);
        _rightLockOnMarker = GameObject.Find(_RLockOnMarkerId).GetComponent<Image>();
        _leftLockOnMarker = GameObject.Find(_LLockOnMarkerId).GetComponent<Image>();

        _rightLockOnMarker.enabled = false;              //�摜���\���ɂ���
        _leftLockOnMarker.enabled = false;               //�摜���\���ɂ���

        //�}�[�J�[�T�C�Y��ݒ�
        _rightLockOnMarker.transform.localScale = new Vector3(20f, 20f, 0.0f);
        _leftLockOnMarker.transform.localScale = new Vector3(20f, 20f, 0.0f);

        //�Q�[���I�u�W�F�N�g�擾
        _rightHand = GameObject.Find(_rightHandId);
        _leftHand = GameObject.Find(_leftHandId);
    }

    // Update is called once per frame
    void Update()
    {
        isHitPlayer = false;                //�������Ă��Ȃ���Ԃɂ���

        //�X���[���[�V������
        if (_isSlowDown)
        {
            _slowElapsedTime += Time.deltaTime;
            LockOn();               //���b�N�I���}�[�J�[�̕\������шʒu����

            //�X�^��������
            if (_animator.GetBool(_animStanId))
            {
                SetNormalTime();        //���Ԃ����ɖ߂�
            }
            //�������Ԃ𒴂�����U���v���C���[�Ƀ_���[�W
            if(_slowTime <= _slowElapsedTime)
            {
                HitBossAttack();
            }
        }
        else
        {
            _elapsedTime += Time.deltaTime;
        }
        //�U�����[�V�����ɓ���
        if (_attackTime <= _elapsedTime)
        {
            SetNextAttack();
            _animator.SetBool(_nextAttack, true);
            SlowDown();
        }
    }

    //���̍U�������肷��
    public void SetNextAttack()
    {
        //�O��ƈႤ�U����������
        //���S�����_�����l�������΂肪�ł邽�ߋp��
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


    //���Ԃ�x�����鏈��
    public void SlowDown()
    {
        _elapsedTime = 0f;
        _slowElapsedTime = 0f;
        _animator.speed = _slowAnimSpeed;
        //���b�N�I���}�[�J�[��\��
        _rightLockOnMarker.enabled = true;
        _leftLockOnMarker.enabled = true;

        _isSlowDown = true;
    }

    //���Ԃ����Ƃɖ߂�����
    public void SetNormalTime()
    {
        _elapsedTime = 0f;
        _slowElapsedTime = 0f;
        _animator.speed = _normalAnimSpeed;
       
        //���b�N�I���}�[�J�[���\����
        _rightLockOnMarker.enabled = false;     
        _leftLockOnMarker.enabled = false;

        //�}�[�J�[�T�C�Y��ݒ�
        _rightLockOnMarker.transform.localScale = new Vector3(20f, 20f, 0.0f);
        _leftLockOnMarker.transform.localScale = new Vector3(20f, 20f, 0.0f);

        //�A�j���[�^�[�̃t���O��false��
        //_animator.SetBool(_animAttackId, false);
        //_animator.SetBool(_animStanId, false);

        _isSlowDown = false;
    }

    //���b�N�I���}�[�J�[�����l�܂ŏk��
    public bool StartLockOn()
    {
        //���b�N�I���}�[�J�[��u���ׂ��ꏊ���擾
        Vector2 rightHandPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, _rightHand.transform.position);
        _rightLockOnMarker.transform.position = new Vector3(rightHandPosition.x, rightHandPosition.y, 0f);
        Vector2 leftHandPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, _leftHand.transform.position);
        _leftLockOnMarker.transform.position = new Vector3(leftHandPosition.x, leftHandPosition.y, 0f);

        //���T�C�Y�܂ŏk������
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

    //�U���O�Ƀ��b�N�I���}�[�J�[��\�����鏈��
    public void LockOn()
    {
        float sin1 = Mathf.Sin(Time.time);
        float sin2 = Mathf.Sin(Time.time);
        float scaling1 = 1.5f;                   //�g��␳�l
        float scaling2 = 1.5f;                   //�g��␳�l

        //���b�N�I���}�[�J�[��u���ׂ��ꏊ���擾
        Vector2 rightHandPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, _rightHand.transform.position);
        _rightLockOnMarker.transform.position = new Vector3(rightHandPosition.x, rightHandPosition.y, 0f);
        Vector2 leftHandPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, _leftHand.transform.position);
        _leftLockOnMarker.transform.position = new Vector3(leftHandPosition.x, leftHandPosition.y, 0f);
       
        //�g��k�����J��Ԃ�
        _rightLockOnMarker.transform.localScale = new Vector3(scaling2 * sin1, scaling2 * sin2, 1.0f);
        _rightLockOnMarker.transform.Rotate(0f, 0f, 120f * Time.deltaTime);
        _leftLockOnMarker.transform.Rotate(0f, 0f, 120f * Time.deltaTime);
        _leftLockOnMarker.transform.localScale = new Vector3(scaling1 * sin1, scaling1 * sin2, 1.0f);
    }

    public bool IsSlowDown()
    {
        return _isSlowDown;
    }

    //�{�X�̍U�����v���C���[�ɓ�������
    public bool HitBossAttack()
    {
        //�v���C���[�ɍU������������
        isHitPlayer = true;
        //���Ԃ����ɖ߂�
        SetNormalTime();
        return isHitPlayer;
    }

    //�A�j���[�V�����C�x���g�FIdle
    //Idle�A�j���[�V�����ɖ߂�����e�p�����[�^��false�ɂ���
    public void Idle()
    {
        _animator.SetBool(_animAttack1Id, false);
        _animator.SetBool(_animAttack2Id, false);
        _animator.SetBool(_animStanId, false);
    }
}
