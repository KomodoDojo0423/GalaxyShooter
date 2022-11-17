using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 3.5f;

    private float _horizontalInput;
    private float _verticalInput;

    [SerializeField]
    private float _fireRate = 0.2f;
    private float _canFire = -1f;

    [SerializeField]
    private GameObject _laserPrefab;
    private Vector3 laserPosition = new Vector3(0, 0.8f, 0);

    void Start()
    {
       transform.position = new Vector3(0, 0, 0); 
    }

    void Update()
    {
      CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }
      
    void CalculateMovement()
    {
        //Player movement inputs
      float _horizontalInput = Input.GetAxis("Horizontal"); 
      float _verticalInput = Input.GetAxis("Vertical");

      transform.Translate(Vector3.right * _horizontalInput * speed * Time.deltaTime);
      transform.Translate(Vector3.up * _verticalInput * speed * Time.deltaTime);

      //Player bounds for Y axis
      if(transform.position.y >= 0)
      {
        transform.position = new Vector3(transform.position.x, 0, 0);
      }
      else if(transform.position.y <= -3.8f)
      {
        transform.position = new Vector3(transform.position.x, -3.8f, 0);
      }

      //Player bounds/wrap around for x axis
      if(transform.position.x >= 11.5f)
      {
        transform.position = new Vector3(-11.5f, transform.position.y, 0);
      }
      else if(transform.position.x <= -11.5f)
      {
        transform.position = new Vector3(11.5f, transform.position.y, 0);
      }

    }

    void FireLaser()
    {
       //fire rate delay
       _canFire = Time.time + _fireRate;
       Instantiate(_laserPrefab, transform.position + laserPosition, Quaternion.identity);
    }
}


