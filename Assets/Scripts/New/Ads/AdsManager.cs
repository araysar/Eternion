using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour
{
    [SerializeField] private string adID = "Interstitial_Android";
    private Character _myPlayer;

    private void Start()
    {
        _myPlayer = GetComponentInParent<Character>();
        Advertisement.Initialize("4481855");
    }

    public IEnumerator WaitToShow()
    {
        while(!Advertisement.IsReady())
        {
            yield return new WaitForEndOfFrame();
        }
        ShowOptions options = new ShowOptions();
        options.resultCallback = ResultCallBack;

        Advertisement.Show(adID, options);
    }


    public void ResultCallBack(ShowResult _result)
    {
        if(_result == ShowResult.Finished && _myPlayer != null)
        {
            _myPlayer.currentHP = _myPlayer.maxHP;
            PlayerPrefs.SetFloat("PlayerHP", _myPlayer.currentHP);
            PlayerPrefs.Save();
            _myPlayer.UpdateLifeBar();
        }
    }
}
