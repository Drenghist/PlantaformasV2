using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class HeroKnightlvl2 : MonoBehaviour {

    [SerializeField] float      m_speed = 4.0f;
    [SerializeField] float      m_jumpForce = 12f;
    [SerializeField] float      m_rollForce = 6.0f;
    [SerializeField] bool       m_noBlood = false;
    [SerializeField] GameObject m_slideDust;

    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private Sensor_HeroKnightlvl2   m_groundSensor;
    private Sensor_HeroKnightlvl2   m_wallSensorR1;
    private Sensor_HeroKnightlvl2   m_wallSensorR2;
    private Sensor_HeroKnightlvl2   m_wallSensorL1;
    private Sensor_HeroKnightlvl2   m_wallSensorL2;
    private bool                m_isWallSliding = false;
    private bool                m_grounded = false;
    private bool                m_rolling = false;
    private int                 m_facingDirection = 1;
    private int                 m_currentAttack = 0;
    private float               m_timeSinceAttack = 0.0f;
    private float               m_delayToIdle = 0.0f;
    private float               m_rollDuration = 8.0f / 14.0f;
    private float               m_rollCurrentTime;
    private bool isDeath;
    public AudioClip sonidoSalto;
    public AudioClip sonidoMoneda;
    public AudioClip sonidoDano;
    public AudioClip sonidoMuerte;
    public AudioClip sonidoAtaque;
    private AudioSource audioSource;
    public float coyoteTime = 0.4f;
    private float coyoteTimer;

    // Use this for initialization
    void Start ()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnightlvl2>();
        m_wallSensorR1 = transform.Find("WallSensor_R1").GetComponent<Sensor_HeroKnightlvl2>();
        m_wallSensorR2 = transform.Find("WallSensor_R2").GetComponent<Sensor_HeroKnightlvl2>();
        m_wallSensorL1 = transform.Find("WallSensor_L1").GetComponent<Sensor_HeroKnightlvl2>();
        m_wallSensorL2 = transform.Find("WallSensor_L2").GetComponent<Sensor_HeroKnightlvl2>();

        //Booleano para que no pueda hacer nada una vez muerto
        isDeath = false;

        //creo el audioSource
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update ()
    {
        
        // Increase timer that controls attack combo
        m_timeSinceAttack += Time.deltaTime;

        // Increase timer that checks roll duration
        if(m_rolling)
            m_rollCurrentTime += Time.deltaTime;

        // Disable rolling if timer extends duration
        if(m_rollCurrentTime > m_rollDuration)
            m_rolling = false;

        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");

        // Swap direction of sprite depending on walk direction
        if (inputX > 0 && !isDeath)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            m_facingDirection = 1;
        }
            
        else if (inputX < 0 && !isDeath)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            m_facingDirection = -1;
        }

        // Move
        if (!m_rolling && !isDeath )
            m_body2d.linearVelocity = new Vector2(inputX * m_speed, m_body2d.linearVelocity.y);

        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeedY", m_body2d.linearVelocity.y);

        // -- Handle Animations --
        //Wall Slide
        m_isWallSliding = (m_wallSensorR1.State() && m_wallSensorR2.State()) || (m_wallSensorL1.State() && m_wallSensorL2.State());
        m_animator.SetBool("WallSlide", m_isWallSliding);
        /*
        //Death
        if (Input.GetKeyDown("e") && !m_rolling)
        {
            m_animator.SetBool("noBlood", m_noBlood);
            m_animator.SetTrigger("Death");
        }*/
            
        /*//Hurt 
        else if (Input.GetKeyDown("q") && !m_rolling)
            m_animator.SetTrigger("Hurt"); */

        //Attack
        if(Input.GetMouseButtonDown(0) && m_timeSinceAttack > 0.25f && !m_rolling && !isDeath)
        {
            m_currentAttack++;

            // Loop back to one after third attack
            if (m_currentAttack > 3)
                m_currentAttack = 1;

            // Reset Attack combo if time since last attack is too large
            if (m_timeSinceAttack > 1.0f)
                m_currentAttack = 1;

            // Call one of three attack animations "Attack1", "Attack2", "Attack3"
            m_animator.SetTrigger("Attack" + m_currentAttack);

            // Reset timer
            m_timeSinceAttack = 0.0f;

            if (sonidoSalto != null && audioSource != null)
            {
                audioSource.PlayOneShot(sonidoAtaque);
            }
        }

        // Block
        /*
         * 
         * Anulo la posibilidad de bloquear y rodar
         * 
         *   else if (Input.GetMouseButtonDown(1) && !m_rolling && !isDeath)
         *   {
         *       m_animator.SetTrigger("Block");
         *       m_animator.SetBool("IdleBlock", true);
         *   }
         *
         *   else if (Input.GetMouseButtonUp(1))
         *       m_animator.SetBool("IdleBlock", false);
         *
         *   // Roll
         *   else if (Input.GetKeyDown("left shift") && !m_rolling && !m_isWallSliding && !isDeath)
         *   {
         *       m_rolling = true;
         *       m_animator.SetTrigger("Roll");
         *       m_body2d.linearVelocity = new Vector2(m_facingDirection * m_rollForce, m_body2d.linearVelocity.y);
         *   }
         *
         */


        //Jump con CoyoteTimer

        if (m_grounded)
        {
            coyoteTimer = coyoteTime; // reinicia el tiempo si está en el suelo
        }
        else
        {
            coyoteTimer -= Time.deltaTime;
        }

        // Salto (se permite si aún está dentro del tiempo de coyote)
        if (Input.GetKeyDown("space") && coyoteTimer > 0 && !m_rolling && !isDeath)
        {
            m_animator.SetTrigger("Jump");
            //m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.linearVelocity = new Vector2(m_body2d.linearVelocity.x, m_jumpForce);
            m_groundSensor.Disable(0.2f);
            if (sonidoSalto != null && audioSource != null)
            {
                audioSource.PlayOneShot(sonidoSalto);
            }
            coyoteTimer = 0f; // para que no pueda volver a saltar
        }


        //Jump
        /*

        else if (Input.GetKeyDown("space") && m_grounded && !m_rolling && !isDeath)
        {
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.linearVelocity = new Vector2(m_body2d.linearVelocity.x, m_jumpForce);
            m_groundSensor.Disable(0.2f);
            if (sonidoSalto != null && audioSource != null)
            {
                audioSource.PlayOneShot(sonidoSalto);
            }
        }
        */

        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon && !isDeath)
        {
            // Reset timer
            m_delayToIdle = 0.05f;
            m_animator.SetInteger("AnimState", 1);
        }

        //Idle
        else
        {
            // Prevents flickering transitions to idle
            m_delayToIdle -= Time.deltaTime;
                if(m_delayToIdle < 0)
                    m_animator.SetInteger("AnimState", 0);
        }
    }

    //Creados por mi
    public void RecibeDaño()
    {
        m_animator.SetTrigger("Hurt");
        if (sonidoDano != null && audioSource != null)
        {
            audioSource.PlayOneShot(sonidoDano);
        }

    }

    public void Paralizate()
    {
        isDeath = true;
    }
    public void Muerete()
    {
        if (sonidoMuerte != null && audioSource != null && !isDeath)
        {
            audioSource.PlayOneShot(sonidoMuerte);
        }
        isDeath = true;
        

    }

    public void aniMuerte()
    {
        m_animator.SetTrigger("Death");
    }

    public void SonidoMoneda()
    {
        if (sonidoMoneda != null && audioSource != null)
        {
            audioSource.PlayOneShot(sonidoMoneda);
        }
    }



    // Animation Events
    // Called in slide animation.
    void AE_SlideDust()
    {
        Vector3 spawnPosition;

        if (m_facingDirection == 1)
            spawnPosition = m_wallSensorR2.transform.position;
        else
            spawnPosition = m_wallSensorL2.transform.position;

        if (m_slideDust != null)
        {
            // Set correct arrow spawn position
            GameObject dust = Instantiate(m_slideDust, spawnPosition, gameObject.transform.localRotation) as GameObject;
            // Turn arrow in correct direction
            dust.transform.localScale = new Vector3(m_facingDirection, 1, 1);
        }
    }
}
