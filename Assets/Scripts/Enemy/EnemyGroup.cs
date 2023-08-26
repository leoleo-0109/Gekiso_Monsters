using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyGroup : MonoBehaviour
{
    [SerializeField] CanvasController canvasController;
    [SerializeField] int enemyCount;
    [SerializeField] GameObject[] enemyList;
    //[SerializeField] Enemy enemy;

    private void Start()
    {
        //enemy = GetComponent<Enemy>();
    }

    Vector3[][] pos = new Vector3[][]
    {
        new []{
            new Vector3(0f,4.3f,0f),
            new Vector3(1.5f,1.5f,0f)
        },
        new[]{
            new Vector3(0f,0.3f,0f),
            new Vector3(-1.5f,-0.5f,0f)
        },
    };
    void Update()
    {
        //if (enemy.GetDead()) {
        //    EnemyDead();
        //}
        if (enemyCount == 0)
        {
            canvasController.QuestStageClear();
        }
    }
    public int GetEnemyCount() 
    {
        return enemyCount;
    }
    public void EnemyDead()
    {
        enemyCount--;
    }
    public void SetEnemy(int area)
    {
        for (int i = 0; i < enemyList.Length; i++)
        {
            for (int ii = 0; ii < enemyList.Length; ii++)
            {
                GameObject go = Instantiate(enemyList[i], pos[area][ii], Quaternion.identity) as GameObject;
                go.transform.parent = GameObject.Find("EnemyList").transform;
            }
        }
    }
}
