using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class HideAndShowIfStackUI : MonoBehaviour
{
    //[LabelText("Show�� �ƴϸ� ������ ����ϴ�.")]
    //public bool HideElse;
    //[HideIf("HideElse")]
    public List<StackUIComponent> hideTargetUI;
    //[LabelText("Hide�� �ƴϸ� ������ �������ϴ�.")]
    //public bool ShowElse;
    //[HideIf("ShowElse")]
    public List<StackUIComponent> showTargetUI;

    private void Awake()
    {
        StackUIManager.Instance.pop_n_Push_Event += Hide;
        StackUIManager.Instance.pop_n_Push_Event += Show;
    }

    void Hide(StackUIComponent cur_Comp)
    {
        //if (HideElse)
        //{
        //    bool result = false;        
        //    foreach (var comp in showTargetUI)  
        //    {
        //        if (comp == cur_Comp)       
        //        {
        //            result = true;
        //            break;
        //        }
        //    }
        //    this.gameObject.SetActive(result);
        //    return;
        //}

        foreach (var comp in hideTargetUI)
        {
            if (comp == cur_Comp)
            {
                this.gameObject.SetActive(false);
                break;
            }
        }
    }
    void Show(StackUIComponent cur_Comp)
    {
        //if (ShowElse)
        //{
        //    bool result = true;
        //    foreach (var comp in hideTargetUI)
        //    {
        //        if (comp == cur_Comp)
        //        {
        //            result = false;
        //            break;
        //        }
        //    }
        //    this.gameObject.SetActive(result);
        //    return;
        //}
        foreach (var comp in showTargetUI)
        {
            if (comp == cur_Comp)
            {
                this.gameObject.SetActive(true);
                break;
            }
        }
    }
}
