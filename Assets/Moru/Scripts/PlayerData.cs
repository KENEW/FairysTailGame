using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GAME_INDEX
{
    Snow_White,         //�鼳����
    Cinderella,         //�ŵ�����
    Pinocchio,          //�ǳ�Ű��
    Little_Mermaid,     //�ξ����
    Jack_And_Beanstalk, //��� �ᳪ��
    Tree_Little_Pigs,    //�Ʊ� ���� ������
    None
}

[System.Serializable]
public class PlayerData
{
    #region instance
    private static PlayerData m_instance;
    public static PlayerData instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = new PlayerData();
            }
            return m_instance;
        }
    }
    #endregion

    #region Events
    public delegate void OnClearGame(GAME_INDEX index);
    public static OnClearGame onClearGame;
    #endregion

    #region Field
    [SerializeField] private GameObject PopUpUI;
    [SerializeField] private Dictionary<GAME_INDEX, int> saveData;
    #endregion

    #region Properties
    public Dictionary<GAME_INDEX, int> SaveData => saveData;
    #endregion


    public PlayerData()
    {
        onClearGame += ClearGame;
        saveData = new Dictionary<GAME_INDEX, int>();
    }

    #region Methods
    /// <summary>
    /// ������ Ŭ�����ϸ� �ݹ�˴ϴ�.
    /// </summary>
    /// <param name="index"></param>
    private void ClearGame(GAME_INDEX index)
    {
        //������ Ŭ���������Ƿ� UI�˾���Ű��
        PlayerPrefs.SetInt(index.ToString(), 1);
        saveData[index] = 1;
        Debug.Log($"{PlayerPrefs.GetInt(index.ToString())} // {saveData[index] }");
    }

    /// <summary>
    /// ������ �ʱ�ȭ�մϴ�.
    /// </summary>
    public void Initialize_GameData()
    {
        for (int i = 0; i < (int)GAME_INDEX.None; i++)
        {
            var key = (GAME_INDEX)i;
            PlayerPrefs.SetInt(key.ToString(), 0);
            saveData.Add((GAME_INDEX)i, 0);
        }
    }

    /// <summary>
    /// �÷��̾� ���ӵ����͸� �ҷ��ɴϴ�.
    /// </summary>
    public void Load_GameData()
    {
        for (int i = 0; i < (int)GAME_INDEX.None; i++)
        {
            var key = (GAME_INDEX)i;
            if (
                !PlayerPrefs.HasKey(key.ToString())
               )
            {
                PlayerPrefs.SetInt(key.ToString(), 0);
                saveData.Add((GAME_INDEX)i, 0);
            }

            else
            {
                int value = PlayerPrefs.GetInt(key.ToString());
                saveData.Add((GAME_INDEX)i, value);
            }
        }
    }
    #endregion
}
