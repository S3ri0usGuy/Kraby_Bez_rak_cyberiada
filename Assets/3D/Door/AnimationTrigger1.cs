using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    [SerializeField] private Animator myDoor;
    [SerializeField] private string doorOpen = "DoorOpen";
    [SerializeField] private string doorClose = "DoorClose";
    [SerializeField] private bool openDoor = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (openDoor == false)
            {
                myDoor.Play(doorOpen, 0, 0.0f);
                openDoor = true;

            }
            else if (openDoor == true)
            {
                myDoor.Play(doorClose, 0, 0.0f);
                openDoor = false;

            }
        }
    }
}
