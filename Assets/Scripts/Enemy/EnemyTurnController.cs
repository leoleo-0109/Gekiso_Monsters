using UnityEngine;

public class EnemyTurnController : MonoBehaviour
{
    // �G�̍s�����I���������ǂ����̃t���O
    private bool isTurnFinished = true;

    // �G�̍s�����I���������Ƃ�񍐂��郁�\�b�h
    public void FinishTurn()
    {
        isTurnFinished = true;
    }

    // �G�̍s�����J�n���郁�\�b�h
    public void StartTurn()
    {
        isTurnFinished = false;
        // �����ɓG�̍s���������L�q����
        Debug.Log("�G�̃^�[���ł��B");
    }

    // �_���[�W���󂯂鏈���̗�
    public void TakeDamage(int damage)
    {
        // �_���[�W���󂯂鏈�����L�q����
    }
}
