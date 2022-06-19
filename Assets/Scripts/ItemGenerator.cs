using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��Q���ƃA�C�e���̐������s���N���X
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

                    /// �r���I�I�I
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
