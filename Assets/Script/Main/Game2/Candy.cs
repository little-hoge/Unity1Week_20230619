
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Candy : MonoBehaviour
{
    // キャンディの種類
    public enum CandyType
    {
        SMALL,
        MEDIUM,
        LARGE,
        TYPE_NUM,
    }

    

    // キャンディの情報
    [System.Serializable]
    public struct CandyInfo
    {
        public Sprite graphic; // テクスチャ
        public CandyType type; // 種類
        public int score;      // 得点
    }



    [SerializeField] private List<CandyInfo> candiesList;
    private SpriteRenderer spriteRenderer;
    private new Rigidbody2D rigidbody2D;
    private long score;
    private CandyType candyType;
    private float speed;



    public void Init()
    {
        TryGetComponent(out spriteRenderer);
        TryGetComponent(out rigidbody2D);
        candyType = (CandyType)(Random.Range((int)CandyType.SMALL, (int)CandyType.TYPE_NUM));
        CandyInfo candyInfo = candiesList[(int)candyType];
        spriteRenderer.sprite = candyInfo.graphic;
        score = candyInfo.score;
        speed = Random.Range(4.5f, 6.0f);
        rigidbody2D.velocity = new Vector2(0.0f, -speed);

        
    }

    private void Awake()
    {
        Init();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    public long GetScore()
    {
        return score;
    }
}
