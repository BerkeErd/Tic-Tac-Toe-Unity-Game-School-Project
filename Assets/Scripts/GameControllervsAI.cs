using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameControllervsAI : MonoBehaviour
{
    public byte whoseTurn; // 1 = X , 0 = O olacak
    public Sprite[] playerIcons; // 0 = X simgesi , 1 = O simgesi 
    public Button[] tictactoeSpaces; // Oynanabilir bölge.
    public int[] markedSpaces; // Hangi bölgenin hangi oyuncu tarafından işaretlendiğini belirtiyor.
    public Text[] winnerTexts;
    public GameObject[] winningLine;

    
    void Start()
    {
        GameSetup();
    }

    void GameSetup()
    {
        whoseTurn = 0;


        
        for (int i = 0; i < tictactoeSpaces.Length; i++)
        {
            tictactoeSpaces[i].interactable = true;
            tictactoeSpaces[i].GetComponent<Image>().sprite = null;

            winningLine[i].SetActive(false);

            markedSpaces[i] = i+10;



        }


    }




    public void TictactoeButton(int TiklananKutu)
    {
        whoseTurn = 1;
        tictactoeSpaces[TiklananKutu].image.sprite = playerIcons[0];
        tictactoeSpaces[TiklananKutu].image.color = Color.white; // çünkü normalde şeffaflar
        tictactoeSpaces[TiklananKutu].interactable = false;

        markedSpaces[TiklananKutu] = 1;

        if(WinnerCheck() == false)
        {
            if (DrawCheck() == false)
            {
                YapayZekaOyna();
            }

        }
       
        
        
    }





    public void WinnerDisplay(int Bas, string Yon)
    {



        if (whoseTurn == 1)
        {
            winnerTexts[1].gameObject.SetActive(true);
            winnerTexts[1].text = "KAZANAN";
        }
        else if (whoseTurn == 0)
        {
            winnerTexts[0].gameObject.SetActive(true);
            winnerTexts[0].text = "KAZANAN";
        }



        switch (Yon)
        {
            case "yatay":
                winningLine[Bas / 3].SetActive(true);
                break;
            case "dikey":
                winningLine[Bas + 3].SetActive(true);
                break;
            case "sol":
                winningLine[Bas + 6].SetActive(true);
                break;
            case "sag":
                winningLine[Bas + 5].SetActive(true);
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

        if (toplamKutu == 5)
        {

            winnerTexts[0].text = "BERABERE";
            winnerTexts[1].text = "BERABERE";
            return true;

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

    public int YapayRastgeleSec()
    {
        int sayi = Random.Range(0, 8);
        Debug.Log("Rastgele sayi : " + sayi);
        
        return sayi;

    }
    public bool YapayHamleYap(int kutu1, int kutu2, int kutu3)
    {
        whoseTurn = 0;
        if (markedSpaces[kutu1] >= 10 )
        {
            tictactoeSpaces[kutu1].image.sprite = playerIcons[1];
            tictactoeSpaces[kutu1].image.color = Color.white; // çünkü normalde şeffaflar
            tictactoeSpaces[kutu1].interactable = false;
            markedSpaces[kutu1] = 0;
            WinnerCheck();
            return true;
        }
        else if (markedSpaces[kutu2] >= 10)
        {
            tictactoeSpaces[kutu2].image.sprite = playerIcons[1];
            tictactoeSpaces[kutu2].image.color = Color.white; // çünkü normalde şeffaflar
            tictactoeSpaces[kutu2].interactable = false;
            markedSpaces[kutu2] = 0;
            WinnerCheck();
            return true;
        }
        else if (markedSpaces[kutu3] >= 10)
        {
            tictactoeSpaces[kutu3].image.sprite = playerIcons[1];
            tictactoeSpaces[kutu3].image.color = Color.white; // çünkü normalde şeffaflar
            tictactoeSpaces[kutu3].interactable = false;
            markedSpaces[kutu3] = 0;
            WinnerCheck();
            return true;
        }
        else
        {
            Debug.Log("Mantıklı kutular dolu.("+kutu1+","+kutu2+","+kutu3+")");
            return false;

        }

        
    }


    public void YapayZekaOyna()
    {
        int temp = 0; //hamle yapıldı mı?

            if ((markedSpaces[0] == markedSpaces[1] || markedSpaces[0] == markedSpaces[2] || markedSpaces[2] == markedSpaces[1]))
            {
                Debug.Log("Yataya koymak lazım");
                if(YapayHamleYap(0,  1,  2))
                {
                    temp++;
                    
                }
                
                
            }

          if (((markedSpaces[3] == markedSpaces[4] || markedSpaces[3] == markedSpaces[5] || markedSpaces[5] == markedSpaces[4])) && (temp == 0))
        {
            Debug.Log("Yataya koymak lazım");
            if (YapayHamleYap(3, 4, 5))
            {
                temp++;

            }


        }

         if (((markedSpaces[6] == markedSpaces[7] || markedSpaces[6] == markedSpaces[8] || markedSpaces[8] == markedSpaces[7])) && (temp == 0))
        {
            Debug.Log("Yataya koymak lazım");
            if (YapayHamleYap(6, 7, 8))
            {
                temp++;

            }


        }

          if (((markedSpaces[0] == markedSpaces[3] || markedSpaces[0] == markedSpaces[6] || markedSpaces[6] == markedSpaces[3])) && (temp == 0))
            {
                Debug.Log("Dikeye koymak lazım");
                if (YapayHamleYap(0,3,6))
                {
                    temp++;
                }


            }

          if (((markedSpaces[1] == markedSpaces[4] || markedSpaces[1] == markedSpaces[7] || markedSpaces[7] == markedSpaces[4])) && (temp == 0))
        {
            Debug.Log("Dikeye koymak lazım");
            if (YapayHamleYap(1, 4, 7))
            {
                temp++;
            }


        }

          if (((markedSpaces[2] == markedSpaces[5] || markedSpaces[2] == markedSpaces[8] || markedSpaces[5] == markedSpaces[8])) && (temp == 0))
        {
            Debug.Log("Dikeye koymak lazım");
            if (YapayHamleYap(2, 5, 8))
            {
                temp++;
            }


        }

          if (((markedSpaces[0] == markedSpaces[4] || markedSpaces[0] == markedSpaces[8] || markedSpaces[4] == markedSpaces[8])) && (temp == 0))
            {
                Debug.Log("Sol çapraza koymak lazım");
                if (YapayHamleYap(0,4,8))
                {
                    temp++;
                }


            }

             if (((markedSpaces[2] == markedSpaces[4] || markedSpaces[2] == markedSpaces[6] || markedSpaces[6] == markedSpaces[4])) && (temp == 0))
            {
                Debug.Log("Sağ çapraza koymak lazım");
                if (YapayHamleYap(2,  4, 6))
                {
                    temp++;
                    
                }


            }
            
        if (temp == 0)
        {       
            Debug.Log("Rastgele koymak lazım");
            while ((YapayHamleYap(YapayRastgeleSec(), YapayRastgeleSec(), YapayRastgeleSec()) == false));              
        }

       

    }

}


