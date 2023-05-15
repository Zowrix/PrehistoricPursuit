using UnityEngine;

public class SlideshowController : MonoBehaviour
{
    private int currentSlideIndex = 0;
    private Transform[] slideImages;
    private bool slideshowActive = true;
    [SerializeField] private GameObject _txtCallToAction;

    private void Start()
    {

        slideImages = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            slideImages[i] = transform.GetChild(i);
            slideImages[i].gameObject.SetActive(false);
        }

        // Activer la premi�re image
        ActivateSlideImage(currentSlideIndex);
    }

    private void Update()
    {
        // V�rifier si le diaporama est actif
        Time.timeScale = 0f;
        if (slideshowActive)
        {
            // V�rifier si un clic est effectu� � l'�cran
            if (Input.GetMouseButtonDown(0))
            {
                // Passer � l'image suivante
                NextSlide();

                // V�rifier si le diaporama est termin� et que la derni�re diapositive a �t� cliqu�e
                if (!slideshowActive && currentSlideIndex >= slideImages.Length - 1)
                {
                    // D�sactiver le conteneur du diaporama
                    gameObject.SetActive(false);
                }
            }
        }

    }

    private void NextSlide()
    {
        // D�sactiver l'image actuelle
        DeactivateSlideImage(currentSlideIndex);

        // Passer � la prochaine image
        currentSlideIndex++;

        // V�rifier si toutes les images ont �t� affich�es
        if (currentSlideIndex >= slideImages.Length)
        {
            ResumeTime();
            // D�sactiver le diaporama
            slideshowActive = false;
            return;
        }

        // Activer la prochaine image
        ActivateSlideImage(currentSlideIndex);

        // Arr�ter le temps pour la nouvelle diapositive
        Time.timeScale = 0f;
    }

    private void ResumeTime()
    {
        // Reprendre le temps
        Time.timeScale = 1f;
    }

    private void ActivateSlideImage(int index)
    {
        slideImages[index].gameObject.SetActive(true);
    }

    private void DeactivateSlideImage(int index)
    {
        slideImages[index].gameObject.SetActive(false);
        _txtCallToAction.SetActive(false);

    }
}