using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PopNPush : MonoBehaviour, ISubmitHandler, IPointerClickHandler
{


    public void OnPointerClick(PointerEventData eventData)
    {
        PopAndPush();
    }

    public void OnSubmit(BaseEventData eventData)
    {
        PopAndPush();
    }
    [SerializeField] private bool isPopAndPush;
    [SerializeField] private StackUIComponent push_comp;

    void Start()
    {

    }

    private void PopAndPush()
    {
        if (push_comp == null) Debug.Log($"push����� �����ϴ�. ��� ����");
        else
        {
            if (isPopAndPush)
            {
                StackUIManager.Instance.Pop();
            }
            push_comp.Show();
        }
    }
}
