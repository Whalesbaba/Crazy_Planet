using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
   public void PlaySE(AudioClip clip)
   {
      Manager.instance.managerSE.SeAudio.PlayOneShot(clip);
   }
}
