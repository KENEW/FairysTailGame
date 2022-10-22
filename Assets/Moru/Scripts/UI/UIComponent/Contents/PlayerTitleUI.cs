using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class PlayerTitleUI : MonoBehaviour
{
    private delegate void OnClick_NoRet_NoParam(AchieveContentUI btn);
    private static event OnClick_NoRet_NoParam onClick;
    bool isOpen = false;

    private Button originBtn;
    [BoxGroup("�ո�"), LabelText("���� �̸�"), SerializeField] private Text playerTitleText;
    //������ �̹��� ȿ��
    private AchieveResult myResult;
    public void Init(AchieveResult result, StackUIComponent targetComp)
    {
        originBtn = GetComponent<Button>();

        myResult = result;
        
        PlayerData.onGetReward += GetReward;

        //�����޼��� ���� �ľ�
        if (PlayerData.instance.IsGetReward[result.MyIndex] > 0)
        {
            originBtn.interactable = true;
            playerTitleText.text = result.Title;
            //������ �Ծ��� �ȸԾ��� üũ
            //�˾�â ����
            originBtn.onClick.AddListener(
                () =>
                {
                    targetComp.Show();
                    targetComp.GetComponent<ChangeTitlePopUp>().Init(result);
                    PlayerData.instance.OnGetReward(result.MyIndex);
                }
                );
        }
        //���� ������
        else
        {
            originBtn.interactable = false;
            playerTitleText.text = "????";
        }
    }
    private void GetReward(ACHEIVE_INDEX index)
    {
        if (index == myResult.MyIndex)
        {
            originBtn.interactable = true;
            playerTitleText.text = myResult.Title;
        }
    }
}
