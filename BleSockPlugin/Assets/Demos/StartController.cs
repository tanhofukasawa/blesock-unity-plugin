using System;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartController : MonoBehaviour
{
    [Serializable]
    public class ButtonInfo
    {
        public Button button;
        public string sceneName;
    }

    public ButtonInfo[] buttonInfos;


    private void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        PermissionHelper.CheckPermission("android.permission.ACCESS_FINE_LOCATION");
#endif

        foreach (var info in buttonInfos)
        {
            info.button.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(info.sceneName);
            });
        }

        buttonInfos[0].button.GetComponent<Selectable>().Select();
    }
}
