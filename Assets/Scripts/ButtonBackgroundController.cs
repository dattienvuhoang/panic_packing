using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBackgroundController : MonoBehaviour
{
   public int index;
   public bool isLocked, isUse;
   public GameObject imgFrame, imgLock;
   private void Start()
   {
      if (isLocked)
      {
         imgLock.SetActive(true);
      }
      else
      {
         imgLock.SetActive(false);
      }

      if (isUse)
      {
         imgFrame.SetActive(true);
      }
      else
      {
         imgFrame.SetActive(false);
      }
   }
}
