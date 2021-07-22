using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;


public class AdScript : MonoBehaviour,   IUnityAdsListener
{
    
#if UNITY_IOS
    private string gameId = "3745182";
#elif UNITY_ANDROID
    private string gameId = "3745183";
#endif


    public string banner = "banner";
    public string rewarded = "rewardedVideo";
    public string normalAd = "video";
    public string nextLevelAd = "passedLevelAd";

    public PlayerMove player;


    void Start()
    {
       
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, false);
        
        

    }
    

    public void StartBanner()
    {
        StartCoroutine(ShowBannerWhenInitialized());
    }

    public void HideBanner()
    {
        Advertisement.Banner.Hide();
    }
     IEnumerator ShowBannerWhenInitialized()
    {
        while (!Advertisement.isInitialized)
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Show(banner);
    }


    public void PassedLevelAd()
    {
        if (Advertisement.IsReady(nextLevelAd))
        {

            Advertisement.Show(nextLevelAd);
        }
        else
        {
            player.AdEnterLevel(player.level + 1);
        }

    }
    public void ShowNormalAd()
    {
        if (Advertisement.IsReady(normalAd))
        {

            Advertisement.Show(normalAd);
        }
        else
        {
            Time.timeScale = 1f;
            player.diamonds = player.tempDiamonds;
            player.bulletCount = (int)player.tempBullets;
            player.usableHearts = (int)player.tempHearts;
            player.diedTimes = 0;

            player.lives = 6f;
            player.AdRessurect();
            SaveSystem.Save(player);
            player.AdEnterLevel(player.level);
        }

    }

    public void ShowRewardedAd()
    {
        if (Advertisement.IsReady(rewarded))
        {
            
            Advertisement.Show(rewarded);
        }
        else
        {
            player.diedTimes=2;
            player.AdRessurect();
        }
    }

    public void OnUnityAdsDidFinish(string surfacingId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            if(surfacingId==rewarded)
            {
                player.diedTimes--;
                player.AdRessurect();
            }
            else if(surfacingId==normalAd)
            {
                Time.timeScale = 1f;
                player.diamonds = player.tempDiamonds;
                player.bulletCount = (int)player.tempBullets;
                player.usableHearts = (int)player.tempHearts;
                player.diedTimes = 0;
               
                player.lives = 6f;
                player.AdRessurect();
                SaveSystem.Save(player);
                player.AdEnterLevel(player.level);
            }
            else if(surfacingId== nextLevelAd)
            {
                player.AdEnterLevel(player.level + 1);
            }
        }
        else if (showResult == ShowResult.Skipped)
        {
            if (surfacingId == rewarded)
            {
                player.diedTimes--;
                player.AdRessurect();
            }
            else if (surfacingId == normalAd)
            {
                Time.timeScale = 1f;
                player.diamonds = player.tempDiamonds;
                player.bulletCount = (int)player.tempBullets;
                player.usableHearts = (int)player.tempHearts;
                player.diedTimes = 0;
                player.lives = 6f;
                player.AdRessurect();
                SaveSystem.Save(player);
                player.AdEnterLevel(player.level);
            }
            else if (surfacingId == nextLevelAd)
            {
                player.AdEnterLevel(player.level + 1);
            }

        }
        else if (showResult == ShowResult.Failed)
        {
            if (surfacingId == rewarded)
            {
                player.diedTimes--;
                player.AdRessurect();
            }
            else if (surfacingId == normalAd)
            {
                Time.timeScale = 1f;
                player.diamonds = player.tempDiamonds;
                player.bulletCount = (int)player.tempBullets;
                player.usableHearts = (int)player.tempHearts;
                player.diedTimes = 0;

                player.lives = 6f;
                player.AdRessurect();
                SaveSystem.Save(player);
                player.AdEnterLevel(player.level);
            }
            else if (surfacingId == nextLevelAd)
            {
                player.AdEnterLevel(player.level + 1);
            }
        }
    }
    

    public void OnUnityAdsReady(string placementId)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidError(string message)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        //throw new System.NotImplementedException();
    }
}
