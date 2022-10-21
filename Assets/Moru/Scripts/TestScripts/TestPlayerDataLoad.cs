using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerDataLoad : MonoBehaviour
{
    public PlayerData data;
    // Start is called before the first frame update
    void Start()
    {
        data = PlayerData.instance;
        PlayerData.Load_GameData();
        PlayerData.Load_PlayerAchieve();
    }
    public void TestAchieve()
    {
        //���� �ý����� ��� ������Ʈ ��������Ʈ �޼���
        //(��ǥġ�� �����ϸ� �ڵ����� ������ �޼��˴ϴ�.
        PlayerData.onUpdateAchieve(ACHEIVE_INDEX.TEST_ACHIEVE, 5);
    }
    public void TestClear()
    {
        PlayerData.onClearAchieve(ACHEIVE_INDEX.ALL_CLEAR);
    }
    

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //�����
            //�ŵ����� ���Ӹ�忡�� 0��° ���������� Ŭ�����߽��ϴ�!
            PlayerData.onClearGame?.Invoke(GAME_INDEX.Cinderella, 0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //�����
            //��� �ᳪ�� ���Ӹ�忡�� 1��° ���������� Ŭ�����߽��ϴ�!
            PlayerData.onClearGame?.Invoke(GAME_INDEX.Jack_And_Beanstalk, 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //�����
            //�ξ���� ���Ӹ�忡�� 2��° ���������� Ŭ�����߽��ϴ�!
            PlayerData.onClearGame?.Invoke(GAME_INDEX.Little_Mermaid, 2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            //�����
            //�ŵ����� ���Ӹ�忡�� 3��° ���������� Ŭ�����߽��ϴ�!
            PlayerData.onClearGame?.Invoke(GAME_INDEX.Cinderella, 3);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //�ŵ����� ���Ӹ���� ��� ���������� Ŭ���� ���θ� �޾ƿɴϴ�.
            var value = PlayerData.GetStageClearDataPerGame(GAME_INDEX.Cinderella);
            for (int i = 0; i < value.Length; i++)
            {
                Debug.Log($"{value[i]}");
            }
            //��� �ᳪ�� ���Ӹ���� 0��° ���������� Ŭ���� ���θ� �޾ƿɴϴ�.
            bool result = PlayerData.GetStageClearDataPerGame(GAME_INDEX.Cinderella, 0);
            Debug.Log($"{result}");
        
        }
    }
}
