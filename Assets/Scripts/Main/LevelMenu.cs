using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Adds all levels to the scroll view in the main menu at start
/// </summary>
public class LevelMenu : MonoBehaviour
{
    [SerializeField] private GameObject scrollView;
    [SerializeField] private GameObject levelTemplate;
    [SerializeField] private RectTransform rect;
    [SerializeField] private List<Level> levels;

    private void Start()
    {
        for (int i = 0; i < levels.Count; i++)
        {
            GameObject obj = Instantiate(levelTemplate);
            LevelCard level = obj.GetComponent<LevelCard>();

            level.ID = levels[i].ID;
            level.levelName.text = levels[i].levelName;
            level.levelDescription.text = levels[i].levelDescription;
            level.levelIcon.sprite = levels[i].levelIcon;

            if (!levels[i].Unlocked)
            {
                obj.GetComponent<Button>().interactable = false;
                level.levelIcon.color = new Color(0.7f, 0.7f, 0.7f, 0.7f);
            }

            obj.transform.SetParent(scrollView.transform, false);
        }
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, levels.Count * 110 + 100);
    }
}