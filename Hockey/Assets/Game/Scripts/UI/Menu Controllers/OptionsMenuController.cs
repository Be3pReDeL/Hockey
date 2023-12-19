using System.Collections;
using UnityEngine;
using UnityEngine.iOS;
using UnityEngine.Networking;
using UniWebViewNamespace;
using UnityEngine.UI;

public class OptionsMenuController : UIController
{
    [SerializeField] private float _duration = 0.3f;
    [SerializeField] private GameObject _privacyPolicyWebView;

    [SerializeField] private string _privacyPolicyURL;
    [SerializeField] private string _appID;

    [SerializeField] private Slider _musicSlider, _vibrationSlider;

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void OnEnable()
    {
        for (int i = 0; i < _tweenObjects.Count; i++)
        {
            _tweenObjects[i].Appear(_duration);
        }
    }

    private void Start(){
        _musicSlider.value =  1 - PlayerPrefs.GetInt("Mute", 0);
        _vibrationSlider.value = PlayerPrefs.GetInt("Vibrate", 0);
    }

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void Back()
    {
        for(int i = 0; i < _tweenObjects.Count; i++)
        {
            _tweenObjects[i].Disappear(_duration);
        }
    }

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void OnMusicSliderChangeValue(){
        if(_musicSlider.value >= 0.7f) {
            _musicSlider.value = 1f;

            PlayerPrefs.SetInt("Mute", 0);
        }

        else if(_musicSlider.value <= 0.3f){
            _musicSlider.value = 0f;

            PlayerPrefs.SetInt("Mute", 1);
        }

        AudioListener.volume = 1 - PlayerPrefs.GetInt("Mute", 0);
    }

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void OnVibrationSliderChangeValue(){
        if(_vibrationSlider.value >= 0.7f) {
            _vibrationSlider.value = 1f;

            PlayerPrefs.SetInt("Vibrate", 1);
        }

        else if(_vibrationSlider.value <= 0.3f){
            _vibrationSlider.value = 0f;

            PlayerPrefs.SetInt("Vibrate", 0);
        }
    }

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void ShareButton(){
        new NativeShare()
			.SetSubject("Plinker!").SetText("Check this cool Plinker game!").SetUrl("https://apps.apple.com/us/app/id" + _appID)
			.SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
			.Share();
    }

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void RateUs(){
        Device.RequestStoreReview();
    }

    [OPS.Obfuscator.Attribute.DoNotRename]
    public void PrivacyPolicy(){
        GameObject webView = Instantiate(_privacyPolicyWebView);

        UniWebView uniWebView = webView.GetComponent<UniWebView>();

        StartCoroutine(GetText(uniWebView));
    }

    [OPS.Obfuscator.Attribute.DoNotRename]
    private IEnumerator GetText(UniWebView uniWebView) {
        UnityWebRequest www = UnityWebRequest.Get(_privacyPolicyURL);
        yield return www.SendWebRequest();
 
        if (www.result != UnityWebRequest.Result.Success)
            Debug.Log(www.error);

        else {
            string pageText = www.downloadHandler.text.Replace("\"", "");

            uniWebView.Show();
            uniWebView.Load(pageText);
        }
    }
}
