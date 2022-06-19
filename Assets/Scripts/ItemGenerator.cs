using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    // 生成位置のステップカウント
    private int _step = 0;

    // ユニティちゃんGameObject
    GameObject unitychanObj;

    private List<GameObject> _objList;


    // Start is called before the first frame update
    void Start()
    {
        // クラスで生成したオブジェクトを格納するリスト
        _objList = new List<GameObject>();

        // ユニティちゃんを取得
        unitychanObj = GameObject.FindGameObjectWithTag(TagName.Player);

    //    // 一定の距離ごとにアイテムを生成
    //    for (int i = _startPos; i < _goalPos; i += 15)
    //    {
    //        // 出現アイテムの抽選
    //        int num = Random.Range(1, 11);
    //    if (num <= 2)
    //    {
    //        // コーンをx軸方向に一直線に生成
    //        for (float j = -1; j <= 1; j += 0.4f)
    //        {
    //            GameObject cone = Instantiate(conePrefab);
    //            cone.transform.position = new Vector3(4 * j, cone.transform.position.y, i);
    //            objList.Add(cone);
    //        }
    //    }
    //    else
    //    {
    //        // レーンごとにアイテムを生成
    //        for (int j = -1; j <= 1; j++)
    //        {
    //            // アイテムの種類を決める
    //            var item = Random.Range(1, 11);
    //            // アイテムを置くZ座標オフセットをランダムに設定
    //            var offsetZ = Random.Range(-5, 6);

    //            // 60%: コイン配置, 30%: 車配置, 10%: 何もなし
    //            if (1 <= item && item <= 6)
    //            {
    //                // コインを生成
    //                var coin = CreateInstanceInRane(coinPrefab, j, i + offsetZ);
    //                objList.Add(coin);
    //                //var coin = Instantiate(coinPrefab);
    //                //coin.transform.position = new Vector3(_posRange*j, coin.transform.position.y, i + offsetZ);
    //            }
    //            else if (7 <= item && item <= 9)
    //            {
    //                // 車を生成
    //                var car = CreateInstanceInRane(carPrefab, j, i + offsetZ);
    //                objList.Add(car);
    //                //var car = Instantiate(carPrefab);
    //                //car.transform.position = new Vector3(_posRange*j, car.transform.position.y, i + offsetZ);
    //            }

    //        }
    //    }
    //}
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
        // ユニティちゃんのZ座標を取得
        var unitychanPosZ = unitychanObj.transform.position.z;

        // nextアイテム生成位置
        var itemPos = _startPos + _step * 15;

        if (_goalPos > itemPos)
        {
            // ユニティちゃんの前方範囲に生成位置が含まれる場合
            if (unitychanPosZ + 50f >= itemPos)
            {
                ItemRoulet(itemPos);

                // ステップカウントを上昇
                _step += 1;
            }
        }

        // nullでない かつ 空ではない
        if (_objList?.Count >0)
        {
            // 要素0を消しに行くのでnullチェックが必要
            if(_objList[0]!= null)
            {
                // 本スクリプト対象で、ユニティちゃんのZ座標より後ろ(マージン10f)のオブジェクトは廃棄
                if (_objList[0].transform.position.z < unitychanPosZ - 10f)
                {
                    // オブジェクトの削除
                    Destroy(_objList[0]);
                    if (_objList.Count > 1)
                    {
                        // リスト要素の削除
                        _objList.RemoveAt(0);
                    }
                    // リスト要素が残り1つの場合
                    else if (_objList.Count == 1)
                    {
                        _objList[0] = null;
                    }
                }
            }

            //// List要素のDestroyを行うのでNullチェック
            //if (objList[0] != null)
            //{
            //    // 本スクリプト対象で、ユニティちゃんのZ座標より小さい(マージン10f)オブジェクトは廃棄
            //    if (objList[0].transform.position.z < unitychanPosZ - 10f)
            //    {
            //        // オブジェクトの削除
            //        Destroy(objList[0]);
            //        if (objList.Count > 1)
            //        {
            //            // リスト要素の削除
            //            objList.RemoveAt(0);
            //        }
            //        else if (objList.Count == 1)
            //        {
            //            objList[0] = null;
            //        }
            //    }
            //}
            //// 要素0が正しく消せなかった場合のフォローアップ
            //else if (objList.Count > 1)
            //{
            //    objList.RemoveAt(0);
            //}
        }
        // null または 空である
        // null　かつ空ではない場合(要素0削除失敗のフォローアップ)
        else if (_objList.Count > 1)
        {
            _objList.RemoveAt(0);
        }
    }

    void ItemRoulet(int distance)
    {
        // 出現アイテムの抽選
        int num = Random.Range(1, 11);
        if (num <= 2)
        {
            // コーンをx軸方向に一直線に生成
            for (float j = -1; j <= 1; j += 0.4f)
            {
                GameObject cone = Instantiate(conePrefab);
                cone.transform.position = new Vector3(4 * j, cone.transform.position.y, distance);
                _objList.Add(cone);
            }
        }
        else
        {
            // レーンごとにアイテムを生成
            for (int j = -1; j <= 1; j++)
            {
                // アイテムの種類を決める
                var item = Random.Range(1, 11);
                // アイテムを置くZ座標オフセットをランダムに設定
                var offsetZ = Random.Range(-5, 6);

                // 60%: コイン配置, 30%: 車配置, 10%: 何もなし
                if (1 <= item && item <= 6)
                {
                    // コインを生成
                    var coin = CreateInstanceInRane(coinPrefab, j, distance + offsetZ);
                    _objList.Add(coin);
                }
                else if (7 <= item && item <= 9)
                {
                    // 車を生成
                    var car = CreateInstanceInRane(carPrefab, j, distance + offsetZ);
                    _objList.Add(car);
                }

            }
        }

    }
}
