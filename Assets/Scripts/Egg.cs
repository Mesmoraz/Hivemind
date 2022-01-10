using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    [SerializeField] private GameManager gameManager = default(GameManager);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.parent != null)
        {
            //Debug.Log("My parent is " + gameObject.transform.parent.name);
            // ProcessHeld();
        }
        

    }

    private void ProcessHeld()
    {
        switch(gameObject.transform.parent.tag)
        {
            case "Player":
                //transform.position = new Vector2(transform.localPosition.x, transform.localPosition.y);
                break;
            case "Enemy":
                //Debug.Log("coordinates of parent = " + transform.parent.position);
                //Debug.Log("coordinates of egg = " + transform.position);
                //transform.position = new Vector2(transform.localPosition.x, transform.localPosition.y);
                //Debug.Log("coordinates of local position = " + transform.localPosition);
                gameManager.enemyHasEgg = true;
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !gameManager.enemyHasEgg)
        {
            transform.SetParent(collision.transform, false);
            transform.position = new Vector2(transform.parent.transform.position.x, transform.parent.transform.position.y + .5f);
            gameManager.enemyHasEgg = true;

            SearchEgg searchEgg = GetComponentInParent<SearchEgg>();
            searchEgg.isEggHeld = true;
            searchEgg.GetClosestExit(gameManager.exits);
            //GetComponentInParent<SearchEgg>().isEggHeld = true;
            //GetComponentInParent<SearchEgg>().GetClosestExit(gameManager.exits);
        }
    }
}
