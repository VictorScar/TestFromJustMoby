using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCubeHandler : MonoBehaviour
{
   private TowerCube _model;
   private TowerCubeView _view;

   public TowerCubeHandler(TowerCube model, TowerCubeView view)
   {
      _model = model;
      _view = view;
   }
}
