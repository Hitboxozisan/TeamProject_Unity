using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamage : MonoBehaviour
{
    //�p�����[�^
    [Tooltip("�{�X�̗̑�")]
    [SerializeField]
    private int _hp;

    [Tooltip("�X�^������܂ł̗̑�")]
    [SerializeField]
    private int _stanHp;

    [Tooltip("�{�X�ɗ^����_���[�W�l")]
    [SerializeField]
    private int _damage;

    [Tooltip("�X�^���_���[�W�l")]
    [SerializeField]
    private int _stanDamage;

    private bool _isDeath = false;       //���񂾂��ǂ���
    private bool _isStan = false;        //�X�^���������ǂ���

    //�A�j���[�V�����p�����[�^�[
    private string _animIdDeath = "Death";
    private string _animIdStan = "Stan";

    private Animator _animator;
    //private bool isDamage;  //�_���[�W���󂯂���
    //private bool isStan;    //�X�^��������

    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out _animator);                          //�A�j���[�^�[�擾
    }

    // Update is called once per frame
    void Update()
    {
        //�̗͂��s������
        if (_hp <= 0)
        {
            _animator.SetBool(_animIdDeath, true);
            _isDeath = true;
        }

        //�X�^���Q�[�W���s������X�^������
        if (_stanHp <= 0)
        {
            _animator.SetBool(_animIdStan, true);
            _isStan = true;
        }
    }
}
