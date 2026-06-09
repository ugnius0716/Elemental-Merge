using UnityEngine;

public class tower : MonoBehaviour
{
    private Transform target;
    private Enemy targetEnemy;

    [ Header("General")]

    public float range = 5f;

    [Header("Use Bullets(default)")]

    public float fireRate = 1f;
    private float fireCountdown = 0f;
    public GameObject ProjectilePrefab;

    
    [Header("Use Laser")]
    public bool useLaser = false;
    public int damageOverTime = 30;
    public float slowPercent = 0.7f;

    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";


    
    public Transform firePoint;

    
    void Update()
    {
        LockOnTarget();

        if (target == null)
        {
            if (useLaser && lineRenderer.enabled) { 
                lineRenderer.enabled = false;
                impactEffect.Stop();
                
            }
            return;
        }

        if (useLaser)
        {
            Laser();
        }
        else
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
            fireCountdown -= Time.deltaTime;
        }
       
    }
    void LockOnTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float closestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                closestEnemy = enemy;
            }

        }

        if (closestEnemy != null && closestDistance <= range)
        {
            target = closestEnemy.transform;
            targetEnemy = closestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }

    }
    void Laser()
    {
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        targetEnemy.Slow(slowPercent);
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
        }
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;

        impactEffect.transform.position = target.position+ dir.normalized;

        impactEffect.transform.rotation = Quaternion.LookRotation(dir);

        

    }
    void Shoot()
    {
        GameObject arrowGO = (GameObject)Instantiate(ProjectilePrefab, firePoint.position, firePoint.rotation);
        projectile arrow = arrowGO.GetComponent<projectile>();

        if (arrow != null)
        {
            arrow.Seek(target);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
