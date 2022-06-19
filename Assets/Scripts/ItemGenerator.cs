using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 障害物とアイテムの生成を行うクラス
/// </summary>
public class ItemGenerator : MonoBehaviour
{
    [SerializeField] GameObject carPrefab;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] GameObject conePrefab;

    // スタート地点
    private int _startPos = 80;
    // ゴール地点
    private int _goalPos = 360;
    // アイテムを出すx方向の範囲
    private float _posRange = 3.4f;

    // Start is called before the first frame update
    void Start()
    {
        // TODO: 生成位置を配列生成する
        // TODO: Stepとして配列を進める
        // TODO: 特殊生成ルールはFunction化
        // CHALLENGE: 生成盤面を保存、ロード可能にする

        // 一定の距離ごとにアイテムを生成
        for(int i= _startPos; i < _goalPos; i += 15)
        {
            // 出現アイテムの抽選
            int num = Random.Range(1,11);
            if (num <= 2)
            {
                // コーンをx軸方向に一直線に生成
                for(float j = -1; j <= 1; j += 0.4f)
                {
                    GameObject cone=Instantiate(conePrefab);
                    cone.transform.position = new Vector3(4*j, cone.transform.position.y, i);
                }
            }
            else
            {
                // レーンごとにアイテムを生成
                for(int j = -1; j <= 1; j++)
                {
                    // アイテムの種類を決める

                    /// 途中！！！
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
