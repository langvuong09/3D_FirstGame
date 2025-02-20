using UnityEngine;
[AddComponentMenu("TienCuong/WeaponManage")]
public class WeaponManager : MonoBehaviour
{
    private static WeaponManager instance;
    public static WeaponManager Instance
    {
        get => instance;
    }
    private void Awake()
    {
        if(instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
    }
}
