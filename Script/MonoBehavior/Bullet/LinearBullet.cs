using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Object
{
    public class LinearBullet : Bullet
    {
        protected override void UpdatePosition()
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, movedTime / lifeTime);
        }
    }
}