using UnityEngine;

public class TagMechanics : MonoBehaviour
{
    public bool isTagged = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            Debug.Log("Players are touching");
            if (isTagged)
            {
                isTagged = false;
                other.GetComponent<TagMechanics>().isTagged = true;
                Debug.Log($"{other.gameObject.name} is now tagged!");
            }
        }
    }
}
