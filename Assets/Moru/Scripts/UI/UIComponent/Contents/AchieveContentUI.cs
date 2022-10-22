using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using PD;

namespace Moru.UI
{
    public class AchieveContentUI : MonoBehaviour
    {
        private delegate void OnClick_NoRet_NoParam(AchieveContentUI btn);
        private static event OnClick_NoRet_NoParam onClick;
        bool isOpen = false;

        private Button originBtn;
        [BoxGroup("�ո�"), LabelText("���� �̸�"), SerializeField] private Text achieveName;
        [BoxGroup("�޸�"), LabelText("���� �̹���"), SerializeField] private Image achieveImg;
        [BoxGroup("�޸�"), LabelText("���� ����"), SerializeField] private Text achieveDesc;
        [BoxGroup("�޸�"), LabelText("���� �ޱ��ư"), SerializeField] private Button btn_GetReward;
        [BoxGroup("�޸�"), LabelText("���� �ޱ� �ؽ�Ʈ"), SerializeField] private Text isGetReward;



        private AchieveResult myResult;

        public void Init(AchieveResult result, StackUIComponent targetComp)
        {
            isOpen = false;
            onClick += AchieveContentUI_onClick;
            originBtn = GetComponent<Button>();
            myResult = result;
            achieveName.text = result.AchieveName;
            achieveImg.sprite = result.Icon;
            achieveDesc.text = result.AchieveDesc;
            achieveImg.gameObject.SetActive(false);


            originBtn.onClick.AddListener(OnClick);
            PlayerData.onGetReward += GetReward;

            //�����޼��� ���� �ľ�
            if (PlayerData.instance.IsAchievement[result.MyIndex] > 0)
            {
                //originBtn.interactable = true;
                //������ �Ծ��� �ȸԾ��� üũ
                if (PlayerData.instance.IsGetReward[result.MyIndex] > 0)
                {
                    //�ȸ����� ��ƼŬ�� �߰�ǥ���� �ִ���.
                    //�˾�â ����
                    btn_GetReward.onClick.AddListener(
                        () =>
                        {
                            targetComp.Show();
                            PlayerData.instance.OnGetReward(result.MyIndex);
                        }
                        );
                    btn_GetReward.interactable = true;
                    isGetReward.text = "���� �ޱ�";
                }
                //�̹� ������ �Ծ��ٰ� ǥ���ؾ���
                else
                {
                    btn_GetReward.interactable = false;
                    isGetReward.text = "�̹� ���� �����Դϴ�.";
                }
            }
            //�޼� ����
            else
            {
                //originBtn.interactable = false;
            }
        }

        private void AchieveContentUI_onClick(AchieveContentUI btn)
        {
            if (btn != this)
            {
                if (isOpen)
                {
                    Hide();
                }
            }
        }

        private void OnClick()
        {
            if (!isOpen)
            {
                isOpen = true;
                achieveName.enabled = false;
                achieveImg.gameObject.SetActive(true);
                onClick?.Invoke(this);
            }
            else
            {
                Hide();
            }
        }

        void Hide()
        {
            isOpen = false;
            achieveName.enabled = true;
            achieveImg.gameObject.SetActive(false);
        }

        private void GetReward(ACHEIVE_INDEX index)
        {
            if (index == myResult.MyIndex)
            {
                btn_GetReward.interactable = false;
                isGetReward.text = "�̹� ���� �����Դϴ�.";
            }
        }
    }
}