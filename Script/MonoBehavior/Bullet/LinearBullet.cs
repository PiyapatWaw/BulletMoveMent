using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Object
{
    public class LinearBullet : Bullet
    {
        protected override void UpdatePosition()
        {
            Vector3 position = Vector3.Lerp(startPosition, targetPosition, movedTime / lifeTime);
            Vector3 direction = position - transform.position;
            transform.forward = direction;
            transform.position = position;
        }
    }
}