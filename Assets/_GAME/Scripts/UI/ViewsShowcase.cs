using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ViewsShowcase : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameLabel;
    [SerializeField] private Transform viewsParent;
    [SerializeField] private List<GameObject> viewPrefabs = new List<GameObject>();

    private int currentView = 0;

    public void ChangeView(int change)
    {
        viewsParent.GetChild(currentView).gameObject.SetActive(false);
        
        currentView += change;

        if (currentView < 0)
            currentView = viewsParent.childCount - 1;
        else if (currentView >= viewsParent.childCount)
            currentView = 0;

        viewsParent.GetChild(currentView).gameObject.SetActive(true);
        string viewName = viewsParent.GetChild(currentView).gameObject.name;
        nameLabel.text =  $"{viewName}<br><b>{(1 + currentView)}/{viewsParent.childCount}</b>";
    }

    private void Start()
    {
        SpawnViews();
        ChangeView(0);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.LeftArrow))
            ChangeView(-1);
        if (Input.GetKeyUp(KeyCode.RightArrow))
            ChangeView(1);
    }

    private void SpawnViews()
    {
        for (int i = 0; i < viewPrefabs.Count; i++)
        {
            GameObject newView = Instantiate(viewPrefabs[i], viewsParent);
            newView.name = viewPrefabs[i].name;
            newView.SetActive(false);
        }
    }
}
