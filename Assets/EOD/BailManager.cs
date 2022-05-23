
using System;
using UnityEngine;

public class BailManager : MonoBehaviour {
    public static BailManager Instance;

    public void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    
    public enum FAMILY_MEMBER {
        SPOUSE,
        CHILD_ONE,
        CHILD_TWO
    }
    
    public bool SpouseBailed { get; private set; }
    public bool ChildOneBailed { get; private set; }
    public bool ChildTwoBailed { get; private set; }

    public void BailOut(FAMILY_MEMBER familyMember) {
        switch (familyMember) {
            case FAMILY_MEMBER.SPOUSE:
                SpouseBailed = true;
                break;
            case FAMILY_MEMBER.CHILD_ONE:
                ChildOneBailed = true;
                break;
            case FAMILY_MEMBER.CHILD_TWO:
                ChildTwoBailed = true;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(familyMember), familyMember, null);
        }
    }
}