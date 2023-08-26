using UnityEngine;

public class PlayerTurnController : MonoBehaviour
{
    [SerializeField] private Player player;
    //public string playerName; // �v���C���[�̖��O�ȂǁA�K�v�ȃv���C���[����ǉ����邱�Ƃ��ł��܂�
    public bool isMyTurn;     // ���̃v���C���[�̃^�[�����ǂ����𔻒肷��t���O

    private void Update()
    {
        TakeAction();
    }

    // �v���C���[�̃^�[�����n�܂鏈��
    public void StartTurn()
    {
        isMyTurn = true;
    }

    // �v���C���[�̃^�[�����I�����鏈��
    public void EndTurn()
    {
        isMyTurn = false;
    }

    // �v���C���[���s����I�������ۂ̏���
    public void TakeAction()
    {
        if (isMyTurn)
        {
            player.PlayerMove();
        }
    }
}
