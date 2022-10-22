using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public enum ACHEIVE_INDEX
{
    /// <summary>
    /// ��� ���ӵ��� ��� ���������� Ŭ������ ��� Ŭ�����Դϴ�.
    /// </summary>
    ALL_CLEAR,
    /// <summary>
    /// �׽�Ʈ������ �ϳ� ����
    /// </summary>
    TEST_ACHIEVE,
    Test2,
    /// <summary>
    /// ���� ������ �ε��� ��ȣ�Դϴ�.
    /// </summary>
    NONE,

}

public partial class PlayerData
{
    #region Const
    private const string isAchieve = "isAchieve";
    private const string isReward = "isReward";
    private const string curValue = "cur_Value";
    private const string maxValue = "max_Value";
    #endregion

    #region Field
    [SerializeField] private AchieveAndTitle achieveAndTitle;
    [ShowInInspector] private Dictionary<ACHEIVE_INDEX, int> isAchievement;
    [ShowInInspector] private Dictionary<ACHEIVE_INDEX, AchieveResult> cur_AchievementValue;
    [ShowInInspector] private Dictionary<ACHEIVE_INDEX, int> isGetReward;

    #endregion

    #region Events
    public delegate void OnClearAchieve(ACHEIVE_INDEX index);
    public static OnClearAchieve onClearAchieve;
    public static OnClearAchieve onGetReward;

    public delegate void OnUpdateAchieve(ACHEIVE_INDEX index, int value);
    public static OnUpdateAchieve onUpdateAchieve;

    

    #endregion

    #region Properties
    public Dictionary<ACHEIVE_INDEX, int> IsAchievement { get => isAchievement; }
    public Dictionary<ACHEIVE_INDEX, AchieveResult> Cur_AchievementValue { get => cur_AchievementValue; }
    public Dictionary<ACHEIVE_INDEX, int> IsGetReward { get => isGetReward; }
    #endregion


    #region Methods

    public static void Load_PlayerAchieve()
    {
        var instance = PlayerData.instance;
        instance.achieveAndTitle = Resources.Load<AchieveAndTitle>("AchieveTitle");
        var resultList = instance.achieveAndTitle.AchieveResults;
        for (int i = 0; i < resultList.Count; i++)
        {
            //isAchievement üũ
            if(!PlayerPrefs.HasKey(
                resultList[i].MyIndex.ToString()+ isAchieve)
                )
            {
                PlayerPrefs.SetInt(
                    resultList[i].MyIndex.ToString() + isAchieve, 0
                    );
                instance.isAchievement.Add(resultList[i].MyIndex, 0);
            }
            else
            {
                int value2 = PlayerPrefs.GetInt(
                    resultList[i].MyIndex.ToString() + isAchieve
                    );
                instance.isAchievement.Add(resultList[i].MyIndex, value2);
            }

            //���� ��� üũ
            instance.cur_AchievementValue.Add(resultList[i].MyIndex, resultList[i]);
            var resultList_Value = resultList[i].Cur_AchievementCondition;
            resultList_Value = PlayerPrefs.GetInt(resultList[i].MyIndex.ToString() + curValue, 0);


            //���� ���� üũ
            int value = PlayerPrefs.GetInt(resultList[i].MyIndex.ToString() + isReward, 0);
            instance.isGetReward.Add(resultList[i].MyIndex, value);
        }
    }

    /// <summary>
    /// ������ ����� ������Ʈ�����ִ� �ݹ�޼���
    /// </summary>
    /// <param name="index"></param>
    /// <param name="value"></param>
    private void onUpdateAchieveCallBack(ACHEIVE_INDEX index, int value)
    {
        AchieveResult result = cur_AchievementValue[index];
        result.Cur_AchievementCondition += value;
        cur_AchievementValue[index] = result;
        var comp = cur_AchievementValue[index];
        var curValue = comp.Cur_AchievementCondition;

        PlayerPrefs.SetInt(index.ToString() + curValue, comp.Cur_AchievementCondition);

        if(comp.Cur_AchievementCondition >= comp.Target_AchievementCondition)
        {
            result.Cur_AchievementCondition = result.Target_AchievementCondition;
            cur_AchievementValue[index] = result;
            PlayerPrefs.SetInt(index.ToString() + curValue, result.Cur_AchievementCondition);
            onClearAchieve?.Invoke(index);
        }
        else
        {
            //���� �������� ����
        }

        Debug.Log($"���� ���൵ : ��ųʸ� = {result.Cur_AchievementCondition} / {result.Target_AchievementCondition}" +
            $"\n�÷��̾� ������ = {PlayerPrefs.GetInt(index.ToString() + curValue)}");
    }

    private void onClearAchieveCallBack(ACHEIVE_INDEX index)
    {
        isAchievement[index] = 1;
        PlayerPrefs.SetInt(index.ToString() + isAchieve, 1);
        //������ �޼��߽��ϴ� �˾�����
        if(PopUpUI != null)
        {
            var obj = MonoBehaviour.Instantiate(PopUpUI);
            obj.GetComponent<AchievePopUp>().SetViewer(cur_AchievementValue[index]);
        }
        //

        Debug.Log($"���� �޼���Ȳ : {cur_AchievementValue[index].AchieveName} ������ Ŭ������ {cur_AchievementValue[index].Title} Īȣ�� ȹ���ߴ�!" +
            $"\n��ųʸ� = {isAchievement[index]}" +
    $"\n�÷��̾� ������ = {PlayerPrefs.GetInt(index.ToString() + isAchieve)}");
    }

    public void OnGetReward(ACHEIVE_INDEX index)
    {
        PlayerPrefs.SetInt(index.ToString() + isReward, 1);
        instance.isGetReward[index] = 1;
        onGetReward?.Invoke(index);
    }
    #endregion
}
