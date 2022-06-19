using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour
{
    private Animator _myAnimator;
    private Rigidbody _myRigidbody;

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
    private const float _movableRange = 3.4f;

    // ゲームの終了判定
    private bool _isEnd = false;
    // ゲーム終了時の速度減衰係数
    private float _coefficient = 0.99f;
    // ゲーム終了時に表示するテキスト
    private GameObject _stateText;

    // スコアテキスト
    private GameObject _scoreText;
    // 得点
    private int _score = 0;

    // ボタン操作イベント
    private bool _isLButtonDown = false;
    private bool _isRButtonDown = false;
    private bool _isJButtonDown = false;


    // Start is called before the first frame update
    void Start()
    {
        this._myAnimator = this.GetComponent<Animator>();

        // 走るアニメーションを開始
        // Speedパラメータ値の設定
        //this.myAnimator.SetFloat("Speed", 1.0f);
        this._myAnimator.SetFloat("Speed", 0.2f);

        this._myRigidbody = this.GetComponent<Rigidbody>();

        // シーン中のstateTextオブジェクトを取得
        this._stateText = GameObject.Find("GameResultText");

        // scoreTextオブジェクト取得
        this._scoreText = GameObject.Find("ScoreText");
    }

    // Update is called once per frame
    void Update()
    {
        // ゲーム終了なら速度を減衰させる
        if (_isEnd)
        {
            this.velocityX *= _coefficient;
            this.velocityY *= _coefficient;
            this.velocityZ *= _coefficient;
            this._myAnimator.speed *= _coefficient;
        }

        // 横方向の入力速度
        var inputVelocityX = 0f;
        // 高さの入力速度
        var inputVelocityY = 0f;

        if ((Input.GetKey(KeyCode.LeftArrow) || this._isLButtonDown) && (-this.transform.position.x < _movableRange))
        {
            // 左方向の速度になるよう反転して代入
            inputVelocityX = -this.velocityX;
        }
        else if ((Input.GetKey(KeyCode.RightArrow) || this._isRButtonDown) && (this.transform.position.x < _movableRange))
        {
            // 右方向の速度を代入
            inputVelocityX = this.velocityX;
        }

        // ジャンプをしていないときにスペース/ジャンプボタンが押されたらジャンプ
        if((Input.GetKeyDown(KeyCode.Space) || this._isJButtonDown) && !IsJump(this.transform.position.y))
        {
            // 上方向の速度を代入
            inputVelocityY = Jump();
        }
        else
        {
            // 現在のY軸速度を代入(重力方向の落下)
            inputVelocityY=this._myRigidbody.velocity.y;
        }
        // Jumpステートの時、Jumpフラグを落とす
        if (this._myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            this._myAnimator.SetBool("Jump", false);
        }

        // ユニティちゃんを前進させる
        this._myRigidbody.velocity = new Vector3(inputVelocityX, inputVelocityY , this.velocityZ);
    }

    // ジャンプ中か
    bool IsJump(float y)
    {
        return y > 0.5;
    }
    float Jump()
    {
        // ジャンプアニメーションの再生
        this._myAnimator.SetBool("Jump", true);
        return this.velocityY;
    }

    // イベントトリガー
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

            // stateTextにGAMEOVER表示
            this._stateText.GetComponent<Text>().text = "GAME OVER";
        }

        if(other.gameObject.CompareTag(TagName.GoalTag))
        {
            this._isEnd = true;

            // stateTextにGAMECLEAR表示
            this._stateText.GetComponent<Text>().text = "CLEAR!!";
        }

        if (other.gameObject.CompareTag(TagName.CoinTag))
        {
            // パーティクル再生
            GetComponent<ParticleSystem>().Play();
            Destroy(other.gameObject);

            // スコア加算
            this._score += 10;
            this._scoreText.GetComponent<Text>().text = $"Score {this._score}pt";
        }
    }
}
