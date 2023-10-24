using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    public void Awake()
    {
        progressBar.fillMethod = Image.FillMethod.Horizontal;
    }
    public void Update()
    {
       
    }
    public Image progressBar; // Reference to the UI Image component representing the loading bar

    // Set the loading bar fill amount based on a float value from 0.0 to 1.0
    public void SetFillAmount(float fillPercentage)
    {
        if (progressBar != null)
        {
            fillPercentage = Mathf.Clamp01(fillPercentage); // Ensure the value is between 0 and 1
            progressBar.fillAmount = fillPercentage;
        }
    } 

    // Reset the loading bar to 0% fill
    public void ResetBar()
    {
        if (progressBar != null)
        {
            progressBar.fillAmount = 0f;
        }
    }

    // Fill the loading bar to 100%
    public void FillTo100Percent()
    {
        if (progressBar != null)
        {
            progressBar.fillAmount = 1f;
        }
    }
}
