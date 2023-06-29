using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed;
    private float attackSize;
    private float attackSpeed;
    private float attackCooldown;
    private float attackMaxDistance;
    private float attackDamage;
    private float currentCooldown = 0f;
    private int playerLevel = 1;
    private float playerExperience = 0f;

    public GameObject attackPrefab;
    public Rigidbody2D rb;
    public Transform shootPoint;

    [SerializeField] private Slider experienceSlider;
    [SerializeField] private Text levelText;

    [SerializeField] private Text moveSpeedText;
    [SerializeField] private Text attackSizeText;
    [SerializeField] private Text attackSpeedText;
    [SerializeField] private Text attackCooldownText;
    [SerializeField] private Text attackMaxDistanceText;
    [SerializeField] private Text attackDamageText;

    private List<GameObject> activeAttacks = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        UpdateAttributes();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical).normalized;
        rb.velocity = movement * moveSpeed;
    }

    void Update()
    {
        experienceSlider.value = playerExperience / (100f * playerLevel);
        levelText.text = playerLevel.ToString();

        // Get the mouse position in world coordinates
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (currentCooldown > 0f)
        {
            currentCooldown -= Time.deltaTime;
        }

        if (Input.GetMouseButton(0) && currentCooldown <= 0f)
        {
            // Calculate the direction from the player to the mouse position
            Vector3 shotDirection = (mousePosition - transform.position);

            ShootAttack(shotDirection);
            currentCooldown = attackCooldown;
        }

        if (playerExperience >= 100 * playerLevel)
        {
            LevelUp();
        }
    }

    void ShootAttack(Vector3 direction)
    {
        // Instantiate the attack object at the shoot point
        GameObject attack = Instantiate(attackPrefab, shootPoint.position, Quaternion.identity);
        Rigidbody2D attackRB = attack.GetComponent<Rigidbody2D>();
        attack.transform.localScale *= attackSize;

        // Set the velocity of the attack to a constant value
        attackRB.velocity = new Vector2(direction.x, direction.y).normalized * attackSpeed;

        // Set the damage of the attack to player damage value
        Attack attackComponent = attack.GetComponent<Attack>();
        attackComponent.damageAmount = attackDamage;

        // Store the attack in the list of active attacks
        activeAttacks.Add(attack);

        // Start a timer to destroy the attack after a certain time
        StartCoroutine(DestroyAttackAfterTime(attack, attackMaxDistance / attackSpeed));
    }

    IEnumerator DestroyAttackAfterTime(GameObject attack, float time)
    {
        yield return new WaitForSeconds(time);

        // Check if the attack is still active (hasn't been destroyed by another means)
        if (attack != null && activeAttacks.Contains(attack))
        {
            Destroy(attack);
            activeAttacks.Remove(attack);
        }
    }

    private void LevelUp()
    {
        playerLevel += 1;
        UpdateAttributes();
        playerExperience = 0;
        Debug.Log("Player leveled up! " + playerLevel);
    }

    private void UpdateAttributes()
    {
        moveSpeed = 4f + (playerLevel / 10f);
        attackSize = 0.5f + playerLevel / 10f;
        attackSpeed = 7.5f + playerLevel / 10f;
        attackCooldown = 1f - playerLevel / 10f;
        attackMaxDistance = 10f + playerLevel;
        attackDamage = 10f * playerLevel;

        moveSpeedText.text = moveSpeed.ToString();
        attackSizeText.text = attackSize.ToString();
        attackSpeedText.text = attackSpeed.ToString();
        attackCooldownText.text = attackCooldown.ToString();
        attackMaxDistanceText.text = attackMaxDistance.ToString();
        attackDamageText.text = attackDamage.ToString();
    }

    public void EnemyHit()
    {
        playerExperience += 20f;
        Debug.Log(playerExperience);
    }

    public void EnemyKilled()
    {
        playerExperience += 50f;
    }
}
