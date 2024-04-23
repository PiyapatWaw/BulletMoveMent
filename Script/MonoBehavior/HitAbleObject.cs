using App.Enum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Object
{

    public class HitAbleObject : MonoBehaviour
    {
        [SerializeField] private EHitType type;
        public EHitType HitType => type;
    }
}