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

        // Activer la première image
        ActivateSlideImage(currentSlideIndex);
    }

    private void Update()
    {
        // Vérifier si le diaporama est actif
        Time.timeScale = 0f;
        if (slideshowActive)
        {
            // Vérifier si un clic est effectué à l'écran
            if (Input.GetMouseButtonDown(0))
            {
                // Passer à l'image suivante
                NextSlide();

                // Vérifier si le diaporama est terminé et que la dernière diapositive a été cliquée
                if (!slideshowActive && currentSlideIndex >= slideImages.Length - 1)
                {
                    // Désactiver le conteneur du diaporama
                    gameObject.SetActive(false);
                }
            }
        }

    }

    private void NextSlide()
    {
        // Désactiver l'image actuelle
        DeactivateSlideImage(currentSlideIndex);

        // Passer à la prochaine image
        currentSlideIndex++;

        // Vérifier si toutes les images ont été affichées
        if (currentSlideIndex >= slideImages.Length)
        {
            ResumeTime();
            // Désactiver le diaporama
            slideshowActive = false;
            return;
        }

        // Activer la prochaine image
        ActivateSlideImage(currentSlideIndex);

        // Arrêter le temps pour la nouvelle diapositive
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