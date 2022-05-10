
using System;
using UnityEngine;

[CreateAssetMenu(order = 0, fileName = "DialogueItem", menuName = "Space Game/DialogueItem")]
public class DialogueItem : ScriptableObject {
    public Sprite Image;
    public String Name;
    public String Text;
}