using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCubeAnimator : MonoBehaviour
{
   [SerializeField] private CubeAnimation[] animations;

   public void PlayAnimation(RectTransform transform, CubeAnimationID animationID, Vector3 newPos)
   {
      if (TryGetAnimationByID(animationID, out var animation))
      {
         animation.Play(transform, newPos);
      }
   }

   private bool TryGetAnimationByID(CubeAnimationID id, out CubeAnimation cubeAnimation)
   {
      if (animations != null)
      {
         foreach (var animation in animations)
         {
            if (animation.AnimationID == id)
            {
               cubeAnimation = animation;
               return true;
            }
         }
      }

      cubeAnimation = null;
      return false;
   }
}
