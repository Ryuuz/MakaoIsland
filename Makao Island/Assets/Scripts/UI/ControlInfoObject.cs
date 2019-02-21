using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ControlInfo", menuName = "ScriptableObjects/ControlInfoObject", order = 1)]
public class ControlInfoObject : ScriptableObject
{
    public ControlAction mControlType;
    public Sprite mDefaultControl;
    public Sprite mGamepadControl;
    public string mControlText;
}
