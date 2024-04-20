using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;

    private GameController gameController;  //* burda bir obje oluşturuyoruz

    private void Start()    //* bu özelliği atarken sıkıntı yaşadık bunu çözmek için unity geliştiricileri böyle bir yol kullanıyor
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }


    void OnTriggerEnter(Collider other)
    {
        // Debug.Log(other);  //*other'ın ne olduğunu öğreniyoruz
        if(other.gameObject.tag == "Boundary")
        {
            return;  //* burda returnün anlamı altındakileri okumadığından diğer kodların okunmasını engelliyor ve cismimiz patlamıyor
        }
        Instantiate(explosion, transform.position, transform.rotation);
        if(other.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation); //*burada konum veriyoruz ve bu konumun olduğu yerde bu efekti gerçekleştiriyor
            gameController.GameOver();  //* patlamadan hemen sonra bu fonksiyonu çağırıyoruz
        }
        Destroy(other.gameObject);
        Destroy(gameObject);
        gameController.UpdateScore();  //* burada astroidlerin patladığında scorun artması için bağlantı kuruyoruz
    }


}
