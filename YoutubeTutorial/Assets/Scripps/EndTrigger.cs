using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    public GameManager gm;
    private void OnTriggerEnter(Collider other)
    {
        gm.WinGame();
    }
}
