using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SetupRotationButtons : MonoBehaviour
{
    public RotatingDisplay rotatingDisplay; // Assign your rotating object here in inspector

    // Names of buttons to find in the scene/Canvas
    private readonly string[] buttonNames = { "ButtonUp", "ButtonDown", "ButtonLeft", "ButtonRight" };

    void Start()
    {
        SetupButton("ButtonUp", rotatingDisplay.OnPressUp, rotatingDisplay.OnReleaseUp);
        SetupButton("ButtonDown", rotatingDisplay.OnPressDown, rotatingDisplay.OnReleaseDown);
        SetupButton("ButtonLeft", rotatingDisplay.OnPressLeft, rotatingDisplay.OnReleaseLeft);
        SetupButton("ButtonRight", rotatingDisplay.OnPressRight, rotatingDisplay.OnReleaseRight);
    }

    void SetupButton(string buttonName, UnityEngine.Events.UnityAction pressAction, UnityEngine.Events.UnityAction releaseAction)
    {
        GameObject buttonObj = GameObject.Find(buttonName);
        if (buttonObj == null)
        {
            Debug.LogWarning($"Button '{buttonName}' not found in the scene.");
            return;
        }

        Button button = buttonObj.GetComponent<Button>();
        if (button == null)
        {
            Debug.LogWarning($"GameObject '{buttonName}' does not have a Button component.");
            return;
        }

        // Add or get EventTrigger component
        EventTrigger trigger = buttonObj.GetComponent<EventTrigger>();
        if (trigger == null)
            trigger = buttonObj.AddComponent<EventTrigger>();

        // Clear existing events to avoid duplicates
        trigger.triggers.Clear();

        // PointerDown event
        EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };
        pointerDownEntry.callback.AddListener((eventData) => pressAction());
        trigger.triggers.Add(pointerDownEntry);

        // PointerUp event
        EventTrigger.Entry pointerUpEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerUp };
        pointerUpEntry.callback.AddListener((eventData) => releaseAction());
        trigger.triggers.Add(pointerUpEntry);
    }
}
