using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;


public class galamaptopixel : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Canvas parentCanvas = null;        // the parent canvas of this UI - only needed to determine if we need the camera  
    [SerializeField] private Canvas endscreen; 
    [SerializeField] private RectTransform rect = null;  
    [SerializeField] private RectTransform pin = null;  
    [SerializeField] private RectTransform pin2 = null;      // the recttransform of the UI object
    [SerializeField] TextMeshProUGUI coordsoverlay;
    [SerializeField] TextMeshProUGUI scoredisplay;
    [SerializeField] private SpriteRenderer backgroundimage;
    [SerializeField] private Button minimizemap;
    [SerializeField] private Button maximizemap;
    [SerializeField] private Animator mapanims;
    private float currentx;
    private float currenty;

    private int rand;
    private int coordsx;
    private int coordsy;
    public TextMeshProUGUI plusscore;
    public Animator plusscoreanim;
    public TextMeshProUGUI endscreenscore;
    public Sprite[] SpritePics;

    public Vector2[] coords;
    // you can serialize this as well - do NOT assign it if the canvas render mode is overlay though
    private Camera UICamera = null;                             // the camera that is rendering this UI

    private int maxSelections = 5;
    private int[] previousSelections = {-1, -1, -1, -1, -1};
    private int currentPlay = 0;
    private float overallscore = 0;
    private bool finished = false;
    private void Start()
    {
        coordsoverlay.text = "Coords: 0, 0";
        if (rect == null)
            rect = GetComponent<RectTransform>();

        if (parentCanvas == null)
            parentCanvas = GetComponentInParent<Canvas>();

        if (UICamera == null && parentCanvas.renderMode == RenderMode.ScreenSpaceCamera)
            UICamera = parentCanvas.worldCamera;
        
        // reset map to minimized and change button
        minimizemap.gameObject.SetActive(false);
        maximizemap.gameObject.SetActive(true);

        plusscore.gameObject.SetActive(false);


        //make sure duplicate image is not USED
        bool findingSHIT = true;
        do {
            rand = Random.Range(0,SpritePics.Length);
            findingSHIT = false;
            foreach (int n in previousSelections) {
                if (n == rand) {
                    //This picture has been used before
                    findingSHIT = true;
                    break;
                }
            }
        } while (findingSHIT);
        previousSelections[currentPlay] = rand;

        backgroundimage.sprite = SpritePics[rand];
        coordsx = Mathf.RoundToInt(coords[rand].x);
        coordsy = Mathf.RoundToInt(coords[rand].y);
        Debug.Log(coordsx.ToString() + ", " + coordsy.ToString());

        pin.position = new Vector2(100000, 100000);
        pin.anchoredPosition = new Vector2(100000,100000);
        pin2.position = new Vector2(100000, 100000);
        pin2.anchoredPosition = new Vector2(100000,100000);
        mapanims.Play("galamapsmall");
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // this UI element has been clicked by the mouse so determine the local position on your UI element
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, eventData.position, UICamera, out Vector2 localPos);
        
        // we now have the local click position of our rect transform, but as you want the (0,0) to be bottom-left aligned, need to adjust it
        currentx = localPos.x + rect.sizeDelta.x * 0.5f;
        currenty = localPos.y + rect.sizeDelta.y * 0.5f;

        localPos.x += rect.rect.width / 2f;
        localPos.y += rect.rect.height / 2f;

        if(localPos.x < 0|| localPos.x >= rect.rect.width || localPos.y < 0 || localPos.y >= rect.rect.height)
        {
            Debug.Log("click out of map bounds");
        }
        else if (!finished)
        {
            // Set the red pin to the position it should be, and set the coords text
            pin.position=localPos / 2f;
            pin.anchoredPosition = localPos;
            coordsoverlay.text = "Coords: " + Mathf.RoundToInt(localPos.x).ToString() + ", " + Mathf.RoundToInt(localPos.y).ToString();
            Debug.Log(Mathf.RoundToInt(localPos.x).ToString() + ", " + Mathf.RoundToInt(localPos.y).ToString());
        }

    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (finished == false) {

                // check if player placed pin
                if(pin.anchoredPosition == new Vector2(100000,100000))
                {
                    Debug.Log("pin not placed");
                    return;
                }
                //SCORE
                float score = Mathf.RoundToInt(Mathf.Sqrt(Mathf.Pow(currentx - coordsx, 2) + Mathf.Pow(currenty - coordsy, 2))) * 2;
                if (score < 10) 
                    score = 0;
                if (score > 500)
                    score = 500;

                //set pin2 to the position where the coords actually were
                pin2.position = new Vector2(coordsx, coordsy);
                pin2.anchoredPosition = new Vector2(coordsx, coordsy);

                Debug.Log(score);
                score = 500 - score;
                plusscore.gameObject.SetActive(true);
                plusscore.text = "+" + score;
                plusscoreanim.Play("plusscorefloatup");
                overallscore += score;
                scoredisplay.text = "Score: " + overallscore.ToString();

                finished = true;
            } else {
                currentPlay++;
                if ( !(currentPlay >= maxSelections) ) {
                    Start();
                } else {
                    // Application.Quit();
                    parentCanvas.gameObject.SetActive(false);
                    backgroundimage.gameObject.SetActive(false);
                    endscreenscore.text = overallscore.ToString() + "\nout  of  2500!";
                    endscreen.gameObject.SetActive(true);
                    Debug.Log("quitgame");
                }
                finished = false;
            }
            
        }
    }
}
