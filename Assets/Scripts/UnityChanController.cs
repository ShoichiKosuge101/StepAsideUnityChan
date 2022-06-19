using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanController : MonoBehaviour
{
    private Animator myAnimator;
    private Rigidbody myRigidbody;

    [SerializeField]
    [Tooltip("���j�e�B�����̑O�i���x")]
    [Range(0,20f)]
    float velocityZ = 16f;

    [SerializeField]
    [Tooltip("���j�e�B�����̉��������x")]
    [Range(0, 20f)]
    float velocityX = 10f;

    [SerializeField]
    [Tooltip("���j�e�B�����̃W�����v��")]
    [Range(0,20f)]
    float velocityY = 10f;

    // ���E�ړ��͈͏��
    private float _movableRange = 3.4f;

    // Start is called before the first frame update
    void Start()
    {
        this.myAnimator = this.GetComponent<Animator>();

        // ����A�j���[�V�������J�n
        // Speed�p�����[�^�l�̐ݒ�
        //this.myAnimator.SetFloat("Speed", 1.0f);
        this.myAnimator.SetFloat("Speed", 0.2f);

        this.myRigidbody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // �������̓��͑��x
        var inputVelocityX = 0f;
        // �����̓��͑��x
        var inputVelocityY = 0f;

        if (Input.GetKey(KeyCode.LeftArrow) && (-this.transform.position.x < this._movableRange))
        {
            // ���E�����̑��x����
            inputVelocityX = -this.velocityX;
        }
        else if (Input.GetKey(KeyCode.RightArrow) && (this.transform.position.x < this._movableRange))
        {
            inputVelocityX = this.velocityX;
        }

        // �W�����v�����Ă��Ȃ��Ƃ��ɃX�y�[�X�������ꂽ��W�����v
        if(Input.GetKeyDown(KeyCode.Space) && !IsJump(this.transform.position.y))
        {
            inputVelocityY = Jump();
        }
        else
        {
            // ���݂�Y�����x����(�d�͕����̗���)
            inputVelocityY=this.myRigidbody.velocity.y;
        }
        // Jump�X�e�[�g�̎��AJump�t���O�𗎂Ƃ�
        if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            this.myAnimator.SetBool("Jump", false);
        }

        // ���j�e�B������O�i������
        this.myRigidbody.velocity = new Vector3(inputVelocityX, 0, this.velocityZ);
    }

    bool IsJump(float y)
    {
        return y > 0.5;
    }

    float Jump()
    {
        // �W�����v�A�j���[�V�����ɑJ��
        this.myAnimator.SetBool("Jump", true);
        // ������̑��x����
        return this.velocityY;
    }
}
