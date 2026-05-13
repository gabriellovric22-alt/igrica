using UnityEngine;

public class PlayerCont : MonoBehaviour 
{
    private CharacterController controller;
    private Vector3 moveDirection;
    private int desiredLane = 1; // 0: lijevo, 1: sredina, 2: desno

    [Header("Postavke kretanja")]
    public float laneDistance = 4f; // Razmak između traka
    public float jumpForce = 12f;
    public float Gravity = -30f;
    [SerializeField] private float forwardSpeed = 8f;
    [SerializeField] private float laneChangeSpeed = 10f;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (controller == null)
        {
            Debug.LogError("Na ovom objektu nedostaje CharacterController komponenta!");
        }
    }

    void Update()
    {
        if (controller == null) return;

        // 1. KRETANJE NAPRIJED
        moveDirection.z = forwardSpeed;

        // 2. GRAVITACIJA I SKOK
        if (controller.isGrounded)
        {
            moveDirection.y = -1f; // Lagani pritisak prema dolje za stabilnost

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                moveDirection.y = jumpForce;
            }
        }
        else
        {
            // Primjenjujemo gravitaciju samo dok smo u zraku
            moveDirection.y += Gravity * Time.deltaTime;
        }

        // 3. PROMJENA TRAKA (Lanes)
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            desiredLane++;
            if (desiredLane == 3) desiredLane = 2;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            desiredLane--;
            if (desiredLane == -1) desiredLane = 0;
        }

        // Izračunaj ciljanu X poziciju
        float targetX = 0;
        if (desiredLane == 0) targetX = -laneDistance;
        else if (desiredLane == 2) targetX = laneDistance;

        // Izračunaj koliko se moramo pomaknuti lijevo/desno (X os)
        float xDiff = targetX - transform.position.x;
        float xMove = xDiff * laneChangeSpeed;

        // 4. FINALNO POMICANJE KROZ CHARACTER CONTROLLER
        // Sklapamo X, Y i Z u jedan vektor pomaka
        Vector3 finalMove = new Vector3(xMove, moveDirection.y, moveDirection.z);
        
        // Pomicanje (multipliciramo s Time.deltaTime da brzina bude neovisna o FPS-u)
        controller.Move(finalMove * Time.deltaTime);
    }
}