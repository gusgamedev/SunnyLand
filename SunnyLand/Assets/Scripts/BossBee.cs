using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossBee : MonoBehaviour
{
    public float timeToGoNextPoint = 1f;
    public float speedAttack = 25f;
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

    bool facingRight = false;

    bool canAttack = false;

    public int movements = 3;

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

    IEnumerator SpawnHive()
    {
        List<int> spawnPointList = new List<int> { 0, 1, 2, 3 };

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            yield return new WaitForSeconds(0.8f);
            int index = Random.Range(0, spawnPointList.Count - 1);
            Instantiate(hive, spawnPoints[spawnPointList[index]].position, Quaternion.identity);
            print(spawnPoints[spawnPointList[index]]);
            spawnPointList.RemoveAt(index);
            
        }

        yield return new WaitForSeconds(1f);
        canAttack = true;
        goToPointA = true;
        nextPoint = pointA;


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
                canAttack = false;

                //chegou no ponto A
                if (goToPointA)
                {
                    Flip();
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

                    Invoke("SetCanAttack", 1.3f);



                }
                //chegou no ponto B
                else if (goToPointB)
                {
                    Flip();
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

                    Invoke("SetCanAttack", 1.3f);
                }
                //chegou no ponto C
                else if (goToPointC)
                {
                    goToPointA = false;
                    goToPointB = false;
                    goToPointC = false;

                    //nextPoint = null;

                    movements = 4;

                    StartCoroutine(SpawnHive());
                }

                
            }
            transform.position = Vector2.MoveTowards(transform.position, nextPoint.position, speedAttack * Time.deltaTime);
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

    }
}
