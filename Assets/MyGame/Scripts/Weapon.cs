using System.Collections;

using UnityEngine;
[AddComponentMenu("TienCuong/Weapon")]
public class Weapon : MonoBehaviour
{
    public enum ShotingMode
    {
        Single,
        Burst,
        Auto
    }
    public enum WeaponModel
    {
        AK,
        Pistol
    }
    [Header("Bullets")]
    public Transform bulletsPawn;
    public GameObject bulletPrefabs;
    public float bulletVelocity = 100f;
    public float bulletPrefabsTime = 3f;
    public int magazineSize = 40;
    [Header("Fire Intensity")]
    [Range(0f, 5f)]
    public float spreadIntensity;
    [Range(0f, 2f)]
    public float shootingdelay = 0.5f;
    [Header("Weapon")]
    public WeaponModel thisWeaponModel;
    public ShotingMode currentShotingMode = ShotingMode.Auto;
    [Header("MuzerFlash")]
    public ParticleSystem muzlerFlash;
    [Header("Audio Weapon")]
    public AudioClip shootClip;
    public AudioClip reloadClip;
    private Camera cam;   
    private bool isShoting, readyToShoot;
    private int bulletLef;
    private float distanceBullet = 200f;
    private Animator anim;
    private int isShotingId;
    private int isReloadingId;
    private bool allowReset;
    void Start()
    {      
        cam = Camera.main;
        bulletLef = magazineSize;
        readyToShoot = true;
        allowReset = true;
        anim = GetComponent<Animator>();
        isShotingId = Animator.StringToHash("IsShoting");
        isReloadingId = Animator.StringToHash("IsReloadingCenter");
        CaculateDirectionAndSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentShotingMode == ShotingMode.Auto)
        {
            isShoting = Input.GetKey(KeyCode.Mouse0);// Nhận cả nhấn và giữ phím
        }
        else if((currentShotingMode == ShotingMode.Burst) || (currentShotingMode == ShotingMode.Single))
        {
            isShoting = Input.GetKeyDown(KeyCode.Mouse0);// Chỉ nhận nhấn phím không nhận giữ phím
        }
        if(isShoting && readyToShoot && bulletLef > 0) 
        { 
            FireWeapon();
        }
        if(Input.GetKeyDown(KeyCode.R) && bulletLef <= magazineSize) 
        {
            Reload();
        }
    }
    private void Reload()
    {
        anim.SetTrigger(isReloadingId);
        AudioManager.Instance.PlaysfxPlayer(reloadClip);
    }
    private void FireWeapon()
    {
        readyToShoot = false;
        muzlerFlash.Play();
        anim.SetTrigger(isShotingId);
        AudioManager.Instance.PlaysfxPlayer(shootClip);
        Vector3 shotingDirection = CaculateDirectionAndSpeed().normalized;
        GameObject bullet = Instantiate(bulletPrefabs,bulletsPawn.position, Quaternion.identity);
        bullet.transform.forward = shotingDirection;
        //bullet.GetComponent<Rigidbody>().AddForce(shotingDirection*bulletVelocity, ForceMode.Impulse); // câu này hoặc câu dưới đều được
        bullet.GetComponent<Rigidbody>().linearVelocity = shotingDirection*bulletVelocity*Time.deltaTime;
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabsTime));
        if (allowReset)
        {
            Invoke("ResetShot", shootingdelay);
            allowReset = false;
        }
        else
        {
            if(bulletLef > 0)
            {
                Invoke("FireWeapon", shootingdelay);
            }
        }
    }
    IEnumerator DestroyBulletAfterTime(GameObject bullet, float bulletTime)
    {
        yield return new WaitForSeconds(bulletTime);
        Destroy(bullet);
    }
    private Vector3 CaculateDirectionAndSpeed()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
        RaycastHit hit;
        Vector3 targetPoint = Vector3.zero;
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(distanceBullet);
        }
        Vector3 direction = targetPoint - bulletsPawn.position;
        float z = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        float y = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        return direction + new Vector3(0,y,z);
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowReset = true;
    }
}
