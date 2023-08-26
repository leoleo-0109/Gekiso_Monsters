using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// �R���[�`���Ɋւ���ėp�N���X
/// </summary>
public static class CoroutineCommon
{
    private static readonly MonoBehaviour mMonoBehaviour;

    /// <summary>
    /// �R���[�`�����Ǘ�����Q�[���I�u�W�F�N�g�𐶐�����R���X�g���N�^
    /// </summary>
    static CoroutineCommon()
    {
        var gameObject = new GameObject("CoroutineCommon");
        GameObject.DontDestroyOnLoad(gameObject);
        mMonoBehaviour = gameObject.AddComponent<MonoBehaviour>();
    }

    /// <summary>
    /// 1 �t���[���ҋ@���Ă��� Action �f���Q�[�g���Ăяo���܂�
    /// </summary>
    public static void CallWaitForOneFrame(Action act)
    {
        mMonoBehaviour.StartCoroutine(DoCallWaitForOneFrame(act));
    }

    /// <summary>
    /// �w�肳�ꂽ�b���ҋ@���Ă��� Action �f���Q�[�g���Ăяo���܂�
    /// </summary>
    //public static void CallWaitForSeconds(float seconds, Action act)
    //{
      //  mMonoBehaviour.StartCoroutine(DoCallWaitForSeconds(seconds, act));
    //}

    /// <summary>
    /// 1 �t���[���ҋ@���Ă��� Action �f���Q�[�g���Ăяo���܂�
    /// </summary>
    private static IEnumerator DoCallWaitForOneFrame(Action act)
    {
        yield return 0;
        act();
    }

    /// <summary>
    /// �w�肳�ꂽ�b���ҋ@���Ă��� Action �f���Q�[�g���Ăяo���܂�
    /// </summary>
    private static IEnumerator DoCallWaitForSeconds(float seconds, Action act)
    {
        yield return new WaitForSeconds(seconds);
        act();
    }
}