using DG.Tweening;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Unity1Week_20230619.Main.Game3
{
    public enum Seasoning
    {
        White,
        Gray,

    }

    public class Request
    {
        public Seasoning T { get; set; }
    }

    public class PlayerController : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI AnnounceText;
        
        Transform White, Gray, Arrow;
        Transform Cooking;
        GameObject missimg, successimg;

        readonly Vector3 SaltDefaultPos = new Vector3(5f, -3f, 0);
        readonly Vector3 PepperDefaultPos = new Vector3(7f, -3f, 0);
        readonly Vector3 CookingDefaultPos = new Vector3(0, 0, 0);
        readonly Vector3 ArrowDefaultPos = new Vector3(5f, -4.5f, 0);

        Seasoning select_seasoning;
        Dictionary<Seasoning, int> requestDict = new Dictionary<Seasoning, int>();

        public int success { private set; get; }
        public int miss { private set; get; }
        public int point_offset { private set; get; }

        public void Init()
        {
            requestDict.Clear();
            RequestNext();
            AnnounceText.text = $"塩：{requestDict[Seasoning.White]}回\n胡椒：{requestDict[Seasoning.Gray]}回";

            White = transform.Find("White");
            Gray = transform.Find("Gray");
            Arrow = transform.Find("Arrow");
            Cooking = transform.Find("Cooking");
            missimg = Cooking.GetChild(0).gameObject;
            successimg = Cooking.GetChild(1).gameObject;
            missimg.SetActive(false);
            successimg.SetActive(false);

            White.position = SaltDefaultPos;
            White.rotation = Quaternion.identity;
            Gray.position = PepperDefaultPos;
            Gray.rotation = Quaternion.identity;
            Arrow.position = new Vector3(White.position.x, ArrowDefaultPos.y, 0);
            Cooking.position = CookingDefaultPos;
            Cooking.rotation = Quaternion.identity;
            select_seasoning = Seasoning.White;

            point_offset = Data.instance.rankingDate.Count + 1;
            success = 0;
            miss = 0;
        }

        void CalcShake(Transform obj, Vector3 defaultPos)
        {
            obj.position = new Vector3(2f, 1.5f);
            obj.rotation = Quaternion.Euler(0f, 0f, 100f);
            obj.DOLocalMove(new Vector3(1.5f, 1.5f), 0.2f)
                .OnComplete(() =>
                {
                    obj.position = defaultPos;
                    obj.rotation = Quaternion.identity;
                });

        }


        public void Controller()
        {
            if (Input.GetButtonDown("Submit"))
            {
                switch (select_seasoning)
                {
                    case Seasoning.White:
                        CalcShake(White, SaltDefaultPos);
                        requestDict[Seasoning.White]--;
                        break;
                    case Seasoning.Gray:
                        CalcShake(Gray, PepperDefaultPos);
                        requestDict[Seasoning.Gray]--;
                        break;
                }
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Arrow.position = new Vector3(SaltDefaultPos.x, ArrowDefaultPos.y, 0);
                select_seasoning = Seasoning.White;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Arrow.position = new Vector3(PepperDefaultPos.x, ArrowDefaultPos.y, 0);
                select_seasoning = Seasoning.Gray;

            }

            RequestJudge();
            RequestUpdate();
        }


        void RequestJudge()
        {
            if (requestDict[Seasoning.White] < 0 || requestDict[Seasoning.Gray] < 0)
            {
                SoundManager.Instance.PlaySe(3);
                miss++;
                RequestNext();
                missimg.SetActive(true);

                StartCoroutine(Function.DelayCoroutine(0.5f, () => {
                    missimg.SetActive(false);
                }));
               

            }
            else if (requestDict[Seasoning.White] == 0 && requestDict[Seasoning.Gray] == 0)
            {
                SoundManager.Instance.PlaySe(0);
                success++;
                RequestNext();
                successimg.SetActive(true);
                StartCoroutine(Function.DelayCoroutine(0.5f, () => {
                    successimg.SetActive(false);
                }));
            }


        }

        void RequestNext()
        {
            requestDict[Seasoning.White] = Random.Range(1, 10);
            requestDict[Seasoning.Gray] = Random.Range(1, 10);
        }

        void RequestUpdate()
        {
            AnnounceText.text = $"塩：{requestDict[Seasoning.White]}回\n胡椒：{requestDict[Seasoning.Gray]}回";
        }

        public int GetScore()
        {
            int score = (success - miss) * (100 * point_offset);
            return System.Math.Clamp(score, 0, int.MaxValue);
        }
    }
}