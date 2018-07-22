using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterProfile : MonoBehaviour {

    [SerializeField] public int charPerKey = 1;
    [SerializeField] public float critRatio = 0.05f;
    [SerializeField] public float failRatio = 0.05f;
    [SerializeField] public float freeCharPerSecond = 1f;
}
