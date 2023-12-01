using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public byte whoseTurn; // 1 = O , 0 = X olacak
    public GameObject[] turnIcons; // Kimin sırası olduğunu gösteriyor
    public Sprite[] playerIcons; // 0 = X simgesi , 1 = O simgesi 
    public Button[] tictactoeSpaces; // Oynanabilir bölge.
    public int[] markedSpaces; // Hangi bölgenin hangi oyuncu tarafından işaretlendiğini belirtiyor.
    public Text[] winnerTexts;
    public Text[] Sureler;
    public GameObject[] winningLine;
    public Image[] SureKutusu; //Sureyi gösteren kutu
    public bool ZamanModu; // True = açık , False = kapalı
    float XSure = 0;
    float OSure = 0;
    int SiyahKutu = 0;
    bool oyunBasladi = false;

    // Use this for initialization
    void Start () {
        GameSetup();
	}

    void GameSetup ()
    {
        whoseTurn = 0;
        turnIcons[0].SetActive(true);
        turnIcons[1].SetActive(false);

        for (int i = 0; i < tictactoeSpaces.Length ; i++)
        {
            tictactoeSpaces[i].interactable = true;
            tictactoeSpaces[i].GetComponent<Image>().sprite = null;

            winningLine[i].SetActive(false);

            markedSpaces[i] = i + 10;

        }

          
    }
	
	// Update is called once per frame
	void Update () {

        
    if(oyunBasladi)
        {
            if (WinnerCheck() == false && DrawCheck() == false)
            {


                Sureler[0].text = "+" + XSure;
                Sureler[1].text = "+" + OSure;
                SiyahKutu = Mathf.RoundToInt(XSure / ((XSure + OSure) / 10));


                if (whoseTurn == 1)
                {
                    XSure += Time.deltaTime;

                }
                else if (whoseTurn == 0)
                {
                    OSure += Time.deltaTime;

                }



                for (int i = 0; i <= SureKutusu.Length; i++)
                {
                    if (i < SiyahKutu)
                    {
                        SureKutusu[i].color = Color.black;
                    }
                    else
                        SureKutusu[i].color = Color.red;

                }

            }
        }

    }

    public void TictactoeButton (int whichNumber)
    {
        tictactoeSpaces[whichNumber].image.sprite = playerIcons[whoseTurn];
        tictactoeSpaces[whichNumber].image.color = Color.white; // çünkü normalde şeffaflar
        tictactoeSpaces[whichNumber].interactable = false;

        markedSpaces[whichNumber] = whoseTurn;
        oyunBasladi = true;

        if (whoseTurn == 1)
        {
            OTikladi();
           
            
        }
        else
        {
            XTikladi();
            
        }
    }

    public void OTikladi()
    {
        
        whoseTurn = 0;

        if (WinnerCheck() == false)
        {
            if (DrawCheck() == false)
            {
                turnIcons[0].SetActive(true);
                turnIcons[1].SetActive(false);
            }
        }

    }

    public void XTikladi()
    {
        whoseTurn = 1;
        
        if (WinnerCheck() == false)
        {
            if (DrawCheck() == false)
            {
                turnIcons[0].SetActive(false);
                turnIcons[1].SetActive(true);
            }
        }
    }

    public void WinnerDisplay(int Bas, string Yon)
    {
       


        if (whoseTurn == 1)
        {
            winnerTexts[0].gameObject.SetActive(true);
            winnerTexts[0].text = "KAZANAN";
        }
        else if(whoseTurn == 0)
        {
            winnerTexts[1].gameObject.SetActive(true);
            winnerTexts[1].text = "KAZANAN";
        }

        

        switch (Yon)
        {
            case "yatay":
                winningLine[Bas/3].SetActive(true);
                break;
            case "dikey":
                winningLine[Bas+3].SetActive(true);
                break;
            case "sol":
                winningLine[Bas+6].SetActive(true);
                break;
            case "sag":
                winningLine[Bas+5].SetActive(true);
                break;

            default:
                break;
        }

        

        for (int i = 0; i < tictactoeSpaces.Length; i++)
        {
            tictactoeSpaces[i].interactable = false;
        }
    }

    public bool DrawCheck()
    {
        int toplamKutu = 0;
        for (int i = 0; i < markedSpaces.Length; i++)
        {
            toplamKutu += markedSpaces[i];
        }
        
        if (toplamKutu == 4)
        {
            if(ZamanModu)
            {
                if(OSure > XSure)
                {
                    winnerTexts[1].gameObject.SetActive(true);
                    winnerTexts[1].text = "KAZANAN";
                    return true;
                }
                else if(XSure > OSure)
                {
                    winnerTexts[0].gameObject.SetActive(true);
                    winnerTexts[0].text = "KAZANAN";
                    return true;
                }
                else
                {
                    winnerTexts[0].text = "BERABERE";
                    winnerTexts[1].text = "BERABERE";
                    return true;
                }
               
            }
            else
            {
                winnerTexts[0].text = "BERABERE";
                winnerTexts[1].text = "BERABERE";
                return true;
            }

            
        }
        return false;

    }

    public bool WinnerCheck()
    {


        if ((markedSpaces[2] == markedSpaces[4]) && (markedSpaces[2] == markedSpaces[6]))  //sağ çapraz
        {
            WinnerDisplay(2, "sag");
            return true;
        }
        else if ((markedSpaces[0] == markedSpaces[4]) && (markedSpaces[0] == markedSpaces[8])) //sol çapraz
        {
            WinnerDisplay(0, "sol");
            return true;
        }
        else if ((markedSpaces[0] == markedSpaces[1]) && (markedSpaces[0] == markedSpaces[2])) //1. yatay
        {
            WinnerDisplay(0, "yatay");
            return true;
        }
        else if ((markedSpaces[3] == markedSpaces[4]) && (markedSpaces[3] == markedSpaces[5])) //2. yatay
        {
            WinnerDisplay(3, "yatay");
            return true;
        }
        else if ((markedSpaces[6] == markedSpaces[7]) && (markedSpaces[6] == markedSpaces[8])) //3. yatay
        {
            WinnerDisplay(6, "yatay");
            return true;
        }
        else if ((markedSpaces[0] == markedSpaces[3]) && (markedSpaces[0] == markedSpaces[6])) //1. dikey
        {
            WinnerDisplay(0, "dikey");
            return true;
        }
        else if ((markedSpaces[1] == markedSpaces[4]) && (markedSpaces[1] == markedSpaces[7])) //2. dikey
        {
            WinnerDisplay(1, "dikey");
            return true;
        }
        else if ((markedSpaces[2] == markedSpaces[5]) && (markedSpaces[2] == markedSpaces[8])) //3. dikey
        {
            WinnerDisplay(2, "dikey");
            return true;
        }
        else
            return false;



    }
}
