using UnityEngine;

public class GeneralButtonManager : MonoBehaviour
{
    public void OnClickSetActiveTheGivenObject(GameObject Panel)
    {
        Panel.SetActive(true);
    }
   public void OnClickSetDisableTheGivenObject(GameObject Panel)
    {
        Panel.SetActive(false);
    }

}
