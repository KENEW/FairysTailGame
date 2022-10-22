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
    /// �÷��̾ ���� ������ ���Ӱ� �������� �ѹ��� �޾ƿɴϴ�.
    /// </summary>
    /// <returns></returns>
    public (GAME_INDEX index, int StageNum) GetCurrentStage()
    {
        var index = PlayerData.instance.Cur_Game_Index;
        var stage = PlayerData.instance.CurStageSelectedNum;
        return (index, stage);
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
