using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //* bu işlemi yapıyoruz ki unity içinde güzel bir sitem olmasını sağlamış oluyoruz (bu işleme serileştirme deniyor)
public class Boundary   //* sınırlarımızı class içine alıyoruz
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour    //* PlayerController classımız Monobehaviour calssımızın alt sınıfı olmuş oluyor PlayerController sınıfından MonoBehaviour sınıfındaki herhangi bir şeye ulaşabiliriz
{
    Rigidbody physic;
    AudioSource audioPlayer; //* burda ses dosyasına ulaşıyoruz

    // public int speed;   //* genelde böyle kullanmıyoruz eğer birçok yerde kullanmak istemiyorsak (bunada serileştirme işlemi deniyor)
    [SerializeField] int speed;
    [SerializeField] int tilt;
    [SerializeField] float nextFire;   
    [SerializeField] float fireRate; //*saniyedeki atış sayısı

    public Boundary boundary;   //* burda bir class oluşturuyoruz ve rahatlıkla yukarıdaki classa ulaşıyoruz   2.kısım nasıl bu classın içinde hangi isimle kullanıcağımızı gösteriyor
    public GameObject shot;
    public GameObject shotSpawn;
    


    void Start()
    {
        physic = GetComponent<Rigidbody>();  //! phisic'in rigidbody olduğunu anlamasını sağlıyor
        audioPlayer = GetComponent<AudioSource>();  //! burada audioPlayer'ın AudioSource 'la alakası olduğunu vurguluyoruz
    }

    void Update()
    {
        

        // && = ve
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            Instantiate(shot, shotSpawn.transform.position, shotSpawn.transform.rotation); //* bizden bir object, position ,rotation alıyor
            audioPlayer.Play(); //* ateşlemeden hemen sonra ses çıkmasını istiyoruz o yüzden buraya koyduk
        }
        
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");


        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        physic.linearVelocity = movement * speed;

        Vector3 position = new Vector3(  //! burada sınırları belirliyoruz
            Mathf.Clamp(physic.position.x, boundary.xMin, boundary.xMax), 
            0, 
            Mathf.Clamp(physic.position.z, boundary.zMin, boundary.zMax)
            );

        physic.position = position;

        physic.rotation = Quaternion.Euler(0, 0, physic.linearVelocity.x * tilt);  //* sağa sola eğim animasyonu
    }
}
