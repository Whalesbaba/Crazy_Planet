using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   public GameObject meelee;

   //public TMPro.TextMeshProUGUI finalKillsText;
   public TMPro.TextMeshProUGUI killsText;

   public GameObject obeliskKeyActivation;

   public static int kills;

   public GameObject menuPanel;
   bool _pause = false;

   private void Start()
   {
      Cursor.visible = true;
      Screen.lockCursor = false;
      obeliskKeyActivation.SetActive(false);
     // finalKillsText.text = "" + PlayerScript.enemyKills;
      killsText.text = "" + PlayerScript.enemyKills;
      Time.timeScale = 1;
   }
   private void Update()
   {

      kills = PlayerScript.enemyKills;
      if (PlayerScript.enemyKills >= 2)
      {
         obeliskKeyActivation.SetActive(true);
      }
      //finalKillsText.text = "" + PlayerScript.enemyKills;
      killsText.text = "" + PlayerScript.enemyKills;

      if (Input.GetKeyDown(KeyCode.Escape))
      {

         if (_pause == false)
         {
            menuPanel.SetActive(false);
            Time.timeScale = 1;
            _pause = true;
         }
         else if (_pause == true)
         {
            menuPanel.SetActive(true);
            Time.timeScale = 0;
            _pause = false;

         }

      }

      if (PlayerScript.playerIsAlive == false)
      {
         menuPanel.SetActive(true);
         Time.timeScale = 0;
      }
   }
   public void Restart()
   {


      PlayerScript.enemyKills = 0;
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
   }

   public void NextLevel()
   {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   }

   public void MainMenu()
   {
      Time.timeScale = 1;
      SceneManager.LoadScene(0);
   }
   public void ExitGame()
   {
      SceneManager.LoadScene(0);
   }

}
