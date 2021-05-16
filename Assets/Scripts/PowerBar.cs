using UnityEngine;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour
{
	public Image healthBarImage;
    public float powerBarMaxTime;
    private float powerBarTime = 0;
    private bool goingUp;
    private bool shot = false;
    private float momentumX = 1000;
    public GameObject otherBar;

    void Start()
    {
        powerBarTime = 0;
        goingUp = true;
    }

    // Update is called once per frame
    void Update()
    {
        printBar();
    }

    void printBar(){

        if (goingUp){
            powerBarTime += Time.deltaTime;
        } else {
            powerBarTime -= Time.deltaTime;
        }

        if(powerBarTime > powerBarMaxTime){
            powerBarTime = powerBarMaxTime;
            goingUp = false;
        } else if (powerBarTime <= 0) {
            powerBarTime = 0;
            goingUp = true;
        }

        healthBarImage.fillAmount = Mathf.Clamp(powerBarTime / powerBarMaxTime, 0, 1f);
    }

    public void Deactivate(){
        otherBar.SetActive(false);
        gameObject.SetActive(false);
    }

    public void Activate(){
        otherBar.SetActive(true);
        gameObject.SetActive(true);
    }

    public float GetPowerBarPercent()
    {
        return powerBarTime / powerBarMaxTime;
    }
}