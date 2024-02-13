using UnityEngine;
using System;
using UniRx;
using UniRx.Triggers;
using System.Threading.Tasks;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.EventSystems;


public class Player : MonoBehaviour
{
    [SerializeField] private PlayerTurnManager playerTurnManager;
    [SerializeField] private BackgroundScroller backgroundScroller;
    public Vector3 force;
    [SerializeField] private bool isHolded = false;

    [SerializeField] float friction = 0f; // 0.985
    [SerializeField] float minForce = 0.2f;
    [SerializeField] private float forcePower = 150f;

    [SerializeField] SpriteRenderer arrow;
    [SerializeField] float arrowScele = 80f; // 矢印の表示倍率
    [SerializeField] GameObject playerBullet;

    public float playerAttack = 10f;
    private IDisposable box = null;
    private Subject<Unit> moveend = new Subject<Unit>();
    private bool turnflag = false;
    public bool shotFlag = false;
    private bool friendFlag = true;
    private EnemyManager enemyManager;
    private CircleCollider2D circleCollider;
    //private PolygonCollider2D polygonCollider;
    //private PolygonCollider2D polygonCollider2;

    private void Awake()
    {
        enemyManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();
        circleCollider = GetComponent<CircleCollider2D>();
        //polygonCollider = GetComponent<PolygonCollider2D>();
        //polygonCollider2 = GetComponent<PolygonCollider2D>();
    }

    private void Start()
    {
        arrow.gameObject.SetActive(false);

        playerTurnManager = FindObjectOfType<PlayerTurnManager>();
    }

    public void PlayerMove()
    {
        if (CanvasController.pauseType == PauseType.pause)
        {
            isHolded = false;
            return;
        }

        if (force.magnitude == 0)
        {
            if (!backgroundScroller.scrolling)
            {
                shotFlag = true;
            }
        }

        force *= friction;
        if (force.magnitude < minForce && turnflag)
        {
            force = default(Vector3);
            moveend.OnNext(Unit.Default);
        }

        if (Input.GetMouseButtonDown(0) && shotFlag && !backgroundScroller.scrolling)
        {
            var hit = Physics2D.Raycast(
                Camera.main.ScreenToWorldPoint(Input.mousePosition),
                Vector2.zero);
            Debug.Log("shotFlag");
            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                arrow.gameObject.SetActive(true);
                isHolded = true;
            }
        }

        if (Input.GetMouseButton(0)) 
        { 
            // mouseとplayerの距離を計算してその長さを使用
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;

            float scale = (transform.position - pos).magnitude / Screen.width * arrowScele;
            if(scale < 1.20f)
            {
                arrow.transform.localScale = new Vector3(scale, scale, 1);
            }
            if(scale >= 1.20f)
            {
                arrow.transform.localScale = new Vector3(1.20f, 1.20f, 1);
            }

            float angle = Vector3.Angle(transform.position - pos, Vector3.up);

            Vector3 cross = Vector3.Cross(transform.position - pos, Vector3.up);
            if (cross.z > 0) 
            {
                angle *= -1;
            }
            arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        if (isHolded && Input.GetMouseButtonUp(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = transform.position.z;

            force = transform.position - pos;
            force *= forcePower;
            GetComponent<Rigidbody2D>().AddForce(force);
            isHolded = false;
            turnflag = true;
            shotFlag = false;
            arrow.gameObject.SetActive(false);
        }
    }

    public void UpdateStream()
    {
        shotFlag = true;
        box = this.UpdateAsObservable().Subscribe(x => PlayerMove());
        turnflag = false;
        friendFlag = false;
        circleCollider.isTrigger = false;
        //polygonCollider.isTrigger = false;
        //polygonCollider2.isTrigger = false;
    }

    public void StopUpdate()
    {
        friendFlag = true;
        box.Dispose();
    }

    public IObservable<Unit> GetMoveEnd()
    {
        return moveend;
    }

    private void OnDestroy()
    {
        moveend.Dispose();
    }

    private async UniTask BeforeSetPlayerBullet()
    {
        if (friendFlag)
        {
            float minEnemyMater = 100f;
            GameObject targetEnemy = gameObject;
            foreach (GameObject enemy in enemyManager.currentStageEnemies)
            {
                if (enemy != null)
                {
                    float mater = (transform.position - enemy.transform.position).magnitude;
                    if (minEnemyMater > Mathf.Abs(mater))
                    {
                        minEnemyMater = mater;
                        targetEnemy = enemy;
                    }
                }
            }
            
            if (targetEnemy != gameObject)
            {
                friendFlag = false;
                Instantiate(playerBullet, transform).GetComponent<PlayerBullet>().ShotPlayerBulletToEnemy(targetEnemy);
                await UniTask.Delay(TimeSpan.FromSeconds(0.1f)); // 待機処理
                Instantiate(playerBullet, transform).GetComponent<PlayerBullet>().ShotPlayerBulletToEnemy(targetEnemy);
                await UniTask.Delay(TimeSpan.FromSeconds(0.1f)); // 待機処理
                Instantiate(playerBullet, transform).GetComponent<PlayerBullet>().ShotPlayerBulletToEnemy(targetEnemy);
            }
        }
    }

    public void SetPlayerBullet()
    {
        BeforeSetPlayerBullet().Forget();
    }

    public void FriendBulletTrue()
    {
        friendFlag = true;
        circleCollider.isTrigger = true;
        //polygonCollider.isTrigger = true;
        //polygonCollider2.isTrigger = true;
    }
}
