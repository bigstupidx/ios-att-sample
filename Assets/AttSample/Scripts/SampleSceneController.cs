using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace AttSample.Scripts {
public class SampleSceneController : MonoBehaviour {

    public Text attStatus;
    public Text idfa;

    private void Start()
    {
        RefreshATTStatus();
    }

    public void ClickRequest()
    {
        StartCoroutine(RequestATT());
    }

    public void RefreshATTStatus()
    {
        Debug.Log("ATT:" + AttPlugin.GetTrackingAuthorizationStatus());
        attStatus.text = "ATT: " + AttPlugin.GetTrackingAuthorizationStatus();
        idfa.text = "idfa\n" + UnityEngine.iOS.Device.advertisingIdentifier;
    }

    private IEnumerator RequestATT()
    {
#if UNITY_EDITOR || UNITY_IOS
        // iOSのATT対応
        if (AttPlugin.IsNotDetermined()) {
            // TODO ATTダイアログの前に独自ダイアログを表示したい場合は、ここに書く
            // ATTダイアログのポップアップ
            yield return AttPlugin.RequestTrackingAuthorization();
        } else
            yield return AttPlugin.RequestTrackingAuthorization();
#endif
        RefreshATTStatus();
        // TODO ATTダイアログの表示が終わったら、広告SDKをイニシャライズ
    }
}
}
