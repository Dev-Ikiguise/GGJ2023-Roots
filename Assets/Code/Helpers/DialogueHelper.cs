using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class DialogueHelper : MonoBehaviour
{
    public static DialogueHelper Instance { get; private set; }

    private Sprite imageToDisplay;
    public Text text;
    private string textToReplace = "A";
    private GameObject newImageGO;
    private Image image;
    private RectTransform rect;

    public List<ImageAssociation> replacements;

    private List<PlacementSet> imageObjects;

    [System.Serializable]
    public class ImageAssociation
    {
        public string stringToReplace;
        public Sprite sprite;
        bool blankText;
    }

    private class PlacementSet
    {
        public GameObject imageObject;
        public Vector3 position;

        public PlacementSet() { }
        public PlacementSet(GameObject imageObject, Vector3 position)
        {
            this.imageObject = imageObject;
            this.position = position;
        }
    }


    public Canvas canvas;
    private Vector3 sz;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        newImageGO = new GameObject();
        imageObjects = new List<PlacementSet>();
    }

    public void CheckForReplacements(bool imageEnabledOnSet = true)
    {

        while(replacements.Any(str => text.text.Contains(str.stringToReplace)))
        {

            ImageAssociation assoc = replacements.FirstOrDefault();
            int index = text.text.Length;
            foreach (ImageAssociation ass in replacements)
            {
                if (text.text.Contains(ass.stringToReplace) && text.text.IndexOf(ass.stringToReplace) < index)
                {
                    index = text.text.IndexOf(ass.stringToReplace);
                    assoc = ass;
                }
            }

            if (text.text.Contains(assoc.stringToReplace))
            {
                Debug.Log("Replacing " + assoc.stringToReplace + " with " + assoc.sprite + " at index " + index);
                textToReplace = assoc.stringToReplace;
                imageToDisplay = assoc.sprite;
                SetReplacePosition(imageEnabledOnSet);

                text.text = text.text.Remove(index, textToReplace.Length).Insert(index, "  ");
            }
        }
    }

    void SetReplacePosition(bool imageEnabledOnSet = true)
    {
        string str = text.text;

        GameObject sub = Instantiate(newImageGO, text.gameObject.transform);

        image = sub.AddComponent<Image>();

        image.sprite = imageToDisplay;
        image.GetComponent<RectTransform>().sizeDelta = new Vector2(text.fontSize, text.fontSize) * 1.25f;
        sub.SetActive(imageEnabledOnSet);

        TextGenerator textGenerator = new TextGenerator(str.Length);

        TextGenerationSettings set = text.GetGenerationSettings(text.gameObject.GetComponent<RectTransform>().rect.size);
        textGenerator.Populate(str, set);

        Vector3 pos = textGenerator.characters[str.IndexOf(textToReplace)].cursorPos;
        sz = new Vector3 (text.fontSize/2, -text.fontSize/2);

        pos = pos / canvas.scaleFactor + sz;
        image.GetComponent<RectTransform>().localPosition = pos;

        imageObjects.Add(new PlacementSet ( sub, pos ));

    }

    public void EnableImagesAfter (Vector2 position, TextDirection direction = TextDirection.TopDownLeftToRight)
    {
        bool vert = (direction == TextDirection.TopDownLeftToRight || direction == TextDirection.TopDownRightToLeft);
        bool hor = (direction == TextDirection.TopDownLeftToRight || direction == TextDirection.BottomUpLeftToRight);

        foreach (PlacementSet img in imageObjects)
        {
            if ((vert ? position.y < img.position.y : position.y > img.position.y) && (hor ? position.x > img.position.x : position.x < img.position.x))
            {
                img.imageObject.SetActive(true);
            }
        }

    }

    void ClearImages()
    {
        while (imageObjects.Count > 0)
        {
            PlacementSet toRemove = imageObjects.First();
            imageObjects.Remove(toRemove);
            Destroy(toRemove.imageObject);
        }
    }
   
    public enum TextDirection
    {
        TopDownLeftToRight,
        TopDownRightToLeft,
        BottomUpLeftToRight,
        BootomUpRightToLeft,
    }

}
