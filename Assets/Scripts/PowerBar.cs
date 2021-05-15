using UnityEngine;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour
{
	public Image healthBarImage;
    public int cursec;
    private bool goingUp;
    private bool shot = false;
    private float momentumX = 1000;
    private GameObject otherBar;

    void Start()
    {
        cursec = 0;
        goingUp = true;
        otherBar = GameObject.Find("/Canvas/HealthBar");
    }

    // Update is called once per frame
    void Update()
    {
        printBar();
    }

    void printBar(){
        if (goingUp){
            cursec++;
        } else {
            cursec--;
        }

        if(cursec > 99){
            goingUp = false;
        } else if (cursec < 2) {
            goingUp = true;
            cursec++;
        }

        healthBarImage.fillAmount = Mathf.Clamp(cursec / 100f, 0, 1f);
    }

    public void Deactivate(){
        otherBar.SetActive(false);
        gameObject.SetActive(false);
    }

    public void Activate(){
        otherBar.SetActive(true);
        gameObject.SetActive(true);
    }
}