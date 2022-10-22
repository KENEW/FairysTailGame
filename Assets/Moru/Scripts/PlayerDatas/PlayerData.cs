using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Moru.UI;

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

namespace PD
{

    [System.Serializable]
    public partial class PlayerData
    {
        #region instance
        private static PlayerData m_instance;
        public static PlayerData instance
        {
            get
            {
                if (m_instance == null)
                {
                    int[] initValue = new int[6] { 10, 10, 10, 10, 10, 10 };
                    m_instance = new PlayerData(initValue);
                }
                return m_instance;
            }
        }
        #endregion

        #region Events
        public delegate void OnClearGame(GAME_INDEX index, int StageNum);
        /// <summary>
        /// ������ Ŭ�������� ��� ���� �ε����� �������� �ѹ��� ���ڷ� �־� Invoke
        /// </summary>
        public static OnClearGame onClearGame;

        public delegate void OnValueChange();
        public event OnValueChange onPlayerNameChange;
        public event OnValueChange onPlayerTitleChange;


        public delegate void OnSelectStage(bool[] arr, GAME_INDEX index);
        public OnSelectStage onSelectStage;

        #endregion

        #region Field
        [SerializeField] private string playerName;
        [SerializeField] private string playerTitle;

        [SerializeField] private GameObject PopUpUI;
        [ShowInInspector] private Dictionary<GAME_INDEX, Dictionary<int, int>> saveData;
        [SerializeField] private int[] stageCountPerGames;
        [SerializeField] private int curStageSeletedNum;
        #endregion

        #region Properties
        public Dictionary<GAME_INDEX, Dictionary<int, int>> SaveData => saveData;
        public string PlayerName { get { return playerName; } set { playerName = value; onPlayerNameChange?.Invoke(); } }
        public string PlayerTitle { get { return playerTitle; } set { playerTitle = value; onPlayerTitleChange?.Invoke(); } }
        public float WholeClearRate
        {
            get
            {
                return GetWholeClearRate();
            }
        }
        public int CurStageSelectedNum => curStageSeletedNum;


        #endregion


        public PlayerData(int[] stageCountPerGames)
        {
            //�������� ���� ����
            onClearGame += ClearGame;
            this.stageCountPerGames = stageCountPerGames;
            saveData = new Dictionary<GAME_INDEX, Dictionary<int, int>>();

            //���ҽ� �ε�
            PopUpUI = Resources.Load<GameObject>("AchieveCanvas");
            chapterStorySO = Resources.Load<ChapterStorySO>("ChapterStorySO");


            ///���� ����///
            isAchievement = new Dictionary<ACHEIVE_INDEX, int>();
            cur_AchievementValue = new Dictionary<ACHEIVE_INDEX, AchieveResult>();
            isGetReward = new Dictionary<ACHEIVE_INDEX, int>();
            onClearAchieve += onClearAchieveCallBack;
            onUpdateAchieve += onUpdateAchieveCallBack;


            //é�� ����
            openChapter = new Dictionary<GAME_INDEX, int>();
            clearChapter = new Dictionary<GAME_INDEX, int>();
            onOpenChapter += OnOpenChapter;
            onClearChapter += OnClearChapter;

        }

        #region Methods
        /// <summary>
        /// ������ Ŭ�����ϸ� �ݹ�˴ϴ�.
        /// </summary>
        /// <param name="index"></param>
        private void ClearGame(GAME_INDEX index, int stageNum)
        {
            //������ Ŭ���������Ƿ� UI�˾���Ű��
            PlayerPrefs.SetInt(index.ToString() + stageNum.ToString(), 1);
            saveData[index][stageNum] = 1;
            Debug.Log($"{PlayerPrefs.GetInt(index.ToString() + stageNum.ToString())} // {saveData[index][stageNum] }");
        }

        /// <summary>
        /// ������ �ʱ�ȭ�մϴ�.
        /// </summary>
        public static void Initialize_GameData()
        {
            var instacne = PlayerData.instance;
            for (int i = 0; i < (int)GAME_INDEX.None; i++)
            {
                var key = (GAME_INDEX)i;
                Dictionary<int, int> stageSaveDic = new Dictionary<int, int>();
                for (int stageNum = 0; stageNum < instance.stageCountPerGames[i]; stageNum++)
                {
                    PlayerPrefs.SetInt(key.ToString() + stageNum.ToString(), 0);
                    stageSaveDic.Add(stageNum, 0);
                }
                instacne.saveData[(GAME_INDEX)i] = stageSaveDic;
            }
        }

        /// <summary>
        /// �÷��̾� ���Ӻ� �������� �����͸� �ҷ��ɴϴ�.
        /// </summary>
        public static void Load_GameData()
        {
            var instacne = PlayerData.instance;
            for (int i = 0; i < (int)GAME_INDEX.None; i++)
            {
                var key = (GAME_INDEX)i;
                Dictionary<int, int> stageSaveDic = new Dictionary<int, int>();
                for (int stageNum = 0; stageNum < instacne.stageCountPerGames[i]; stageNum++)
                {
                    if (
                        !PlayerPrefs.HasKey(key.ToString() + stageNum.ToString())
                       )
                    {
                        PlayerPrefs.SetInt(key.ToString() + stageNum.ToString(), 0);
                        stageSaveDic.Add(stageNum, 0);
                    }
                    else
                    {
                        int value = PlayerPrefs.GetInt(key.ToString() + stageNum.ToString());
                        stageSaveDic.Add(stageNum, value);
                    }
                }
                instacne.saveData.Add((GAME_INDEX)i, stageSaveDic);
            }
        }

        /// <summary>
        /// �ش���ӿ��� �ش� ���������� Ŭ���� ���θ� �޾ƿɴϴ�.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="stageNum"></param>
        public static bool GetStageClearDataPerGame(GAME_INDEX index, int stageNum)
        {
            var instacne = PlayerData.instance;
            int value = instance.saveData[index][stageNum];
            bool retVal = false;
            if (value > 0)
            {
                retVal = true;
            }
            else
            {
                retVal = false;
            }
            return retVal;
        }

        /// <summary>
        /// �ش� ������ ��� ���������� Ŭ���� ���θ� �迭�� �޾ƿɴϴ�.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool[] GetStageClearDataPerGame(GAME_INDEX index)
        {
            var instacne = PlayerData.instance;
            int count = instacne.saveData[index].Count;
            bool[] retVal = new bool[count];
            for (int i = 0; i < retVal.Length; i++)
            {
                int value = instance.saveData[index][i];
                if (value > 0)
                {
                    retVal[i] = true;
                }
                else
                {
                    retVal[i] = false;
                }
            }
            return retVal;
        }

        public static float GetGameIndexClearRate(GAME_INDEX index)
        {
            var arr = GetStageClearDataPerGame(index);
            int rate = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i])
                {
                    rate++;
                }
            }
            float retVal = (float)rate / (float)arr.Length;
            return retVal;
        }

        private static float GetWholeClearRate()
        {
            float stageCount = 0;
            float clearCount = 0;
            for (int i = 0; i < (int)GAME_INDEX.None; i++)
            {
                var arr = GetStageClearDataPerGame((GAME_INDEX)i);
                stageCount += arr.Length;
                for (int j = 0; j < arr.Length; j++)
                {
                    if (arr[j])
                    {
                        clearCount++;
                    }
                }
            }
            float retVal = clearCount / stageCount;
            return retVal;
        }
        #endregion
    }
}