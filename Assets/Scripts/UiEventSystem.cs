// Shared UI selection helper. Some blockout scenes had no EventSystem, so calling
// EventSystem.current.SetSelectedGameObject(...) threw on win/pause. Every menu script
// goes through this class instead of touching EventSystem.current directly.
using UnityEngine;
using UnityEngine.EventSystems;

public static class UiEventSystem
{
    public static void Ensure()
    {
        if (Object.FindAnyObjectByType<EventSystem>() != null) return;

        GameObject go = new GameObject("EventSystem");
        go.AddComponent<EventSystem>();
        go.AddComponent<StandaloneInputModule>();
    }

    public static void Select(GameObject button)
    {
        if (button == null) return;
        Ensure();

        EventSystem events = EventSystem.current != null
            ? EventSystem.current
            : Object.FindAnyObjectByType<EventSystem>();

        if (events != null) events.SetSelectedGameObject(button);
    }
}
