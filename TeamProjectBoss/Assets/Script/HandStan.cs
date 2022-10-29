using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandStan : MonoBehaviour
{
    [Tooltip("�{�X�̃X�^���̗�")]
    [SerializeField]
    private int hp = 50;

    private int damage = 5;

    public bool isHitStan;

    //�I�u�W�F�N�gTag
    private string bulletId = "Bullet";
    
    //�A�j���[�V����ID
    private string animStanId = "Stan";

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        //�A�j���[�^�[�擾
        TryGetComponent(out animator);
    }

    // Update is called once per frame
    void Update()
    {
        isHitStan = false;
        if(hp == 0)
        {
            //�X�^��������
            animator.SetBool(animStanId, true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //�e������������
        if(other.tag == bulletId)
        {
            //�Q�[�W�����炷
            //hp -= damage;
            isHitStan = true;       //����������Ԃɂ���
        }
    }
}
