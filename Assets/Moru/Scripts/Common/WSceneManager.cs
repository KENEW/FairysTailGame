using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Moru.UI;
using PD;

public enum SceneIndex
{
    MainPage = 0,
    Snow_White,         //�鼳����
    Cinderella,         //�ŵ�����
    Pinocchio,          //�ǳ�Ű��
    Little_Mermaid,     //�ξ����
    Jack_And_Beanstalk, //��� �ᳪ��
    Tree_Little_Pigs,    //�Ʊ� ���� ������
    None

}

public class WSceneManager : MonoBehaviour
{
    private static WSceneManager m_Instacne;
    public static WSceneManager instance
    {
        get
        {
            if (m_Instacne == null)
            {
                m_Instacne = FindObjectOfType<WSceneManager>(true);
            }
            if (m_Instacne == null)
            {
                var obj = new GameObject(typeof(WSceneManager).Name);
                var comp = obj.AddComponent<WSceneManager>();
                DontDestroyOnLoad(comp.gameObject);
                m_Instacne = comp;
                
            }
            return m_Instacne;
        }
    }

    /// <summary>
    /// ���� ��ε��մϴ�.
    /// </summary>
    public void ReLoad_ThisScene()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        MoveScene(index);
    }
    /// <summary>
    /// ���� ��ε��ϸ� ���������� ������ŵ�ϴ�.
    /// </summary>
    public void ReLoad_ThisScene_WtihStageUp()
    {
        PD.PlayerData.instance.CurStageSelectedNum++;
        int index = SceneManager.GetActiveScene().buildIndex;
        MoveScene(index);
    }


    public void MoveScene(SceneIndex index)
    {
        SceneManager.LoadScene((int)index);
    }
    public void MoveScene(int index)
    {
        if (index >= (int)SceneIndex.MainPage && index < (int)SceneIndex.None)
        {
            SceneManager.LoadScene(index);
        }
        else
        {
            Debug.Log($"{index} : ��ȿ���� ���� �ε���, ��ȿ���� �־��ּ���. \n" +
                $"�ε��� ���� : {(int)SceneIndex.MainPage } ~ {(int)SceneIndex.None - 1}");
            return;
        }
    }

    public void MoveChapterSelectPage()
    {
        MoveScene(SceneIndex.MainPage);
        StackUIManager.onLoadEvent = new StackUIManager.OnLoadEvent();
        StackUIManager.onLoadEvent.AddListener(
            () =>
            {
                StackUIManager.GoToTargetUIComponent(StackUIManager.ChapterPage);
            }
            );
    }
    public void MoveStageSeletedPage()
    {
        MoveScene(SceneIndex.MainPage);
        StackUIManager.onLoadEvent = new StackUIManager.OnLoadEvent();
        StackUIManager.onLoadEvent.AddListener(
            () =>
            {
                StackUIManager.GoToTargetUIComponent(StackUIManager.LobbyPage);
                var boolsArr = PlayerData.GetStageClearDataPerGame(PlayerData.instance.Cur_Game_Index);
                Debug.Log($"���࿩�� Ȯ��");
                //StackUIManager.LobbyPage.GetComponent<LevelSelectUI>().SetUp(boolsArr, PlayerData.instance.Cur_Game_Index);
            }
            );
    }
}
