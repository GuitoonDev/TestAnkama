using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    [SerializeField, EnumFlag("Target Bonus")] private PlayerBonus targetBonus = default(PlayerBonus);
    [SerializeField] private float timeDuration = 5f;

    public PlayerBonus TargetBonus => targetBonus;
    public float TimeDuration => timeDuration;
}
