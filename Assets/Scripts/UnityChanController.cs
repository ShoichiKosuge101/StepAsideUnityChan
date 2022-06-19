using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    // �Q�[���I�����ɕ\������e�L�X�g
    private GameObject _stateText;

    // �X�R�A�e�L�X�g
    private GameObject _scoreText;
    // ���_
    private int _score = 0;

    // �{�^������C�x���g
    private bool _isLButtonDown = false;
    private bool _isRButtonDown = false;
    private bool _isJButtonDown = false;


    // Start is called before the first frame update
    void Start()
    {
        this._myAnimator = this.GetComponent<Animator>();

        // ����A�j���[�V�������J�n
        // Speed�p�����[�^�l�̐ݒ�
        //this.myAnimator.SetFloat("Speed", 1.0f);
        this._myAnimator.SetFloat("Speed", 0.2f);

        this._myRigidbody = this.GetComponent<Rigidbody>();

        // �V�[������stateText�I�u�W�F�N�g���擾
        this._stateText = GameObject.Find("GameResultText");

        // scoreText�I�u�W�F�N�g�擾
        this._scoreText = GameObject.Find("ScoreText");
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

        if ((Input.GetKey(KeyCode.LeftArrow) || this._isLButtonDown) && (-this.transform.position.x < _movableRange))
        {
            // �������̑��x�ɂȂ�悤���]���đ��
            inputVelocityX = -this.velocityX;
        }
        else if ((Input.GetKey(KeyCode.RightArrow) || this._isRButtonDown) && (this.transform.position.x < _movableRange))
        {
            // �E�����̑��x����
            inputVelocityX = this.velocityX;
        }

        // �W�����v�����Ă��Ȃ��Ƃ��ɃX�y�[�X/�W�����v�{�^���������ꂽ��W�����v
        if((Input.GetKeyDown(KeyCode.Space) || this._isJButtonDown) && !IsJump(this.transform.position.y))
        {
            // ������̑��x����
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
        this._myRigidbody.velocity = new Vector3(inputVelocityX, inputVelocityY , this.velocityZ);
    }

    // �W�����v����
    bool IsJump(float y)
    {
        return y > 0.5;
    }
    float Jump()
    {
        // �W�����v�A�j���[�V�����̍Đ�
        this._myAnimator.SetBool("Jump", true);
        return this.velocityY;
    }

    // �C�x���g�g���K�[
    public void GetMyJumpButtonDown() => this._isJButtonDown = true;
    public void GetMyJumpButtonUp() => this._isJButtonDown = false;
    public void GetMyLeftButtonDown() => this._isLButtonDown = true;
    public void GetMyLeftButtonUp()=>this._isLButtonDown=false;
    public void GetMyRightButtonDown() => this._isRButtonDown = true;
    public void GetMyRightButtonUp()=> this._isRButtonDown=false;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(TagName.CarTag) || other.gameObject.CompareTag(TagName.TrafficConeTag))
        {
            this._isEnd = true;

            // stateText��GAMEOVER�\��
            this._stateText.GetComponent<Text>().text = "GAME OVER";
        }

        if(other.gameObject.CompareTag(TagName.GoalTag))
        {
            this._isEnd = true;

            // stateText��GAMECLEAR�\��
            this._stateText.GetComponent<Text>().text = "CLEAR!!";
        }

        if (other.gameObject.CompareTag(TagName.CoinTag))
        {
            // �p�[�e�B�N���Đ�
            GetComponent<ParticleSystem>().Play();
            Destroy(other.gameObject);

            // �X�R�A���Z
            this._score += 10;
            this._scoreText.GetComponent<Text>().text = $"Score {this._score}pt";
        }
    }
}
