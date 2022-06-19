using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ユニティちゃんを追いかけるカメラスクリプト
/// </summary>

public class MyCameraController : MonoBehaviour
{
    private GameObject _unityChan;
    private float _difference;

    // Start is called before the first frame update
    void Start()
    {
        this._unityChan = GameObject.Find("unitychan");

        // ユニティちゃんとカメラのZ座標の差を求める
        this._difference = _unityChan.transform.position.z - this.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(0,this.transform.position.y,this._unityChan.transform.position.z - _difference);
    }
}
