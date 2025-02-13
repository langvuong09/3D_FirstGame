using System;

using UnityEngine;
[AddComponentMenu("TienCuong/BulletDamageEnemy")]
public class BulletDamageEnemy : MonoBehaviour
{
    public GameObject fx;
    public GameObject explusionPrefabs;
    public int bulletDamage = 20;
    private void OnCollisionEnter(Collision objectHit)
    {
        if(objectHit != null)
        {
            if (objectHit.collider.CompareTag("Wall"))
            {
                //Debug.Log("Va cham tuong");
                CreateBulletImpactEffect(objectHit);
                Destroy(gameObject);
            }
            if (objectHit.collider.CompareTag("Box"))
            {
                //Debug.Log("Va cham tuong");
                CreateExplusionEffect(objectHit);
                Destroy(gameObject);
            }
        }
    }

    private void CreateExplusionEffect(Collision objectHit)
    {
        Instantiate(explusionPrefabs, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void CreateBulletImpactEffect(Collision objectHit)
    {
        ContactPoint contactPoint = objectHit.contacts[0];
        GameObject hole = Instantiate(fx, contactPoint.point, Quaternion.LookRotation(contactPoint.normal));
        hole.transform.SetParent(objectHit.gameObject.transform);
    }
}
