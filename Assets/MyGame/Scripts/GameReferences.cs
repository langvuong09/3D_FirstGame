using UnityEngine;
[AddComponentMenu("TienCuong/GameRefences")]
public class GameRefences : MonoBehaviour
{
    private static GameRefences instance;
    public static GameRefences Instance
    {
        get => instance;
    }
    [Header("Variable FX Prefabs")]
    public GameObject fxBulletsPrefabs;
    public GameObject explutionPrefabs;
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
