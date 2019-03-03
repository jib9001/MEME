using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class EnemyScript : MonoBehaviour
{
    GameObject player;
    FirstPersonController firstPerson;
    UnityEngine.AI.NavMeshAgent nav;
    public float range = 15.0f;
    RaycastHit hit;

    public int startingHealth = 100;
    public int currentHealth;

    public Image image;

    //public Texture myTexture;
    //bool bossDead;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();

        firstPerson = player.GetComponent<FirstPersonController>();

        currentHealth = startingHealth;

        if (gameObject.tag == "Boss")
        {
            image.enabled = false;
        }
    }

    public void takeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            if (gameObject.tag == "Boss")
            {
                image.enabled = true;
            }
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            firstPerson.takeDamage(5);
        }
    }

    // Update is called once per frame
    void Update()
    {
        var rayDirection = player.transform.position - transform.position;
        Physics.Raycast(transform.position, rayDirection, out hit);
        if (Vector3.Distance(player.transform.position, transform.position) <= range && hit.transform == player.transform)
        {
            nav.SetDestination(player.transform.position);
        }
    }
}
