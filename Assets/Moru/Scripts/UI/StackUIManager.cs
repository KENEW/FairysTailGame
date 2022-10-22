using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;

public class StackUIManager : SingleToneMono<StackUIManager>
{
    [ShowInInspector] private Stack<StackUIComponent> ui_Stack = new Stack<StackUIComponent>();
    public StackUIComponent cur_UIComponent;
    public StackUIComponent start_UIComponent;
    public StackUIComponent close_UIComponent;
    public delegate void StackUI_Event(StackUIComponent stackUI);
    public event StackUI_Event pop_n_Push_Event;

    public PlayerData playerData;

    protected override void Awake()
    {
        base.Awake();
        if (ui_Stack == null)
        { ui_Stack = new Stack<StackUIComponent>(); }
        var UIComps = FindObjectsOfType<StackUIComponent>(true);
        foreach (var uicomps in UIComps)
        {
            uicomps.Hide();
        }
        start_UIComponent?.Show();

        //플레이어 데이터 로드
        playerData = PlayerData.instance;
        PlayerData.Load_GameData();             //플레이어의 각 챕터별 스테이지 정보 리드
        PlayerData.Load_PlayerAchieve();        //플레이어의 업적 정보 리드
        PlayerData.Load_ChapterData();           //플레이어의 챕터정보 리드


        //테스트셋팅
        //playerData.SaveData[GAME_INDEX.Cinderella][0] = 1;
        //playerData.SaveData[GAME_INDEX.Jack_And_Beanstalk][0] = 1;
        //playerData.SaveData[GAME_INDEX.Jack_And_Beanstalk][1] = 1;
        //playerData.SaveData[GAME_INDEX.Jack_And_Beanstalk][2] = 1;
        //playerData.SaveData[GAME_INDEX.Jack_And_Beanstalk][3] = 1;
        //playerData.SaveData[GAME_INDEX.Little_Mermaid][0] = 1;
        //playerData.SaveData[GAME_INDEX.Pinocchio][0] = 1;
        //playerData.SaveData[GAME_INDEX.Pinocchio][5] = 1;
        //playerData.SaveData[GAME_INDEX.Pinocchio][7] = 1;
        //playerData.SaveData[GAME_INDEX.Snow_White][0] = 1;
        //playerData.onClearChapter?.Invoke(GAME_INDEX.Cinderella);
        //playerData.onClearChapter?.Invoke(GAME_INDEX.Little_Mermaid);


        PlayerData.CheckChapterPoint();     //스테이지 및 챕터 점검


        //급한대로 이렇게
        var objs = FindObjectsOfType<ChapterBtn>(true);
        foreach(var comp in objs)
        {
            comp.UpdateChapterButton(comp.MyIndex);
        }
    }


    public void Pop()
    {
        ui_Stack.Pop().Hide();
        pop_n_Push_Event?.Invoke(ui_Stack.Peek());
    }

    public void Push(StackUIComponent comp)
    {
        ui_Stack.Push(comp);
        pop_n_Push_Event?.Invoke(comp);
    }

    public void QuitApplication()
    {
        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            Application.Quit();
        }
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#endif

    }

    public void GoToTargetUIComponent(StackUIComponent target)
    {
        if (target == null) return;
        if (ui_Stack.Peek() != start_UIComponent)
        {
            while (ui_Stack.Peek() == start_UIComponent)
            {
                ui_Stack.Pop().Hide();
            }
        }
        var comps = target.transform.GetComponentsInParent<StackUIComponent>(true);
        for (int i = comps.Length-1; i >= 0; i--)
        {
            comps[i].Show();
        }
    }


    private void Update()
    {
        if (ui_Stack.Count != 0)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (ui_Stack.Peek() == start_UIComponent)
                {
                    close_UIComponent.Show();
                    return;
                }
                Pop();
            }
        }
    }
}