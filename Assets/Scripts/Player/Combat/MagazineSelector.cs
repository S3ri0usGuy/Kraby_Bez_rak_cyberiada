using UnityEngine;

public class MagazineSelector : MonoBehaviour
{
    [SerializeField]
    private GameObject empty;
    [SerializeField]
    private GameObject full;

    [SerializeField]
    private bool adapt;

    [SerializeField]
    private PlayerCombat playerCombat;

    private void OnEnable()
    {
        if (adapt)
        {
            empty.SetActive(playerCombat.BulletsInMagazine == 0);
            full.SetActive(playerCombat.BulletsInMagazine > 0);
        }
        else
        {
            empty.SetActive(false);
            full.SetActive(true);
        }
    }
}
