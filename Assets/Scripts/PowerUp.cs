using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    [SerializeField]//0 = Triple, 1 = Speed, 2 = Shields
    private int _powerupID;

    void Update()
    {
        transform.Translate(Vector2.down * _speed * Time.deltaTime);

        if (transform.position.y <= -6)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                if(_powerupID == 0)
                {
                    player.TripleShotActive();
                }
                else if(_powerupID == 1)
                {
                    player.SpeedPowerupActive();
                }
                else if (_powerupID == 2)
                {
                    Debug.Log("Shields Collected");
                }
                
            }

            Destroy(this.gameObject);
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
