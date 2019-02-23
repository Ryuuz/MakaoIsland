using UnityEngine;

[CreateAssetMenu(fileName = "ControlInfo", menuName = "ScriptableObjects/ControlInfoObject", order = 1)]
public class ControlInfoObject : ScriptableObject
{
    public ControlAction mControlType;

    //Sprite showing the control for keyboard/mouse
    public Sprite mDefaultControl;
    //Sprite showing the control for gamepad
    public Sprite mGamepadControl;
    //Text explaining what the control does
    public string mControlText;
}
