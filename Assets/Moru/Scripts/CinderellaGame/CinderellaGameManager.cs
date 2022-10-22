using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace Moru.Cinderella
{
    public class CinderellaGameManager : SingleToneMono<CinderellaGameManager>
    {
        [BoxGroup("�׽�Ʈ���?")] [SerializeField] private bool isTest = true;
        [BoxGroup("�׽�Ʈ���?"), ShowIf("isTest")] public int GameStageNum;


        [BoxGroup("��������Ʈ ����")] [SerializeField] private Sprite[] Atype;
        [BoxGroup("��������Ʈ ����")] [SerializeField] private Sprite[] Btype;
        [BoxGroup("��������Ʈ ����")] [SerializeField] private Sprite[] Ctype;
        [BoxGroup("��������Ʈ ����")] [SerializeField] private Sprite[] Dtype;



        [BoxGroup("��������Ʈ ��")] [SerializeField] private Transform Atype_parent;
        [BoxGroup("��������Ʈ ��")] [SerializeField] private Transform[] Atype_Answer;

        [BoxGroup("��������Ʈ ��")] [SerializeField] private Transform Btype_parent;
        [BoxGroup("��������Ʈ ��")] [SerializeField] private Transform[] Btype_Answer;

        [BoxGroup("��������Ʈ ��")] [SerializeField] private Transform Ctype_parent;
        [BoxGroup("��������Ʈ ��")] [SerializeField] private Transform[] Ctype_Answer;

        [BoxGroup("��������Ʈ ��")] [SerializeField] private Transform Dtype_parent;
        [BoxGroup("��������Ʈ ��")] [SerializeField] private Transform[] Dtype_Answer;

        [BoxGroup("��������Ʈ ���� ������")] [SerializeField] private float[] Collect_Offset = new float[4];

        [BoxGroup("Ÿ�̸�")] [SerializeField] private float[] timerList = new float[20];

        [BoxGroup("ĵ���� ������")] [SerializeField] private float xSize;
        [BoxGroup("ĵ���� ������")] [SerializeField] private float ySize;

        [BoxGroup("���� ����")]
        [SerializeField] Transform PuzzelPivot;
        [BoxGroup("���� ����")] [SerializeField] Transform[] selectedAnswer;
        [BoxGroup("���� ����")] [SerializeField] float cur_Offset;
        [BoxGroup("���� ����")] [SerializeField] float maxTimer;
        [BoxGroup("���� ����")] [SerializeField] float cur_Timer;

        [BoxGroup("�ΰ� ������Ʈ��"), SerializeField, LabelText("�ŵ�����")] private SpriteRenderer Cinderella;
        [BoxGroup("�ΰ� ������Ʈ��"), SerializeField, LabelText("���")] private SpriteRenderer StepMom;
        [BoxGroup("�ΰ� ������Ʈ��"), SerializeField, LabelText("���� ��")] private Sprite[] failSprite;
        [BoxGroup("�ΰ� ������Ʈ��"), SerializeField, LabelText("���� UI")] private GameObject ClearUI;
        [BoxGroup("�ΰ� ������Ʈ��"), SerializeField, LabelText("���� UI")] private GameObject FailUI;
        [BoxGroup("�ΰ� ������Ʈ��"), SerializeField, LabelText("Ÿ�̸� UI")] private GameObject TimerUI;

        public GameObject selectedPiece;

        private int pieceCount;

        public bool isGameOver;
        #region Events
        public delegate void OnValueChange(float cur, float max);
        public event OnValueChange onTimerValueChange;
        #endregion

        private void Start()
        {
            if (!isTest)
            {
                GameStageNum = PlayerData.instance.CurStaggSelectedNum;
            }
            if (GameStageNum < 0) { Debug.Log($"�߸��� �������� �ѹ�, �������"); return; }
            else if (GameStageNum < 4)
            {
                SetInit(Atype);
                Atype_parent.gameObject.SetActive(true);
                selectedAnswer = Atype_Answer;
                cur_Offset = Collect_Offset[0];
            }
            else if (GameStageNum < 8)
            {
                SetInit(Btype);
                Btype_parent.gameObject.SetActive(true);
                selectedAnswer = Btype_Answer;
                cur_Offset = Collect_Offset[1];
            }
            else if (GameStageNum < 12)
            {
                SetInit(Ctype);
                Ctype_parent.gameObject.SetActive(true);
                selectedAnswer = Ctype_Answer;
                cur_Offset = Collect_Offset[2];
            }
            else if (GameStageNum < 20)
            {
                SetInit(Dtype);
                Dtype_parent.gameObject.SetActive(true);
                selectedAnswer = Dtype_Answer;
                cur_Offset = Collect_Offset[3];
            }

            pieceCount = selectedAnswer.Length;
            maxTimer = timerList[GameStageNum];
            cur_Timer = maxTimer;
            TimerUI.AddComponent<MoruTimer>();
            isGameOver = false;
        }

        private void SetInit(Sprite[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                var obj = new GameObject(arr[i].name);
                obj.transform.SetParent(PuzzelPivot);
                float x = Random.Range(-xSize, xSize);
                float y = Random.Range(-ySize, ySize);
                obj.transform.position = new Vector3(x, y, 0);

                var comp = obj.AddComponent<SpriteRenderer>();
                comp.sprite = arr[i];
                comp.sortingOrder = 3;

                var customComp = obj.AddComponent<PuzzelPiece>();
                customComp.Init(i);

                var collider = obj.AddComponent<PolygonCollider2D>();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (!hit) return;
                if (hit.transform.gameObject.TryGetComponent<PuzzelPiece>(out var comp))
                {
                    if (comp.isCanMoved)
                    {
                        selectedPiece = hit.transform.gameObject;
                    }
                }

            }
            if (selectedPiece != null)
            {
                Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                selectedPiece.transform.position = new Vector3(pos.x, pos.y, 0);
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (selectedPiece != null)
                {
                    var comp = selectedPiece.GetComponent<PuzzelPiece>();
                    selectedPiece = null;
                    if (comp.CheckCollect(selectedAnswer[comp.myIndex], cur_Offset))
                    {
                        pieceCount--;
                    }
                }
            }

            if (cur_Timer > 0 && !isGameOver)
            {
                cur_Timer -= Time.deltaTime;
                onTimerValueChange?.Invoke(cur_Timer, maxTimer);
            }
            else if (cur_Timer <= 0 && !isGameOver)
            {
                //���� ����
                isGameOver = true;
                SetGameOver();
            }

            if (pieceCount <= 0 && !isGameOver)
            {
                isGameOver = true;
                SetGameClear();
            }

            if (isGameOver && Input.anyKey)
            {
                //������������ ���ư���
            }
        }

        private void SetGameClear()
        {
            Debug.Log($"���� Ŭ����!");
            //�÷��̾� ������ �ݿ�
            ClearUI?.SetActive(true);
            if (PlayerData.instance != null)
            {
                PlayerData.onClearGame(GAME_INDEX.Cinderella, GameStageNum);
            }
        }

        private void SetGameOver()
        {
            Debug.Log($"���� ����!");
            FailUI?.SetActive(true);
            Cinderella.sprite = failSprite[0];
            StepMom.sprite = failSprite[1];
        }
    }

    public class PuzzelPiece : MonoBehaviour
    {
        public bool isCanMoved;
        private int _myIndex;
        public int myIndex { get => _myIndex; }
        public void Init(int index)
        {
            _myIndex = index;
            isCanMoved = true;
        }

        public bool CheckCollect(Transform target, float offset)
        {
            if (target == null) return false;

            float distance = Vector2.Distance(this.transform.position, target.position);
            distance = Mathf.Abs(distance);

            if (distance < offset)
            {
                this.transform.position = target.position;
                isCanMoved = false;
                Destroy(this.GetComponent<PuzzelPiece>());
                Destroy(this.GetComponent<PolygonCollider2D>());
                return true;
            }
            else
            {
                return false;
            }

        }
    }

    public class MoruTimer : MonoBehaviour
    {
        Text text;
        private void Start()
        {
            CinderellaGameManager.Instance.onTimerValueChange += UpdateTimer;
            text = GetComponent<Text>();
        }

        void UpdateTimer(float _cur, float _max)
        {
            float m = _cur / 60;
            float s = _cur % 60;
            text.text = $"{m.ToString("F0")}:{s.ToString("F0")}";
        }
    }

}