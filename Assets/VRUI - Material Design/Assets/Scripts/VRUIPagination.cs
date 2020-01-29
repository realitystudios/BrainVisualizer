using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// VRUI Pagination Component
public class VRUIPagination : MonoBehaviour {

	[SerializeField] GameObject leftButton;
	[SerializeField] GameObject rightButton;
	[SerializeField] GameObject content;
	[SerializeField] GameObject carousel;
	[SerializeField] GameObject pageIndicator;

	RectTransform contentRectTransform;
    ScrollRect scrollRect;
    VRUIRadio[] radioBtns;
    HorizontalLayoutGroup contentLayoutGroup;

	float carouselWidth;
	float desiredPosition;

	int pages = 0;
	int currentIndex = 0;
	bool isPagingTo = false;

	void Start () {

        // Initialize values and create pagination objects
		
		carouselWidth = carousel.GetComponent<RectTransform> ().rect.width;

        scrollRect = carousel.GetComponent<ScrollRect>();

		contentRectTransform = content.GetComponent<RectTransform> ();

		contentLayoutGroup = content.GetComponent<HorizontalLayoutGroup> ();

        scrollRect.onValueChanged.AddListener ((Vector2 v) => UpdatePositionIndex(scrollRect.horizontalNormalizedPosition));

        StartCoroutine(CreatePagination());

    }

	void Update () {

        //If pagination is activated scroll to the desired position

        if (isPagingTo) {

			scrollRect.horizontalNormalizedPosition = Mathf.Lerp (scrollRect.horizontalNormalizedPosition, desiredPosition, Time.deltaTime * 5f);

			if (MyApproximately (scrollRect.horizontalNormalizedPosition, desiredPosition, 0.0005f)) {

				scrollRect.horizontalNormalizedPosition = desiredPosition;

				isPagingTo = false;

            }
		}
	}

    // Radio button onClick event
    void PageTo (Transform radio) {

         ScrollToPage(radio.GetSiblingIndex());

    }

	void UpdatePositionIndex (float pos) {

        // Update the highlighted radio button

        EnableButton(leftButton, pos >= 0.01f);

        EnableButton(rightButton, pos <= 0.99f);

        currentIndex = Mathf.FloorToInt(pos * contentRectTransform.rect.width / carouselWidth);
 
        radioBtns[Mathf.Max(Mathf.Min(currentIndex, pages - 1), 0)].isOn = true;
    
    }

	private bool MyApproximately (float a, float b, float c) {
		return Mathf.Abs (a - b) < c;
	}

    /* Calculate the size of the carousel and the number of pages, 
     * and create a radio button for each page.
     * */
    IEnumerator CreatePagination ()
    {
        yield return null;

        pages = Mathf.CeilToInt(contentRectTransform.rect.width / carouselWidth);

        if (pages > 1)
        {

            for (int i = 1; i < pages; i++)
            {

                GameObject radio = Instantiate(pageIndicator, transform);

                radio.GetComponent<VRUIRadio>().onPointerClick.AddListener((b) => PageTo(radio.transform));
            }


            radioBtns = gameObject.GetComponentsInChildren<VRUIRadio>();

            pageIndicator.GetComponent<VRUIRadio>().isOn = true;

            pageIndicator.GetComponent<VRUIRadio>().onPointerClick.AddListener((b) => PageTo(pageIndicator.transform));

            EnableButton(leftButton, false);

            EnableButton(rightButton, true);
        }
        else
        {
            gameObject.SetActive(false);
            leftButton.transform.parent.gameObject.SetActive(false);
            rightButton.transform.parent.gameObject.SetActive(false);
        }

        GameObject.Find("VRUI-Manager").gameObject.GetComponent<VRUIColorPalette>().UpdateColors();

    }

    void EnableButton (GameObject btn, bool enable)
    {
        // Enable/disable left/right buttons
        if (enable)
        {
            btn.GetComponent<Button>().interactable = true;
            btn.GetComponent<CanvasGroup>().alpha = 1f;

        } else
        {
            btn.GetComponent<Button>().interactable = false;
            btn.GetComponent<CanvasGroup>().alpha = 0.3f;
        }
    }

    // Left and right buttons onClick events
    public void PageLeft ()
    {
        ScrollToPage(--currentIndex);
    }

    public void PageRight ()
    {
        ScrollToPage(++currentIndex);
    }

    // Calculate the desired pagination position and enable scrolling to the desired position
    void ScrollToPage(int index)
    {
        currentIndex = Mathf.Max(0, Mathf.Min(pages - 1, index));

        float pagePosition = currentIndex * (carouselWidth - (contentLayoutGroup.spacing - contentLayoutGroup.padding.left));

        float pagePositionNormalized = pagePosition / (contentRectTransform.rect.width - carouselWidth);

        desiredPosition = Mathf.Min(pagePositionNormalized, 1f);

        isPagingTo = true;
    }

    // Stop pagination if user drag on the carousel
    public void OnCarouselDrag ()
    {
        isPagingTo = false;
    }
}
