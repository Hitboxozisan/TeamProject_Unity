using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBody : MonoBehaviour
{
    [Tooltip("�{�X�̗̑�")]
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
        //�e�ƐڐG������_���[�W
        if(other.tag == BulletId)
        {
            //HP�����炷
            //HP -= 1;
            isHitBody = true;
        }
    }
}
