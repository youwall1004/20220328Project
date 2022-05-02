using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stand : MonoBehaviour
{
    private IEnumerator coroutine;
    public Vector3 targetPosition;

    void Update()
    {
        //coroutine = FlatfromMove();
        StartCoroutine(FlatfromMove());
    }

    IEnumerator FlatfromMove()
    {
        float speed = 3;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        yield return new WaitForSeconds(3);

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, -1), speed * Time.deltaTime);
    }
}
