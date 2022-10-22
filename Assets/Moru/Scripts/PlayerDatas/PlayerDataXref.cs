using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PD;

public class PlayerDataXref
{
    #region instance
    private static PlayerDataXref m_instance;
    public static PlayerDataXref instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = new PlayerDataXref();
            }
            return m_instance;
        }
    }
    #endregion

    #region Methods
    /// <summary>
    /// �ش� ������ ���������� Ŭ�����մϴ�.
    /// </summary>
    /// <param name="index"></param>
    /// <param name="stageNum"></param>
    public void ClearGame(GAME_INDEX index, int stageNum)
    {
        PlayerData.onClearGame(index, stageNum);
    }

    /// <summary>
    /// �÷��̾ ���� ������ �������� �ѹ��� �޾ƿɴϴ�.
    /// </summary>
    /// <returns></returns>
    public int GetStageNum()
    {
        return PlayerData.instance.CurStageSelectedNum;
    }

    /// <summary>
    /// �ش� é�͸� ���ϴ�. (�ر��մϴ�. Ŭ��� �ƴմϴ�.)
    /// </summary>
    /// <param name="index"></param>
    public void OpenChapter(GAME_INDEX index)
    {
        PlayerData.instance.onOpenChapter?.Invoke(index);
    }

    /// <summary>
    /// �ش� é�͸� Ŭ�����մϴ�.
    /// </summary>
    /// <param name="index"></param>
    public void ClearChapter(GAME_INDEX index)
    {
        PlayerData.instance.onClearChapter?.Invoke(index);
    }


    /// <summary>
    /// �ش� ������ Ŭ������ѹ����ϴ�..
    /// </summary>
    /// <param name="index"></param>
    public void SetAchieveSuccess(ACHEIVE_INDEX index)
    {
        PlayerData.onClearAchieve(index);
    }

    /// <summary>
    /// �ش������ ���簪�� addValue��ŭ ���մϴ�.
    /// </summary>
    /// <param name="index"></param>
    /// <param name="addValue"></param>
    public void AddAchievementValue(ACHEIVE_INDEX index, int addValue)
    {
        PlayerData.onUpdateAchieve(index, addValue);
    }



    #endregion
}
