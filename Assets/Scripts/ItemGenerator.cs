using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        // TODO: �����ʒu��z�񐶐�����
        // TODO: Step�Ƃ��Ĕz���i�߂�
        // TODO: ���ꐶ�����[����Function��
        // CHALLENGE: �����Ֆʂ�ۑ��A���[�h�\�ɂ���
        //      �e�����_���l��Save, Hash����v����ꍇ�͒l���Œ�n���H

        // ���̋������ƂɃA�C�e���𐶐�
        for(int i= _startPos; i < _goalPos; i += 15)
        {
            // �o���A�C�e���̒��I
            int num = Random.Range(1,11);
            if (num <= 2)
            {
                // �R�[����x�������Ɉ꒼���ɐ���
                for(float j = -1; j <= 1; j += 0.4f)
                {
                    GameObject cone=Instantiate(conePrefab);
                    cone.transform.position = new Vector3(4*j, cone.transform.position.y, i);
                }
            }
            else
            {
                // ���[�����ƂɃA�C�e���𐶐�
                for(int j = -1; j <= 1; j++)
                {
                    // �A�C�e���̎�ނ����߂�
                    var item = Random.Range(1,11);
                    // �A�C�e����u��Z���W�I�t�Z�b�g�������_���ɐݒ�
                    var offsetZ = Random.Range(-5,6);

                    // 60%: �R�C���z�u, 30%: �Ԕz�u, 10%: �����Ȃ�
                    if (1 <= item && item <= 6)
                    {
                        // �R�C���𐶐�
                        var coin = CreateInstanceInRane(coinPrefab,j,i+offsetZ);
                        //var coin = Instantiate(coinPrefab);
                        //coin.transform.position = new Vector3(_posRange*j, coin.transform.position.y, i + offsetZ);
                    }
                    else if (7 <= item && item <= 9)
                    {
                        // �Ԃ𐶐�
                        var car = CreateInstanceInRane(carPrefab, j,i+offsetZ);
                        //var car = Instantiate(carPrefab);
                        //car.transform.position = new Vector3(_posRange*j, car.transform.position.y, i + offsetZ);
                    }

                }
            }
        }
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
        
    }
}
