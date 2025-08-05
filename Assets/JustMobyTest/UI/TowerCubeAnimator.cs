using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using ScarFramework.UI;
using UnityEngine;

public class TowerCubeAnimator : MonoBehaviour
{
   [SerializeField] private CubeAnimation[] animations;

   public Tween PlayAnimation(UIView view, CubeAnimationID animationID, Vector3 newPos)
   {
      if (TryGetAnimationByID(animationID, out var animation))
      {
         return animation.Play(view, newPos);
      }

      return null;
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
