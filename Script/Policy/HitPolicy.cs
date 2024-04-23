
using App.Enum;
using App.Object;
using System.Collections.Generic;

namespace App.Policy
{
    public struct HitPolicy
    {
        public readonly HitAbleObject hitAbleObject;
        public readonly EHitType hitType;
        public readonly List<HitAction> actions;

        public HitPolicy(HitAbleObject hitAbleObject, EHitType hitType, List<HitAction> actions)
        {
            this.hitAbleObject = hitAbleObject;
            this.hitType = hitType;
            this.actions = actions;
        }
    }

    public struct HitAction
    {
        public readonly EHitAction action;
        public readonly int parameter;

        public HitAction(EHitAction action, int parameter) : this()
        {
            this.action = action;
            this.parameter = parameter;
        }
    }
}