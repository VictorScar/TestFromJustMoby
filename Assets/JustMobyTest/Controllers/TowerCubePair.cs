using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCubePair
{
   private TowerCube _model;
   private TowerCubeView _view;

   public TowerCubePair(TowerCube model, TowerCubeView view)
   {
      _model = model;
      _view = view;
   }

   public TowerCubeView View => _view;
   public TowerCube Model => _model;
}
