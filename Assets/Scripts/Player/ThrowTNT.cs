using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowTNT : MonoBehaviour
{
    public Transform TNT;
    public Slider chargeDisplay;

    private Movement playerMovement;
    private SpriteRenderer spriteRenderer;

    private Vector3 cursorWorldPosition = Vector3.zero;
    private Vector3 oldCursorWorldPosition = Vector3.zero;

    private float throwVelocity = 0;
    private float maxThrowVelocity = 20;

    private bool chargingThrow = false;
    private bool reloading = false;

    private float reloadTime = 0.5f;

    void Start()
    {
        playerMovement = GameMemory.getPlayer().GetComponent<Movement>();
        chargeDisplay.maxValue = maxThrowVelocity;
        spriteRenderer = GetComponent<SpriteRenderer>();

        endChargeUp();
    }

    private void FixedUpdate()
    {
        oldCursorWorldPosition = cursorWorldPosition;
        if (chargingThrow)
        {
            throwVelocity += 0.3f;
            throwVelocity = Mathf.Clamp(throwVelocity, 0, maxThrowVelocity);
            chargeDisplay.value = throwVelocity;
        }
        moveChargeDisplay();
    }

    void Update()
    {
        cursorWorldPosition = GameMemory.currentCamera.ScreenToWorldPoint(Input.mousePosition);

        if (chargingThrow && cursorWorldPosition.x > transform.position.x) playerMovement.facing = true;
        else if(chargingThrow) playerMovement.facing = false;

        rotateArm();

        if(MyInput.getInputDown_ThrowTNT() && !reloading)
        {
            beginChargeUp();
        }

        if(MyInput.getInputUp_ThrowTNT() && !reloading)
        {
            reloading = true;
            Invoke("reload", reloadTime);

            Transform currentTNT = Instantiate(TNT, transform.position, Quaternion.Euler(0, 0, 90));
            currentTNT.GetComponent<Rigidbody2D>().AddForce((cursorWorldPosition - transform.position).normalized * throwVelocity, ForceMode2D.Impulse);

            endChargeUp();
        }
    }

    private void rotateArm()
    {
        transform.LookAt(cursorWorldPosition, Vector3.forward);

        float angle = -transform.rotation.eulerAngles.z + 180 * playerMovement.facing.ToByte();

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void moveChargeDisplay()
    {
        chargeDisplay.GetComponent<RectTransform>().position = GameMemory.currentCamera.WorldToScreenPoint(transform.position + Vector3.up);
    }

    private void beginChargeUp()
    {
        chargingThrow = true;
        chargeDisplay.gameObject.SetActive(true);
        spriteRenderer.enabled = true;
    }

    private void endChargeUp()
    {
        chargingThrow = false;
        throwVelocity = 0;
        chargeDisplay.value = 0;
        chargeDisplay.gameObject.SetActive(false);
        spriteRenderer.enabled = false;
    }

    private void reload()
    {
        reloading = false;
    }
}
