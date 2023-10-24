using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetScript : MonoBehaviour
{
    public bool magnetIsActive = false;
    public GameObject magnetIdentifier;
    public GameObject[] diamonds;
    // Update is called once per frame
    void Update()
    {
        if (magnetIsActive)
        {
            diamonds = GameObject.FindGameObjectsWithTag("reward");
            foreach(GameObject d in diamonds)
            {
                d.transform.position = Vector3.Lerp(d.transform.position, transform.position, 30f);
                Debug.Log("Works");
                Invoke("offMagnet", 5f);
            }
        }
        if (!magnetIsActive)
            foreach (GameObject d in diamonds)
                d.transform.position = new Vector3(0, 0, 0);

    }

    private void offMagnet()
    {
        magnetIsActive = false;
        magnetIdentifier.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Magnet"))
        {
            magnetIsActive = true;
            magnetIdentifier.SetActive(true);

            //other.GetComponent<Animator>().SetTrigger("Collected");
        }
    }


}
