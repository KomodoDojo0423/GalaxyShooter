using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;

    public float horizontalInput;
    public float verticalInput;

    // Start is called before the first frame update
    void Start()
    {
       transform.position = new Vector3(0, 0, 0); 
    }

    // Update is called once per frame
    void Update()
    {
      
      CalculateMovement();

    }
      
    void CalculateMovement()
    {
        //Player movement inputs
      float horizontalInput = Input.GetAxis("Horizontal"); 
      float verticalInput = Input.GetAxis("Vertical");

      transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
      transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);

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
}


