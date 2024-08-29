using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    bool canOpen;

    public Transform openedPos;
    public float openingSpeed = 2.0f;

    private Vector3 closedPos;

    private void Start()
    {
        // Salva a posição inicial (fechada) da porta
        closedPos = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canOpen = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        canOpen = false;
    }

    private void Update()
    {
        if (canOpen && Input.GetButtonDown("Fire2"))
        {
            StartCoroutine(OpenDoor());
        }
    }

    IEnumerator OpenDoor()
    {
        float elapsedTime = 0f;
        Vector3 startingPos = transform.position;

        while (elapsedTime < 1f)
        {
            transform.position = Vector3.Lerp(startingPos, openedPos.position, elapsedTime);
            elapsedTime += Time.deltaTime * openingSpeed;
            yield return null;
        }

        transform.position = openedPos.position;
    }
}
