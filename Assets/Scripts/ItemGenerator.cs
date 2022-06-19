using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// ��Q���ƃA�C�e���̐������s���N���X
/// �A�C�e���������_���z�u
/// �R�[���͈�Ĕz�u�̓���p�^�[���Ƃ��Ď���
/// �z�u��Z���W�̓��Ԋu�Ń`�F�b�N
/// ���ۂ̐����ꏊ�͂������炳��Ƀ����_����Offset
/// �e���[���Ŕ�����s��
///
/// [�����䗦]�@
/// 20% : �R�[���z�u(����p�^�[��)
/// 80% : �A�C�e���z�u(���������܂�)
///   [����]
///   60% : �R�C���z�u
///   30% : �Ԕz�u
///   10% : ��������
/// </summary>
public class ItemGenerator : MonoBehaviour
{
    [SerializeField] GameObject carPrefab;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] GameObject conePrefab;

    // �X�^�[�g�n�_
    private int _startPos = 80;
    // �S�[���n�_
    private int _goalPos = 360;
    // �A�C�e�����o��x�����͈̔�
    private float _posRange = 3.4f;

    // �����ʒu�̃X�e�b�v�J�E���g
    private int _step = 0;

    // ���j�e�B�����GameObject
    GameObject unitychanObj;

    private List<GameObject> _objList;


    // Start is called before the first frame update
    void Start()
    {
        // �N���X�Ő��������I�u�W�F�N�g���i�[���郊�X�g
        _objList = new List<GameObject>();

        // ���j�e�B�������擾
        unitychanObj = GameObject.FindGameObjectWithTag(TagName.Player);

    //    // ���̋������ƂɃA�C�e���𐶐�
    //    for (int i = _startPos; i < _goalPos; i += 15)
    //    {
    //        // �o���A�C�e���̒��I
    //        int num = Random.Range(1, 11);
    //    if (num <= 2)
    //    {
    //        // �R�[����x�������Ɉ꒼���ɐ���
    //        for (float j = -1; j <= 1; j += 0.4f)
    //        {
    //            GameObject cone = Instantiate(conePrefab);
    //            cone.transform.position = new Vector3(4 * j, cone.transform.position.y, i);
    //            objList.Add(cone);
    //        }
    //    }
    //    else
    //    {
    //        // ���[�����ƂɃA�C�e���𐶐�
    //        for (int j = -1; j <= 1; j++)
    //        {
    //            // �A�C�e���̎�ނ����߂�
    //            var item = Random.Range(1, 11);
    //            // �A�C�e����u��Z���W�I�t�Z�b�g�������_���ɐݒ�
    //            var offsetZ = Random.Range(-5, 6);

    //            // 60%: �R�C���z�u, 30%: �Ԕz�u, 10%: �����Ȃ�
    //            if (1 <= item && item <= 6)
    //            {
    //                // �R�C���𐶐�
    //                var coin = CreateInstanceInRane(coinPrefab, j, i + offsetZ);
    //                objList.Add(coin);
    //                //var coin = Instantiate(coinPrefab);
    //                //coin.transform.position = new Vector3(_posRange*j, coin.transform.position.y, i + offsetZ);
    //            }
    //            else if (7 <= item && item <= 9)
    //            {
    //                // �Ԃ𐶐�
    //                var car = CreateInstanceInRane(carPrefab, j, i + offsetZ);
    //                objList.Add(car);
    //                //var car = Instantiate(carPrefab);
    //                //car.transform.position = new Vector3(_posRange*j, car.transform.position.y, i + offsetZ);
    //            }

    //        }
    //    }
    //}
    }

    // Prefab�����[����ɐ���
    protected GameObject CreateInstanceInRane(GameObject prefab, int pos_rane, int pos_z)
    {
        var pref = Instantiate(prefab);
        pref.transform.position = new Vector3(_posRange * pos_rane, pref.transform.position.y, pos_z);
        return pref;
    }

    // Update is called once per frame
    void Update()
    {
        // ���j�e�B������Z���W���擾
        var unitychanPosZ = unitychanObj.transform.position.z;

        // next�A�C�e�������ʒu
        var itemPos = _startPos + _step * 15;

        if (_goalPos > itemPos)
        {
            // ���j�e�B�����̑O���͈͂ɐ����ʒu���܂܂��ꍇ
            if (unitychanPosZ + 50f >= itemPos)
            {
                ItemRoulet(itemPos);

                // �X�e�b�v�J�E���g���㏸
                _step += 1;
            }
        }

        // null�łȂ� ���� ��ł͂Ȃ�
        if (_objList?.Count >0)
        {
            // �v�f0�������ɍs���̂�null�`�F�b�N���K�v
            if(_objList[0]!= null)
            {
                // �{�X�N���v�g�ΏۂŁA���j�e�B������Z���W�����(�}�[�W��10f)�̃I�u�W�F�N�g�͔p��
                if (_objList[0].transform.position.z < unitychanPosZ - 10f)
                {
                    // �I�u�W�F�N�g�̍폜
                    Destroy(_objList[0]);
                    if (_objList.Count > 1)
                    {
                        // ���X�g�v�f�̍폜
                        _objList.RemoveAt(0);
                    }
                    // ���X�g�v�f���c��1�̏ꍇ
                    else if (_objList.Count == 1)
                    {
                        _objList[0] = null;
                    }
                }
            }

            //// List�v�f��Destroy���s���̂�Null�`�F�b�N
            //if (objList[0] != null)
            //{
            //    // �{�X�N���v�g�ΏۂŁA���j�e�B������Z���W��菬����(�}�[�W��10f)�I�u�W�F�N�g�͔p��
            //    if (objList[0].transform.position.z < unitychanPosZ - 10f)
            //    {
            //        // �I�u�W�F�N�g�̍폜
            //        Destroy(objList[0]);
            //        if (objList.Count > 1)
            //        {
            //            // ���X�g�v�f�̍폜
            //            objList.RemoveAt(0);
            //        }
            //        else if (objList.Count == 1)
            //        {
            //            objList[0] = null;
            //        }
            //    }
            //}
            //// �v�f0�������������Ȃ������ꍇ�̃t�H���[�A�b�v
            //else if (objList.Count > 1)
            //{
            //    objList.RemoveAt(0);
            //}
        }
        // null �܂��� ��ł���
        // null�@����ł͂Ȃ��ꍇ(�v�f0�폜���s�̃t�H���[�A�b�v)
        else if (_objList.Count > 1)
        {
            _objList.RemoveAt(0);
        }
    }

    void ItemRoulet(int distance)
    {
        // �o���A�C�e���̒��I
        int num = Random.Range(1, 11);
        if (num <= 2)
        {
            // �R�[����x�������Ɉ꒼���ɐ���
            for (float j = -1; j <= 1; j += 0.4f)
            {
                GameObject cone = Instantiate(conePrefab);
                cone.transform.position = new Vector3(4 * j, cone.transform.position.y, distance);
                _objList.Add(cone);
            }
        }
        else
        {
            // ���[�����ƂɃA�C�e���𐶐�
            for (int j = -1; j <= 1; j++)
            {
                // �A�C�e���̎�ނ����߂�
                var item = Random.Range(1, 11);
                // �A�C�e����u��Z���W�I�t�Z�b�g�������_���ɐݒ�
                var offsetZ = Random.Range(-5, 6);

                // 60%: �R�C���z�u, 30%: �Ԕz�u, 10%: �����Ȃ�
                if (1 <= item && item <= 6)
                {
                    // �R�C���𐶐�
                    var coin = CreateInstanceInRane(coinPrefab, j, distance + offsetZ);
                    _objList.Add(coin);
                }
                else if (7 <= item && item <= 9)
                {
                    // �Ԃ𐶐�
                    var car = CreateInstanceInRane(carPrefab, j, distance + offsetZ);
                    _objList.Add(car);
                }

            }
        }

    }
}
