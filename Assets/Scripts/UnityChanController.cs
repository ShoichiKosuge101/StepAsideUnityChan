using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanController : MonoBehaviour
{
    private Animator myAnimator;
    private Rigidbody myRigidbody;

    [SerializeField]
    [Tooltip("ユニティちゃんの前進速度")]
    [Range(0,20f)]
    float velocityZ = 16f;

    [SerializeField]
    [Tooltip("ユニティちゃんの横方向速度")]
    [Range(0, 20f)]
    float velocityX = 10f;

    [SerializeField]
    [Tooltip("ユニティちゃんのジャンプ力")]
    [Range(0,20f)]
    float velocityY = 10f;

    // 左右移動範囲上限
    private float _movableRange = 3.4f;

    // Start is called before the first frame update
    void Start()
    {
        this.myAnimator = this.GetComponent<Animator>();

        // 走るアニメーションを開始
        // Speedパラメータ値の設定
        //this.myAnimator.SetFloat("Speed", 1.0f);
        this.myAnimator.SetFloat("Speed", 0.2f);

        this.myRigidbody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // 横方向の入力速度
        var inputVelocityX = 0f;
        // 高さの入力速度
        var inputVelocityY = 0f;

        if (Input.GetKey(KeyCode.LeftArrow) && (-this.transform.position.x < this._movableRange))
        {
            // 左右方向の速度を代入
            inputVelocityX = -this.velocityX;
        }
        else if (Input.GetKey(KeyCode.RightArrow) && (this.transform.position.x < this._movableRange))
        {
            inputVelocityX = this.velocityX;
        }

        // ジャンプをしていないときにスペースが押されたらジャンプ
        if(Input.GetKeyDown(KeyCode.Space) && !IsJump(this.transform.position.y))
        {
            inputVelocityY = Jump();
        }
        else
        {
            // 現在のY軸速度を代入(重力方向の落下)
            inputVelocityY=this.myRigidbody.velocity.y;
        }
        // Jumpステートの時、Jumpフラグを落とす
        if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            this.myAnimator.SetBool("Jump", false);
        }

        // ユニティちゃんを前進させる
        this.myRigidbody.velocity = new Vector3(inputVelocityX, 0, this.velocityZ);
    }

    bool IsJump(float y)
    {
        return y > 0.5;
    }

    float Jump()
    {
        // ジャンプアニメーションに遷移
        this.myAnimator.SetBool("Jump", true);
        // 上方向の速度を代入
        return this.velocityY;
    }
}
