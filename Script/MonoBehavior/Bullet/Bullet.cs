using App.Enum;
using App.Interface;
using App.Policy;
using System.Collections.Generic;
using UnityEngine;


namespace App.Object
{

    public abstract class Bullet : MonoBehaviour, IMovement
    {
        [SerializeField] protected float moveSpeed;
        protected Vector3 startPosition;
        protected Vector3 targetPosition;
        protected float movedTime = 0;
        protected float lifeTime;

        protected virtual void Start()
        {
            startPosition = this.transform.position;
            GetLifeTime();
            targetPosition = transform.position + transform.forward * (moveSpeed * lifeTime);
        }

        protected virtual void GetLifeTime()
        {
            lifeTime = 10;
        }

        private void FixedUpdate()
        {
            Move();
        }

        public virtual void Move()
        {
            var hitPolicies = OnMove();

            if (hitPolicies.Count > 0)
            {
                ExecutePolices(hitPolicies);
            }

            if (movedTime >= lifeTime)
            {
                Destroy();
            }
            else
            {
                UpdatePosition();
            }
            movedTime += Time.deltaTime;
        }

        protected abstract void UpdatePosition();

        protected virtual List<HitPolicy> OnMove()
        {
            var hits = Physics.OverlapSphere(transform.position,1);
            List<HitPolicy> policies = new List<HitPolicy>();
            foreach (var item in hits)
            {
                if(item.TryGetComponent<HitAbleObject>(out var hitable))
                {
                    List<HitAction> actions = new List<HitAction>();
                    actions.Add(new HitAction(EHitAction.Destroy,(int)EDestroy.Self));

                    policies.Add(new HitPolicy(hitable, hitable.HitType, actions));
                }
            }

            return policies;
        }

        protected virtual void Destroy()
        {
            Destroy(this.gameObject);
        }

        protected virtual void ExecutePolices(List<HitPolicy> policies)
        {
            foreach (var policy in policies)
            {
                foreach (var action in policy.actions)
                {
                    if(action.action == EHitAction.Destroy)
                    {
                        if(action.parameter == (int)EDestroy.Self)
                        {
                            Destroy();
                        }
                        else if (action.parameter == (int)EDestroy.Target)
                        {
                            Destroy(policy.hitAbleObject);
                        }
                    }
                    else if(action.action == EHitAction.TakeDamage)
                    {
                        // do take damage
                    }
                }
            }
        }
    }
}
