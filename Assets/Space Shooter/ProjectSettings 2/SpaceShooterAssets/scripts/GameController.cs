using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  //* bu paket textler için
using UnityEngine.SceneManagement;  //* Bu paket sayesinde çok rahatlıkla oyunu tekrar başlatıyoruz
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject hazard;
    public int spawnCount;
    public float spawnWait;
    public float startSpawn;
    public float waveWait;

    public TextMeshProUGUI scoreText;      //* score yazısı için
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI restartText;
    public TextMeshProUGUI quitText;
    public int score;   //* score sayımı için integer değer lazım

    private bool gameOver;
    private bool restart;


    void Update()   //* burada restart true olduktan sonra sürekli R yada Q ya basıılıyormu kontrol ediyoruz
    {
        if(restart == true)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(0); //* burada oyunu tekrar başlatıyoruz
                
            }
            if(Input.GetKeyDown(KeyCode.Q))
            {
                Application.Quit();
                Debug.Log("Oyun Kapandı!");
            }
        }

    }

    IEnumerator SpawnValues()
    {
        yield return new WaitForSeconds(startSpawn); //* oyuncuya bir saniye zaman tanıyoruz hazırlanması için
        while (true)  //* BU FONKSİYON SAYESİNDE SÜREKLİ ASTROİD GELİYOR AMA GAMEOVER = TRUE İLE BUNU KIRIYORUZ
        {
            
            for (int i = 0; i < spawnCount; i++)//i += 1
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-3, 3), 0, 10);
                Quaternion spawnRotation = Quaternion.identity;//*mecburen quaternion vermemiz gerekiyor identity vererek boş bir değer döndürebiliyoruz(bir şey değişmemiş oluyor)

                Instantiate(hazard, spawnPosition, spawnRotation);

                //**Coroutine
                //*1.IEnumerator döndürmek zorundadır.
                //*2.En az 1 adet yield ifadesi bulunmak zorundadır.
                //*3.Coroutinler çağrılırken mutlaka StartCoroutine metoduyla çağrılmalıdır.

                yield return new WaitForSeconds(spawnWait); //* bu bir coroutine kodudur buraya girdiğimiz değer kadar bekletiyor
            }
            yield return new WaitForSeconds(waveWait);
            if(gameOver == true)
            {
                restartText.text = "Press 'R' for Restart";
                quitText.text = "Press 'Q' for Quit";
                restart = true;
                break;
            }

        }

    }

    public void UpdateScore()
    {
        score += 10;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over";
        gameOver = true;
    }

    void Start()
    {
        gameOverText.text = "";
        restartText.text = "";
        quitText.text = "";
        gameOver = false;       //* burada başlangıçta bu 
        restart = false;
        StartCoroutine(SpawnValues());
        
    }

    
}
