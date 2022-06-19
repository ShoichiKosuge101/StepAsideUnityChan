using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanController : MonoBehaviour
{
    private Animator _myAnimator;
    private Rigidbody _myRigidbody;

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
    private const float _movableRange = 3.4f;

    // �Q�[���̏I������
    private bool _isEnd = false;
    // �Q�[���I�����̑��x�����W��
    private float _coefficient = 0.99f;

    // Start is called before the first frame update
    void Start()
    {
        this._myAnimator = this.GetComponent<Animator>();

        // ����A�j���[�V�������J�n
        // Speed�p�����[�^�l�̐ݒ�
        //this.myAnimator.SetFloat("Speed", 1.0f);
        this._myAnimator.SetFloat("Speed", 0.2f);

        this._myRigidbody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // �Q�[���I���Ȃ瑬�x������������
        if (_isEnd)
        {
            this.velocityX *= _coefficient;
            this.velocityY *= _coefficient;
            this.velocityZ *= _coefficient;
            this._myAnimator.speed *= _coefficient;
        }

        // �������̓��͑��x
        var inputVelocityX = 0f;
        // �����̓��͑��x
        var inputVelocityY = 0f;

        if (Input.GetKey(KeyCode.LeftArrow) && (-this.transform.position.x < _movableRange))
        {
            // ���E�����̑��x����
            inputVelocityX = -this.velocityX;
        }
        else if (Input.GetKey(KeyCode.RightArrow) && (this.transform.position.x < _movableRange))
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
            inputVelocityY=this._myRigidbody.velocity.y;
        }
        // Jump�X�e�[�g�̎��AJump�t���O�𗎂Ƃ�
        if (this._myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            this._myAnimator.SetBool("Jump", false);
        }

        // ���j�e�B������O�i������
        this._myRigidbody.velocity = new Vector3(inputVelocityX, 0, this.velocityZ);
    }

    // �W�����v����
    bool IsJump(float y)
    {
        return y > 0.5;
    }

    float Jump()
    {
        // �W�����v�A�j���[�V�����ɑJ��
        this._myAnimator.SetBool("Jump", true);
        // ������̑��x����
        return this.velocityY;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(TagName.CarTag) || other.gameObject.CompareTag(TagName.TrafficConeTag))
        {
            this._isEnd = true;
        }

        if(other.gameObject.CompareTag(TagName.GoalTag))
        {
            this._isEnd = true;
        }

        if (other.gameObject.CompareTag(TagName.CoinTag))
        {
            // �p�[�e�B�N���Đ�
            GetComponent<ParticleSystem>().Play();
            Destroy(other.gameObject);
        }
    }
}
