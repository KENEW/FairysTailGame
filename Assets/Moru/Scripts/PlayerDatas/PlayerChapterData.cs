using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public partial class PlayerData
{
    #region Const
    private const string isOpen = "isOpen";
    private const string isClear = "isClear";
    #endregion

    #region Field

    [SerializeField] private ChapterStorySO chapterStorySO;
    [ShowInInspector] private Dictionary<GAME_INDEX, int> openChapter;
    [ShowInInspector] private Dictionary<GAME_INDEX, int> clearChapter;
    #endregion

    #region Properties
    public ChapterStorySO ChapterStorySO => chapterStorySO;
    #endregion

    #region Events
    public delegate void OnChapter(GAME_INDEX index);
    public OnChapter onOpenChapter;
    public OnChapter onClearChapter;
    #endregion

    public static void Load_ChapterData()
    {

        var instance = PlayerData.instance;
        for (int i = 0; i < (int)GAME_INDEX.None; i++)
        {
            //OpenChapter;
            int value = PlayerPrefs.GetInt(((GAME_INDEX)i).ToString() + isOpen, 0);
            instance.openChapter.Add((GAME_INDEX)i, value);

            //ClearChapter;
            int value2 = PlayerPrefs.GetInt(((GAME_INDEX)i).ToString() + isClear, 0);
            instance.clearChapter.Add((GAME_INDEX)i, value2);
        }
        //ù��° é�ʹ� �׻� ����
        PlayerPrefs.SetInt(GAME_INDEX.Snow_White.ToString() + isOpen, 1);
        instance.openChapter[GAME_INDEX.Snow_White] = 1;
    }

    public static bool IsOpenChapter(GAME_INDEX index)
    {
        var instance = PlayerData.instance;
        int value = PlayerPrefs.GetInt((index).ToString() + isOpen);
        int dicValue = instance.openChapter[index];
        bool retVal = false;
        if (value == dicValue)
        {
            if (value != 0)
            {
                retVal = true;
            }
        }
        return retVal;
    }

    public static bool IsClearChapter(GAME_INDEX index)
    {
        var instance = PlayerData.instance;
        int value = PlayerPrefs.GetInt((index).ToString() + isClear, 0);
        int dicValue = instance.clearChapter[index];
        bool retVal = false;
        if (value == dicValue)
        {
            if (value != 0)
            {
                retVal = true;
            }
        }
        return retVal;
    }

    public static void CheckChapterPoint()
    {
        for (int i = 0; i < (int)GAME_INDEX.None; i++)
        {
            bool isOpen = false;
            bool isClear = true;
            var arr = GetStageClearDataPerGame((GAME_INDEX)i);
            foreach (var ele in arr)
            {
                //�ϳ��� false�� ��������
                if (!ele)
                {
                    isClear = false;
                }
                //�ϳ��� true��
                if (ele)
                {
                    isOpen = true;
                }
                Debug.Log($"{ele}");
            }
            if (isClear)
            {
                var instance = PlayerData.instance;
                instance?.OnClearChapter((GAME_INDEX)i);
            }
            if(isOpen)
            {
                var instance = PlayerData.instance;
                instance?.OnOpenChapter((GAME_INDEX)i);
            }
        }
    }


    private void OnOpenChapter(GAME_INDEX index)
    {
        PlayerPrefs.SetInt(index.ToString() + isOpen, 1);
        instance.openChapter[index] = 1;
    }
    private void OnClearChapter(GAME_INDEX index)
    {
        PlayerPrefs.SetInt(index.ToString() + isClear, 1);
        instance.clearChapter[index] = 1;
    }
}
