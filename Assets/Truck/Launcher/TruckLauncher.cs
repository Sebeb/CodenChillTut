using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckLauncher : MonoBehaviour
{
    public float reloadTime, aimingDeadzone, minVelocity, maxVelocity, fullChargeTime;
    public bool loaded = true, charging = false;
    public Transform loadedMissile;
    public RectTransform chargerMask;
    public float chargedPower;
    public GameObject missile;
    private int playerNumber;

    void Start()
    {
        playerNumber = GetComponentInParent<TruckDriver>().playerNumber;
    }

    void Update()
    {
        if ((Input.GetAxisRaw("Fire Trigger " + playerNumber) > 0 || Input.GetButtonDown("Fire Button " + playerNumber)) && loaded)
        {
            Charge();
        }
        else if (charging)
        {
            Fire();
        }

        Aim();
    }

    private void Aim()
    {
        Vector2 aimVector = new Vector2(Input.GetAxisRaw("Aim Horizontal " + playerNumber), Input.GetAxisRaw("Aim Vertical " + playerNumber));

        if (aimVector.magnitude < aimingDeadzone) return;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Vector2.SignedAngle(Vector2.up, aimVector) * -1));
    }

    private void Charge()
    {
        charging = true;
        chargedPower = Mathf.Clamp01(chargedPower + (Time.deltaTime / reloadTime));
        chargerMask.transform.localScale = new Vector3(chargedPower, 1, 1);
    }

    private void Fire()
    {
        charging = false;
        loaded = false;
        
        GameObject launchedMissile = Instantiate(missile, loadedMissile.position, loadedMissile.rotation);
        launchedMissile.GetComponent<Rigidbody2D>().velocity = transform.up * Mathf.Lerp(minVelocity, maxVelocity, chargedPower);
        print(launchedMissile.GetComponent<Rigidbody2D>().velocity);
        
        loadedMissile.gameObject.SetActive(false);

        chargedPower = 0;
        chargerMask.transform.localScale = new Vector3(0, 1, 1);

        StartCoroutine(Reload());

    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);

        loaded = true;
        loadedMissile.gameObject.SetActive(true);

    }
}