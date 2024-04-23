using UnityEngine;

namespace App.Object
{
    public class CurveBullet : Bullet
    {
        [SerializeField] private Transform Target;
        Vector3 offset;

        protected override void Start()
        {
            base.Start();
            if (Target)
            {
                targetPosition = Target.position;
                GetLifeTime();
            }
            offset = GetMidPoint(startPosition, targetPosition, Vector3.Distance(startPosition, targetPosition));
        }

        protected override void GetLifeTime()
        {
            if (Target)
                lifeTime = Vector3.Distance(startPosition, targetPosition) / moveSpeed;
            else
                base.GetLifeTime();
        }

        protected override void UpdatePosition()
        {
            transform.position = CalculateBezierPoint(movedTime / lifeTime, startPosition, targetPosition, offset);
        }

        private Vector3 CalculateBezierPoint(float time, Vector3 startPosition, Vector3 endPosition, Vector3 controlPoint)
        {
            float u = 1 - time;
            float uu = u * u;

            Vector3 point = uu * startPosition;
            point += 2 * u * time * controlPoint;
            point += time * time * endPosition;

            return point;
        }

        Vector3 GetMidPoint(Vector3 pos1, Vector3 pos2, float offset)
        {
            Vector3 dir = (pos2 - pos1).normalized;
            Vector3 perpDir = Vector3.Cross(dir, Vector3.back);
            Vector3 midPoint = (pos1 + pos2) / 2f;
            Vector3 offsetPoint = midPoint + (perpDir * offset);

            offsetPoint += Vector3.up * Mathf.Clamp(1,10, (Vector3.Distance(pos1, pos2) / moveSpeed));


            return offsetPoint;
        }
    }
}