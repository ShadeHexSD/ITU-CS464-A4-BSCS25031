// Lecture 10, slide 50. For a third-person camera several metres away: hear from where the player is,
// facing the way the camera faces. Put this and the AudioListener on an EMPTY GameObject, and REMOVE the
// AudioListener from the camera. (If your camera is first-person or close behind the player, skip this.)
using UnityEngine;

public class ListenerOnPlayer : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform cameraTransform;

    void LateUpdate() => transform.SetPositionAndRotation(player.position, cameraTransform.rotation);
}
