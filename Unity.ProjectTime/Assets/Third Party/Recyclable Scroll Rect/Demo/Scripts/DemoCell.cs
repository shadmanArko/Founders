﻿using Third_Party.Recyclable_Scroll_Rect.Main.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.UI;

//Cell class for demo. A cell in Recyclable Scroll Rect must have a cell class inheriting from ICell.
//The class is required to configure the cell(updating UI elements etc) according to the data during recycling of cells.
//The configuration of a cell is done through the DataSource SetCellData method.
//Check RecyclableScrollerDemo class
namespace Third_Party.Recyclable_Scroll_Rect.Demo.Scripts
{
    public class DemoCell : MonoBehaviour, ICell
    {
        //UI
        public Text nameLabel;
        public Text genderLabel;
        public Text idLabel;

        //Model
        private ContactInfo _contactInfo;
        private int _cellIndex;

        private void Start()
        {
            //Can also be done in the inspector
            GetComponent<Button>().onClick.AddListener(ButtonListener);
        }

        //This is called from the SetCell method in DataSource
        public void ConfigureCell(ContactInfo contactInfo,int cellIndex)
        {
            _cellIndex = cellIndex;
            _contactInfo = contactInfo;

            nameLabel.text = contactInfo.Name;
            genderLabel.text = contactInfo.Gender;
            idLabel.text = contactInfo.id;
        }

    
        private void ButtonListener()
        {
            Debug.Log("Index : " + _cellIndex +  ", Name : " + _contactInfo.Name  + ", Gender : " + _contactInfo.Gender);
        }
    }
}
