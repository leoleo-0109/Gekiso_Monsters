using System.Collections;
using UnityEngine;
using UniRx;
using System;

public class BackgroundScroller : MonoBehaviour
{
    public static BackgroundScroller Instance; // Singleton

    public float scrollSpeed = 2.0f; // �w�i�̃X�N���[�����x
    public float stopPositionY = -5.7f; // ��~����Y���W
    private bool scrolling = false;
    private int enemiesCount; // �G�̐����Ǘ�����ϐ�

    private Vector3 startPosition; // �����ʒu
    private Transform backgroundContainer; // �w�i�̃R���e�i
    public Subject<Unit> scroll = new();

    private void Awake()
    {
        backgroundContainer = transform; // �R���e�i��Transform���擾

        // �����ʒu���L��
        startPosition = backgroundContainer.position;

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        //// �G��S���|������ɃX�N���[�����J�n
        //if (AllEnemiesDefeated() && !scrolling)
        //{
        //    scrolling = true;
        //    // ���̃}�b�v�̓G���o�������鏈�����Ăяo��
        //    StartCoroutine(ScrollBackgroundAndSpawnNextMap());
        //}

        // �X�N���[������Y���W��ω�������
        if (scrolling)
        {
            // �ォ�牺�ɃX�N���[�����邽�߁A�w�i��Y���W�����炷
            backgroundContainer.Translate(Vector3.down * scrollSpeed * Time.deltaTime);

            // ����̈ʒu�Œ�~
            if (backgroundContainer.position.y <= (startPosition.y + stopPositionY))
            {
                startPosition = backgroundContainer.position;
                scrolling = false;
                StartCoroutine(ScrollBackgroundAndSpawnNextMap());
            }
        }
    }

    public void CheckAllEnemiesDefeated()
    {
        // �G�̐����X�V
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemiesCount = enemies.Length;

        // �G��S�ł������ꍇ�ɔw�i���X�N���[��������
        if (enemiesCount == 0 && !scrolling)
        {
            scrolling = true;
            // ���̃}�b�v�̓G���o�������鏈�����Ăяo��
        }
    }

    //private void StartNextMap()
    //{
    //    // �w�i���X�N���[�������鏈�����J�n
    //    StartCoroutine(ScrollBackgroundAndSpawnNextMap());
    //}

    private IEnumerator ScrollBackgroundAndSpawnNextMap()
    {
        // �����ҋ@���Ă��玟�̃}�b�v�̓G���o��������
        yield return new WaitForSeconds(1.0f);
        scroll.OnNext(Unit.Default);
    }

    private bool AllEnemiesDefeated()
    {
        // �G��S���|�������ǂ����̔��胍�W�b�N������
        return enemiesCount == 0;
    }
}
