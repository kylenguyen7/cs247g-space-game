
using System;
using UnityEngine;

[CreateAssetMenu(order = 0, fileName = "DialogueItem", menuName = "Space Game/DialogueItem")]
public class DialogueItem : ScriptableObject {
    public Sprite Image;
    public String Name;
    public String Text;
    public DialogueType Type;

    public enum DialogueType {
        NORMAL,
        END_DAY,
        RESTART_DAY
    }
}