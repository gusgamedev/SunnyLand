using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossBee : MonoBehaviour
{
    public float timeToGoNextPoint = 1f;
    public float speedAttack = 16f;
    public float speedReturn = 20f;

    public Transform pointA;
    public Transform pointB;
    public Transform pointC;

    public GameObject hive;
    public Transform[] spawnPoints;
    

    Transform nextPoint;

    bool goToPointA = false;
    bool goToPointB = false;
    bool goToPointC = false;

    bool canAttack = false;

    public int movements = 3;

    private IEnumerator courotine;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = pointA.position;
        nextPoint = pointB;
        goToPointB = true;
        Invoke("SetCanAttack", 2f);
    }

    // Update is called once per frame
    void Update()
    {

        MoveEnemy();
    }

    void SpawnHive()
    {
        var spawnPointList = spawnPoints.ToList();

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            int index = Random.Range(0, spawnPointList.Count - 1);
            spawnPointList.RemoveAt(index);
            courotine = InstantiateHive(spawnPoints[index], Random.Range(1f, 2.5f));
            StartCoroutine(courotine);
        }

        print("ataca novamente");
    }

    IEnumerator InstantiateHive(Transform spawnPosition, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        print("espaunou " + spawnPosition.position);
        //Instantiate(hive, spawnPosition.position, Quaternion.identity);
    }

    void SetCanAttack()
    {
        canAttack = true;
    }



    private void MoveEnemy()
    {
        if (canAttack)
        { 
            if (nextPoint.position.x == transform.position.x)
            {
                //chegou no ponto A
                if (goToPointA)
                {
                    movements--;
                    goToPointA = false;

                    if (movements <= 0)
                    {
                        nextPoint = pointC;
                        goToPointB = false;
                        goToPointC = true;
                    }
                    else
                    {
                        nextPoint = pointB;
                        goToPointB = true;
                        goToPointC = false;
                    }

                }
                //chegou no ponto B
                else if (goToPointB)
                {
                    movements--;
                    goToPointB = false;

                    if (movements <= 0)
                    {
                        nextPoint = pointC;
                        goToPointA = false;
                        goToPointC = true;
                    }
                    else
                    {
                        nextPoint = pointA;
                        goToPointA = true;
                        goToPointC = false;
                    }
                }
                //chegou no ponto C
                else if (goToPointC)
                {
                    goToPointA = false;
                    goToPointB = false;
                    goToPointC = false;

                    //nextPoint = null;

                    canAttack = false;
                    movements = 3;

                    Invoke("SpawnHive", 2f);
                }
            }
            transform.position = Vector2.MoveTowards(transform.position, nextPoint.position, speedAttack * Time.deltaTime);
        }
    }

    private void Flip()
    {
        //facingRight = !facingRight;
       // transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

    }
}
