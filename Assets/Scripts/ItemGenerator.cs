using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 障害物とアイテムの生成を行うクラス
/// アイテムをランダム配置
/// コーンは一斉配置の特殊パターンとして実装
/// 配置はZ座標の等間隔でチェック
/// 実際の生成場所はそこからさらにランダムでOffset
/// 各レーンで判定を行う
///
/// [生成比率]　
/// 20% : コーン配置(特殊パターン)
/// 80% : アイテム配置(生成無し含む)
///   [内訳]
///   60% : コイン配置
///   30% : 車配置
///   10% : 生成無し
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
        //      各ランダム値をSave, Hashが一致する場合は値を固定渡し？

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
                    var item = Random.Range(1,11);
                    // アイテムを置くZ座標オフセットをランダムに設定
                    var offsetZ = Random.Range(-5,6);

                    // 60%: コイン配置, 30%: 車配置, 10%: 何もなし
                    if (1 <= item && item <= 6)
                    {
                        // コインを生成
                        var coin = CreateInstanceInRane(coinPrefab,j,i+offsetZ);
                        //var coin = Instantiate(coinPrefab);
                        //coin.transform.position = new Vector3(_posRange*j, coin.transform.position.y, i + offsetZ);
                    }
                    else if (7 <= item && item <= 9)
                    {
                        // 車を生成
                        var car = CreateInstanceInRane(carPrefab, j,i+offsetZ);
                        //var car = Instantiate(carPrefab);
                        //car.transform.position = new Vector3(_posRange*j, car.transform.position.y, i + offsetZ);
                    }

                }
            }
        }
    }

    // Prefabをレーン上に生成
    protected GameObject CreateInstanceInRane(GameObject prefab, int pos_rane, int pos_z)
    {
        var pref = Instantiate(prefab);
        pref.transform.position = new Vector3(_posRange * pos_rane, pref.transform.position.y, pos_z);
        return pref;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
