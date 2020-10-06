using UnityEngine;

public class PermissionHelper
{
    /// <summary>
    /// Androidパーミッション許可と設定
    ///  Usage : PermissionHelper.CheckPermission("android.permission.ACCESS_COARSE_LOCATION");
    /// </summary>
    public static bool CheckPermission(string permission, bool checkOnly = false)
    {
#if UNITY_ANDROID
        using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        using (var activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
        using (var compat = new AndroidJavaClass("android.support.v4.app.ActivityCompat"))
        {
            var check = compat.CallStatic<int>("checkSelfPermission", activity, permission);

            if (check == 0)
            {
                return true;
            }

            if (!checkOnly)
            {
                int REQUEST_CODE = 1;
                compat.CallStatic("requestPermissions", activity, new string[] {
                    permission
                }, REQUEST_CODE);

                //再チェック
                check = compat.CallStatic<int>("checkSelfPermission", activity, permission);
                if (check == 0)
                {
                    return true;
                }
            }

            // "設定からパーミッションを許可してください。機能が使用できません。";
        }
#endif
        return false;
    }
}
