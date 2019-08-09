using UnityEngine;
using UnityEngine.UI;

public class ScoreBuilder : MonoBehaviour
{
    [SerializeField]
    private string songName;

    [SerializeField]
    private Notes[] melody;

    [SerializeField]
    private Transform notesParent;

    [SerializeField]
    private Text textName;

    [SerializeField]
    private Sprite topSprite;

    [SerializeField]
    private Sprite rightSprite;

    [SerializeField]
    private Sprite bottomSprite;

    [SerializeField]
    private Sprite leftSprite;

    [SerializeField]
    private Sprite aSprite;

    [SerializeField]
    private Image notePrefab;

    [SerializeField]
    private Vector2 offset = new Vector2(75, 30);

    public enum Notes
    {
        A, Bottom, Right, Left, Top
    }

    private void Start()
    {
        textName.text = songName;

        var i = 0;
        foreach(var note in melody)
        {
            var prefab = Instantiate(notePrefab);
            prefab.transform.SetParent(notesParent);
            prefab.transform.localPosition = new Vector3(i++ * offset.x, (int)note * offset.y);
            switch (note)
            {
                case Notes.A:
                    prefab.sprite = aSprite;
                    break;
                case Notes.Bottom:
                    prefab.sprite = bottomSprite;
                    break;
                case Notes.Right:
                    prefab.sprite = rightSprite;
                    break;
                case Notes.Left:
                    prefab.sprite = leftSprite;
                    break;
                case Notes.Top:
                    prefab.sprite = topSprite;
                    break;
            }
        }
    }
}
