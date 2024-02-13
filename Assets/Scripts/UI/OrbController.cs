using UnityEngine;
using TMPro;

public class OrbController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI orbTextHeader;
    [SerializeField] private TextMeshProUGUI orbTextStaminRecovery;
    private static float maxOrb = 100f; // ����̍ő�l
    public static float currentOrb = maxOrb; // ���݂̓��쐔

    public void Start()
    {
        //currentOrb = maxOrb;
        UpdateOrbUI(); // ���쐔�̌��ݒl���X�V
    }

    public void UseOrb()
    {
        currentOrb--;
        UpdateOrbUI();
    }

    public static void ResetOrb()
    {
        currentOrb = maxOrb;
    }

    private void UpdateOrbUI()
    {
        orbTextHeader.text = currentOrb.ToString(); // �e�L�X�g�ɓ��쐔��\��
        orbTextStaminRecovery.text = currentOrb.ToString(); // �e�L�X�g�ɓ��쐔��\��
    }
}
